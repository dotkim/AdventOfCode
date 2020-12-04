using System;
using System.Text.RegularExpressions;

namespace Aoc
{
  public static class _04
  {
    private static string[] _Input = FileHelper.GetInput("04/input").Split("\r\n\r\n");
    public static void Run()
    {
      int valid = 0;
      int count = 0;
      foreach (string line in _Input)
      {
        count += Regex.Matches(line, @"(eyr|hcl|ecl|iyr|byr|hgt|pid):").Count;
        if (count >= 7) valid++;
        count = 0;
      }
      Console.WriteLine($"Day 4.1:\t{valid}");

      valid = 0;
      foreach (string line in _Input)
      {
        count += Regex.Matches(line, @"eyr:(?:202[0-9]|2030)|hcl:#[0-9a-f]{6}|ecl:(?:amb|blu|brn|gry|grn|hzl|oth)|iyr:(?:201[0-9]|2020)|byr:(?:19[0-9][0-9]|200[0-3])|(?:hgt:(?:59|(?:6[0-9]|7[0-6])in|1(?:[5-8][0-9]|9[0-3])cm))|(?:pid:\d{9}\b)").Count;
        if (count >= 7) valid++;
        count = 0;
      }
      Console.WriteLine($"Day 4.2:\t{valid}");
    }
  }
}