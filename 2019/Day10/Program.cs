using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class Program
    {
        class Asteroid
        {
            public Point Location { get; set; }

            public List<Asteroid> Neighbours = new List<Asteroid>();

            public Asteroid(Point p)
            {
                Location = p;
            }

            public int SetVisible(Asteroid asteroid)
            {
                return Neighbours.Count;
            }
        }

        class Map
        {
            public int Width { get; set; }
            public int Heigth { get; set; }

            private Dictionary<Point, bool> Spots = new Dictionary<Point, bool>();
            private List<Asteroid> Asteroids = new List<Asteroid>();

            public Map(string[] map)
            {
                Width = map[0].Length;
                Heigth = map.Length;

                for (int y = 0; y < Heigth; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Point point = new Point(x, y);
                        char node = map[y][x];
                        bool empty = false;
                        if (node == '#')
                        {
                            empty = true;
                            Asteroids.Add(new Asteroid(point));
                        }
                        Spots.Add(point, empty);
                    }
                }
            }

            public Asteroid Process()
            {
                int highestCount = 0;
                foreach (Asteroid asteroid in Asteroids)
                {
                    asteroid.Neighbours.AddRange(Asteroids.Where(ast => ast.Location != asteroid.Location));
                }
                return new Asteroid(new Point(0, 0));
            }
        }

        static void Main()
        {
            string[] input = ".#..#\n.....\n#####\n....#\n...##".Split("\n");
            Map map = new Map(input);
            map.Process();
        }
    }
}