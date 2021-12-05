namespace Aoc
{
  public class Command
  {
    public string Direction { get; set; }
    public int Distance { get; set; }

    public Command(string dir, int dis)
    {
      Direction = dir;
      Distance = dis;
    }
  }
}