using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc
{
  public static class _01
  {
    public static void Run()
    {
      string[] input = FileHelper.GetInput(@"./01/input").Split("\n");
      List<string> sanitized = input.Where(line => line != "").ToList();
      List<int> height = new List<int>(Array.ConvertAll(sanitized.ToArray(), int.Parse));

      int count = 0;
      int prev = 0;
      foreach (int h in height)
      {
        if (prev == 0)
        {
          prev = h;
          continue;
        }
        if (prev < h) count++;
        prev = h;
      }

      Console.WriteLine($"Day 1.1:\t{count}");

      count = 0;
      prev = 0;
      for (int i = 0; i < height.Count-2; i++)
      {
        int h = height.GetRange(i, 3).Sum();
        if (prev == 0)
        {
          prev = h;
          continue;
        }
        if (prev < h) count++;
        prev = h;
      }

      Console.WriteLine($"Day 1.2:\t{count}");
    }
  }
}