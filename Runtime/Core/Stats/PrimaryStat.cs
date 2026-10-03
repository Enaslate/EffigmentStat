using System;
using System.Collections.Generic;

namespace Effigment.Stat.Core.Stats
{
    public class PrimaryStat : StatBase
    {
        public override float Current => Math.Clamp(
            BaseValue + _totalModifiersValue, Min, Max);

        public PrimaryStat(
            float current,
            float max = 999,
            float min = 1,
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