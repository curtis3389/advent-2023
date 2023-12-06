namespace Advent2023.Common;

public class Range : IComparable<Range>
{
    protected bool Equals(Range other)
    {
        return Start == other.Start && Count == other.Count;
    }

    public int CompareTo(Range? other) => this.Start.CompareTo(other.Start);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Range)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, Count);
    }

    public override string ToString() => $"{this.Start}-{this.End}";

    public static readonly Range Empty = new (0, 0);

    public Range(long start, long count)
    {
        this.Start = count == 0
            ? 0
            : start;
        this.Count = count;
    }

    public long Start { get; }

    public long Count { get; }

    public long End => this.Limit - 1;

    public long Limit => this.Start + this.Count;

    public long LowerLimit => this.Start - 1;

    public bool Contains(long value) => value >= this.Start && value <= this.End;

    public bool IsEmpty => this.Count == 0;

    public RangeComparison Compare(Range other)
    {
        var before = this.SubRangeLessThan(other.Start);
        var after = this.SubRangeGreaterThan(other.End);
        var overlap = this.SubRangeBetween(other.LowerLimit, other.Limit);
        return new RangeComparison(before, overlap, after);
    }

    public Range SubRangeLessThan(long value)
    {
        if (this.Contains(value))
        {
            return new Range(this.Start, value - this.Start);
        }

        if (value <= this.Start)
        {
            return Range.Empty;
        }

        return this;
    }

    public Range SubRangeGreaterThan(long value)
    {
        if (this.Contains(value))
        {
            return new Range(value + 1, this.Count - (value - this.LowerLimit));
        }

        if (value >= this.End)
        {
            return Range.Empty;
        }

        return this;
    }

    public Range SubRangeBetween(long lowerLimit, long upperLimit) =>
        this.SubRangeGreaterThan(lowerLimit)
            .SubRangeLessThan(upperLimit);

    public static bool IsContiguous(Range left, Range? right)
    {
        return left.Limit == right?.Start;
    }

    public static Range Join(Range a, Range b)
    {
        return new Range(a.Start, a.Count + b.Count);
    }
}
