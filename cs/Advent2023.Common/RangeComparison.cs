namespace Advent2023.Common;

public class RangeComparison
{
    public Range Before { get; }
    public Range Overlap { get; }
    public Range After { get; }
    public IEnumerable<Range> Remains => new[]{this.Before, this.After}.Where(r => !r.IsEmpty);

    public RangeComparison(Range before, Range overlap, Range after)
    {
        this.Before = before;
        this.Overlap = overlap;
        this.After = after;
    }

    public override string ToString() => $"({this.Before};{this.Overlap};{this.After})";
}
