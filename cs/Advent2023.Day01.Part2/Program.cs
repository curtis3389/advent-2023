using Advent2023.Common;

namespace Advent2023.Day01.Part2;

class Program
{
    static void Main(string[] args)
    {
        var sum = File.ReadAllLines(args.First())
            .Select(Util.BetterParseCalibrationValue)
            .Sum();
        Console.WriteLine(sum);
    }
}
