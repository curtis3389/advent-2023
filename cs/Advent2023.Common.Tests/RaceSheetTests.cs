// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common.Tests;

/// <summary>
/// Represents tests for the RaceSheet type.
/// </summary>
public class RaceSheetTests
{
    private static readonly IList<string> ExampleData = new List<string>
    {
        "Time:      7  15   30",
        "Distance:  9  40  200",
    };

    /// <summary>
    /// Verifies that example data is parsed correctly.
    /// </summary>
    [Fact]
    public void Parses()
    {
        var expected = new[] { (7L, 9L), (15L, 40L), (30L, 200L) };
        var sheet = new RaceSheet(ExampleData);

        Assert.Equal(3, sheet.Races.Count);
        sheet.Races.ForEach(race =>
        {
            Assert.Contains(race, expected);
        });
    }

    /// <summary>
    /// Verifies that ways to win are calculated correctly.
    /// </summary>
    [Fact]
    public void CalculatesWaysToWin()
    {
        var expected = new[] { 4, 8, 9 };
        var sheet = new RaceSheet(ExampleData);
        var ways = sheet.WaysToWin();

        Assert.Equal(expected.Length, ways.Count);
        for (var i = 0; i < ways.Count; ++i)
        {
            Assert.Equal(expected[i], ways[i]);
        }
    }
}
