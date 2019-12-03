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

        public Entry(int x, int y, string d)
        {
            Point = new Point(x, y);
            Direction = new Direction() { D = d };
        }
    }

    class Program
    {
        private static readonly FileHandler FileHandler = new FileHandler("C:\\Github\\AdventOfCode\\Day3\\cables.txt");

        private static Entry Parse(string action, Point point)
        {
            string direction = action[0..1];
            Int32.TryParse(action[1..], out int distance);

            int x = point.X;
            int y = point.Y;

            switch (direction)
            {
                case "U":
                    x += distance;
                    break;
                case "D":
                    x -= distance;
                    break;
                case "R":
                    y += distance;
                    break;
                case "L":
                    y -= distance;
                    break;
            }

            return new Entry(x, y, direction);
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
                        int diff;
                        Point newPoint = result.Point;
                        string dire = result.Direction.D;

                        switch (dire)
                        {
                            case "U":
                                diff = lastPoint.X + newPoint.X;
                                while (lastPoint.X < newPoint.X)
                                {
                                    if (coordinates.ContainsKey(new Point(lastPoint.Y))) Console.WriteLine("Found key");
                                    lastPoint.X++;
                                }
                                break;
                            case "D":
                                diff = lastPoint.X - newPoint.X;
                                while (lastPoint.X > newPoint.X)
                                {
                                    if (coordinates.ContainsKey(new Point(lastPoint.Y))) Console.WriteLine("Found key");
                                    lastPoint.X--;
                                }
                                break;
                            case "R":
                                diff = lastPoint.Y + newPoint.Y;
                                while (lastPoint.Y < newPoint.Y)
                                {
                                    if (coordinates.ContainsKey(new Point(lastPoint.X))) Console.WriteLine("Found key");
                                    lastPoint.Y++;
                                }
                                break;
                            case "L":
                                diff = lastPoint.Y - newPoint.Y;
                                while (lastPoint.Y > newPoint.Y)
                                {
                                    if (coordinates.ContainsKey(new Point(lastPoint.X))) Console.WriteLine("Found key");
                                    lastPoint.Y--;
                                }
                                break;
                        }
                    }
                }
            }

        }
    }
}
