using System;
using System.IO;
using System.Collections.Generic;

namespace Day8
{
    class Layer
    {
        public int ID { get; set; }
        private Dictionary<int, int> Count { get; set; }
        public string Data { get; set; }
        public Layer(string l)
        {
            Data = l;
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
            string imageData = File.ReadAllLines(@"C:\Github\AdventOfCode\Day8\image.txt")[0];
            int width = 25;
            int height = 6;

            Dictionary<int, Layer> layers = new Dictionary<int, Layer>();

            int id = 1;
            int lSize = width * height;
            for (int i = 0; imageData.Length > i; i += lSize)
            {
                int end = i + lSize;
                string layer = imageData[i..end];
                layers.Add(id, new Layer(layer));
                id++;
            }

            int check = 0;
            int n = 0;
            foreach (int lid in layers.Keys)
            {
                int c = layers[lid].GetCount()[0];
                if (n == 0)
                {
                    n = c;
                    check = lid;
                }
                else if (c < n) {
                    n = c;
                    check = lid;
                }
            }

            Dictionary<int, int> count = layers[check].GetCount();
            int output = count[1] * count[2];
            Console.WriteLine("Part 1: " + output.ToString());

            List<String> image = new List<string>();
            string pixels = "";

            foreach (int lid in layers.Keys)
            {
                string layer = layers[lid].Data;

                foreach (char pixel in layer)
                {
                    if (pixels.Length == width)
                    {
                        image.Add(pixels);
                        Console.WriteLine(pixels);
                        pixels = "";
                    }
                    if (!pixel.Equals('2')) pixels += pixel.ToString();
                }
            }
        }
    }
}
