using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

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

            string[] FileContent = FileHandler.Read();
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

            return manhtnrng;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Part 1: " + Part1().ToString());
            Console.WriteLine("Part 2: " + Part2().ToString());
        }
    }
}
