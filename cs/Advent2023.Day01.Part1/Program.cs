using Advent2023.Common;

namespace Advent2023.Day01.Part1;

public class Program
{
    public static void Main(string[] args)
    {
        var sum = File.ReadAllLines(args.First())
            .Select(Util.ParseCalibrationValue)
            .Sum();
        Console.WriteLine(sum);
    }
}
