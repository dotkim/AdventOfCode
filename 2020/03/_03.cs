using System;
using System.Collections.Generic;

namespace Aoc
{
  public static class _03
  {
    private static string[] _Input = FileHelper.GetInput("03/input").Split("\r\n");
    public static void Run()
    {
      List<Tuple<int, int>> slopes = new List<Tuple<int, int>>
      {
        (1, 1).ToTuple(),
        (5, 1).ToTuple(),
        (7, 1).ToTuple(),
        (1, 2).ToTuple(),
      };

      long count = CheckSlope(3, 1);
      Console.WriteLine($"Day 3.1:\t{count}");
      foreach (var slope in slopes) count *= CheckSlope(slope.Item1, slope.Item2);
      Console.WriteLine($"Day 3.2:\t{count}");
    }

    private static long CheckSlope(int right, int down)
    {
      int location = 0;
      long count = 0;

      for (int i = 0; i < _Input.Length; i += down)
      {
        string row = _Input[i];
        if (location >= row.Length) location -= row.Length;
        if (row[location] == '#') count++;
        location += right;
      }

      return count;
    }
  }
}