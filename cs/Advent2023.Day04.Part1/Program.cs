using Advent2023.Common;

namespace Advent2023.Day04.Part1;

class Program
{
    static void Main(string[] args)
    {
        var total = File.ReadAllLines(args.First())
            .Select(line => new ScratchCard(line))
            .Select(card => card.Points)
            .Sum();
        Console.WriteLine(total);
    }
}
