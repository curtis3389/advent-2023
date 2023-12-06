// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

using System.Text.RegularExpressions;

/// <summary>
/// Represents an elven scratch card.
/// </summary>
public class ScratchCard
{
    private static readonly Regex LineRegex = new("Card\\s+(\\d+):\\s+(.*)\\|\\s+(.*)");

    /// <summary>
    /// Initializes a new instance of the <see cref="ScratchCard"/> class.
    /// </summary>
    /// <param name="line">The line to read the card from.</param>
    public ScratchCard(string line)
    {
        var match = LineRegex.Match(line);
        this.Id = int.Parse(match.Groups[1].Value);
        this.WinningNumbers = match.Groups[2].Value.Split(' ').Where(s => s != string.Empty).Select(int.Parse).ToList();
        this.Numbers = match.Groups[3].Value.Split(' ').Where(s => s != string.Empty).Select(int.Parse).ToList();
    }

    /// <summary>
    /// Gets the ID of this card.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the numbers on this card.
    /// </summary>
    public IList<int> Numbers { get; }

    /// <summary>
    /// Gets the winning numbers for this card.
    /// </summary>
    public IList<int> WinningNumbers { get; }

    /// <summary>
    /// Gets the number of matches this card has.
    /// </summary>
    public int Matches => this.Numbers.Intersect(this.WinningNumbers).Count();

    /// <summary>
    /// Gets the points this card is worth.
    /// </summary>
    public int Points => (int)Math.Pow(2, this.Matches - 1);

    /// <summary>
    /// Calculates the winning total for the given pile of scratch cards.
    /// </summary>
    /// <param name="pile">The pile of scratch cards to get the total of.</param>
    /// <returns>The winning total for the pile.</returns>
    public static int WinningTotal(IList<ScratchCard> pile)
    {
        ScratchCard Card(int id) => pile.Single(card => card.Id == id);
        var counts = new Dictionary<int, int>(pile.Select(card => new KeyValuePair<int, int>(card.Id, 1)));
        counts.Keys
            .Order()
            .ForEach(key => Enumerable.Range(key + 1, Card(key).Matches)
                .ForEach(rewardKey => counts[rewardKey] += counts[key]));
        return counts.Values.Sum();
    }
}
