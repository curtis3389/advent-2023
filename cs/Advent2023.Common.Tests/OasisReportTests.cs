// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common.Tests;

/// <summary>
/// Represents tests for the OasisReport type.
/// </summary>
public class OasisReportTests
{
    private static readonly IList<string> ExampleData = new List<string>
    {
        "0 3 6 9 12 15",
        "1 3 6 10 15 21",
        "10 13 16 21 30 45",
    };

    /// <summary>
    /// Verifies OasisReport can extrapolate the example next values.
    /// </summary>
    [Fact]
    public void CalculatesExampleNextValues()
    {
        var expected = new int[] { 18, 28, 68 };
        var report = new OasisReport(ExampleData);

        var nextValues = report.ExtrapolateNextValues();

        for (var i = 0; i < expected.Length; ++i)
        {
            Assert.Equal(expected[i], nextValues[i]);
        }
    }

    /// <summary>
    /// Verifies OasisReport can extrapolate the example previous values.
    /// </summary>
    [Fact]
    public void CalculatesExamplePreviousValues()
    {
        var expected = new int[] { -3, 0, 5 };
        var report = new OasisReport(ExampleData);

        var nextValues = report.ExtrapolatePreviousValues();

        for (var i = 0; i < expected.Length; ++i)
        {
            Assert.Equal(expected[i], nextValues[i]);
        }
    }
}
