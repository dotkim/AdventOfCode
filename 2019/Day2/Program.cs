using System;
using System.IO;
using System.Collections.Generic;

namespace Day2
{
    class Instruction
    {
        private string[] IntCode { get; set; }
        public Instruction(string[] ic)
        {
            IntCode = ic;
        }

        public int Process(string noun = "", string verb = "")
        {
            string[] input = new string[IntCode.Length];
            IntCode.CopyTo(input, 0);

            if (noun != "" & verb != "")
            {
                input[1] = noun;
                input[2] = verb;
            }

            Opcodes opcodes = new Opcodes();
            Compute compute = new Compute();

            for (int i = 0; i < input.Length; i += 4)
            {
                string operation = opcodes.Parse(input[i]);
                if (operation == "HALT") break;

                int[] positions = Array.ConvertAll(input[(i + 1)..(i + 4)], Int32.Parse);

                int[] sections = new int[3];
                for (int index = 0; index < sections.Length; index++)
                {
                    sections[index] = Int32.Parse(input[positions[index]]);
                }

                input[positions[2]] = compute
                    .Get(operation)
                    .Invoke(sections[0], sections[1])
                    .ToString();
            }

            return Int32.Parse(input[0]);
        }

        public int FindCombination(int num)
        {
            int verb = 0;
            int noun;

            int sum = 0;

            for (noun = 0; noun <= 99; noun++)
            {
                if (sum == num) return (100 * noun) + verb;
                for (verb = 0; verb <= 99; verb++)
                {
                    sum = Process(noun.ToString(), verb.ToString());
                    if (sum == num) return (100 * noun) + verb;
                }
            }

            return 0;
        }
    }

    class Opcodes
    {
        private Dictionary<string, string> Code = new Dictionary<string, string>
        {
            { "01", "ADD" },
            { "02", "MUL" },
            { "03", "IN" },
            { "04", "OUT" },
            { "99", "HALT" },
            { "00", "UNKNOWN" }
        };

        public string Parse(string c)
        {
            if (c.Length == 1) c = "0" + c;
            if (Code.ContainsKey(c)) return Code[c];
            return Code["00"];
        }
    }

    class Compute
    {
        private Dictionary<string, Func<int, int, int>> Actions = new Dictionary<string, Func<int, int, int>>();

        public Compute()
        {
            Actions.Add("ADD", Add);
            Actions.Add("MUL", Multiply);
        }

        public int Add(int n1, int n2)
        {
            return n1 + n2;
        }

        public int Multiply(int n1, int n2)
        {
            return n1 * n2;
        }

        public Func<int, int, int> Get(string key) { return Actions[key]; }
    }

    class Program
    {
        private static string[] GetFile(string file) { return File.ReadAllText(file).Split(","); }

        private static void GA()
        {
            // Gravity Assist
            string[] input = GetFile(@"C:\Github\AdventOfCode\Day2\IntCode.txt");
            Instruction instruction = new Instruction(input);
            Console.WriteLine("Day 2, Part 1:\t" + instruction.Process("12", "2").ToString());
            Console.WriteLine("Day 2, Part 2:\t" + instruction.FindCombination(19690720).ToString());
        }

        static void Main()
        {
            GA();
        }
    }
}
