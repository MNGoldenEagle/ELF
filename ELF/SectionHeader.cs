using MiscUtil.IO;
using System;
using System.IO;

namespace ELF
{
    public class SectionHeader
    {
        private readonly Header Parent;

        public int NameIndex; /* names are in the .shstrtab section */

        public SectionType Type;

        public SectionFlags Flags;

        public uint SegmentInMemVirtAddress;

        public uint SegmentImageOffset;

        public int SegmentImageSize;

        public uint LinkIndex;

        public uint Info;

        public uint Alignment;

        public uint FixedEntrySize;

        public ISection SectionData;

        public SectionHeader(Header parent, EndianBinaryReader reader, short sectionHeaderSize)
        {
            Parent = parent;
            NameIndex = reader.ReadInt32();
            Type = (SectionType)reader.ReadUInt32();
            Flags = (SectionFlags)reader.ReadUInt32();
            SegmentInMemVirtAddress = reader.ReadUInt32();
            SegmentImageOffset = reader.ReadUInt32();
            SegmentImageSize = reader.ReadInt32();
            LinkIndex = reader.ReadUInt32();
            Info = reader.ReadUInt32();
            Alignment = reader.ReadUInt32();
            FixedEntrySize = reader.ReadUInt32();

            var offset = reader.BaseStream.Position;

            switch (Type)
            {
                case SectionType.RELOC:
                    reader.BaseStream.Seek(SegmentImageOffset, SeekOrigin.Begin);
                    SectionData = new SimpleRelocations(parent, this, reader, SegmentImageSize / Relocation.SIZE);
                    reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                    break;
                case SectionType.RELOC_ADD:
                    reader.BaseStream.Seek(SegmentImageOffset, SeekOrigin.Begin);
                    SectionData = new AddendRelocations(parent, this, reader, SegmentImageSize / AddendRelocation.SIZE);
                    reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                    break;
                case SectionType.SYM_TABLE:
                    reader.BaseStream.Seek(SegmentImageOffset, SeekOrigin.Begin);
                    SectionData = new SymbolTable(parent, this, reader, SegmentImageSize / Symbol.SIZE);
                    reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                    break;
                case SectionType.STR_TABLE:
                    reader.BaseStream.Seek(SegmentImageOffset, SeekOrigin.Begin);
                    SectionData = new StringTable(reader, SegmentImageSize);
                    reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                    break;
                default:
                    // IGNORE
                    break;
            }
        }

        public void Describe()
        {
            Console.WriteLine("{0} SECTION HEADER --- {1}", Type.ToString().ToUpperInvariant(), GetName());
            Console.WriteLine("Data Offset: {0,8:X}  Data Size: {1}B", SegmentImageOffset, SegmentImageSize);
            Console.WriteLine("Virt Base Address: {0,8:X}  Alignment: {1}B", SegmentInMemVirtAddress, Math.Pow(Alignment, 2));
            Console.WriteLine("Link Index: {0,8:X}  Info: {1,8:X}", LinkIndex, Info);
            Console.WriteLine();

            if (SectionData != null)
            {
                SectionData.Describe();
            }
        }

        public string GetName()
        {
            try
            {
                return (Parent.SectionHeaders[Parent.SectionNameIndex].SectionData as StringTable).GetString(NameIndex);
            }
            catch (Exception)
            {
                return "NAME NOT FOUND";
            }
        }
    }
}
