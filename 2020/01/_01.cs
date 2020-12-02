using System;
using System.Collections.Generic;

namespace Aoc
{
  public static class _01
  {
    public static void Run()
    {
      string[] input = FileHelper.GetInput(@"./01/input").Split("\n");
      List<int> expenses = new List<int>(Array.ConvertAll(input, int.Parse));

      foreach (int expense in expenses)
      {
        if (expenses.Contains(2020 - expense))
        {
          Console.WriteLine($"Day 1.1:\t{expense * (2020 - expense)}");
          break;
        }
      }

      bool flag = false;
      foreach (int expense in expenses)
      {
        foreach (int compare in expenses)
        {
          int check = (2020 - expense) - compare;
          if (expenses.Contains(check))
          {
            Console.WriteLine($"Day 1.2:\t{expense * compare * check}");
            flag = true;
            break;
          }
        }
        if (flag) break;
      }
    }
  }
}