// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

using System.Diagnostics;
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

        throw new UnreachableException();
    }

    /// <summary>
    /// Gets the number of steps needed to go from *A to *Z if you were a ghost.
    /// </summary>
    /// <returns>The number of steps.</returns>
    public ulong GetGhostStepCount()
    {
        var count = 0UL;
        var currentLocations = this.Map.Keys.Where(key => key.EndsWith('A')).ToList();
        var steps = this.Steps.RepeatIndefinitely();
        foreach (var step in steps)
        {
            currentLocations = currentLocations.Select(location => step == 'L'
                    ? this.Map[location].Left
                    : this.Map[location].Right)
                .ToList();
            count += 1;

            if (currentLocations.All(location => location.EndsWith('Z')))
            {
                return count;
            }
        }

        throw new UnreachableException();
    }

    /// <summary>
    /// Gets the number of steps needed to go from *A to *Z if you were a ghost.
    /// </summary>
    /// <details>
    /// This is more efficient than GetGhostStepCount(), but could be better with alignedStart + lcm.
    /// </details>
    /// <returns>The number of steps.</returns>
    public ulong GetFastGhostStepCount()
    {
        var loops = this.FindLoops();
        var multiples = loops.Select(Multiples).ToList();
        return LowestCommon(multiples);
    }

    /// <summary>
    /// Get the lowest value common to all the given enumerations.
    /// </summary>
    /// <param name="lists">The ordered enumerations to find the lowest of.</param>
    /// <returns>The lowest value common to the enumerations.</returns>
    private static ulong LowestCommon(IList<IEnumerable<ulong>> lists)
    {
        var enumerators = lists.Select(list => list.GetEnumerator()).ToList();
        enumerators.ForEach(e => e.MoveNext());
        var currents = enumerators.Select(e => e.Current).ToList();
        enumerators.ForEach(e => e.MoveNext());

        void AdvanceAll()
        {
            var first = currents[0];
            for (var i = 0; i < currents.Count; ++i)
            {
                while (currents[i] < first)
                {
                    currents[i] = enumerators[i].Current;
                    enumerators[i].MoveNext();
                }
            }
        }

        void AdvanceFirst()
        {
            currents[0] = enumerators[0].Current;
            enumerators[0].MoveNext();
        }

        while (currents.Any(current => current != currents[0]))
        {
            if (currents.All(current => current <= currents[0]))
            {
                AdvanceAll();
            }
            else
            {
                AdvanceFirst();
            }
        }

        enumerators.ForEach(enumerator => enumerator.Dispose());

        return currents[0];
    }

    /// <summary>
    /// Gets all the ending indexes of the given loop.
    /// </summary>
    /// <param name="loop">The loop to get the endings of.</param>
    /// <returns>The ending indexes of the loop.</returns>
    private static IEnumerable<ulong> Multiples(Loop loop)
    {
        var current = loop.Start;
        while (true)
        {
            yield return current;
            current += loop.Length;
        }
    }

    /// <summary>
    /// Finds the loops in the given map.
    /// </summary>
    /// <returns>The loops in the map.</returns>
    private IList<Loop> FindLoops()
    {
        var currentLocations = this.Map.Keys.Where(key => key.EndsWith('A')).ToList();
        var histories = currentLocations.Select(location => new List<string> { location }).ToList();
        var steps = this.Steps.RepeatIndefinitely();
        foreach (var step in steps)
        {
            currentLocations = currentLocations.Select(location => step == 'L'
                    ? this.Map[location].Left
                    : this.Map[location].Right)
                .ToList();
            for (var i = 0; i < currentLocations.Count; ++i)
            {
                histories[i].Add(currentLocations[i]);
            }

            if (histories.All(history => history.Count(l => l.EndsWith('Z')) > 1))
            {
                break;
            }
        }

        return histories
            .Select(history =>
            {
                var endings = history
                    .Select((l, i) => (l, i))
                    .Where(pair => pair.l.EndsWith('Z'))
                    .ToList();
                var start = (ulong)endings[0].i;
                var length = (ulong)endings[1].i - start;
                return new Loop
                {
                    Start = start,
                    Length = length,
                };
            }).ToList();
    }

    /// <summary>
    /// Represents a loop in the ghost's path.
    /// </summary>
    private class Loop
    {
        /// <summary>
        /// Gets the index of the first ending encountered.
        /// </summary>
        public ulong Start { get; init; }

        /// <summary>
        /// Gets the number of steps in this loop until each ending.
        /// </summary>
        public ulong Length { get; init; }
    }
}
