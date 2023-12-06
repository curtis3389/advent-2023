// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

using System.Text.RegularExpressions;

/// <summary>
/// Represents an elven farmer's almanac.
/// </summary>
public class Almanac
{
    private readonly IList<ItemMap> maps;

    /// <summary>
    /// Initializes a new instance of the <see cref="Almanac"/> class.
    /// </summary>
    /// <param name="lines">The input lines to parse.</param>
    public Almanac(IList<string> lines)
    {
        this.Seeds = ReadSeeds(lines.First());
        this.SeedRanges = this.Seeds
            .Pairs()
            .Select(pair => new Range(pair.First, pair.Second))
            .Order()
            .ToList();
        this.maps = Split(lines.Skip(1))
            .Select(group => new ItemMap(group))
            .ToList();
    }

    /// <summary>
    /// Gets the seeds to plant.
    /// </summary>
    public IList<long> Seeds { get; }

    /// <summary>
    /// Gets the seed ranges to plant.
    /// </summary>
    public IList<Range> SeedRanges { get; }

    /// <summary>
    /// Calculates the lowest initial planting location.
    /// </summary>
    /// <returns>The lowest initial location.</returns>
    public long LowestInitialLocation()
    {
        var initialLocations = this.MapSeeds(this.Seeds);
        return initialLocations.Min();
    }

    /// <summary>
    /// Calculates the lowest initial location, correctly.
    /// </summary>
    /// <returns>The correct lowest initial location.</returns>
    public long CorrectLowestLocation()
    {
        return this.MapSeedRanges(this.SeedRanges).Min()!.Start;
    }

    /// <summary>
    /// Reads the seed values from the given line.
    /// </summary>
    /// <param name="line">The input line to read the values from.</param>
    /// <returns>The seed values.</returns>
    private static IList<long> ReadSeeds(string line)
    {
        var match = Regex.Match(line, "seeds: ((\\d+\\s*)+)");
        return match.Groups[1].Value.Split(' ')
            .Select(long.Parse)
            .ToList();
    }

    /// <summary>
    /// Maps the given value using the given map.
    /// </summary>
    /// <param name="item">The value to  map.</param>
    /// <param name="map">The map to map the value with.</param>
    /// <returns>The mapped value.</returns>
    private static long Map(long item, ItemMap map)
    {
        return map.Mappings
            .Where(mapping => mapping.MapsItem(item))
            .Select(mapping => mapping.Map(item))
            .SingleOrDefault(item);
    }

