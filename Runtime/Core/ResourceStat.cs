using System;

namespace Effigment.Stat.Core
{
    public class ResourceStat<T> : IStat
        where T : IStatKey
    {
        public int Max => _cachedMax;
        public int Min { get; protected set; }
        public int Current { get; protected set; }

        protected StatMap<T> _stats;
        protected Func<StatMap<T>, int> _formula;

        protected int _cachedMax;

        public ResourceStat(StatMap<T> stats, Func<StatMap<T>, int> formula, int? current = null, int min = 0)
        {
            if (stats == null) throw new ArgumentNullException(nameof(stats));
            if (formula == null) throw new ArgumentNullException(nameof(formula));

            _stats = stats;
            _formula = formula;
            Refresh();

            if (Max < Min) throw new ArgumentException($"{nameof(Max)} cant be less {nameof(min)}");
            Min = min;

            Current = current == null
                ? Max 
                : Math.Clamp(current.Value, Min, Max);
        }

        public void SetValue(int value) =>
            Current = Math.Clamp(value, Min, Max);

        public void IncreaseValue(int value) =>
            Current = Math.Min(Current + value, Max);

        public void DecreaseValue(int value) =>
            Current = Math.Max(Min, Current - value);

        public void Refresh()
        {
            _cachedMax = _formula.Invoke(_stats);

            if (Current > _cachedMax)
                Current = _cachedMax;
        }
    }
}