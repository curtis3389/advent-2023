// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common.Tests;

/// <summary>
/// Represents tests for the Almanac type.
/// </summary>
public class AlmanacTests
{
    private static readonly string ExampleInput = @"seeds: 79 14 55 13
seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";

    /// <summary>
    /// Verifies that almanac parses the example data correctly.
    /// </summary>
    [Fact]
    public void ParsesExample()
    {
        var expectedSeeds = new List<long> { 79, 14, 55, 13 };
        var almanac = new Almanac(ExampleInput.Split('\n'));

        Assert.All(expectedSeeds, seed => Assert.Contains(seed, almanac.Seeds));
        Assert.All(almanac.Seeds, seed => Assert.Contains(seed, expectedSeeds));

        Assert.Equal(35, almanac.LowestInitialLocation());
    }

    /// <summary>
    /// Verifies that almanac calculates the example for part 2 correctly.
    /// </summary>
    [Fact]
    public void CalculatesPart2Example()
    {
        var almanac = new Almanac(ExampleInput.Split('\n'));
        Assert.Equal(46, almanac.CorrectLowestLocation());
    }
}
