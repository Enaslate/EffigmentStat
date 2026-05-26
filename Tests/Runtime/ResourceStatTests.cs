using Effigment.Stat.Core;
using NUnit.Framework;

namespace Effigment.Stat.Tests
{
    [TestFixture]
    public class ResourceStatTests
    {
        private StatMap<TestStatKey> _stats;

        [SetUp]
        public void SetUp()
        {
            _stats = new();
            _stats.Add(new TestStatKey(TestExtensions.PrimaryStatName), new PrimaryStat(1));
            _stats.Add(new TestStatKey(TestExtensions.ResourceStatName), new ResourceStat<TestStatKey>(_stats, TestExtensions.FormulaByPrimaryStat));
        }

        [Test]
        public void Max_AtOnce_Succes()
        {
            var resourceStat = _stats.Get(new TestStatKey(TestExtensions.ResourceStatName));
            var calculation = TestExtensions.FormulaByPrimaryStat(_stats);
            Assert.That(resourceStat.Max, Is.EqualTo(calculation));
        }

        [Test]
        public void Max_AfterChangePrimaryStat_WithRefresh_Succes()
        {
            var primaryStat = _stats.Get(new TestStatKey(TestExtensions.PrimaryStatName));
            (primaryStat as PrimaryStat).IncreaseValue(1);

            var resourceStat = _stats.Get(new TestStatKey(TestExtensions.ResourceStatName));
            (resourceStat as ResourceStat<TestStatKey>).Refresh();
            var calculation = TestExtensions.FormulaByPrimaryStat(_stats);

            Assert.That(resourceStat.Max, Is.EqualTo(calculation));
        }

        [Test]
        public void Max_AfterChangePrimaryStat_WithoutRefresh_Succes()
        {
            var primaryStat = _stats.Get(new TestStatKey(TestExtensions.PrimaryStatName));
            (primaryStat as PrimaryStat).IncreaseValue(1);

            var resourceStat = _stats.Get(new TestStatKey(TestExtensions.ResourceStatName));
            var calculation = TestExtensions.FormulaByPrimaryStat(_stats);

            Assert.That(resourceStat.Max, !Is.EqualTo(calculation));
        }
    }
}
