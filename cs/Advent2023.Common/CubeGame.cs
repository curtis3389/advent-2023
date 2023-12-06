// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

using System.Text.RegularExpressions;

/// <summary>
/// Represents a single game of the cube game.
/// </summary>
public class CubeGame
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CubeGame"/> class.
    /// </summary>
    /// <param name="line">The input line to parse the cube game from.</param>
    public CubeGame(string line)
    {
        var parts = line.Split(':');
        var left = parts[0];
        var right = parts[1];
        this.Id = int.Parse(Regex.Match(left, "Game (\\d+)").Groups[1].Value);
        this.Rounds = right.Split(';')
            .Select(draw => (IList<Blocks>)draw
                .Split(',')
                .Select(Blocks.Parse)
                .ToList())
            .ToList();
    }

    /// <summary>
    /// Gets the ID of this game.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the rounds of this game.
    /// </summary>
    public IList<IList<Blocks>> Rounds { get; }

    /// <summary>
    /// Checks whether this game could have been played with the given block counts.
    /// </summary>
    /// <param name="redCount">The number of red blocks available.</param>
    /// <param name="greenCount">The number of green blocks available.</param>
    /// <param name="blueCount">The number of blue blocks available.</param>
    /// <returns>true if this game was possible; false if not.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If an unknown color is present.</exception>
    public bool Possible(int redCount, int greenCount, int blueCount)
    {
        return this.Rounds.All(round => round.All(draw => draw.Color switch
        {
            BlockColor.Blue => draw.Count <= blueCount,
            BlockColor.Green => draw.Count <= greenCount,
            BlockColor.Red => draw.Count <= redCount,
            _ => throw new ArgumentOutOfRangeException(),
        }));
    }

    /// <summary>
    /// Calculates the "power" of this game.
    /// </summary>
    /// <returns>The power of this game.</returns>
    public int Power()
    {
        var all = this.Rounds.SelectMany(r => r).ToList();
        var minRed = all.Where(b => b.Color == BlockColor.Red).Max(b => b.Count);
        var minGreen = all.Where(b => b.Color == BlockColor.Green).Max(b => b.Count);
        var minBlue = all.Where(b => b.Color == BlockColor.Blue).Max(b => b.Count);
        return minRed * minGreen * minBlue;
    }
}
