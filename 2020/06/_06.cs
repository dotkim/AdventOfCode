using System;
using System.Linq;

namespace Aoc
{
  public static class _06
  {
    private static string[] _Input = FileHelper.GetInput("06/input").Split("\r\n\r\n");
    public static void Run()
    {
      var gps = _Input.Select(g => g.Split("\r\n").Select(g => g.ToCharArray().AsEnumerable()));
      var p1 = gps.SelectMany(grp => grp.Aggregate((g1, g2) => g1.Union(g2))).Count();
      var p2 = gps.SelectMany(grp => grp.Aggregate((g1, g2) => g1.Intersect(g2))).Count();

      Console.WriteLine($"Day 6.1:\t{p1}");
      Console.WriteLine($"Day 6.2:\t{p2}");
    }
  }
}