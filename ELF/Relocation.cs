using MiscUtil.IO;

namespace ELF
{
    public class Relocation : ICommonRelocation
    {
        public const int SIZE = 8;

        public int Offset { get; set; }

        public uint SymbolTargetIndex { get; set; }

        public RelocationType Type { get; set; }

        public Relocation(EndianBinaryReader reader)
        {
            Offset = reader.ReadInt32();

            var info = reader.ReadUInt32();

            SymbolTargetIndex = info >> 8;
            Type = (RelocationType)(info & 0x000000FF);
        }
    }
}
