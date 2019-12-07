using System;
using System.IO;

namespace Day5
{
    class FileHandler
    {
        private String Path { get; set; }

        public FileHandler(string path)
        {
            Path = path;
        }

        public string Read()
        {
            return File.ReadAllText(Path);
        }

        public int[] ParseIntArray(string data)
        {
            if (data == "") return new int[1] { 99 };
            return Array.ConvertAll(data.Split(','), int.Parse);
        }
    }

    static class Part1
    {
        static public int Process(int[] codes, int noun, int verb)
        {
            int[] clone = new int[codes.Length];
            codes.CopyTo(clone, 0);

            clone[1] = noun;
            clone[2] = verb;

            for (int i = 0; i < clone.Length; i += 4)
            {
                if (clone[i] == 99) break;

                int pos1 = clone[i + 1];
                int pos2 = clone[i + 2];
                int pos3 = clone[i + 3];

                if (clone[i] == 1)
                {
                    clone[pos3] = clone[pos1] + clone[pos2];
                }
                else if (clone[i] == 2)
                {
                    clone[pos3] = clone[pos1] * clone[pos2];
                }
                else if (clone[i] == 3)
                {

                }
                else if (clone[i] == 4)
                {

                }
            }

            return clone[0];
        }
    }
    class Program
    {
        private static readonly FileHandler FileHandler = new FileHandler(@"C:\Github\AdventOfCode\Day5\IntCodes.txt");
        static void Main(string[] args)
        {
            string File = FileHandler.Read();
            int[] IntCodes = FileHandler.ParseIntArray(File);
            Console.WriteLine("Part 1: " + Part1.Process(IntCodes, 0, 0));
        }
    }
}
