using System;
using System.Collections.Generic;

namespace Aoc
{
  public static class _05
  {
    private static string[] _Input = FileHelper.GetInput("05/input").Split("\r\n");
    public static void Run()
    {
      int min = 1000;
      int max = 0;
      List<int> seats = new List<int>();
      foreach (string pass in _Input)
      {
        if (string.IsNullOrEmpty(pass)) continue;
        var rows = new PlaneRange(0, 127);
        var cols = new PlaneRange(0, 7);
        for (int i = 0; i < 7; i++) rows = Check(pass[i], rows);
        for (int i = 7; i < 10; i++) cols = Check(pass[i], cols);

        if (pass[6] == 'F') rows.Final = rows.Start;
        else if (pass[6] == 'B') rows.Final = rows.End;

        if (pass[9] == 'L') cols.Final = cols.Start;
        else if (pass[9] == 'R') cols.Final = cols.End;

        max = Math.Max(max, (rows.Final * 8) + cols.Final);
        min = Math.Min(min, (rows.Final * 8) + cols.Final);
        seats.Add((rows.Final * 8) + cols.Final);
      }
      Console.WriteLine($"Day 5.1:\t{max}");

      int missing = 0;
      for (int i = min; i < max; i++)
      {
        if (missing != 0) break;
        if (!seats.Contains(i) & (seats.Contains(i-1) & seats.Contains(i+1)))
          missing = i;
      }
      Console.WriteLine($"Day 5.2:\t{missing}");
    }

    private static PlaneRange Check(char mod, PlaneRange range)
    {
      int half = (int)Math.Ceiling((range.End - range.Start) / 2.0);
      if (mod == 'F' | mod == 'L') range.End -= half;
      else if (mod == 'B' | mod == 'R') range.Start += half;
      return range;
    }
  }

  public class PlaneRange
  {
    public int Start { get; set; }
    public int End { get; set; }
    public int Final { get; set; }
    public PlaneRange(int s, int e)
    {
      Start = s;
      End = e;
    }
  }
}