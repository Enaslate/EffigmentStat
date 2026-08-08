using System;
using System.Collections.Generic;

namespace Effigment.Stat.Core.Stats
{
    public class ResourceStat<T> : StatBase
        where T : IStatKey
    {
        public override int Max => _cachedMax + _totalModifiersValue;
        public override int Current => BaseValue;

        protected StatMap<T> _stats;
        protected Func<StatMap<T>, int> _formula;

        private int _cachedMax;

        public ResourceStat(
            StatMap<T> stats,
            Func<StatMap<T>, int> formula,
            int? current = null,
            int min = 0,
            List<StatModifier> modifiers = null)
            : base(modifiers)
        {
            if (stats == null) throw new ArgumentNullException(nameof(stats));
            if (formula == null) throw new ArgumentNullException(nameof(formula));

            _stats = stats;
            _formula = formula;
            Refresh();

            if (Max < Min) throw new ArgumentException($"{nameof(Max)} cant be less {nameof(min)}");
            Min = min;

            if (current == null)
                BaseValue = Max;
            else
                SetValue(current.Value);
        }

        public void Refresh()
        {
            _cachedMax = _formula.Invoke(_stats);

            if (Current > Max)
                Current = Max;
        }
    }
}