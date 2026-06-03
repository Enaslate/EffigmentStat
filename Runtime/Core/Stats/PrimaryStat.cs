using System;
using System.Collections.Generic;

namespace Effigment.Stat.Core.Stats
{
    public class PrimaryStat : StatBase
    {
        public override int Current => Math.Clamp(
            BaseValue + _totalModifiersValue, Min, Max);

        public PrimaryStat(
            int current,
            int max = 999,
            int min = 1,
            List<StatModifier> modifiers = null)
            : base(modifiers)
        {
            if (max < min) throw new ArgumentException($"{nameof(max)} cant be less {nameof(min)}");

            Max = max;
            Min = min;
            SetValue(current);
        }
    }
}