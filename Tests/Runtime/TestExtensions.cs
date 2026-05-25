using Effigment.Stats.Core;

namespace Effigment.Stats.Tests
{
    public class TestExtensions
    {
        public const string PrimaryStatName = "primary";
        public const string DerivedStatName = "derived";
        public const string ResourceStatName = "resource";

        public static int FormulaByPrimaryStat(StatMap<TestStatKey> map)
        {
            return map.Get(new TestStatKey(TestExtensions.PrimaryStatName)).Current + 1;
        }
    }
}