// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common.Tests;

/// <summary>
/// Represents tests for the GalaxyMap type.
/// </summary>
public class GalaxyMapTests
{
    private static readonly IList<string> ExampleData = new List<string>
    {
        "...#......",
        ".......#..",
        "#.........",
        "..........",
        "......#...",
        ".#........",
        ".........#",
        "..........",
        ".......#..",
        "#...#.....",
    };

    /// <summary>
    /// Verifies that GalaxyMap can parse the example data.
    /// </summary>
    [Fact]
    public void ParsesExampleData()
    {
        var map = new GalaxyMap(ExampleData);
    }

    /// <summary>
    /// Verifies that GalaxyMap can calculate the example shortest paths.
    /// </summary>
    [Fact]
    public void CalculatesExampleShortestPaths()
    {
        var map = new GalaxyMap(ExampleData);

        Assert.Equal(374, map.GetShortestPathLengths().Sum());
    }
}
