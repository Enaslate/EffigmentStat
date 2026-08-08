namespace Effigment.Stat.Core
{
    public class StatModifier
    {
        public int Value { get; private set; }
        public ModifierType Type { get; private set; }

        public StatModifier(int value, ModifierType modifierType, IStatKey targetStat = null)
        {
            Value = value;
            Type = modifierType;
        }
    }
}