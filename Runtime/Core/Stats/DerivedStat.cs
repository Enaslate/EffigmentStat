using System;
using System.Collections.Generic;

namespace Effigment.Stat.Core.Stats
{
    public class DerivedStat<T> : StatBase
        where T : IStatKey
    {
        public override int Current => Math.Clamp(
            BaseValue + _formulaResult + _totalModifiersValue, Min, Max);

        private int _formulaResult => _formula.Invoke(_stats);

        protected StatMap<T> _stats;
        protected Func<StatMap<T>, int> _formula;

        public DerivedStat(
            StatMap<T> stats,
            Func<StatMap<T>, int> formula,
            int max,
            int min = 0,
            int baseValue = 0,
            List<StatModifier> modifiers = null)
            : base(modifiers)
        {
            if (stats == null) throw new ArgumentNullException(nameof(stats));
            if (formula == null) throw new ArgumentNullException(nameof(formula));
            if (max < min) throw new ArgumentException($"{nameof(max)} cant be less {nameof(min)}");

            _stats = stats;
            _formula = formula;
            Max = max;
            Min = min;
            SetValue(baseValue);
        }
    }
}