namespace Advent2023.Common.Tests;

public class EngineSchematicTests
{
    private static readonly IList<string> examlpeLines =
    new List<string> {
        "467..114..",
        "...*......",
        "..35..633.",
        "......#...",
        "617*......",
        ".....+.58.",
        "..592.....",
        "......755.",
        "...$.*....",
        ".664.598..",
    };

    private static readonly IList<int> examplePartNumbers = new List<int>
    {
        467, 35, 633, 617, 592, 755, 664, 598,
    };

    private static readonly IList<int> exampleGearRatios = new List<int> { 16345, 451490 };

    [Fact]
    public void ParsesExample()
    {
        var schematic = new EngineSchematic(examlpeLines);

        Assert.DoesNotContain(114, schematic.PartNumbers);
        Assert.DoesNotContain(58, schematic.PartNumbers);
        foreach (var partNumber in examplePartNumbers)
        {
            Assert.Contains(partNumber, schematic.PartNumbers);
        }

        Assert.Equal(4361, schematic.PartNumbers.Sum());
    }

    [Fact]
    public void ParsesGearRatios()
    {
        var schematic = new EngineSchematic(examlpeLines);

        foreach (var gearRatio in exampleGearRatios)
        {
            Assert.Contains(gearRatio, schematic.GearRatios);
        }

        Assert.Equal(467835, schematic.GearRatios.Sum());
    }
}
