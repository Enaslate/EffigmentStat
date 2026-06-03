using System;
using System.Collections.Generic;
using System.Linq;

namespace Effigment.Stat.Core.Stats
{
    public abstract class StatBase : IStat
    {
        public virtual int Max { get; protected set; }
        public virtual int Min { get; protected set; }
        public virtual int Current { get; protected set; }

        public virtual int BaseValue { get; protected set; }
        protected int _totalModifiersValue;

        public List<StatModifier> Modifiers { get; protected set; }

        protected StatBase(List<StatModifier> modifiers)
        {
            Modifiers = modifiers ?? new();
        }

        public void SetValue(int value)
        {
            BaseValue = Math.Clamp(value, Min, Max);
            CalculateTotalModifiersValue();
        }

        public void IncreaseValue(int value)
        {
            BaseValue = Math.Min(Current + value, Max);
            CalculateTotalModifiersValue();
        }

        public void DecreaseValue(int value)
        {
            BaseValue = Math.Max(Min, Current - value);
            CalculateTotalModifiersValue();
        }

        public void CalculateTotalModifiersValue()
        {
            _totalModifiersValue = 0;

            foreach (var modifier in Modifiers)
            {
                _totalModifiersValue += modifier.Type switch
                {
                    ModifierType.Flat => modifier.Value,
                    ModifierType.Percent =>
                        (int)((float)BaseValue * ((float)modifier.Value / 100f)),
                    _ => 
                        throw new ArgumentOutOfRangeException($"Unsupported modifier type: {modifier.Type}"),
                };
            }
        }

        public IEnumerable<StatModifier> GetModifiers(Func<StatModifier, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return Modifiers.Where(predicate).Select(x => x);
        }

        public void AddModifier(StatModifier modifier)
        {
            if (modifier == null) throw new ArgumentNullException(nameof(modifier));

            if (Modifiers.Contains(modifier))
                throw new InvalidOperationException("Modifier already added");

            Modifiers.Add(modifier);
            CalculateTotalModifiersValue();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            if (modifier == null) throw new ArgumentNullException(nameof(modifier));

            Modifiers.Remove(modifier);
            CalculateTotalModifiersValue();
        }

        public void Clear()
        {
            Modifiers.Clear();
            CalculateTotalModifiersValue();
        }
    }
}