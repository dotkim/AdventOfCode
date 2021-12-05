using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc
{
  public static class _04
  {
    public class Board
    {
      public Board()
      {
        Lots = new List<Lot>();
      }

      public List<Lot> Lots { get; set; }
      public int Size { get; set; } = 5;

      public void Mark(int number)
      {
        Lots.FindAll(n => n.Number == number).ForEach(n => n.Marked = true);
      }

      public bool HasBingo()
      {
        for (int i = 0; i < Size; i++)
        {
          bool row = Lots.FindAll(n => n.Row == i).All(l => l.Marked == true);
          bool column = Lots.FindAll(n => n.Column == i).All(l => l.Marked == true);
          if (row || column) return true;
        }

        return false;
      }
    }

    public class Lot
    {
      public Lot(int num, int r, int c)
      {
        Number = num;
        Row = r;
        Column = c;
      }

      public int Number { get; set; }
      public int Row { get; set; }
      public int Column { get; set; }
      public bool Marked { get; set; } = false;
    }

    private static string[] _Input = FileHelper.GetInput("04/input").Split("\n\n");

    public static void Run()
    {
      List<string> input = new List<string>(_Input);
      List<Board> boards = new List<Board>();

      List<int> drawn = new List<int>(input[0].Split(",").Select(int.Parse).ToList());
      input.RemoveAt(0);

      Regex rx = new Regex(@"\d+");

      foreach (string dat in input)
      {
        var board = new Board();
        var matches = rx.Matches(dat).Select(m => int.Parse(m.Value)).ToList();

        for (int r = 0; r < board.Size; r++)
        {
          for (int c = 0; c < board.Size; c++)
          {
            int index = r * board.Size + c;
            board.Lots.Add(new Lot(matches[index], r, c));
          }
        }

        boards.Add(board);
      }

      List<Board> squid = new List<Board>(boards);

      foreach (int draw in drawn)
      {
        boards.ForEach(b => b.Mark(draw));
        if (boards.Any(b => b.HasBingo()))
        {
          Board winner = boards.Find(b => b.HasBingo());
          int sum = winner.Lots.FindAll(l => l.Marked == false).Sum(n => n.Number);
          Console.WriteLine($"Day 4.1:\t{sum * draw}");
          break;
        }
      }

      foreach (int draw in drawn)
      {
        squid.ForEach(b => b.Mark(draw));
        if (squid.Count == 1)
        {
          if (squid[0].HasBingo())
          {
            int sum = squid[0].Lots.FindAll(l => l.Marked == false).Sum(n => n.Number);
            Console.WriteLine($"Day 4.2:\t{sum * draw}");
            break;
          }
        }
        else if (squid.Any(b => b.HasBingo()))
        {
          squid.RemoveAll(b => b.HasBingo());
        }
      }
    }
  }
}
