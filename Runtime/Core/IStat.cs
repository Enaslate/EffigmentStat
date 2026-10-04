using Effigment.Stat.Core.Stats;
using System;
using System.Collections.Generic;

namespace Effigment.Stat.Core
{
    public interface IStat
    {
        event Action<StatBase> Changed;
        float Max { get; }
        float Min { get; }
        float Current { get; }
        IEnumerable<StatModifier> GetModifiers(Func<StatModifier, bool> predicate);
        void AddModifier(StatModifier modifier);
        void RemoveModifier(StatModifier modifier);
        public void IncreaseValue(float value);
        public void DecreaseValue(float value);
    }
}