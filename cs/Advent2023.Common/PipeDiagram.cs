// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

/// <summary>
/// Represents a diagram of pipes.
/// </summary>
public class PipeDiagram
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PipeDiagram"/> class.
    /// </summary>
    /// <param name="lines">The input lines to parse.</param>
    public PipeDiagram(IList<string> lines)
    {
        this.Diagram = lines
            .Select(line => (IList<char>)line.ToList())
            .ToList();
    }

    /// <summary>
    /// Gets the diagram.
    /// </summary>
    public IList<IList<char>> Diagram { get; }

    /// <summary>
    /// Gets the longest distance from the start.
    /// </summary>
    /// <returns>The longest distance from the start.</returns>
    public int GetLongestDistance()
    {
        return (int)this.GetDistances()
            .SelectMany(row => row)
            .Where(d => d != null)
            .Max()!;
    }

    /// <summary>
    /// Prints the given distance table.
    /// </summary>
    /// <param name="distances">The distance table to print.</param>
    private static void PrintDistances(IList<IList<int?>> distances)
    {
        foreach (var row in distances)
        {
            foreach (var column in row)
            {
                Console.Write(column?.ToString() ?? ".");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    /// <summary>
    /// Gets the table of distances from the start.
    /// </summary>
    /// <returns>The table of distances.</returns>
    private IList<IList<int?>> GetDistances()
    {
        var distances = this.Diagram
            .Select(row => (IList<int?>)row
                .Select(_ => (int?)null)
                .ToList())
            .ToList();
        var locationQueue = new List<(int X, int Y)> { this.GetStart() };
        while (locationQueue.Count != 0)
        {
            var location = locationQueue.PopFront();
            distances[location.X][location.Y] = this.GetDistance(location, distances);
            var neighbors = this.GetNeighbors(location)
                .Where(l => distances[l.X][l.Y] == null)
                .Where(l => this.Connected(location, l));
            locationQueue.AddRange(neighbors);
        }

        return distances;
    }

    /// <summary>
    /// Gets the location of the start.
    /// </summary>
    /// <returns>The start location.</returns>
    private (int X, int Y) GetStart()
    {
        return this.Diagram
            .SelectMany((row, x) => row.Select((c, y) => (c, x, y)).ToList())
            .Where(t => t.c == 'S')
            .Select(t => (t.x, t.y))
            .First();
    }

    /// <summary>
    /// Gets the distance for the given location.
    /// </summary>
    /// <param name="location">The location to get the distance for.</param>
    /// <param name="distances">The current table of distances.</param>
    /// <returns>The distance the location is from the start.</returns>
    private int GetDistance((int X, int Y) location, IList<IList<int?>> distances)
    {
        var distance = this.GetNeighbors(location)
            .Where(neighbor => this.Connected(location, neighbor))
            .Select(l => distances[l.X][l.Y])
            .FirstOrDefault(distance => distance != null);
        return distance == null
            ? 0
            : (int)distance + 1;
    }

    /// <summary>
    /// Gets the neighboring location to the given location.
    /// </summary>
    /// <param name="location">The location to get the neighbors of.</param>
    /// <returns>The neighboring locations.</returns>
    private IList<(int X, int Y)> GetNeighbors((int X, int Y) location)
    {
        return new List<(int X, int Y)>
            {
                (location.X - 1, location.Y),
                (location.X + 1, location.Y),
                (location.X, location.Y - 1),
                (location.X, location.Y + 1),
            }
            .Where(l => l is { X: >= 0, Y: >= 0 } && l.X < this.Diagram.Count && l.Y < this.Diagram[0].Count)
            .ToList();
    }

    /// <summary>
    /// Checks whether the given locations are connected.
    /// </summary>
    /// <param name="a">The first location to check.</param>
    /// <param name="b">The second location to check.</param>
    /// <returns>true if the locations are connected; false otherwise.</returns>
    private bool Connected((int X, int Y) a, (int X, int Y) b)
    {
        return this.Diagram[a.X][a.Y] switch
        {
            '|' => (this.Above(a, b) && this.ConnectsDown(b)) || (this.Below(a, b) && this.ConnectsUp(b)),
            '-' => (this.Left(a, b) && this.ConnectsRight(b)) || (this.Right(a, b) && this.ConnectsLeft(b)),
            'L' => (this.Above(a, b) && this.ConnectsDown(b)) || (this.Right(a, b) && this.ConnectsLeft(b)),
            'J' => (this.Above(a, b) && this.ConnectsDown(b)) || (this.Left(a, b) && this.ConnectsRight(b)),
            '7' => (this.Below(a, b) && this.ConnectsUp(b)) || (this.Left(a, b) && this.ConnectsRight(b)),
            'F' => (this.Below(a, b) && this.ConnectsUp(b)) || (this.Right(a, b) && this.ConnectsLeft(b)),
            '.' => false,
            'S' => (this.Right(a, b) && this.ConnectsLeft(b)) || (this.Left(a, b) && this.ConnectsRight(b)) ||
                   (this.Above(a, b) && this.ConnectsDown(b)) || (this.Below(a, b) && this.ConnectsUp(b)),
            _ => false,
        };
    }

    /// <summary>
    /// Checks whether the second location is to the right of the first.
    /// </summary>
    /// <param name="a">The location in the center.</param>
    /// <param name="b">The location to check if is to the right.</param>
    /// <returns>true if the location is to the right; false otherwise.</returns>
    private bool Right((int X, int Y) a, (int X, int Y) b) => b.X == a.X && b.Y == a.Y + 1;

    /// <summary>
    /// Checks whether the second location is to the left of the first.
    /// </summary>
    /// <param name="a">The location in the center.</param>
    /// <param name="b">The location to check if is to the left.</param>
    /// <returns>true if the location is to the left; false otherwise.</returns>
    private bool Left((int X, int Y) a, (int X, int Y) b) => b.X == a.X && b.Y == a.Y - 1;

    /// <summary>
    /// Checks whether the second location is above the first.
    /// </summary>
    /// <param name="a">The location in the center.</param>
    /// <param name="b">The location to check if is above.</param>
    /// <returns>true if the location is above; false otherwise.</returns>
    private bool Above((int X, int Y) a, (int X, int Y) b) => b.Y == a.Y && b.X == a.X - 1;

    /// <summary>
    /// Checks whether the second location is below the first.
    /// </summary>
    /// <param name="a">The location in the center.</param>
    /// <param name="b">The location to check if is below.</param>
    /// <returns>true if the location is below; false otherwise.</returns>
    private bool Below((int X, int Y) a, (int X, int Y) b) => b.Y == a.Y && b.X == a.X + 1;

    /// <summary>
    /// Checks if the given location connects down.
    /// </summary>
    /// <param name="l">The location.</param>
    /// <returns>true if it connects down; false otherwise.</returns>
    private bool ConnectsDown((int X, int Y) l) => this.Diagram[l.X][l.Y] is '|' or '7' or 'F' or 'S';

    /// <summary>
    /// Checks if the given location connects up.
    /// </summary>
    /// <param name="l">The location.</param>
    /// <returns>true if it connects up; false otherwise.</returns>
    private bool ConnectsUp((int X, int Y) l) => this.Diagram[l.X][l.Y] is '|' or 'L' or 'J' or 'S';

    /// <summary>
    /// Checks if the given location connects left.
    /// </summary>
    /// <param name="l">The location.</param>
    /// <returns>true if it connects left; false otherwise.</returns>
    private bool ConnectsLeft((int X, int Y) l) => this.Diagram[l.X][l.Y] is '-' or 'J' or '7' or 'S';

    /// <summary>
    /// Checks if the given location connects right.
    /// </summary>
    /// <param name="l">The location.</param>
    /// <returns>true if it connects right; false otherwise.</returns>
    private bool ConnectsRight((int X, int Y) l) => this.Diagram[l.X][l.Y] is '-' or 'F' or 'L' or 'S';
}
