using MiscUtil.IO;
using System;

namespace ELF
{
    public class SymbolTable : ISection
    {
        private readonly Header Root;

        private SectionHeader Parent;

        public Symbol[] Symbols;

        public SymbolTable(Header root, SectionHeader parent, EndianBinaryReader reader, int count)
        {
            Root = root;
            Parent = parent;

            var symbols = new Symbol[count];

            for (uint i = 0; i < count; i++)
            {
                symbols[i] = new Symbol(root, parent, reader);
            }

            Symbols = symbols;
        }

        public void Describe()
        {
            Console.WriteLine("SYMBOL TABLE");

            for (long i = 0; i < Symbols.LongLength; i++)
            {
                var sym = Symbols[i];
                Console.WriteLine("  {0}. {1}", (i + 1).ToString().PadLeft(3), sym.GetName());
                Console.WriteLine("       Symbol Type: {0}  Binding Type: {1}", sym.SymType, sym.BindType);
                Console.WriteLine("       Value: {0,8:X}  Size: {1,8:X}", sym.Value, sym.Size);
                Console.WriteLine("       Padding: {0,8:X} Parent Index: {1,8:X}", sym.Padding, sym.ParentSectionIndex);
            }
        }
    }
}
