using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Advent2023.Common;

public enum BlockColor
{
    Blue,
    Green,
    Red,
}

public class Blocks
{
    public int Count { get; set; }
    public BlockColor Color { get; set; }

    public static Blocks Parse(string s)
    {
        var match = Regex.Match(s, "(\\d+)\\s+(\\w+)");
        return new Blocks
        {
            Count = int.Parse(match.Groups[1].Value),
            Color = ToBlockColor(match.Groups[2].Value),
        };
    }

    private static BlockColor ToBlockColor(string s)
    {
        return s switch
        {
            "blue" => BlockColor.Blue,
            "green" => BlockColor.Green,
            "red" => BlockColor.Red,
        };
    }
}

public class CubeGame
{
    private readonly int id;
    private readonly IList<IList<Blocks>> rounds;

    public CubeGame(string line)
    {
        var parts = line.Split(':');
        var left = parts[0];
        var right = parts[1];
        this.id = int.Parse(Regex.Match(left, "Game (\\d+)").Groups[1].Value);
        this.rounds = right.Split(';')
            .Select(draw => (IList<Blocks>)draw
                .Split(',')
                .Select(Blocks.Parse)
                .ToList())
            .ToList();
    }

    public int Id => this.id;
    public IList<IList<Blocks>> Rounds => this.rounds;

    public bool Possible(int redCount, int greenCount, int blueCount)
    {
        return this.rounds.All(round => round.All(draw => draw.Color switch
        {
            BlockColor.Blue => draw.Count <= blueCount,
            BlockColor.Green => draw.Count <= greenCount,
            BlockColor.Red => draw.Count <= redCount,
            _ => throw new ArgumentOutOfRangeException(),
        }));
    }

    public int Power()
    {
        var all = this.rounds.SelectMany(r => r).ToList();
        var minRed = all.Where(b => b.Color == BlockColor.Red).Max(b => b.Count);
        var minGreen = all.Where(b => b.Color == BlockColor.Green).Max(b => b.Count);
        var minBlue = all.Where(b => b.Color == BlockColor.Blue).Max(b => b.Count);
        return minRed * minGreen * minBlue;
    }
}
