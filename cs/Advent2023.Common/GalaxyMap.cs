// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

/// <summary>
/// Represents a map of galaxies in space.
/// </summary>
public class GalaxyMap
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GalaxyMap"/> class.
    /// </summary>
    /// <param name="lines">The input data to parse.</param>
    public GalaxyMap(IList<string> lines)
    {
        this.Map = ParseMap(lines);
    }

    /// <summary>
    /// Represents the possible contents of a location on a map.
    /// </summary>
    public enum LocationContents
    {
        /// <summary>
        /// An empty location.
        /// </summary>
        Empty,

        /// <summary>
        /// A location with a galaxy.
        /// </summary>
        Galaxy,
    }

    /// <summary>
    /// Gets the contents of this map.
    /// </summary>
    public IList<IList<LocationContents>> Map { get; }

    /// <summary>
    /// Gets the lengths of the shortest paths between galaxies.
    /// </summary>
    /// <returns>The lengths of the shortest paths between galaxies.</returns>
    public IList<int> GetShortestPathLengths()
    {
        var galaxyLocations = GetGalaxyLocations(ExpandMap(this.Map));
        var pairs = galaxyLocations.SelectMany(a => galaxyLocations.Select(b => (A: a, B: b)));
        var distinctPairs = pairs
            .Distinct(new LambdaEqualityComparer<(Vec2 A, Vec2 B)>(PairsEqual, PairsEqualHash))
            .ToList();
        return distinctPairs.Select(pair => GetDistance(pair.A, pair.B)).ToList();
    }

    /// <summary>
    /// Gets the lengths of the shortest paths between galaxies.
    /// </summary>
    /// <returns>The lengths of the shortest paths between galaxies.</returns>
    public IList<int> GetShortestPathLengths2()
    {
        var galaxyLocations = GetGalaxyLocations(this.Map);
        var pairs = galaxyLocations.SelectMany(a => galaxyLocations.Select(b => (A: a, B: b)));
        var distinctPairs = pairs
            .Distinct(new LambdaEqualityComparer<(Vec2 A, Vec2 B)>(PairsEqual, PairsEqualHash))
            .ToList();
        var emptyRows = EmptyRows(this.Map);
        var emptyColumns = EmptyColumns(this.Map);
        return distinctPairs.Select(pair => this.GetDistance2(pair.A, pair.B, emptyRows, emptyColumns)).ToList();
    }

    /// <summary>
    /// Gets the locations of the galaxies in the given map data.
    /// </summary>
    /// <param name="map">The map data to get the locations from.</param>
    /// <returns>The locations of the galaxies.</returns>
    private static IList<Vec2> GetGalaxyLocations(IList<IList<LocationContents>> map)
    {
        return map
            .SelectMany((row, rowIndex) => row.Select((contents, columnIndex) => (rowIndex, columnIndex, contents)))
            .Where(t => t.contents == LocationContents.Galaxy)
            .Select(t => new Vec2(t.rowIndex, t.columnIndex))
            .ToList();
    }

    /// <summary>
    /// Parses the map data from the given input lines.
    /// </summary>
    /// <param name="lines">The input lines to parse.</param>
    /// <returns>The map data.</returns>
    private static IList<IList<LocationContents>> ParseMap(IList<string> lines)
    {
        return lines.Select(line => (IList<LocationContents>)line
                .Select(c => c == '#'
                    ? LocationContents.Galaxy
                    : LocationContents.Empty)
                .ToList())
            .ToList();
    }

    /// <summary>
    /// Doubles blank rows/colomns in the given map.
    /// </summary>
    /// <param name="map">The map to expand.</param>
    /// <returns>The expanded version of the map.</returns>
    private static IList<IList<LocationContents>> ExpandMap(IList<IList<LocationContents>> map)
    {
        var newMap = map.Select(row => (IList<LocationContents>)new List<LocationContents>(row)).ToList();
        var emptyRows = EmptyRows(newMap);
        var emptyColumns = EmptyColumns(newMap);
        emptyRows.ForEach(rowIndex =>
            newMap.Insert(rowIndex, new List<LocationContents>(newMap[rowIndex])));
        emptyColumns.ForEach(columnIndex =>
            newMap.ForEach(row =>
                row.Insert(columnIndex, LocationContents.Empty)));
        return newMap;
    }

    private static IList<int> EmptyRows(IList<IList<LocationContents>> map) => map
        .Select((row, rowIndex) => (row, rowIndex))
        .Where(t => t.row.All(content => content == LocationContents.Empty))
        .Select(t => t.rowIndex)
        .OrderDescending()
        .ToList();

    private static IList<int> EmptyColumns(IList<IList<LocationContents>> map) => map
        .SelectMany(row => row.Select((contents, columnIndex) => (contents, columnIndex)))
        .GroupBy(a => a.columnIndex)
        .Where(group => group.All(pair => pair.contents == LocationContents.Empty))
        .Select(group => group.Key)
        .OrderDescending()
        .ToList();

    /// <summary>
    /// Gets the distance between the given locations.
    /// </summary>
    /// <param name="a">The first location.</param>
    /// <param name="b">The second location.</param>
    /// <returns>The distance between the locations.</returns>
    private static int GetDistance(Vec2 a, Vec2 b)
    {
        return Math.Abs(b.X - a.X) + Math.Abs(b.Y - a.Y);
    }

    /// <summary>
    /// Checks if the given pairs are equal/unique.
    /// This gets rid of transposed duplicates.
    /// </summary>
    /// <param name="pair1">The first pair.</param>
    /// <param name="pair2">The second pair.</param>
    /// <returns>true if the pairs are equal; false otherwise.</returns>
    private static bool PairsEqual((Vec2 A, Vec2 B) pair1, (Vec2 A, Vec2 B) pair2)
    {
        return (Equals(pair1.A, pair2.A) && Equals(pair1.B, pair2.B)) ||
               (Equals(pair1.A, pair2.B) && Equals(pair1.B, pair2.A));
    }

    /// <summary>
    /// Hashes the given pair for the PairsEqual function.
    /// </summary>
    /// <param name="pair1">The pair to hash.</param>
    /// <returns>The hash of the pair.</returns>
    private static int PairsEqualHash((Vec2 A, Vec2 B) pair1)
    {
        var sorted = new List<Vec2> { pair1.A, pair1.B };
        sorted.Sort((a, b) =>
        {
            var hashA = a.GetHashCode();
            var hashB = b.GetHashCode();
            return hashA < hashB ? -1 : hashA == hashB ? 0 : 1;
        });
        return HashCode.Combine(sorted[0], sorted[1]);
    }

    /// <summary>
    /// Returns the lesser of the given values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <typeparam name="T">The type of the values.</typeparam>
    /// <returns>The lesser of the given values.</returns>
    private static T Lesser<T>(T a, T b)
        where T : IComparable<T>
        => a.CompareTo(b) < 0 ? a : b;

    /// <summary>
    /// Returns the greater of the given values.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <typeparam name="T">The type of the values.</typeparam>
    /// <returns>The greater of the given values.</returns>
    private static T Greater<T>(T a, T b)
        where T : IComparable<T>
        => a.CompareTo(b) > 0 ? a : b;

    /// <summary>
    /// Gets the distance between the given locations.
    /// </summary>
    /// <details>
    /// This treats empty rows/columns as 1,000,000.
    /// </details>
    /// <param name="a">The first location.</param>
    /// <param name="b">The second location.</param>
    /// <param name="emptyRows">The empty rows on the map.</param>
    /// <param name="emptyColumns">The empty columns on the map.</param>
    /// <returns>The distance between the locations.</returns>
    private int GetDistance2(Vec2 a, Vec2 b, IList<int> emptyRows, IList<int> emptyColumns)
    {
        var lowerX = Lesser(a.X, b.X);
        var upperX = Greater(a.X, b.X);
        var lowerY = Lesser(a.Y, b.Y);
        var upperY = Greater(a.Y, b.Y);
        var rowsCrossed = emptyRows.Count(r => r > lowerX && r < upperX);
        var columnsCrossed = emptyColumns.Count(r => r > lowerY && r < upperY);
        var x = (upperX - lowerX) + (rowsCrossed * 999999);
        var y = (upperY - lowerY) + (columnsCrossed * 999999);
        return x + y;
    }
}
