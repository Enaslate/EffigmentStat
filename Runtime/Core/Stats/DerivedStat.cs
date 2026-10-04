using System;
using System.Collections.Generic;

namespace Effigment.Stat.Core.Stats
{
    public class DerivedStat<T> : StatBase, IDisposable
        where T : IStatKey
    {
        private float _formulaResult;

        protected StatMap<T> _stats;
        protected Func<StatMap<T>, float> _formula;
        private List<IStat> _dependencies;

        public DerivedStat(
            StatMap<T> statMap,
            Func<StatMap<T>, float> formula,
            float max,
            float min = 0,
            float baseValue = 0,
            T[] dependencies = null,
            List<StatModifier> modifiers = null)
            : base(modifiers)
        {
            if (statMap == null) throw new ArgumentNullException(nameof(statMap));
            if (formula == null) throw new ArgumentNullException(nameof(formula));
            if (max < min) throw new ArgumentException($"{nameof(max)} cant be less {nameof(min)}");

            _stats = statMap;
            _formula = formula;
            Max = max;
            Min = min;
            SetValue(baseValue);

            if (dependencies != null)
            {
                _dependencies = new();
                foreach (var dep in dependencies)
                {
                    var stat = _stats.Get(dep);
                    stat.Changed += OnChanged;
                    _dependencies.Add(stat);
                }
            }
        }

        private void OnChanged(StatBase @base)
        {
            Recalculate();
        }

        protected override void Recalculate()
        {
            CalculateTotalModifiersValue();
            _formulaResult = _formula.Invoke(_stats);
            Current = Math.Clamp(BaseValue + _formulaResult + _totalModifiersValue, Min, Max);
            NotifyChanged();
        }

        public void Dispose()
        {
            if (_dependencies != null)
            {
                foreach (var stat in _dependencies)
                    stat.Changed -= OnChanged;
                _dependencies.Clear();
            }
        }
    }
}