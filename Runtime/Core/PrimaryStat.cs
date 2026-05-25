using System;

namespace Effigment.Stats.Core
{
    public class PrimaryStat : IStat
    {
        public int Max { get; protected set; }
        public int Min { get; protected set; }
        public int Current { get; protected set; }

        public PrimaryStat(int current, int max = 999, int min = 1)
        {
            if (max < min) throw new ArgumentException($"{nameof(max)} cant be less {nameof(min)}");

            Max = max;
            Min = min;
            SetValue(current);
        }

        public void SetValue(int value) =>
            Current = Math.Clamp(value, Min, Max);

        public void IncreaseValue(int value) =>
            Current = Math.Min(Current + value, Max);

        public void DecreaseValue(int value) =>
            Current = Math.Max(Min, Current - value);
    }
}