using System;
using System.IO;
using System.Collections.Generic;

namespace Day8
{
    class Layer
    {
        private Dictionary<int, int> Count { get; set; }
        public Layer(string l)
        {
            Count = new Dictionary<int, int>();
            foreach (char num in l)
            {
                Int32.TryParse(num.ToString(), out int n);
                if (!Count.ContainsKey(n)) Count.Add(n, 1);
                else Increment(n);
            }
        }

        private void Increment(int n)
        {
            Count[n]++;
        }

        public Dictionary<int, int> GetCount() { return Count; }
    }

    class Program
    {
        static void Main()
        {
            string image = File.ReadAllLines(@"C:\Github\AdventOfCode\Day8\image.txt")[0];
            //string image = "123456789012";
            int width = 25;
            int height = 6;

            Dictionary<string, Layer> layers = new Dictionary<string, Layer>();

            for (int y = 0; height > y; y++)
            {
                for (int x = 0*y; width > x; x++)
                {
                    string layer = image.Substring(x, width);
                    if (!layers.ContainsKey(layer)) layers.Add(layer, new Layer(layer));
                }
            }

            string check = "";
            int n = 0;
            foreach (string layer in layers.Keys)
            {
                int c = layers[layer].GetCount()[0];
                if (n == 0)
                {
                    n = c;
                    check = layer;
                }
                else if (c < n) {
                    n = c;
                    check = layer;
                }
            }

            Dictionary<int, int> count = layers[check].GetCount();
            int output = count[1] * count[2];
            Console.WriteLine("Part 1: " + output.ToString());
        }
    }
}
