// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common.Tests;

/// <summary>
/// Represents tests for the PipeDiagram tests.
/// </summary>
public class PipeDiagramTests
{
    private static readonly IList<string> ExampleData = new List<string>
    {
        "-L|F7",
        "7S-7|",
        "L|7||",
        "-L-J|",
        "L|-JF",
    };

    private static readonly IList<string> ExampleData2 = new List<string>
    {
        "7-F7-",
        ".FJ|7",
        "SJLL7",
        "|F--J",
        "LJ.LJ",
    };

    /// <summary>
    /// Verifies PipeDiagram calculates the example distances correctly.
    /// </summary>
    [Fact]
    public void CalculatesExampleDistances()
    {
        var diagram = new PipeDiagram(ExampleData);
        Assert.Equal(4, diagram.GetLongestDistance());

        var diagram2 = new PipeDiagram(ExampleData2);
        Assert.Equal(8, diagram2.GetLongestDistance());
    }
}
