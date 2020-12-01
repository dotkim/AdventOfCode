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

        public int Process()
        {
            string[] code = new string[Code.Length];
            Code.CopyTo(code, 0);

            Compute compute = new Compute();

            int i = 0;
            //for (int i = 0; i < input.Length; i += 4)
            while (i < code.Length)
            {
                Instruction instruction = new Instruction(code[i..(i + 4)]);

                if (instruction.Operation == "IN")
                {
                    //Console.Write("The program requires an input, please provide a number: ");
                    //string input = Console.ReadLine();
                    string input = "1";
                    code[Int32.Parse(code[i + 1])] = input;
                    i += instruction.Next;
                    continue;
                }

                int position = Int32.Parse(code[i + 4]);
                List<Parameter> parameters = instruction.GetParameters();

                foreach (Parameter parameter in parameters)
                {
                    if (parameter.Mode == 0)
                    {
                        parameter.Value = Int32.Parse(code[parameter.Value]);
                    }
                    /*else if (parameter.Mode == 1)
                    {
                        code[i + parameter.Number] = parameter.Value.ToString();
                    }*/
                }

                code[position] = compute
                    .Get(instruction.Operation)
                    .Invoke(parameters[0].Value, parameters[1].Value)
                    .ToString();

                i += instruction.Next;

                //old code
                /*string operation = opcode.Parse(code[i]);
                if (operation == "IN") Console.ReadLine();
                if (operation == "HALT") break;

                int[] positions = Array.ConvertAll(code[(i + 1)..(i + 4)], Int32.Parse);

                int[] sections = new int[3];
                for (int index = 0; index < sections.Length; index++)
                {
                    sections[index] = Int32.Parse(code[positions[index]]);
                }

                code[positions[2]] = compute
                    .Get(operation)
                    .Invoke(sections[0], sections[1])
                    .ToString();*/
            }

            return Int32.Parse(code[0]);
        }
    }

    class Instruction
    {
        public string Operation { get; }
        public int Next { get; }
        private Opcode OperationCodes = new Opcode();
        private List<Parameter> Parameters { get; set; } = new List<Parameter>();

        public Instruction(string[] instruction)
        {
            Operation = OperationCodes.Parse(instruction[0]);
            if (Operation == "IN" | Operation == "OUT")
            {
                Next = 2;
                int prm = Int32.Parse(instruction[1]);
                Parameters.Add(new Parameter(1, prm));
            }
            else
            {
                Next = 4;
                SortedDictionary<int, int> modes = OperationCodes.GetParameterModes(instruction[0]);
                if (modes.Count != 0)
                {
                    foreach (KeyValuePair<int, int> prm in modes)
                    {
                        Parameters.Add(new Parameter(prm.Key, Int32.Parse(instruction[prm.Key]), prm.Value));
                    }
                }
                else
                {
                    int i = 1;
                    foreach (string prm in instruction[1..])
                    {
                        Parameters.Add(new Parameter(i, Int32.Parse(prm), 0));
                        i ++;
                    }
                }
            }
        }

        public List<Parameter> GetParameters() { return Parameters; }
    }

    class Parameter
    {
        public int Number { get; }
        public int Mode { get; }
        public int Value { get; set; }

        public Parameter(int number, int value, int mode = 0)
        {
            Number = number;
            Value = value;
            Mode = mode;
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
