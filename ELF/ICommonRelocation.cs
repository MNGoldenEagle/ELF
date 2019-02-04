namespace ELF
{
    public interface ICommonRelocation
    {
        int Offset { get; set; }

        uint SymbolTargetIndex { get; set; }

        RelocationType Type { get; set; }
    }
}
