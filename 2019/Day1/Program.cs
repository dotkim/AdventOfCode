using System;
using System.IO;

namespace Day1
{
    class FileHandler
    {
        private String Path { get; set; }

        public FileHandler(string path)
        {
            Path = path;
        }

        public string[] ReadLines()
        {
            return File.ReadAllLines(Path);
        }
    }

    class Part1
    {
        // Find the sum of the required fuel for all modules
        private readonly FileHandler FileHandler;
        public readonly string[] FileContent;

        public Part1(string path)
        {
            FileHandler = new FileHandler(path);
            FileContent = FileHandler.ReadLines();
        }

        public int GetFuel()
        {
            int sum = 0;

            foreach (string line in FileContent)
            {
                int mass = ConvertToInt(line);
                sum += (mass / 3) - 2;
            }

            return sum;
        }

        public int ConvertToInt(string mass)
        {
            if (!Int32.TryParse(mass, out int i))
            {
                i = 0;
            }
            return i;
        }
    }

    class Part2
    {
        // Find the absolute sum for the mass of the added fuel.
        // I need to calculate the mass of the fuel as well.
        private readonly Part1 Part1;
        
        public Part2(string path)
        {
            // I use the GetFuel method in the second part
            Part1 = new Part1(path);
        }

        private int Reduce(int mass)
        {
            int sum = 0;
            while (mass > 0)
            {
                mass = (mass / 3) - 2;
                if (mass > 0) sum += mass;
            }
            return sum;
        }

        public int GetFuel()
        {
            int sum = 0;
            foreach (string line in Part1.FileContent)
            {
                sum += Reduce(Part1.ConvertToInt(line));
            }
            return sum;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Part 1: " + new Part1(args[0]).GetFuel().ToString());
            Console.WriteLine("Part 2: " + new Part2(args[0]).GetFuel().ToString());
        }
    }
}