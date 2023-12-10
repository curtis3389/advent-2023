// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common.Tests;

/// <summary>
/// Represents tests for the DesertMap type.
/// </summary>
public class DesertMapTests
{
    private static readonly IList<string> ExampleData = new List<string>
    {
        "LLR",
        string.Empty,
        "AAA = (BBB, BBB)",
        "BBB = (AAA, ZZZ)",
        "ZZZ = (ZZZ, ZZZ)",
    };

    private static readonly IList<string> ExampleData2 = new List<string>
    {
        "LR",
        string.Empty,
        "11A = (11B, XXX)",
        "11B = (XXX, 11Z)",
        "11Z = (11B, XXX)",
        "22A = (22B, XXX)",
        "22B = (22C, 22C)",
        "22C = (22Z, 22Z)",
        "22Z = (22B, 22B)",
        "XXX = (XXX, XXX)",
    };

    private static readonly IList<string> ExampleData3 = new List<string>
    {
        "LR",
        string.Empty,
        "11A = (11B, XXX)",
        "11B = (XXX, 11Z)",
        "11Z = (11B, XXX)",
        "22A = (22B, XXX)",
        "22B = (22C, 22C)",
        "22C = (22Z, 22Z)",
        "22Z = (22B, 22B)",
        "XXX = (XXX, XXX)",
        "33A = (33B, XXX)",
        "33B = (XXX, 33C)",
        "33C = (33D, XXX)",
        "33D = (XXX, 33E)",
        "33E = (33F, XXX)",
        "33F = (XXX, 33G)",
        "33G = (33H, XXX)",
        "33H = (XXX, 33Z)",
        "33Z = (33H, XXX)",
    };

    /// <summary>
    /// Verifies that DesertMap parses the example data and calculates the step count.
    /// </summary>
    [Fact]
    public void ParsesExample()
    {
        var map = new DesertMap(ExampleData);
        Assert.Equal(6, map.GetStepCount());
    }

    /// <summary>
    /// Verifies that DesertMap calculates the ghost step count correctly.
    /// </summary>
    [Fact]
    public void CalculatesGhostSteps()
    {
        var map = new DesertMap(ExampleData2);
        Assert.Equal(6UL, map.GetGhostStepCount());
    }

    /// <summary>
    /// Verifies that DesertMap calculates the ghost step count correctly for the extended example.
    /// </summary>
    [Fact]
    public void CalculatesGhostStepsForExtended()
    {
        var map = new DesertMap(ExampleData3);
        Assert.Equal(12UL, map.GetGhostStepCount());
    }

    /// <summary>
    /// Verifies that DesertMap calculates the ghost step count correctly with the "Fast" method.
    /// </summary>
    [Fact]
    public void FastCalculatesGhostSteps()
    {
        var map = new DesertMap(ExampleData2);
        Assert.Equal(6UL, map.GetFastGhostStepCount());
    }

    /// <summary>
    /// Verifies that DesertMap calculates the ghost step count correctly with
    /// the "Fast" method for the extended example.
    /// </summary>
    [Fact]
    public void FastCalculatesGhostStepsTest()
    {
        var map = new DesertMap(ExampleData3);
        Assert.Equal(12UL, map.GetFastGhostStepCount());
    }
}
