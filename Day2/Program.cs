using System;
using System.IO;

namespace Day2
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
            if (data == "") return new int[1] {99};
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
            }

            return clone[0];
        }
    }

    static class Part2
    {
        static public int BruteForce(int[] codes, int num)
        {
            int verb = 0;
            int sum = 0;
            int noun;

            for (noun = 0; noun <= 99; noun++)
            {
                if (sum == num) return (100 * noun) + verb;
                for (verb = 0; verb <= 99; verb++)
                {
                    sum = Part1.Process(codes, noun, verb);
                    if (sum == num) return (100 * noun) + verb;
                }
            }

            return 0;
        }
    }

    class Program
    {
        private static readonly FileHandler FileHandler = new FileHandler("C:\\Github\\AdventOfCode\\Day2\\IntCode.txt");
        static void Main(string[] args)
        {
            string File = FileHandler.Read();
            int[] IntCodes = FileHandler.ParseIntArray(File);
            Console.WriteLine("Part 1: " + Part1.Process(IntCodes, 12, 2).ToString());
            Console.WriteLine("Part 2: " + Part2.BruteForce(IntCodes, 19690720).ToString());
        }
    }
}
