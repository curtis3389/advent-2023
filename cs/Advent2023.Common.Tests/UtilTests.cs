// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common.Tests;

/// <summary>
/// Represents tests for function in the Util type.
/// </summary>
public class UtilTests
{
    /// <summary>
    /// Verifies that ParseCalibrationValue correctly parses the examples.
    /// </summary>
    [Fact]
    public void ParsesExamples()
    {
        Assert.Equal(12, Util.ParseCalibrationValue("1abc2"));
        Assert.Equal(38, Util.ParseCalibrationValue("pqr3stu8vwx"));
        Assert.Equal(15, Util.ParseCalibrationValue("a1b2c3d4e5f"));
        Assert.Equal(77, Util.ParseCalibrationValue("treb7uchet"));
    }

    /// <summary>
    /// Verifies that BetterParseCalibrationValue correctly parses the examples.
    /// </summary>
    [Fact]
    public void BetterParsesExamples()
    {
        Assert.Equal(12, Util.BetterParseCalibrationValue("1abc2"));
        Assert.Equal(38, Util.BetterParseCalibrationValue("pqr3stu8vwx"));
        Assert.Equal(15, Util.BetterParseCalibrationValue("a1b2c3d4e5f"));
        Assert.Equal(77, Util.BetterParseCalibrationValue("treb7uchet"));
    }

    /// <summary>
    /// Verifies that BetterParseCalibrationValue correctly parses the second set of examples.
    /// </summary>
    [Fact]
    public void BetterParsesSecondExamples()
    {
        Assert.Equal(29, Util.BetterParseCalibrationValue("two1nine"));
        Assert.Equal(83, Util.BetterParseCalibrationValue("eightwothree"));
        Assert.Equal(13, Util.BetterParseCalibrationValue("abcone2threexyz"));
        Assert.Equal(24, Util.BetterParseCalibrationValue("xtwone3four"));
        Assert.Equal(42, Util.BetterParseCalibrationValue("4nineeightseven2"));
        Assert.Equal(14, Util.BetterParseCalibrationValue("zoneight234"));
        Assert.Equal(76, Util.BetterParseCalibrationValue("7pqrstsixteen"));
    }

    /// <summary>
    /// Verifies that BetterParseCalibrationValue correctly parses the critical edge case.
    /// </summary>
    [Fact]
    public void BetterParsesEdgeCase()
    {
        Assert.Equal(21, Util.BetterParseCalibrationValue("twone"));
    }
}
