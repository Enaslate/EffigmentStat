using System;
using System.Collections.Generic;
using System.Linq;

namespace Effigment.Stat.Core.Stats
{
    public abstract class StatBase : IStat
    {
        public event Action<StatBase> Changed;
        public virtual float Max { get; protected set; }
        public virtual float Min { get; protected set; }
        public virtual float Current { get; protected set; }

        public virtual float BaseValue { get; protected set; }
        protected float _totalModifiersValue;

        public List<StatModifier> Modifiers { get; protected set; }

        protected StatBase(List<StatModifier> modifiers)
        {
            Modifiers = modifiers ?? new();
        }

        public void SetValue(float value)
        {
            BaseValue = Math.Clamp(value, Min, Max);
            Recalculate();
        }

        public void IncreaseValue(float value)
        {
            BaseValue = Math.Min(BaseValue + value, Max);
            Recalculate();
        }

        public void DecreaseValue(float value)
        {
            BaseValue = Math.Max(Min, BaseValue - value);
            Recalculate();
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
                        (float)((float)BaseValue * ((float)modifier.Value / 100f)),
                    _ => 
                        throw new ArgumentOutOfRangeException($"Unsupported modifier type: {modifier.Type}"),
                };
            }
        }

        public IEnumerable<StatModifier> GetModifiers(Func<StatModifier, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return Modifiers.Where(predicate).Select(x => x).ToArray();
        }

        public void AddModifier(StatModifier modifier)
        {
            if (modifier == null) throw new ArgumentNullException(nameof(modifier));

            if (Modifiers.Contains(modifier))
                throw new InvalidOperationException("Modifier already added");

            Modifiers.Add(modifier);
            Recalculate();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            if (modifier == null) throw new ArgumentNullException(nameof(modifier));

            Modifiers.Remove(modifier);
            Recalculate();
        }

        public void Clear()
        {
            Modifiers.Clear();
            Recalculate();
        }

        protected virtual void Recalculate()
        {
            CalculateTotalModifiersValue();
            NotifyChanged();
        }

        protected void NotifyChanged()
        {
            Changed?.Invoke(this);
        }
    }
}