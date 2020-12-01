using System.IO;

namespace Aoc
{
  public static class FileHelper
  {
    public static string GetInput(string file)
    {
      return File.ReadAllText(file);
    }
  }
}