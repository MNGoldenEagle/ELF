using MiscUtil.IO;
using System;

namespace ELF
{
    public class AddendRelocations : ISection
    {
        private readonly Header Root;

        private readonly SectionHeader Parent;

        public AddendRelocation[] Relocations;

        public AddendRelocations(Header root, SectionHeader parent, EndianBinaryReader reader, int count)
        {
            Root = root;
            Parent = parent;

            Relocations = new AddendRelocation[count];

            for (uint i = 0; i < count; i++)
            {
                Relocations[i] = new AddendRelocation(reader);
            }
        }

        public void Describe()
        {
            Console.WriteLine("ADDEND RELOCATION TABLE FOR {0}",
                Root.SectionHeaders[Parent.Info].GetName());

            SymbolTable symbols = (SymbolTable)Root.SectionHeaders[Parent.LinkIndex].SectionData;

            for (long i = 0; i < Relocations.LongLength; i++)
            {
                var reloc = Relocations[i];
                Console.WriteLine("  {0}. Symbol {1} + {2} fix {3} @ {4,8:X}",
                    (i + 1).ToString().PadLeft(3),
                    symbols.Symbols[reloc.SymbolTargetIndex].GetName(),
                    reloc.Addendum,
                    reloc.Type,
                    reloc.Offset);
            }
        }
    }
}
