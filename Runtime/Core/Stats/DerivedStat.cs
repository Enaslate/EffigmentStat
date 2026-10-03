using System;
using System.Collections.Generic;

namespace Effigment.Stat.Core.Stats
{
    public class DerivedStat<T> : StatBase
        where T : IStatKey
    {
        public override float Current => Math.Clamp(
            BaseValue + _formulaResult + _totalModifiersValue, Min, Max);

        private float _formulaResult => _formula.Invoke(_statMap);

        protected StatMap<T> _statMap;
        protected Func<StatMap<T>, float> _formula;

        public DerivedStat(
            StatMap<T> statMap,
            Func<StatMap<T>, float> formula,
            float max,
            float min = 0,
            float baseValue = 0,
            List<StatModifier> modifiers = null)
            : base(modifiers)
        {
            if (statMap == null) throw new ArgumentNullException(nameof(statMap));
            if (formula == null) throw new ArgumentNullException(nameof(formula));
            if (max < min) throw new ArgumentException($"{nameof(max)} cant be less {nameof(min)}");

            _statMap = statMap;
            _formula = formula;
            Max = max;
            Min = min;
            SetValue(baseValue);
        }
    }
}