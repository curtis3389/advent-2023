using Advent2023.Common;

namespace Advent2023.Day03.Part2;

class Program
{
    static void Main(string[] args)
    {
        var schematic = new EngineSchematic(File.ReadAllLines(args.First()));
        var sum = schematic.GearRatios.Sum();
        Console.WriteLine(sum);
    }
}
