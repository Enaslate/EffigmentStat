using Effigment.Stats.Core;
using System;

namespace Effigment.Stats.Tests
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