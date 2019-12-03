using System;
using System.IO;

namespace Day3
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

    class Program
    {
        public class Coordinate
        {
            public int[] point;
            public Coordinate(int x, int y)
            {
                point = Point(x, y);
            }

            static int[] Point(int x, int y)
            {
                return new int[2] { x, y };
            }
        }

        class Turtle
        {
            private Coordinate Start { get; set; }
            private Coordinate Current { get; set; }

            public Turtle(int x, int y)
            {
                Start = new Coordinate(x, y);
            }

            public int[] Move(string action)
            {
                string direction = action[0..1];
                Int32.TryParse(action[1..], out int distance);

                if (Current == null) Current = Start;

                int x = Current.point[0];
                int y = Current.point[1];

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

                Current = new Coordinate(x, y);
                return Current.point;
            }

            public int Distance(int[] p1, int[] p2)
            {
                return (Math.Abs(p1[0] - p2[0]) + Math.Abs(p1[0] - p2[0]));
            }
        }

        private static readonly FileHandler FileHandler = new FileHandler("C:\\Github\\AdventOfCode\\Day3\\cables.txt");
        
        static void Main(string[] args)
        {
            string[] FileContent = FileHandler.Read();
            
            foreach (string line in FileContent)
            {
                Turtle turtle = new Turtle(0, 0);

                string[] actions = line.Split(',');
                for (int i = 0; i < actions.Length; i++)
                {
                    int[] point = turtle.Move(actions[i]);
                    Console.WriteLine(point[0].ToString() + ", " + point[1].ToString());
                }
            }
        }
    }
}
