using Advent2023.Common;

namespace Advent2023.Day05.Part1;

class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines(args[0]).ToList();
        var almanac = new Almanac(lines);
        var lowestLocation = almanac.LowestInitialLocation();
        Console.WriteLine(lowestLocation);
    }
}
