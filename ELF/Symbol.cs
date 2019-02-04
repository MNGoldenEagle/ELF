using MiscUtil.IO;

namespace ELF
{
    public class Symbol
    {
        public const int SIZE = 16;

        private readonly Header Root;

        private readonly SectionHeader ParentHeader;

        public int NameIndex;

        public int Value;

        public uint Size;

        public BindingType BindType;

        public SymbolType SymType;

        public byte Padding;

        public ushort ParentSectionIndex;

        public Symbol(Header root, SectionHeader parent, EndianBinaryReader reader)
        {
            Root = root;
            ParentHeader = parent;

            NameIndex = reader.ReadInt32();
            Value = reader.ReadInt32();
            Size = reader.ReadUInt32();

            var info = reader.ReadByte();
            BindType = (BindingType)(info >> 4);
            SymType = (SymbolType)(info & 0x0F);

            Padding = reader.ReadByte();
            ParentSectionIndex = reader.ReadUInt16();
        }

        public string GetName()
        {
            var stringSectionIndex = ParentHeader.LinkIndex;
            StringTable table = (StringTable)Root.SectionHeaders[stringSectionIndex].SectionData;
            return table.GetString(NameIndex);
        }
    }
}
