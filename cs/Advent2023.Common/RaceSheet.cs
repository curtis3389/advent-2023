// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

using System.Text.RegularExpressions;

/// <summary>
/// Represents a sheet with race information.
/// </summary>
public class RaceSheet
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RaceSheet"/> class.
    /// </summary>
    /// <param name="lines">The lines to parse.</param>
    public RaceSheet(IList<string> lines)
    {
        var times = Regex.Split(lines[0], "\\s+")
            .Skip(1)
            .Select(long.Parse)
            .ToList();
        var distances = Regex.Split(lines[1], "\\s+")
            .Skip(1)
            .Select(long.Parse)
            .ToList();
        this.Races = times
            .Select((time, index) => (time, distances[index]))
            .ToList();
    }

    /// <summary>
    /// Gets the races on this race sheet.
    /// </summary>
    public IList<(long TimeAllowed, long RecordDistance)> Races { get; }

    /// <summary>
    /// Calculates the number of ways to win for each race on this race sheet.
    /// </summary>
    /// <returns>The numbers of ways to win for each race.</returns>
    public IList<long> WaysToWin()
    {
        return this.Races
            .Select(race => LongRange(0, race.TimeAllowed + 1)
                .Select(hold => DistanceTraveled(hold, race.TimeAllowed))
                .Where(distance => distance > race.RecordDistance)
                .Select(_ => 1L)
                .Sum())
            .ToList();
    }

    private static IEnumerable<long> LongRange(long start, long count)
    {
        for (long i = 0; i < count; ++i)
        {
            yield return start + i;
        }
    }

    /// <summary>
    /// Gets the distance traveled for the given hold time and race time.
    /// </summary>
    /// <param name="holdTime">The time to hold down the button.</param>
    /// <param name="timeAllowed">The time allowed for the race.</param>
    /// <returns>The distance traveled.</returns>
    private static long DistanceTraveled(long holdTime, long timeAllowed) => (timeAllowed - holdTime) * holdTime;
}
