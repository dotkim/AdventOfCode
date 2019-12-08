using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day3_2
{
    class FileHandler
    {
        private String Path { get; set; }

        public FileHandler(string path)
        {
            Path = path;
        }

        public string[] Read()
        {
            return File.ReadAllLines(Path);
        }
    }

    class Direction
    {
        public string D { get; set; }
    }

    class Entry
    {
        public Point Point { get; set; }
        public Direction Direction { get; set; }
        public int[] Range { get; set; }

        public Entry(int x, int y, string d, int[] range)
        {
            Point = new Point(x, y);
            Direction = new Direction() { D = d };
            Range = range;
        }
    }

    class Program
    {
        private static readonly FileHandler FileHandler = new FileHandler("C:\\Github\\AdventOfCode\\Day3\\cables.txt");

        private static Entry Parse(string action, Point point)
        {
            string direction = action[0..1];
            Int32.TryParse(action[1..], out int distance);
            int[] range = null;

            int x = point.X;
            int y = point.Y;

            switch (direction)
            {
                case "U":
                    y += distance;
                    range = CreateRange(y, point.Y);
                    break;
                case "D":
                    y -= distance;
                    range = CreateRange(y, point.Y);
                    break;
                case "R":
                    x += distance;
                    range = CreateRange(x, point.X);
                    break;
                case "L":
                    x -= distance;
                    range = CreateRange(x, point.X);
                    break;
            }

            return new Entry(x, y, direction, range);
        }

        private static int[] CreateRange(int x, int y)
        {
            int min = Math.Min(x, y);
            int max = Math.Max(x, y);

            int[] range = new int[max - min];
            int a = 0;
            for (int i = min; i < max; i++) { range[a] = i; a++; }
            return range;
        }

        private static int Part1()
        {
            Dictionary<Point, string> coor = new Dictionary<Point, string>();
            Point prpt;
            coor.Add(new Point(0, 0), "0");

            int wire = 1;

            //string[] FileContent = FileHandler.Read();
            string[] FileContent = "R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83".Split("\n");
            foreach (string line in FileContent)
            {
                prpt = new Point(0, 0);
                int lnid = 1;
                foreach (string mact in line.Split(","))
                {
                    Entry res = Parse(mact, prpt);

                    if (wire == 1)
                    {
                        int[] rang = res.Range;
                        string dir = res.Direction.D;

                        foreach (int i in rang)
                        {
                            Point p = new Point();
                            if (dir == "U" || dir == "D")
                            {
                                p = new Point(res.Point.X, i);
                                if (!coor.ContainsKey(p)) coor.Add(p, lnid.ToString());
                            }
                            else if (dir == "L" || dir == "R")
                            {
                                p = new Point(i, res.Point.Y);
                            }

                            if (!coor.ContainsKey(p) && !p.IsEmpty) coor.Add(p, lnid.ToString());
                        }
                    }
                    else
                    {
                        int[] rang = res.Range;
                        string dir = res.Direction.D;

                        foreach (int i in rang)
                        {
                            Point p = new Point();
                            if (dir == "U" || dir == "D")
                            {
                                p = new Point(res.Point.X, i);
                            }
                            else if (dir == "L" || dir == "R")
                            {
                                p = new Point(i, res.Point.Y);
                            }

                            if (coor.ContainsKey(p) && !p.IsEmpty)
                            {
                                coor[p] = "X";
                            }
                        }
                    }

                    prpt = res.Point;
                    lnid++;
                }

                wire++;
            }

            int manhtnrng = 0;

            foreach (Point key in coor.Keys)
            {
                if (coor[key] == "X")
                {
                    int r = (Math.Abs(key.X) + Math.Abs(key.Y));
                    if (manhtnrng == 0) manhtnrng = r;
                    else manhtnrng = Math.Min(r, manhtnrng);
                }
            }

            return manhtnrng;
        }

        private static int Part2()
        {
            Dictionary<Point, string> coor = new Dictionary<Point, string>();
            Point prpt;
            coor.Add(new Point(0, 0), "0");

            int wire = 1;

            string[] FileContent = FileHandler.Read();
            foreach (string line in FileContent)
            {
                prpt = new Point(0, 0);
                int lnid = 1;
                int range = 0;
                foreach (string mact in line.Split(","))
                {
                    Entry res = Parse(mact, prpt);

                    if (wire == 1)
                    {
                        int[] rang = res.Range;
                        string dir = res.Direction.D;

                        foreach (int i in rang)
                        {
                            Point p = new Point();
                            if (dir == "U" || dir == "D")
                            {
                                p = new Point(res.Point.X, i);
                                if (!coor.ContainsKey(p)) coor.Add(p, lnid.ToString());
                            }
                            else if (dir == "L" || dir == "R")
                            {
                                p = new Point(i, res.Point.Y);
                            }

                            if (!coor.ContainsKey(p) && !p.IsEmpty) coor.Add(p, lnid.ToString());
                        }
                    }
                    else
                    {
                        int[] rang = res.Range;
                        range += rang.Length;
                        string dir = res.Direction.D;

                        foreach (int i in rang)
                        {
                            Point p = new Point();
                            if (dir == "U" || dir == "D")
                            {
                                p = new Point(res.Point.X, i);
                            }
                            else if (dir == "L" || dir == "R")
                            {
                                p = new Point(i, res.Point.Y);
                            }

                            if (coor.ContainsKey(p) && !p.IsEmpty)
                            {
                                coor[p] = "X" + range.ToString();
                            }
                        }
                    }

                    prpt = res.Point;
                    lnid++;
                }

                wire++;
            }

            string lineid = null;
            int jumps = 0;
            int minjumps = 0;
            foreach (Point key in coor.Keys)
            {
               /* jumps++;
                if (coor[key] == "0") continue;
                if (lineid == null) lineid = coor[key];
                if (coor[key].StartsWith("X"))
                {
                    List<Point> line = new List<Point>();
                    line.Add(coor[key]);
                    foreach (Point p in coor.Keys)
                    {
                        if (Int32.Parse(coor[p]) =< Int32.Parse(lineid)) line.Add(p);
                    }
                    line.OrderBy(p => p.X).ThenBy(p => p.Y);
                    foreach (Point p in line)
                    {
                        if (p == key) break;
                    }
                    Int32.TryParse(coor[key][1..], out int j);
                    jumps += j;
                    if (minjumps == 0) minjumps = jumps;
                    else minjumps = Math.Min(minjumps, jumps);
                    lineid = null;
                }
                else
                {
                    lineid = coor[key];
                }*/
            }

            return minjumps;
        }

        static void Main()
        {
            Console.WriteLine("Part 1: " + Part1().ToString());
            Console.WriteLine("Part 2: " + Part2().ToString());
        }
    }
}
