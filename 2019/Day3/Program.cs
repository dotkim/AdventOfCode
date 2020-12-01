using System;
using System.IO;
using System.Collections.Generic;

namespace Day3
{
    class Line
    {
        public int ID { get; set; }
        public Dictionary<Coordinate, Point> Coordinates { get; set; }
        private Program.Wire Wire { get; set; }
        public Coordinate Start { get; set; }
        public Coordinate End { get; set; }
        public int Lenght { get; set; }

        public Line(int id, Program.Wire w)
        {
            ID = id;
            Wire = w;
            Coordinates = new Dictionary<Coordinate, Point>();
            Lenght = 0;
        }

        public void Add(Point p)
        {
            Dictionary<Coordinate, Point> wireCoordinates = Wire.GetCoordinates();
            if (!wireCoordinates.ContainsKey(p.Coordinate)) {
                Coordinates.Add(p.Coordinate, p);
            }
            if (Coordinates.Count == 0) Start = p.Coordinate;
            End = p.Coordinate;
            Lenght = Math.Abs(Start.X - End.X) + Math.Abs(Start.Y - End.Y);
        }
        public Dictionary<Coordinate, Point> Get() { return Coordinates; }
        public int GetCrossLength(Coordinate cross)
        {
            return Math.Abs(Start.X - cross.X) + Math.Abs(Start.Y - cross.Y);
        }
    }

    class Point
    {
        public Coordinate Coordinate { get; set; }
        public Line Line { get; set; }
        public bool Cross { get; set; }

        public Point(int x, int y, Line l, bool c = false)
        {
            Coordinate = new Coordinate(x, y);
            Line = l;
            Cross = c;
        }

        public bool IsCross() { return Cross; }
        public void Up(int y)
        {
            for (int i = 0; y >= i; i++)
            {
                Line.Add(new Point(Coordinate.X, Coordinate.Y + i, Line));
            }
        }
        public void Down(int y)
        {
            for (int i = 0; y >= i; i++)
            {
                Line.Add(new Point(Coordinate.X, Coordinate.Y - i, Line));
            }
        }

        public void Right(int x)
        {
            for (int i = 0; x >= i; i++)
            {
                Line.Add(new Point(Coordinate.X + i, Coordinate.Y, Line));
            }
        }
        public void Left(int x)
        {
            for (int i = 0; x >= i; i++)
            {
                Line.Add(new Point(Coordinate.X - i, Coordinate.Y, Line));
            }
        }

        public void SetCross(bool c) { Cross = c; }
        public int GetId() { return Line.ID; }
    }

    struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class Program
    {
        public class Increment
        {
            private int Number { get; set; }
            public Increment(int start)
            {
                Number = start;
            }

            public int Get()
            {
                Number++;
                return Number;
            }
        }

        public class Wire
        {
            private int ID { get; set; } = 0;
            private Dictionary<Coordinate, Point> Coordinates { get; set; }
            private Dictionary<int, Line> Lines { get; set; }
            public int Length { get; set; }

            public Wire()
            {
                ID = new Increment(ID).Get();
                Coordinates = new Dictionary<Coordinate, Point>();
                Lines = new Dictionary<int, Line>();
                Length = 0;
            }

            public void AddLine(Line l)
            {
                Lines.Add(l.ID, l);
                Length += l.Lenght;
            }

            public void AddCoordinate(Point p) { Coordinates.Add(p.Coordinate, p); }
            public Dictionary<Coordinate, Point> GetCoordinates() { return Coordinates; }
            public Dictionary<int, Line> GetLines() { return Lines; }
        }

        static void Main(string[] args)
        {
            string[] commands = File.ReadAllLines(@"C:\Github\AdventOfCode\Day3\cables.txt");

            List<Wire> wires = new List<Wire>();

            foreach (string cmd in commands)
            {
                Wire wire = new Wire();
                Line zero = new Line(0, wire);
                Point previous = new Point(0, 0, zero);
                wire.AddCoordinate(previous);
                wire.AddLine(zero);

                Increment linecrement = new Increment(0);

                foreach (string action in cmd.Split(","))
                {
                    Line line = new Line(linecrement.Get(), wire);

                    string direction = action[0..1];
                    Int32.TryParse(action[1..], out int distance);

                    int x = previous.Coordinate.X;
                    int y = previous.Coordinate.Y;
                    Point start = new Point(x, y, line);

                    switch (direction)
                    {
                        case "U":
                            y += distance;
                            start.Up(distance);
                            break;
                        case "D":
                            y -= distance;
                            start.Down(distance);
                            break;
                        case "R":
                            x += distance;
                            start.Right(distance);
                            break;
                        case "L":
                            x -= distance;
                            start.Left(distance);
                            break;
                    }
                    previous = new Point(x, y, line);
                    Dictionary<Coordinate, Point> coords = line.Get();
                    foreach (Coordinate coor in coords.Keys) { wire.AddCoordinate(coords[coor]); }
                    wire.AddLine(line);
                }

                wires.Add(wire);
            }

            Dictionary<Coordinate, Point> wire1 = wires[0].GetCoordinates();
            Dictionary<Coordinate, Point> wire2 = wires[1].GetCoordinates();

            foreach (Coordinate coor in wire2.Keys)
            {
                if (coor.X == 0 && coor.Y == 0) continue;
                if (wire1.ContainsKey(coor))
                {
                    wire1[coor].SetCross(true);
                    wire2[coor].SetCross(true);
                }
            }

            int range = 0;
            foreach (Coordinate coor in wire1.Keys)
            {
                if (wire1[coor].Cross)
                {
                    int x = wire1[coor].Coordinate.X;
                    int y = wire1[coor].Coordinate.Y;
                    int r = (Math.Abs(x) + Math.Abs(y));
                    if (range == 0) range = r;
                    else range = Math.Min(r, range);
                }
            }
            Console.WriteLine("Part 1: " + range.ToString());

            int minRange = 0;
            Dictionary<int, Line> w1lines = wires[0].GetLines();
            Dictionary<int, Line> w2lines = wires[1].GetLines();
            foreach (Coordinate coor in wire1.Keys)
            {
                if (wire1[coor].Cross)
                {
                    int w1line = wire1[coor].Line.ID - 1;
                    int crosslen = w1lines[w1line + 1].GetCrossLength(coor);
                    int w1len = crosslen;
                    while (w1line != 0)
                    {
                        w1len += w1lines[w1line].Lenght;
                        w1line = w1lines[w1line - 1].ID;
                    }

                    int w2line = wire2[coor].Line.ID - 1;
                    crosslen = w2lines[w2line + 1].GetCrossLength(coor);
                    int w2len = crosslen;
                    while (w2line != 0)
                    {
                        w2len += w2lines[w2line].Lenght;
                        w2line = w2lines[w2line - 1].ID;
                    }

                    if (minRange == 0) minRange = w1len + w2len;
                    else minRange = Math.Min(minRange, (w1len + w2len));
                }
            }

            Console.WriteLine("Part 2: " + minRange.ToString());
        }
    }
}
