using System.Text.RegularExpressions;

namespace Advent2023.Common;

public class Vec2
{
    public Vec2(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public int X { get; }

    public int Y { get; }

    protected bool Equals(Vec2 other)
    {
        return this.X == other.X && this.Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Vec2)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.X, this.Y);
    }
}

public class EngineSchematic
{
    private static readonly Regex numberRegex = new ("(\\d+)");
    private static readonly Regex symbolRegex = new ("([^.\\d])");

    public EngineSchematic(IList<string> lines)
    {
        var numbers = lines
            .SelectMany<string, (int, Match)>((line, index) => numberRegex.Matches(line).Select(match => (index, match)))
            .Select(pair => new SchematicNumber(pair.Item1, pair.Item2))
            .ToList();
        var symbols = lines
            .SelectMany((line, index) => symbolRegex
                .Matches(line)
                .Select(match => new SchematicSymbol
                {
                    Location = new Vec2(index, match.Index),
                    Value = match.Value[0],
                }))
            .ToList();
        var partNumbers = numbers.Where(number => number.IsPartNumber(symbols)).ToList();
        this.PartNumbers = partNumbers.Select(number => number.Value).ToList();

        bool IsGear(SchematicSymbol symbol) => symbol.Value == '*' && partNumbers.Count(number => number.IsAdjacent(symbol)) == 2;
        int GetGearRatio(SchematicSymbol symbol) => partNumbers
                .Where(number => number.IsAdjacent(symbol))
                .Select(number => number.Value)
                .Aggregate((prev, next) => prev * next);
        var gears = symbols.Where(IsGear).ToList();
        this.GearRatios = gears.Select(GetGearRatio).ToList();
    }

    public IList<int> GearRatios { get; }

    public IList<int> PartNumbers { get; }

    private class SchematicNumber
    {
        public SchematicNumber(int rowIndex, Match match)
        {
            this.Value = int.Parse(match.Value);
            this.Location = new Vec2(rowIndex, match.Index);
            this.Length = match.Length;
            this.AdjacentLocations = new List<Vec2>
                {
                    new Vec2(this.Location.X, this.Location.Y - 1),
                    new Vec2(this.Location.X, this.Location.Y + this.Length),
                }
                .Concat(Enumerable.Range(this.Location.Y - 1, this.Length + 2)
                    .SelectMany(y => new []
                    {
                        new Vec2(this.Location.X - 1, y),
                        new Vec2(this.Location.X + 1, y),
                    }))
                .ToList();
        }

        public int Value { get; }
        public Vec2 Location { get; }
        public int Length { get; }
        public IList<Vec2> AdjacentLocations { get; }

        public bool IsAdjacent(Vec2 location) => this.AdjacentLocations.Contains(location);

        public bool IsAdjacent(SchematicSymbol symbol) => this.IsAdjacent(symbol.Location);

        public bool IsPartNumber(IList<SchematicSymbol> symbols) => symbols.Any(this.IsAdjacent);
    }

    private class SchematicSymbol
    {
        public Vec2 Location { get; set; }
        public char Value { get; set; }
    }
}
