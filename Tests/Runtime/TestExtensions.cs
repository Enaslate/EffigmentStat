using Effigment.Stat.Core;

namespace Effigment.Stat.Tests
{
    public class TestExtensions
    {
        public const string PrimaryStatName = "primary";
        public const string DerivedStatName = "derived";
        public const string ResourceStatName = "resource";

        public static float FormulaByPrimaryStat(StatMap<TestStatKey> map)
        {
            return map.Get(new TestStatKey(TestExtensions.PrimaryStatName)).Current + 1;
        }
    }
}