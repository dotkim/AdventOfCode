using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
  public static class _05
  {
    public static void Run()
    {
      string[] input = FileHelper.GetInput(@"./05/input").Split("\n");
      List<string> niceWords = new List<string>();

      foreach (string word in input)
      {
        if (!HasDuplicateChars(word)) continue;
        if (!HasSafeWords(word)) continue;
        if (!HasVowels(word)) continue;

        niceWords.Add(word);
      }

      Console.WriteLine($"Day 5.1:\t{niceWords.Count.ToString()}");
    }

    private static bool HasVowels(string s)
    {
      int count = Regex.Matches(s, @"[aeiou]").Count;
      if (count >= 3) return true;
      return false;
    }

    private static bool HasDuplicateChars(string s)
    {
      if (Regex.Matches(s, @"([a-z])\1").Count > 0) return true;
      return false;
    }

    private static bool HasSafeWords(string s)
    {
      if (s.Contains("ab") |
          s.Contains("cd") |
          s.Contains("pq") |
          s.Contains("xy")) return false;
      return true;
    }
  }
}