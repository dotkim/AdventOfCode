using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
  public static class _02
  {
    public static void Run()
    {
      string[] input = FileHelper.GetInput(@"./02/input").Split("\r\n");
      List<string> passwords = new List<string>(input);

      List<string> valid = new List<string>();

      foreach (string pass in passwords)
      {
        Password p = new Password(pass);
        int count = Regex.Matches(p.Pw, p.Policy.ToString()).Count;
        if (count >= p.Nums[0] & count <= p.Nums[1])
          valid.Add(pass);
      }
      Console.WriteLine($"Day 2.1:\t{valid.Count}");

      valid.Clear();
      foreach (string pass in passwords)
      {
        Password p = new Password(pass);
        if (p.Pw[p.Nums[0] - 1] == p.Policy ^
          p.Pw[p.Nums[1] - 1] == p.Policy)
          valid.Add(pass);
      }
      Console.WriteLine($"Day 2.2:\t{valid.Count}");
    }
  }

  public class Password
  {
    public int[] Nums { get; set; }
    public char Policy { get; set; }
    public string Pw { get; set; }

    public Password(string s)
    {
      string[] policy = s.Split(": ")[0].Split(" ");
      Nums = Array.ConvertAll(policy[0].Split("-"), int.Parse);
      Policy = char.Parse(policy[1]);
      Pw = s.Split(": ")[1];
    }
  }
}