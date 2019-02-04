using MiscUtil.IO;
using System;

namespace ELF
{
    public class SimpleRelocations : ISection
    {
        private readonly Header Root;

        private readonly SectionHeader Parent;

        public Relocation[] Relocations;

        public SimpleRelocations(Header root, SectionHeader parent, EndianBinaryReader reader, int count)
        {
            Root = root;
            Parent = parent;

            Relocations = new Relocation[count];

            for (uint i = 0; i < count; i++)
            {
                Relocations[i] = new Relocation(reader);
            }
        }

        public void Describe()
        {
            Console.WriteLine("RELOCATION TABLE FOR {0}",
                Root.SectionHeaders[Parent.Info].GetName());

            SymbolTable symbols = (SymbolTable)Root.SectionHeaders[Parent.LinkIndex].SectionData;

            for (long i = 0; i < Relocations.LongLength; i++)
            {
                var reloc = Relocations[i];
                Console.WriteLine("  {0}. Symbol {1} fix {2} @ {3,8:X}",
                    (i + 1).ToString().PadLeft(3),
                    symbols.Symbols[reloc.SymbolTargetIndex].GetName(),
                    reloc.Type,
                    reloc.Offset);
            }
        }
    }
}
