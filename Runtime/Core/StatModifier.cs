namespace Effigment.Stat.Core
{
    public class StatModifier
    {
        public float Value { get; private set; }
        public ModifierType Type { get; private set; }

        public StatModifier(float value, ModifierType modifierType, IStatKey targetStat = null)
        {
            Value = value;
            Type = modifierType;
        }
    }
}