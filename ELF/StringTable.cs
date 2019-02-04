using MiscUtil.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELF
{
    public class StringTable : ISection
    {
        private readonly string chars;

        public Dictionary<int, string> Strings;

        public StringTable(EndianBinaryReader reader, int size)
        {
            chars = new string(Encoding.ASCII.GetChars(Encoding.Convert(Encoding.ASCII, Encoding.Default, reader.ReadBytes(size))));
            var strings = new Dictionary<int, string>();
            int start = 0, count = 0;

            for (int i = 0; i < chars.Length; i++)
            {
                char b = chars[i];

                if (b != '\0')
                {
                    count++;
                }
                else
                {
                    var str = chars.Substring(start, count);
                    strings.Add(start, str);
                    start = i + 1;
                    count = 0;
                }
            }

            Strings = strings;
        }

        public string GetString(int index)
        {
            if (Strings.ContainsKey(index))
            {
                return Strings[index];
            }

            if (index < 0 || index >= chars.Length)
            {
                throw new IndexOutOfRangeException("String index exceeds size of string table.");
            }

            var terminator = chars.IndexOf('\0', index);

            if (terminator < 0)
            {
                throw new FormatException("String table does not end with null byte.");
            }

            var str = chars.Substring(index, terminator - index);
            Strings.Add(index, str);
            return str;
        }

        public void Describe()
        {
            Console.WriteLine("STRING TABLE");
            var valueList = new List<string>(Strings.Values);
            for (int i = 0; i < valueList.Count; i++)
            {
                Console.WriteLine("  {0}. {1}", i + 1, valueList[i]);
            }
        }
    }
}
