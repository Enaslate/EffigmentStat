using Effigment.Stat.Core;
using Effigment.Stat.Core.Stats;
using NUnit.Framework;
using System.Linq;

namespace Effigment.Stat.Tests
{
    [TestFixture]
    public class StatBaseTests
    {
        private StatBase _stat;

        [SetUp]
        public void SetUp()
        {
            _stat = new PrimaryStat(10);
        }

        [Test]
        public void AddModifier_WhenFlatModifier_Succuss()
        {
            var modifier = new StatModifier(1, ModifierType.Flat);

            _stat.AddModifier(modifier);
            var expectedCurrent = _stat.BaseValue + modifier.Value;

            Assert.That(_stat.Modifiers.Contains(modifier), Is.True);
            Assert.That(_stat.Current, Is.EqualTo(expectedCurrent));
        }

        [Test]
        public void AddModifier_WhenPercentModifier_Succuss()
        {
            var modifier = new StatModifier(-50, ModifierType.Percent);

            _stat.AddModifier(modifier);
            var expectedCurrent = _stat.BaseValue * modifier.Value;

            Assert.That(_stat.Modifiers.Contains(modifier), Is.True);
            Assert.That(_stat.Current, Is.EqualTo(5));
        }

        [Test]
        public void RemoveModifier_Success()
        {
            var modifier = new StatModifier(1, ModifierType.Flat);
            _stat.AddModifier(modifier);

            _stat.RemoveModifier(modifier);

            Assert.That(_stat.Modifiers.Count, Is.EqualTo(0));
            Assert.That(_stat.Current, Is.EqualTo(_stat.BaseValue));
        }

        [Test]
        public void Clear_Success()
        {
            var flatModifier = new StatModifier(1, ModifierType.Flat);
            var percentModifier = new StatModifier(1, ModifierType.Flat);
            _stat.AddModifier(flatModifier);
            _stat.AddModifier(percentModifier);

            _stat.Clear();

            Assert.That(_stat.Modifiers.Count, Is.EqualTo(0));
            Assert.That(_stat.Current, Is.EqualTo(_stat.BaseValue));
        }

        [Test]
        public void GetModifiers_WhenValidPredicate_Success()
        {
            var flatModifier = new StatModifier(1, ModifierType.Flat);
            var percentModifier = new StatModifier(1, ModifierType.Percent);

            _stat.AddModifier(flatModifier);
            _stat.AddModifier(percentModifier);

            var modifiers = _stat.GetModifiers((stat) => stat.Type == ModifierType.Flat);

            Assert.That(_stat.Modifiers.Count, Is.EqualTo(2));
            Assert.That(modifiers.Count(), Is.EqualTo(1));
        }
    }
}