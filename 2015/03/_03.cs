using System;
using System.Collections.Generic;
using System.Drawing;

namespace Aoc
{
  public static class _03
  {
    public static void Run()
    {
      string input = FileHelper.GetInput(@"./03/input");
      Dictionary<Point, int> houses = new Dictionary<Point, int>();

      Point location = new Point(0, 0);
      houses.Add(location, 1);

      foreach (char c in input)
      {
        location = CheckDirection(location, c);
        if (houses.ContainsKey(location)) houses[location] += 1;
        else houses.Add(location, 1);
      }
      Console.WriteLine($"Day 3.1:\t{houses.Count}");

      houses.Clear();
      Point santaLoc = new Point(0, 0);
      Point roboLoc = new Point(0, 0);
      houses.Add(santaLoc, 2);
      
      for (int i = 0; i < input.Length; i+=2)
      {
        santaLoc = CheckDirection(santaLoc, input[i]);
        roboLoc = CheckDirection(roboLoc, input[i+1]);

        if (houses.ContainsKey(santaLoc)) houses[santaLoc] += 1;
        else houses.Add(santaLoc, 1);

        if (houses.ContainsKey(roboLoc)) houses[roboLoc] += 1;
        else houses.Add(roboLoc, 1);
      }
      Console.WriteLine($"Day 3.2:\t{houses.Count}");
    }

    private static Point CheckDirection(Point loc, char dir)
    {
      switch (dir)
      {
        case '^': loc.Y++; break;
        case '<': loc.X--; break;
        case '>': loc.X++; break;
        case 'v': loc.Y--; break;
      }

      return loc;
    }
  }
}