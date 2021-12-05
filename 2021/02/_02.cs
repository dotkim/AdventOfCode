using System;
using System.Collections.Generic;
using System.Drawing;

namespace Aoc
{
  public static class _02
  {
    private static string[] _Input = FileHelper.GetInput("02/input").Split("\n");
    public static void Run()
    {
      List<Command> commands = new List<Command>();

      Point location = new Point(0, 0);

      foreach (string act in _Input)
      {
        if (string.IsNullOrEmpty(act)) continue;
        string[] split = act.Split(" ");
        commands.Add(new Command(split[0], int.Parse(split[1])));
      }

      foreach (Command c in commands)
      {
        switch (c.Direction)
        {
          case "forward":
            location.X += c.Distance;
            break;
          case "up":
            location.Y += c.Distance;
            break;
          case "down":
            location.Y -= c.Distance;
            break;
        }
      }

      Console.WriteLine($"Day 2.1:\t{Math.Abs(location.X * location.Y)}");

      location = new Point(0, 0);
      int aim = 0;

      foreach (Command c in commands)
      {
        switch (c.Direction)
        {
          case "forward":
            location.X += c.Distance;
            location.Y += c.Distance * aim;
            break;
          case "up":
            aim -= c.Distance;
            break;
          case "down":
            aim += c.Distance;
            break;
        }
      }
      Console.WriteLine($"Day 2.2:\t{Math.Abs(location.X * location.Y)}");
    }
  }
}
