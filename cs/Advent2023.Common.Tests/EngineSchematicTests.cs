// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common.Tests;

/// <summary>
/// Represents tests for the EngineSchematic type.
/// </summary>
public class EngineSchematicTests
{
    private static readonly IList<string> ExampleLines = new List<string>
    {
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

    private static readonly IList<int> ExamplePartNumbers = new List<int>
    {
        467, 35, 633, 617, 592, 755, 664, 598,
    };

    private static readonly IList<int> ExampleGearRatios = new List<int> { 16345, 451490 };

    /// <summary>
    /// Verifies that engine schematic parses the example correctly.
    /// </summary>
    [Fact]
    public void ParsesExample()
    {
        var schematic = new EngineSchematic(ExampleLines);

        Assert.DoesNotContain(114, schematic.PartNumbers);
        Assert.DoesNotContain(58, schematic.PartNumbers);
        foreach (var partNumber in ExamplePartNumbers)
        {
            Assert.Contains(partNumber, schematic.PartNumbers);
        }

        Assert.Equal(4361, schematic.PartNumbers.Sum());
    }

    /// <summary>
    /// Verifies that engine schematic parses gear ratios correctly.
    /// </summary>
    [Fact]
    public void ParsesGearRatios()
    {
        var schematic = new EngineSchematic(ExampleLines);

        foreach (var gearRatio in ExampleGearRatios)
        {
            Assert.Contains(gearRatio, schematic.GearRatios);
        }

        Assert.Equal(467835, schematic.GearRatios.Sum());
    }
}
