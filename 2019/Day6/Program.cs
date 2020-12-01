using System;
using System.IO;
using System.Collections.Generic;

namespace Day6
{
    class Orbit
    {
        private int IndirectCount { get; set; } = 0;
        public string Direct { get; set; }
        public string Name { get; set; }

        public Orbit(string d, string n)
        {
            Name = n;
            Direct = d;
        }

        public void Increment()
        {
            IndirectCount++;
        }

        public int GetIndirectOrbits()
        {
            return IndirectCount;
        }
    }

    class Program
    {
        static void Main()
        {
            Dictionary<string, Orbit> orbitmap = new Dictionary<string, Orbit>
            {
                { "COM", new Orbit("None", "COM") }
            };

            int totalOrbits = 0;

            string[] map = File.ReadAllLines(@"C:\Github\AdventOfCode\Day6\Map.txt");
            foreach (string orbits in map)
            {
                string[] nodes = orbits.Split(")");
                if (!orbitmap.ContainsKey(nodes[1]))
                {
                    orbitmap.Add(nodes[1], new Orbit(nodes[0], nodes[1]));
                }
            }

            foreach (string key in orbitmap.Keys)
            {
                Orbit orbit = orbitmap[key];
                if (orbit.Direct == "None") continue;

                string indirect = orbitmap[orbit.Direct].Direct;
                while (indirect != "None")
                {
                    orbit.Increment();
                    indirect = orbitmap[indirect].Direct;
                }

                totalOrbits += orbit.GetIndirectOrbits();
            }

            totalOrbits += orbitmap.Count - 1;
            Console.WriteLine("Part 1: " + totalOrbits.ToString());

            Orbit you = orbitmap["YOU"];
            Orbit san = orbitmap["SAN"];
            string direct = san.Direct;
            List<string> sanPath = new List<string>();
            while (direct != "None")
            {
                sanPath.Add(direct);
                direct = orbitmap[direct].Direct;
            }

            direct = you.Direct;
            List<string> youPath = new List<string>();
            while (!sanPath.Contains(direct))
            {
                youPath.Add(direct);
                direct = orbitmap[direct].Direct;
            }

            int ind = sanPath.IndexOf(direct);
            int totalHops = sanPath.GetRange(0, ind).Count + youPath.Count;
            Console.WriteLine("Part 2: " + totalHops.ToString());
        }
    }
}