using System;
using System.Collections.Generic;

namespace Effigment.Stat.Core.Stats
{
    public class ResourceStat<T> : StatBase, IDisposable
        where T : IStatKey
    {
        public override float Max => _cachedMax + _totalModifiersValue;
        public override float Current => BaseValue;

        protected StatMap<T> _stats;
        protected Func<StatMap<T>, float> _formula;

        private float _cachedMax;
        private List<IStat> _dependencies;

        public ResourceStat(
            StatMap<T> stats,
            Func<StatMap<T>, float> formula,
            float? current = null,
            float min = 0,
            T[] dependencies = null,
            List<StatModifier> modifiers = null)
            : base(modifiers)
        {
            if (stats == null) throw new ArgumentNullException(nameof(stats));
            if (formula == null) throw new ArgumentNullException(nameof(formula));

            _stats = stats;
            _formula = formula;
            CalculateTotalModifiersValue();
            Refresh();

            if (Max < min) throw new ArgumentException($"{nameof(Max)} cant be less {nameof(min)}");
            Min = min;

            if (current == null)
                BaseValue = Max;
            else
                SetValue(current.Value);

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
            Refresh();
        }

        public void Refresh()
        {
            _cachedMax = _formula.Invoke(_stats);
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