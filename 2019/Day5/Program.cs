using System;
using System.IO;
using System.Collections.Generic;

namespace Day5
{
    class IntegerComputer
    {
        private string[] Code { get; set; }
        public IntegerComputer(string[] ic)
        {
            Code = ic;
        }

        public int Process(string noun = "", string verb = "")
        {
            string[] input = new string[Code.Length];
            Code.CopyTo(input, 0);

            if (noun != "" & verb != "")
            {
                input[1] = noun;
                input[2] = verb;
            }

            Opcode opcode = new Opcode();
            Compute compute = new Compute();
            int i = 0;
            //for (int i = 0; i < input.Length; i += 4)
            while (i < input.Length)
            {
                Instruction instruction = new Instruction(input[i..(i + 4)]);

                //old code
                string operation = opcode.Parse(input[i]);
                if (operation == "IN") Console.ReadLine();
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

                i += instruction.
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

    class Instruction
    {
        public string Operation { get; }
        public int Next { get; }
        private Opcode OperationCodes = new Opcode();
        private List<Parameter> Parameters { get; set; }

        public Instruction(string[] instruction)
        {
            Operation = OperationCodes.Parse(instruction[0]);
            if (Operation == "IN" | Operation == "OUT")
            {
                Next = 2;
                int[] para = new int[2] { Int32.Parse(instruction[1]), 0 };
                Parameters.Add(new Parameter(para));
            }
            else
            {
                Next = 4;
                foreach (KeyValuePair<int, int> prm in OperationCodes.GetParameterModes(instruction[0]))
                {

                }
            }

        }
    }

    class Parameter
    {
        public int Mode { get; }
        public int Value { get; }

        public Parameter(int[] values, int mode = 0)
        {
            Mode = mode;
            Value = values[Mode];
        }
    }

    class Opcode
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

        public SortedDictionary<int, int> GetParameterModes(string c)
        {
            SortedDictionary<int, int> dict = new SortedDictionary<int, int>();
            if (c.Length <= 1) return dict;
            //{ 1, Int32.Parse(c[^2].ToString()) }
            char[] reverse = c.ToCharArray();
            Array.Reverse(reverse);

            int i = 0;
            foreach (char r in reverse[0..^1])
            {
                i++;
                dict.Add(i, Int32.Parse(r.ToString()));
            }

            return dict;
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

        private static void TEST()
        {
            // Thermal Environment Supervision Terminal
            string[] input = GetFile(@"C:\Github\AdventOfCode\Day5\IntCodes.txt");
            IntegerComputer instruction = new IntegerComputer(input);
            Console.WriteLine("Day 5, Part 1:\t" + instruction.Process().ToString());
        }

        static void Main()
        {
            TEST();
        }
    }
}
