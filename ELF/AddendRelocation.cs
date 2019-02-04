using MiscUtil.IO;

namespace ELF
{
    public class AddendRelocation : ICommonRelocation
    {
        public const int SIZE = 12;

        public int Offset { get; set; }
        public uint SymbolTargetIndex { get; set; }
        public RelocationType Type { get; set; }

        public short Addendum;

        public AddendRelocation(EndianBinaryReader reader)
        {
            Offset = reader.ReadInt32();

            var info = reader.ReadUInt32();

            SymbolTargetIndex = info >> 8;
            Type = (RelocationType)(info & 0x0000FFFF);

            Addendum = reader.ReadInt16();
        }
    }
}
