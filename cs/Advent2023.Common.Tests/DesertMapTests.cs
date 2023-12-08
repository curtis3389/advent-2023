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

    /// <summary>
    /// Verifies that DesertMap parses the example data and calculates the step count.
    /// </summary>
    [Fact]
    public void ParsesExample()
    {
        var map = new DesertMap(ExampleData);
        Assert.Equal(6, map.GetStepCount());
    }
}
