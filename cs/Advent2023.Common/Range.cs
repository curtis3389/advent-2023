// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

/// <summary>
/// Represents a range of values.
/// </summary>
public class Range : IComparable<Range>
{
    /// <summary>
    /// Gets the empty range.
    /// </summary>
    public static readonly Range Empty = new(0, 0);

    /// <summary>
    /// Initializes a new instance of the <see cref="Range"/> class.
    /// </summary>
    /// <param name="start">The start of the range.</param>
    /// <param name="count">The number of items in the range.</param>
    public Range(long start, long count)
    {
        this.Start = count == 0
            ? 0
            : start;
        this.Count = count;
    }

    /// <summary>
    /// Gets the first value in this range.
    /// </summary>
    public long Start { get; }

    /// <summary>
    /// Gets the number of values in this range.
    /// </summary>
    public long Count { get; }

    /// <summary>
    /// Gets the last value in this range.
    /// </summary>
    public long End => this.Limit - 1;

    /// <summary>
    /// Gets the value after the last value in this range.
    /// </summary>
    public long Limit => this.Start + this.Count;

    /// <summary>
    /// Gets the value before the first value in this range.
    /// </summary>
    public long LowerLimit => this.Start - 1;

    /// <summary>
    /// Gets a value indicating whether this range is empty.
    /// </summary>
    public bool IsEmpty => this.Count == 0;

    /// <summary>
    /// Checks if the given two ranges, in order, are contiguous.
    /// </summary>
    /// <param name="left">The lesser of the ranges.</param>
    /// <param name="right">The greater of the ranges.</param>
    /// <returns>true if the ranges can be combined; false otherwise.</returns>
    public static bool IsContiguous(Range left, Range? right) => left.Limit == right?.Start;

    /// <summary>
    /// Combines the given ranges.
    /// </summary>
    /// <param name="a">The lesser of the ranges.</param>
    /// <param name="b">The greater of the ranges.</param>
    /// <returns>A single range equivalent of the given ranges.</returns>
    public static Range Join(Range a, Range b) => new(a.Start, a.Count + b.Count);

    /// <inheritdoc />
    public int CompareTo(Range? other) => other != null
        ? this.Start.CompareTo(other.Start)
        : 1;

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

        return obj.GetType() == this.GetType() && this.Equals((Range)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(this.Start, this.Count);

    /// <inheritdoc />
    public override string ToString() => $"{this.Start}-{this.End}";

    /// <summary>
    /// Checks if this range contains the given value.
    /// </summary>
    /// <param name="value">The value to check if is in the range.</param>
    /// <returns>true if the value is in this range; false otherwise.</returns>
    public bool Contains(long value) => value >= this.Start && value <= this.End;

    /// <summary>
    /// Compares this range to the given range.
    /// </summary>
    /// <param name="other">The other range to compare this to.</param>
    /// <returns>The results of the comparison.</returns>
    public RangeComparison Compare(Range other)
    {
        var before = this.SubRangeLessThan(other.Start);
        var after = this.SubRangeGreaterThan(other.End);
        var overlap = this.SubRangeBetween(other.LowerLimit, other.Limit);
        return new RangeComparison(before, overlap, after);
    }

    /// <summary>
    /// Gets the part of this range that is less than the given value.
    /// </summary>
    /// <param name="value">The value to get the range less than.</param>
    /// <returns>The part of this range that is less than the value.</returns>
    public Range SubRangeLessThan(long value)
    {
        if (this.Contains(value))
        {
            return new Range(this.Start, value - this.Start);
        }

        return value <= this.Start
            ? Range.Empty
            : this;
    }

    /// <summary>
    /// Gets the part of this range that is greater than the given value.
    /// </summary>
    /// <param name="value">The value to get the range greater than.</param>
    /// <returns>The part of this range that is greater than the value.</returns>
    public Range SubRangeGreaterThan(long value)
    {
        if (this.Contains(value))
        {
            return new Range(value + 1, this.Count - (value - this.LowerLimit));
        }

        return value >= this.End
            ? Range.Empty
            : this;
    }

    /// <summary>
    /// Gets the part of this range that is between the given values.
    /// </summary>
    /// <param name="lowerLimit">The value to get the range greater than.</param>
    /// <param name="upperLimit">The value to get the range less than.</param>
    /// <returns>The part of this range that is between the values..</returns>
    public Range SubRangeBetween(long lowerLimit, long upperLimit) =>
        this.SubRangeGreaterThan(lowerLimit)
            .SubRangeLessThan(upperLimit);

    /// <summary>
    /// Determines whether this is equal to the given range.
    /// </summary>
    /// <param name="other">The range to compare this to.</param>
    /// <returns>true if this is equal to the other range; false otherwise.</returns>
    private bool Equals(Range other) => this.Start == other.Start && this.Count == other.Count;
}
