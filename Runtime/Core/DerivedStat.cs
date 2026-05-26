using System;

namespace Effigment.Stat.Core
{
    public class DerivedStat<T> : IStat
        where T : IStatKey
    {
        public virtual int Max { get; protected set; }
        public virtual int Min { get; protected set; }
        public virtual int Current => Math.Clamp(_formula.Invoke(_stats), Min, Max);

        protected StatMap<T> _stats;
        protected Func<StatMap<T>, int> _formula;

        public DerivedStat(
            StatMap<T> stats,
            Func<StatMap<T>, int> formula,
            int max,
            int min = 0)
        {
            if (stats == null) throw new ArgumentNullException(nameof(stats));
            if (formula == null) throw new ArgumentNullException(nameof(formula));
            if (max < min) throw new ArgumentException($"{nameof(max)} cant be less {nameof(min)}");

            _stats = stats;
            _formula = formula;
            Max = max;
            Min = min;
        }
    }
}