namespace Effigment.Stats.Core
{
    public interface IStat
    {
        int Max { get; }
        int Min { get; }
        int Current { get; }
    }
}