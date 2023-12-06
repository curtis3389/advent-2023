// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

using System.Text.RegularExpressions;

/// <summary>
/// Represents a number of a single color of blocks.
/// </summary>
public class Blocks
{
    /// <summary>
    /// Gets the number of blocks.
    /// </summary>
    public int Count { get; init; }

    /// <summary>
    /// Gets the color of the blocks.
    /// </summary>
    public BlockColor Color { get; init; }

    /// <summary>
    /// Parses the given input string into a blocks value.
    /// </summary>
    /// <param name="s">The input string to parse.</param>
    /// <returns>A new Blocks value.</returns>
    public static Blocks Parse(string s)
    {
        var match = Regex.Match(s, "(\\d+)\\s+(\\w+)");
        return new Blocks
        {
            Count = int.Parse(match.Groups[1].Value),
            Color = ToBlockColor(match.Groups[2].Value),
        };
    }

    /// <summary>
    /// Gets the block color for the given string.
    /// </summary>
    /// <param name="s">The input string to get the color for.</param>
    /// <returns>The block color.</returns>
    private static BlockColor ToBlockColor(string s)
    {
        return s switch
        {
            "blue" => BlockColor.Blue,
            "green" => BlockColor.Green,
            "red" => BlockColor.Red,
            _ => throw new ArgumentOutOfRangeException(nameof(s), s, $"Unknown color: {s}"),
        };
    }
}