    /// <summary>
    /// Splits the given lines into groups of lines separated by blank lines.
    /// </summary>
    /// <param name="lines">The lines to split.</param>
    /// <returns>Groups of lines separated by blank lines.</returns>
    private static IList<IList<string>> Split(IEnumerable<string> lines)
    {
        var groups = new List<IList<string>> { new List<string>() };
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                groups.Add(new List<string>());
            }
            else
            {
                groups.Last().Add(line);
            }
        }

        return groups.Where(group => group.Count != 0).ToList();
    }

    /// <summary>
    /// Maps the given seed ranges to location ranges.
    /// </summary>
    /// <param name="seedRanges">The seed ranges to map.</param>
    /// <returns>The mapped location ranges.</returns>
    private IEnumerable<Range> MapSeedRanges(IList<Range> seedRanges) =>
        this.MapRangesFromTo("seed", "location", seedRanges);

    /// <summary>
    /// Maps the given ranges from the given starting set to the given destination set.
    /// </summary>
    /// <param name="from">The starting set of the ranges.</param>
    /// <param name="to">The destination set of the ranges.</param>
    /// <param name="ranges">The ranges to map.</param>
    /// <returns>The mapped ranges.</returns>
    private IEnumerable<Range> MapRangesFromTo(string from, string to, IEnumerable<Range> ranges)
    {
        var result = ranges;
        var current = from;
        while (current != to)
        {
            var currentMap = this.maps.Single(map => map.From == current);
            result = currentMap.Map(result);
            current = currentMap.To;
        }

        return result;
    }

    /// <summary>
    /// Maps the given seed values to location values.
    /// </summary>
    /// <param name="seeds">The seed values to map.</param>
    /// <returns>The mapped location values.</returns>
    private IList<long> MapSeeds(IList<long> seeds) => seeds.Select(this.MapSeed).ToList();

    /// <summary>
    /// Maps the given seed value to a location value.
    /// </summary>
    /// <param name="seed">The seed value to map.</param>
    /// <returns>The mapped location value.</returns>
    private long MapSeed(long seed) => this.MapFromTo("seed", "location", seed);

    /// <summary>
    /// Maps the given value from the given starting set to the given destination set.
    /// </summary>
    /// <param name="from">The starting set of the value.</param>
    /// <param name="to">The destination set of the value.</param>
    /// <param name="item">The value to map.</param>
    /// <returns>The mapped value.</returns>
    private long MapFromTo(string from, string to, long item)
    {
        var result = item;
        var current = from;
        while (current != to)
        {
            var currentMap = this.maps.Single(map => map.From == current);
            result = Map(result, currentMap);
            current = currentMap.To;
        }

        return result;
    }

    /// <summary>
    /// Represents a mapping of a range of values.
    /// </summary>
    private class Mapping : IComparable<Mapping>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mapping"/> class.
        /// </summary>
        /// <param name="line">The line to parse.</param>
        public Mapping(string line)
        {
            var match = Regex.Match(line, "(\\d+)\\s+(\\d+)\\s+(\\d+)");
            var destinationStart = long.Parse(match.Groups[1].Value);
            var sourceStart = long.Parse(match.Groups[2].Value);
            var count = long.Parse(match.Groups[3].Value);
            this.SourceRange = new Range(sourceStart, count);
            this.DestinationRange = new Range(destinationStart, count);
        }

        /// <summary>
        /// Gets the source range of this mapping.
        /// </summary>
        public Range SourceRange { get; }

        /// <summary>
        /// Gets the destination range of this mapping.
        /// </summary>
        public Range DestinationRange { get; }

        /// <summary>
        /// Checks whether this maps the given value.
        /// </summary>
        /// <param name="item">The value to check if this maps.</param>
        /// <returns>true if this maps the value; false otherwise.</returns>
        public bool MapsItem(long item) => this.SourceRange.Contains(item);

        /// <summary>
        /// Checks if this maps any values in the given range.
        /// </summary>
        /// <param name="range">The range to check if this maps.</param>
        /// <returns>true if this maps a value in the range; false otherwise.</returns>
        public bool MapsRange(Range range) => !this.SourceRange.Compare(range).Overlap.IsEmpty;

        /// <summary>
        /// Maps the given value.
        /// </summary>
        /// <param name="item">The value to map.</param>
        /// <returns>The mapped value.</returns>
        public long Map(long item) => this.DestinationRange.Start + (item - this.SourceRange.Start);

        /// <summary>
        /// Maps the given range that has been trimmed to fit the source range.
        /// </summary>
        /// <param name="range">The trimmed range to map.</param>
        /// <returns>The mapped range.</returns>
        public Range Map(Range range) => new(this.Map(range.Start), range.Count);

        /// <inheritdoc />
        public int CompareTo(Mapping? other) => other != null
            ? this.SourceRange.CompareTo(other.SourceRange)
            : 1;

        /// <inheritdoc />
        public override string ToString() => $"{this.SourceRange}=>{this.DestinationRange}";
    }

    private class ItemMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemMap"/> class.
        /// </summary>
        /// <param name="lines">The lines to parse.</param>
        public ItemMap(IList<string> lines)
        {
            var match = Regex.Match(lines.First(), "(\\w+)-to-(\\w+)\\s+map:");
            this.From = match.Groups[1].Value;
            this.To = match.Groups[2].Value;
            this.Mappings = lines
                .Skip(1)
                .Select(line => new Mapping(line))
                .Order()
                .ToList();
        }

        /// <summary>
        /// Gets the set this maps from.
        /// </summary>
        public string From { get; }

        /// <summary>
        /// Gets the set this maps to.
        /// </summary>
        public string To { get; }

        /// <summary>
        /// Gets the mappings in this map.
        /// </summary>
        public IList<Mapping> Mappings { get; }

        /// <inheritdoc />
        public override string ToString() => $"{this.From}-to-{this.To} ({this.Mappings.Count})";

        /// <summary>
        /// Maps the given ranges.
        /// </summary>
        /// <param name="ranges">The ranges to map.</param>
        /// <returns>The mapped ranges.</returns>
        public IEnumerable<Range> Map(IEnumerable<Range> ranges)
        {
            var pairs = this.GetMappedRanges(ranges);
            var results = pairs.Select(pair => pair.Mapping == null
                ? pair.Range
                : pair.Mapping.Map(pair.Range));
            return SortAndMerge(results);
        }

        /// <summary>
        /// Sorts the given ranges and combines them, if possible.
        /// </summary>
        /// <param name="ranges">The ranges to sort and merge.</param>
        /// <returns>The sorted and merged ranges.</returns>
        private static IEnumerable<Range> SortAndMerge(IEnumerable<Range> ranges)
        {
            var queue = ranges.Order().ToList();
            while (queue.Count != 0)
            {
                var current = queue.PopFront();
                while (Range.IsContiguous(current, queue.PeekFront()))
                {
                    current = Range.Join(current, queue.PopFront());
                }

                yield return current;
            }
        }

        /// <summary>
        /// Gets the ranges and their mappings for mapping the given ranges.
        /// </summary>
        /// <param name="ranges">The ranges to map.</param>
        /// <returns>The actual ranges to map and their mappings.</returns>
        private IEnumerable<(Range Range, Mapping? Mapping)> GetMappedRanges(IEnumerable<Range> ranges)
        {
            var queue = ranges.Order().ToList();
            var mapped = new List<(Range, Mapping?)>();
            while (queue.Count != 0)
            {
                var next = queue.PopFront();
                var mapping = this.Mappings.FirstOrDefault(mapping => mapping.MapsRange(next));
                if (mapping != null)
                {
                    var comparison = next.Compare(mapping.SourceRange);
                    mapped.Add((comparison.Overlap, mapping));
                    queue.AddRange(comparison.Remains);
                }
                else
                {
                    mapped.Add((next, null));
                }
            }

            return mapped;
        }
    }
}
