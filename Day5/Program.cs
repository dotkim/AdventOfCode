using System;
using System.IO;
using System.Collections.Generic;

namespace Day5
{
    class Instruction
    {
        private string[] IntCode { get; set; }
        public Instruction(string[] ic, string noun = "", string verb = "")
        {
            IntCode = ic;
            if (noun != "" & verb != "")
            {
                IntCode[1] = noun;
                IntCode[2] = verb;
            }
        }

        public int Process()
        {
            string[] input = new string[IntCode.Length];
            IntCode.CopyTo(input, 0);

            for (int i = 0; i < input.Length; i += 4)
            {
                string oper = new Op().Parse(input[i]);
                if (oper == "EXIT") break;

                int[] sections = Array.ConvertAll(input[(i + 1)..(i + 4)], Int32.Parse);

                int[] positions = new int[3];

                for (int index = 0; index < positions.Length; index++)
                {
                    positions[index] = Int32.Parse(input[sections[index]]);
                }

                input[sections[2]] = new Compute(positions[0], positions[1])
                    .Get(oper)
                    .Invoke()
                    .ToString();
            }

            return Int32.Parse(input[0]);
        }
    }

    class Op
    {
        public Dictionary<string, string> Code = new Dictionary<string, string>
        {
            { "01", "ADD" },
            { "02", "MUL" },
            { "03", "STDIN" },
            { "04", "STDOUT" },
            { "99", "EXIT" },
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
        private int Input { get; set; }
        private int Num { get; set; }

        private Dictionary<string, Func<int>> Actions = new Dictionary<string, Func<int>>();

        public Compute(int i, int n)
        {
            Input = i;
            Num = n;
            Actions.Add("ADD", Add);
            Actions.Add("MUL", Multiply);
        }

        public int Add()
        {
            return Input + Num;
        }

        public int Multiply()
        {
            return Input * Num;
        }

        public Func<int> Get(string key) { return Actions[key]; }
    }

    class Program
    {
        static void Main()
        {
            string fileContent = File.ReadAllText(@"C:\Github\AdventOfCode\Day2\IntCode.txt");
            string[] input = fileContent.Split(',');
            Console.WriteLine("Part 1: " + new Instruction(input, "12", "2").Process().ToString());
        }
    }
}
