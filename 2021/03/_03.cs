using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc
{
  public static class _03
  {
    private static string[] _Input = FileHelper.GetInput("03/input").Split("\n");

    public class Count
    {
      public int Zero { get; set; } = 0;
      public int One { get; set; } = 0;

      public string GetGamma()
      {
        if (Zero > One) return "0";
        return "1";
      }

      public string GetEpsilon()
      {
        if (Zero < One) return "0";
        return "1";
      }
    };

    public static void Run()
    {
      List<Count> counts = new List<Count>();
      
      for (int i = 0; i < _Input[0].Length; i++)
      {
        counts.Add(new Count());
      }

      foreach (string lit in _Input)
      {
        if (string.IsNullOrEmpty(lit)) continue;
        for (int i = 0; i < lit.Length; i++)
        {
          char c = lit[i];
          if (c.Equals('1')) counts[i].One++;
          else counts[i].Zero++;
        }
      }

      string gammaBuilder = "";
      string epsilonBuilder = "";

      foreach (Count c in counts)
      {
        gammaBuilder += c.GetGamma();
        epsilonBuilder += c.GetEpsilon();
      }

      int gamma = Convert.ToInt32(gammaBuilder, 2);
      int epsilon = Convert.ToInt32(epsilonBuilder, 2);

      Console.WriteLine($"Day 3.1:\t{gamma * epsilon}");
      //Console.WriteLine($"Day 3.2:\t{}");
    }
  }
}
