using Advent2023.Common;

namespace Advent2023.Day02.Part2;

class Program
{
    static void Main(string[] args)
    {
        var sum = File.ReadAllLines(args.First())
            .Select(line => new CubeGame(line))
            .Select(game => game.Power())
            .Sum();
        Console.WriteLine(sum);
    }
}
