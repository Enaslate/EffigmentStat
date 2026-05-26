using Effigment.Stat.Core;
using System;

namespace Effigment.Stat.Tests
{
    public struct TestStatKey : IStatKey, IEquatable<TestStatKey>
    {
        public string Key { get; private set; }

        public TestStatKey(string key)
        {
            Key = key;
        }

        public bool Equals(TestStatKey other) => Key == other.Key;
        public override int GetHashCode() => Key.GetHashCode();
    }
}