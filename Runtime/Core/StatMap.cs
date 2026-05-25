using System.Collections.Generic;

namespace Effigment.Stats.Core
{
    public class StatMap<T>
        where T : IStatKey
    {
        private Dictionary<T, IStat> _stats;

        public StatMap(Dictionary<T, IStat> stats = null)
        {
            _stats = stats ?? new();
        }

        public bool TryGetValue(T key, out IStat result)
        {
            if (!_stats.TryGetValue(key, out result))
                return false;

            return true;
        }

        public IStat Get(T key) => _stats[key];
        public void Add(T key, IStat stat) => _stats.Add(key, stat);
        public void Remove(T key) => _stats.Remove(key);
        public void Clear() => _stats.Clear();
    }
}