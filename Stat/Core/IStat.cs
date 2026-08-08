using System;
using System.Collections.Generic;

namespace Effigment.Stat.Core
{
    public interface IStat
    {
        int Max { get; }
        int Min { get; }
        int Current { get; }
        IEnumerable<StatModifier> GetModifiers(Func<StatModifier, bool> predicate);
        void AddModifier(StatModifier modifier);
        void RemoveModifier(StatModifier modifier);
    }
}