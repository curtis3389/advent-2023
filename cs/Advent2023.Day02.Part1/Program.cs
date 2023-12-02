using Advent2023.Common;

namespace Advent2023.Day02.Part1;

class Program
{
    static void Main(string[] args)
    {
        var sum = File.ReadAllLines(args.First())
            .Select(line => new CubeGame(line))
            .Where(game => game.Possible(12, 13, 14))
            .Select(game => game.Id)
            .Sum();
        Console.WriteLine(sum);
    }
}
