using MiscUtil.IO;
using System;
using System.IO;

namespace ELF
{
    public class ProgramHeader
    {
        private readonly Header Parent;

        public SegmentType Type;

        public int ImageOffset;

        public uint InMemVirtualAddress;

        public uint InMemPhysicalAddress;

        public int SegmentImageSize;

        public int SegmentInMemSize;

        public SegmentFlags Flags;

        public uint Alignment;

        public byte[] Data;

        public ProgramHeader(Header parent, EndianBinaryReader reader, short programHeaderSize)
        {
            Parent = parent;
            Type = (SegmentType)reader.ReadInt32();
            ImageOffset = reader.ReadInt32();
            InMemVirtualAddress = reader.ReadUInt32();
            InMemPhysicalAddress = reader.ReadUInt32();
            SegmentImageSize = reader.ReadInt32();
            SegmentInMemSize = reader.ReadInt32();
            Flags = (SegmentFlags)reader.ReadUInt32();
            Alignment = reader.ReadUInt32();

            var currentPosition = reader.BaseStream.Position;

            reader.Seek(ImageOffset, SeekOrigin.Begin);
            Data = reader.ReadBytes(SegmentImageSize);
            reader.BaseStream.Seek(currentPosition, SeekOrigin.Begin);
        }

        public void Describe()
        {
            Console.WriteLine("{0} PROGRAM HEADER", Type.ToString().ToUpperInvariant());
            Console.WriteLine("Data Offset: {0,8:X}  Data Size: {1}B", ImageOffset, SegmentImageSize);
            Console.WriteLine("Virt Address: {0,8:X}   Phys Address: {1,8:X}", InMemVirtualAddress, InMemPhysicalAddress);
            Console.WriteLine("Loaded Image Size: {0}B  Alignment: {1}B", SegmentInMemSize, Math.Pow(Alignment, 2));
            Console.WriteLine("Flags: {0}", Flags);
        }
    }
}
