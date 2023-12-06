using System.Text.RegularExpressions;

namespace Advent2023.Common;

class Mapping : IComparable<Mapping>
{
    public Range SourceRange { get; }
    public Range DestinationRange { get; }

    public Mapping(string line)
    {
        var match = Regex.Match(line, "(\\d+)\\s+(\\d+)\\s+(\\d+)");
        var destinationStart = long.Parse(match.Groups[1].Value);
        var sourceStart = long.Parse(match.Groups[2].Value);
        var count = long.Parse(match.Groups[3].Value);
        this.SourceRange = new Range(sourceStart, count);
        this.DestinationRange = new Range(destinationStart, count);
    }

    public bool MapsItem(long item) => this.SourceRange.Contains(item);
    public bool MapsRange(Range range) => !this.SourceRange.Compare(range).Overlap.IsEmpty;
    public long Map(long item) => this.DestinationRange.Start + (item - this.SourceRange.Start);
    public Range Map(Range range) => new (this.Map(range.Start), range.Count);

    /// <inheritdoc />
    public int CompareTo(Mapping? other) => this.SourceRange.CompareTo(other.SourceRange);

    public override string ToString() => $"{this.SourceRange}=>{this.DestinationRange}";
}

class ItemMap
{
    public string From { get; set; }
    public string To { get; set; }
    public IList<Mapping> Mappings { get; set; }

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

    public override string ToString() => $"{this.From}-to-{this.To} ({this.Mappings.Count})";

    public IEnumerable<Range> Map(IEnumerable<Range> ranges)
    {
        IEnumerable<(Range, Mapping?)> pairs = this.GetMappedRanges(ranges);
        var results = pairs.Select(pair => pair.Item2 == null
            ? pair.Item1
            : pair.Item2.Map(pair.Item1));
        return SortAndMerge(results);
    }

    private IEnumerable<(Range, Mapping?)> GetMappedRanges(IEnumerable<Range> ranges)
    {
        var queue = ranges.Order().ToList();
        var mapped = new List<(Range, Mapping?)>();
        while (queue.Count != 0)
        {
            var next = queue.Dequeue();
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

    private static IEnumerable<Range> SortAndMerge(IEnumerable<Range> ranges)
    {
        var queue = ranges.Order().ToList();
        while (queue.Count != 0)
        {
            var current = queue.Dequeue();
            while (Range.IsContiguous(current, queue.Next()))
            {
                current = Range.Join(current, queue.Dequeue());
            }

            yield return current;
        }
    }
}

public class Almanac
{
    private readonly IList<ItemMap> maps;

    public Almanac(IList<string> lines)
    {
        this.Seeds = ReadSeeds(lines.First());
        this.SeedRanges = this.Seeds
            .Pairs()
            .Select(pair => new Range(pair.Item1, pair.Item2))
            .Order()
            .ToList();
        this.maps = Split(lines.Skip(1))
            .Select(group => new ItemMap(group))
            .ToList();
    }

    public IList<long> Seeds { get; }

    public IList<Range> SeedRanges { get; }

    public long LowestInitialLocation()
    {
        var initialLocations = this.MapSeeds(this.Seeds);
        return initialLocations.Min();
    }

    public long CorrectLowestLocation()
    {
        return this.MapSeedRanges(this.SeedRanges).Min().Start;
    }

    private IEnumerable<Range> MapSeedRanges(IList<Range> seedRanges) =>
        this.MapRangesFromTo("seed", "location", seedRanges);

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

    private IList<long> MapSeeds(IList<long> seeds) => seeds.Select(this.MapSeed).ToList();

    private long MapSeed(long seed) => this.MapFromTo("seed", "location", seed);

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

    private static IList<long> ReadSeeds(string line)
    {
        var match = Regex.Match(line, "seeds: ((\\d+\\s*)+)");
        return match.Groups[1].Value.Split(' ')
            .Select(s => long.Parse(s))
            .ToList();
    }

    private static long Map(long item, ItemMap map)
    {
        return map.Mappings
            .Where(mapping => mapping.MapsItem(item))
            .Select(mapping => mapping.Map(item))
            .SingleOrDefault(item);
    }

    private static IList<IList<string>> Split(IEnumerable<string> lines)
    {
        var groups = new List<IList<string>>();
        groups.Add(new List<string>());
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
}
