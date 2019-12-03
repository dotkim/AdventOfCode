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
                    x += distance;
                    range = CreateRange(x, point.X);
                    break;
                case "D":
                    x -= distance;
                    range = CreateRange(x, point.X);
                    break;
                case "R":
                    y += distance;
                    range = CreateRange(y, point.Y);
                    break;
                case "L":
                    y -= distance;
                    range = CreateRange(y, point.Y);
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

        static void Main(string[] args)
        {
            Dictionary<Point, Direction> coordinates = new Dictionary<Point, Direction>();
            Point lastPoint;
            int step = 0;

            string[] FileContent = FileHandler.Read();
            foreach (string line in FileContent)
            {
                lastPoint = new Point(0, 0);
                step++;
                foreach (string action in line.Split(","))
                {
                    Entry result = Parse(action, lastPoint);

                    if ((step % 2) == 1)
                    {
                        lastPoint = result.Point;
                        coordinates.Add(result.Point, result.Direction);
                    }
                    else if ((step % 2) == 0)
                    {
                        int[] range = result.Range;
                        
                        for (int i = range[0]; i < range.Length; i++)
                        {
                            if (result.Direction.D == "U" || result.Direction.D == "D")
                            {
                                foreach (Point p in coordinates.Keys)
                                {
                                    if (p.X == i)
                                    {
                                        Console.WriteLine("Found X Coordinate to control: " + p.ToString());
                                    }
                                }
                            }
                            else if (result.Direction.D == "L" || result.Direction.D == "R")
                            {
                                foreach (Point p in coordinates.Keys)
                                {
                                    if (p.Y == i)
                                    {
                                        Console.WriteLine("Found Y Coordinate to control: " + p.ToString());
                                        if (coordinates[p].D == "U" && p.Y > result.Point.Y)
                                        {
                                            Console.WriteLine("Found a wire going up, while being lower than another wire.");
                                            if (result.Direction.D == "L" && p.X < result.Point.X)
                                            {
                                                Console.WriteLine("Found a crossing wire, looking for cross point.");
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        lastPoint = result.Point;
                    }
                }
            }

        }
    }
}
