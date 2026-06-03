namespace Effigment.Stat.Core
{
    public interface IStat
    {
        int Max { get; }
        int Min { get; }
        int Current { get; }
        void AddModifier(StatModifier modifier);
        void RemoveModifier(StatModifier modifier);
    }
}