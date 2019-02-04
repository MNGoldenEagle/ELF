using MiscUtil.IO;
using System;
using System.IO;
using System.Text;

namespace ELF
{
    public class Header
    {
        public string MagicString;

        public BitFormat BitType;

        public EndianFormat Endianness;

        public byte Version;

        public TargetABI ABI;

        public long Padding;

        public BinaryType Type;

        public BinaryISA ISA;

        public int VersionELF;

        public int EntryPointAddr;

        public ProgramHeader[] ProgramHeaders;

        public SectionHeader[] SectionHeaders;

        public FlagsMIPS Flags;

        public ushort ELFHeaderSize;

        public ushort SectionNameIndex;

        public Header(EndianBinaryReader reader)
        {
            byte[] convertedBytes = Encoding.Convert(Encoding.ASCII, Encoding.Default, reader.ReadBytes(4));
            MagicString = new string(Encoding.ASCII.GetChars(convertedBytes));
            BitType = (BitFormat)reader.ReadByte();
            Endianness = (EndianFormat)reader.ReadByte();
            Version = reader.ReadByte();
            ABI = (TargetABI)reader.ReadByte();
            Padding = reader.ReadInt64();
            Type = (BinaryType)reader.ReadUInt16();
            ISA = (BinaryISA)reader.ReadUInt16();
            VersionELF = reader.ReadInt32();
            EntryPointAddr = reader.ReadInt32();

            var programHeaderOffset = reader.ReadInt32();
            var sectionHeaderOffset = reader.ReadInt32();

            Flags = (FlagsMIPS)reader.ReadInt32();

            ELFHeaderSize = reader.ReadUInt16();

            var programHeaderSize = reader.ReadInt16();
            var programHeaderCount = reader.ReadInt16();
            var sectionHeaderSize = reader.ReadInt16();
            var sectionHeaderCount = reader.ReadInt16();

            SectionNameIndex = reader.ReadUInt16();

            ProgramHeaders = new ProgramHeader[programHeaderCount];

            reader.Seek(programHeaderOffset, SeekOrigin.Begin);
            for (int i = 0; i < programHeaderCount; i++)
            {
                ProgramHeaders[i] = new ProgramHeader(this, reader, programHeaderSize);
            }

            SectionHeaders = new SectionHeader[sectionHeaderCount];

            reader.Seek(sectionHeaderOffset, SeekOrigin.Begin);
            for (int i = 0; i < sectionHeaderCount; i++)
            {
                SectionHeaders[i] = new SectionHeader(this, reader, sectionHeaderSize);
            }
        }

        public void Describe()
        {
            Console.WriteLine("ELF OVERLAY HEADER");
            Console.WriteLine("Magic Number: {0}   Version: {1}   ELF Version: {2}", MagicString, Version, VersionELF);
            Console.WriteLine("Bitness: {0}   Endianness: {1}", BitType, Endianness);
            Console.WriteLine("ABI: {0}  Arch: {1}", ABI, ISA);
            Console.WriteLine("Obj Type: {0}  Obj Flags: {1}", Type, Flags);
            Console.WriteLine("Entrypoint: {0,8:X}", EntryPointAddr);
            Console.WriteLine();
            Console.WriteLine("PROGRAM HEADERS");
            for (int i = 0; i < ProgramHeaders.Length; i++)
            {
                Console.WriteLine("{0}. {1} @ {2,8:X}", i + 1, ProgramHeaders[i].Type, ProgramHeaders[i].ImageOffset);
            }
            for (int i = 0; i < ProgramHeaders.Length; i++)
            {
                Console.WriteLine();
                ProgramHeaders[i].Describe();
            }
            Console.WriteLine();
            Console.WriteLine("SECTION HEADERS");
            for (int i = 0; i < SectionHeaders.Length; i++)
            {
                Console.WriteLine("{0}. {1}: {2} @ {3,8:X}",
                    i + 1,
                    SectionHeaders[i].Type,
                    SectionHeaders[SectionNameIndex].GetName(),
                    SectionHeaders[i].SegmentImageOffset);
            }
            for (int i = 0; i < SectionHeaders.Length; i++)
            {
                Console.WriteLine();
                SectionHeaders[i].Describe();
            }
        }
    }
}
