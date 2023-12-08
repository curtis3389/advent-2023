// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common.Tests;

/// <summary>
/// Represents tests for the CamelCardHand type.
/// </summary>
public class CamelCardHandTests
{
    private static readonly IList<string> ExampleData = new List<string>
    {
        "32T3K 765",
        "T55J5 684",
        "KK677 28",
        "KTJJT 220",
        "QQQJA 483",
    };

    // this is from reddit: https://www.reddit.com/r/adventofcode/comments/18cr4xr/2023_day_7_better_example_input_not_a_spoiler/
    private static readonly IList<string> BetterExampleData = new List<string>
    {
        "2345A 1",
        "Q2KJJ 13",
        "Q2Q2Q 19",
        "T3T3J 17",
        "T3Q33 11",
        "2345J 3",
        "J345A 2",
        "32T3K 5",
        "T55J5 29",
        "KK677 7",
        "KTJJT 34",
        "QQQJA 31",
        "JJJJJ 37",
        "JAAAA 43",
        "AAAAJ 59",
        "AAAAA 61",
        "2AAAA 23",
        "2JJJJ 53",
        "JJJJ2 41",
    };

    /// <summary>
    /// Verifies that the type parses a line correctly.
    /// </summary>
    [Fact]
    public void ParsesExample()
    {
        var expected = new List<CamelCard>
        {
            CamelCard.Three,
            CamelCard.Two,
            CamelCard.Ten,
            CamelCard.Three,
            CamelCard.King,
        };
        var hand = new CamelCardHand(ExampleData[0]);

        Assert.Equal(765, hand.Bid);
        for (var i = 0; i < expected.Count; ++i)
        {
            Assert.Equal(expected[i], hand.Cards[i]);
        }
    }

    /// <summary>
    /// Verifies that hands can be ordered correctly.
    /// </summary>
    [Fact]
    public void CanBeOrdered()
    {
        var total = ExampleData
            .Select(line => new CamelCardHand(line))
            .Order()
            .Select((hand, rank) => (rank + 1) * hand.Bid)
            .Sum();

        Assert.Equal(6440, total);
    }

    /// <summary>
    /// Verifies that hands can be ordered correctly by the joker versions.
    /// </summary>
    [Fact]
    public void CanBeOrderedWithJokers()
    {
        var total = ExampleData
            .Select(line => new CamelCardHand(line))
            .Order(new CamelCardHand.JokerComparer())
            .Select((hand, rank) => (rank + 1) * hand.Bid)
            .Sum();

        Assert.Equal(5905, total);
    }

    /// <summary>
    /// Verifies that the joker ordering covers all edge cases.
    /// </summary>
    [Fact]
    public void CoversEdgeCases()
    {
        var ordered = BetterExampleData
            .Select(line => new CamelCardHand(line))
            .Order(new CamelCardHand.JokerComparer())
            .ToList();
        var total = ordered
            .Select((hand, rank) => (rank + 1) * hand.Bid)
            .Sum();

        Assert.Equal(6839, total);
    }
}
