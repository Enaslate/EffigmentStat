using Effigment.Stat.Core;
using NUnit.Framework;

namespace Effigment.Stat.Tests
{
    [TestFixture]
    public class DerivedStatTests
    {
        private const int MaxDerived = 10;

        private StatMap<TestStatKey> _stats;

        [SetUp]
        public void SetUp()
        {
            _stats = new();
            _stats.Add(new TestStatKey(TestExtensions.PrimaryStatName), new PrimaryStat(1));
            _stats.Add(new TestStatKey(TestExtensions.DerivedStatName),
                new DerivedStat<TestStatKey>(_stats, TestExtensions.FormulaByPrimaryStat, MaxDerived));
        }

        [Test]
        public void Current_AtOnce_Succes()
        {
            var derivedStat = _stats.Get(new TestStatKey(TestExtensions.DerivedStatName));
            var calculation = TestExtensions.FormulaByPrimaryStat(_stats);
            Assert.That(derivedStat.Current, Is.EqualTo(calculation));
        }

        [Test]
        public void Current_AfterChangePrimaryStat_Succes()
        {
            var primaryStat = _stats.Get(new TestStatKey(TestExtensions.PrimaryStatName));
            (primaryStat as PrimaryStat).IncreaseValue(1);

            var derivedStat = _stats.Get(new TestStatKey(TestExtensions.DerivedStatName));
            var calculation = TestExtensions.FormulaByPrimaryStat(_stats);

            Assert.That(derivedStat.Current, Is.EqualTo(calculation));
        }

        [Test]
        public void Current_WhenValueMoreMax_Succes()
        {
            var derivedStat = _stats.Get(new TestStatKey(TestExtensions.DerivedStatName));

            var primaryStat = _stats.Get(new TestStatKey(TestExtensions.PrimaryStatName));
            (primaryStat as PrimaryStat).SetValue(derivedStat.Current + MaxDerived);

            var calculation = TestExtensions.FormulaByPrimaryStat(_stats);

            Assert.That(derivedStat.Current, Is.EqualTo(derivedStat.Max));
        }
    }
}
