// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

/// <summary>
/// Represents a 2D vector.
/// </summary>
public class Vec2
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Vec2"/> class.
    /// </summary>
    /// <param name="x">The x part of the vector.</param>
    /// <param name="y">The y part of the vector.</param>
    public Vec2(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    /// <summary>
    /// Gets the x part of this vector.
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Gets the y part of this vector.
    /// </summary>
    public int Y { get; }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == this.GetType() && this.Equals((Vec2)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(this.X, this.Y);
    }

    /// <summary>
    /// Determines whether the given vector is equal to this vector.
    /// </summary>
    /// <param name="other">The other vector to compare to.</param>
    /// <returns>true if the vectors are equal; false otherwise.</returns>
    private bool Equals(Vec2 other) => this.X == other.X && this.Y == other.Y;
}
