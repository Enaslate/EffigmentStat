using System;
using System.Collections.Generic;

namespace Effigment.Stat.Core
{
    public interface IStat
    {
        float Max { get; }
        float Min { get; }
        float Current { get; }
        IEnumerable<StatModifier> GetModifiers(Func<StatModifier, bool> predicate);
        void AddModifier(StatModifier modifier);
        void RemoveModifier(StatModifier modifier);
    }
}