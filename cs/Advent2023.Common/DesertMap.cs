// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

using System.Text.RegularExpressions;

/// <summary>
/// Represents a map of the desert.
/// </summary>
public class DesertMap
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DesertMap"/> class.
    /// </summary>
    /// <param name="lines">The input lines to parse.</param>
    public DesertMap(IList<string> lines)
    {
        this.Steps = lines[0].ToCharArray().ToList();
        this.Map = lines
            .Skip(2)
            .Select(line => Regex.Match(line, "(\\w+)\\s*=\\s*\\((\\w+),\\s*(\\w+)\\)"))
            .Select<Match, (string Key, (string Left, string Right) Value)>(match =>
                (match.Groups[1].Value, (match.Groups[2].Value, match.Groups[3].Value)))
            .ToDictionary(tuple => tuple.Key, tuple => tuple.Value);
    }

    /// <summary>
    /// Gets the map of the desert.
    /// Keys are locations and values are left/right destinations.
    /// </summary>
    public IDictionary<string, (string Left, string Right)> Map { get; }

    /// <summary>
    /// Gets the steps listed on the map.
    /// </summary>
    public IList<char> Steps { get; }

    /// <summary>
    /// Gets the number of steps needed to go from AAA to ZZZ.
    /// </summary>
    /// <returns>The number of steps.</returns>
    public int GetStepCount()
    {
        var count = 0;
        var current = "AAA";
        var steps = this.Steps.RepeatIndefinitely();
        foreach (var step in steps)
        {
            current = step == 'L'
                ? this.Map[current].Left
                : this.Map[current].Right;
            count += 1;

            if (current == "ZZZ")
            {
                return count;
            }
        }

        return count;
    }
}
