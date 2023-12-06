// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

/// <summary>
/// Represent a comparison between two ranges.
/// </summary>
public class RangeComparison
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RangeComparison"/> class.
    /// </summary>
    /// <param name="before">The range of elements before the other range.</param>
    /// <param name="overlap">The range of elements in the other range.</param>
    /// <param name="after">The range of elements after the other range.</param>
    public RangeComparison(Range before, Range overlap, Range after)
    {
        this.Before = before;
        this.Overlap = overlap;
        this.After = after;
    }

    /// <summary>
    /// Gets the range of elements that are before the other range.
    /// </summary>
    public Range Before { get; }

    /// <summary>
    /// Gets the range of elements that are in the other range.
    /// </summary>
    public Range Overlap { get; }

    /// <summary>
    /// Gets the range of elements that are after the other range.
    /// </summary>
    public Range After { get; }

    /// <summary>
    /// Gets the range(s) that are not in the other range.
    /// </summary>
    public IEnumerable<Range> Remains => new[] { this.Before, this.After }.Where(r => !r.IsEmpty);

    /// <inheritdoc />
    public override string ToString() => $"({this.Before};{this.Overlap};{this.After})";
}
