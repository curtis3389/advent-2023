using Advent2023.Common;

namespace Advent2023.Day04.Part2;

class Program
{
    static void Main(string[] args)
    {
        var pile = File.ReadAllLines(args.First())
            .Select(line => new ScratchCard(line))
            .ToList();
        var total = ScratchCard.WinningTotal(pile);
        Console.WriteLine(total);
    }
}
