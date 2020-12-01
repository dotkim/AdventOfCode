using System;

namespace Aoc
{
  public static class _01
  {
    public static void Run()
    {
      string input = FileHelper.GetInput(@"./01/input");
      int floor = 0;
      bool FirstBasement = true;
      int FirstBasementValue = 1;

      for (int i = 0; i < input.Length; i++)
      {
        if (input[i].Equals('(')) floor++;
        else floor--;
        if (floor == -1 && FirstBasement)
        {
          FirstBasementValue += i;
          FirstBasement = false;
        }
      }

      Console.WriteLine($"Day 1.1:\tSanta will go to floor {floor.ToString()}");
      Console.WriteLine($"Day 1.2:\tSanta entered the basement at character {FirstBasementValue}");
    }

  }
}