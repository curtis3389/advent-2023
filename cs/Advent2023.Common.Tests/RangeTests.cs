namespace Advent2023.Common.Tests;

using Range = Advent2023.Common.Range;

public class RangeTests
{
    private static readonly string exampleLine = "50 98 2";
    private static readonly string exampleLine2 = "52 50 48";

    [Fact]
    public void ComparesToSelf()
    {
        var range = new Range(98, 2);
        var comparison = range.Compare(range);
        Assert.True(comparison.Before.IsEmpty);
        Assert.True(comparison.After.IsEmpty);
        Assert.Equal(range, comparison.Overlap);
    }

    [Fact]
    public void ComparesToOverlapping()
    {
        var range = new Range(10, 10); // 10-19
        var lesserRange = new Range(5, 10); // 5-14
        var greaterRange = new Range(15, 10); // 15-24

        var comparison = range.Compare(lesserRange);
        Assert.True(comparison.Before.IsEmpty);
        Assert.Equal(new Range(10, 5), comparison.Overlap);
        Assert.Equal(new Range(15, 5), comparison.After);

        comparison = range.Compare(greaterRange);
        Assert.True(comparison.After.IsEmpty);
        Assert.Equal(new Range(15, 5), comparison.Overlap);
        Assert.Equal(new Range(10, 5), comparison.Before);
    }

    [Fact]
    public void ComparesToContained()
    {
        var range = new Range(10, 10); // 10-19
        var containedRange = new Range(13, 4);

        var comparison = range.Compare(containedRange);
        Assert.Equal(new Range(10, 3), comparison.Before);
        Assert.Equal(new Range(17, 3), comparison.After);
        Assert.Equal(containedRange, comparison.Overlap);

        comparison = containedRange.Compare(range);
        Assert.True(comparison.Before.IsEmpty);
        Assert.True(comparison.After.IsEmpty);
        Assert.Equal(containedRange, comparison.Overlap);
    }
}
