// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

using System.Text.RegularExpressions;

/// <summary>
/// Represents an engine schematic.
/// </summary>
public class EngineSchematic
{
    private static readonly Regex NumberRegex = new("(\\d+)");
    private static readonly Regex SymbolRegex = new("([^.\\d])");

    /// <summary>
    /// Initializes a new instance of the <see cref="EngineSchematic"/> class.
    /// </summary>
    /// <param name="lines">The input lines to parse the schematic from.</param>
    public EngineSchematic(IList<string> lines)
    {
        var numbers = lines
            .SelectMany<string, (int, Match)>((line, index) => NumberRegex.Matches(line).Select(match => (index, match)))
            .Select(pair => new SchematicNumber(pair.Item1, pair.Item2))
            .ToList();
        var symbols = lines
            .SelectMany((line, index) => SymbolRegex
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

    /// <summary>
    /// Gets the gear ratios in this schematic.
    /// </summary>
    public IList<int> GearRatios { get; }

    /// <summary>
    /// Gets the part numbers in this schematic.
    /// </summary>
    public IList<int> PartNumbers { get; }

    /// <summary>
    /// Represents a number in a schematic.
    /// </summary>
    private class SchematicNumber
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SchematicNumber" /> class.
        /// </summary>
        /// <param name="rowIndex">The index of the row the number is on.</param>
        /// <param name="match">The regex match for the number.</param>
        public SchematicNumber(int rowIndex, Match match)
        {
            this.Value = int.Parse(match.Value);
            this.Location = new Vec2(rowIndex, match.Index);
            this.Length = match.Length;
            this.AdjacentLocations = new List<Vec2>
                {
                    new(this.Location.X, this.Location.Y - 1),
                    new(this.Location.X, this.Location.Y + this.Length),
                }
                .Concat(Enumerable.Range(this.Location.Y - 1, this.Length + 2)
                    .SelectMany(y => new[]
                    {
                        new Vec2(this.Location.X - 1, y),
                        new Vec2(this.Location.X + 1, y),
                    }))
                .ToList();
        }

        /// <summary>
        /// Gets the value of this number.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Gets the location of the start of this number.
        /// </summary>
        public Vec2 Location { get; }

        /// <summary>
        /// Gets the length of this number.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Gets the locations adjacent to this number.
        /// </summary>
        public IList<Vec2> AdjacentLocations { get; }

        /// <summary>
        /// Checks whether the given location is adjacent to this number.
        /// </summary>
        /// <param name="location">The location to check if it is adjacent.</param>
        /// <returns>true if the location is adjacent to this; false otherwise.</returns>
        public bool IsAdjacent(Vec2 location) => this.AdjacentLocations.Contains(location);

        /// <summary>
        /// Checks whether the given symbol is adjacent to this number.
        /// </summary>
        /// <param name="symbol">The symbol to check if adjacent.</param>
        /// <returns>true if the symbol is adjacent to this; false otherwise.</returns>
        public bool IsAdjacent(SchematicSymbol symbol) => this.IsAdjacent(symbol.Location);

        /// <summary>
        /// Checks whether this is a part number for the given symbols.
        /// </summary>
        /// <param name="symbols">The symbols on the schematic.</param>
        /// <returns>true if this is a part number; false otherwise.</returns>
        public bool IsPartNumber(IList<SchematicSymbol> symbols) => symbols.Any(this.IsAdjacent);
    }

    /// <summary>
    /// Represents a symbol in a schematic.
    /// </summary>
    private class SchematicSymbol
    {
        /// <summary>
        /// Gets the location of this symbol.
        /// </summary>
        public required Vec2 Location { get; init; }

        /// <summary>
        /// Gets the value of this symbol.
        /// </summary>
        public required char Value { get; init; }
    }
}
