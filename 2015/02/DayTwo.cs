using System;

namespace Aoc
{
  public static class DayTwo
  {
    public static void RunFirstAndSecond()
    {
      string[] input = FileHelper.GetInput(@"./02/input").Split("\n");
      int feetToOrder = 0;
      int ribbonToOrder = 0;

      // To find area: 2*l*w + 2*w*h + 2*h*l
      foreach (string box in input)
      {
        if (string.IsNullOrEmpty(box)) continue;
        string[] dimensions = box.Split("x");
        var l = int.Parse(dimensions[0]);
        var w = int.Parse(dimensions[1]);
        var h = int.Parse(dimensions[2]);

        int area = 2*l*w + 2*w*h + 2*h*l;
        int min = Math.Min(l*w, Math.Min(w*h, h*l));

        int[] ss = new int[] {l, w, h};
        Array.Sort(ss);

        int ribbon = ss[0] + ss[0] + ss[1] + ss[1];
        int bow = l*w*h;

        feetToOrder += area + min;
        ribbonToOrder += ribbon + bow;
      }

      Console.WriteLine($"Day 2.1: The elves should order a total {feetToOrder} sq.ft. of wrapping paper.");
      Console.WriteLine($"Day 2.2: The elves should order a total {ribbonToOrder} ft. of ribbon.");
    }

  }
}