using System;
using System.Security.Cryptography;
using System.Text;

namespace Aoc
{
  public static class _04
  {
    public static void Run()
    {
      string input = "yzbqklnj";
      string hash = ComputeMD5(input + "1");
      int count = 1;

      bool first = false;
      bool second = false;

      while (!first | !second)
      {
        count++;
        hash = ComputeMD5(input + count.ToString());
        if (hash.Substring(0, 5) == "00000" & !first)
        {
          Console.WriteLine($"Day 4.1:\t{count.ToString()}");
          first = true;
        }
        else if (hash.Substring(0, 6) == "000000" & !second)
        {
          Console.WriteLine($"Day 4.2:\t{count.ToString()}");
          second = true;
        }
      }
    }

    private static string ComputeMD5(string input)
    {
      using (MD5 md5 = MD5.Create())
      {
        byte[] inBytes = Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inBytes);

        var sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
          sb.Append(hashBytes[i].ToString("X2"));
        
        return sb.ToString();
      }
    }
  }
}