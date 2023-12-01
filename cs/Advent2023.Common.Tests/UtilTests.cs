namespace Advent2023.Common.Tests;

public class UtilTests
{
    [Fact]
    public void ParsesExamples()
    {
        Assert.Equal(12, Util.ParseCalibrationValue("1abc2"));
        Assert.Equal(38, Util.ParseCalibrationValue("pqr3stu8vwx"));
        Assert.Equal(15, Util.ParseCalibrationValue("a1b2c3d4e5f"));
        Assert.Equal(77, Util.ParseCalibrationValue("treb7uchet"));
    }
    
    [Fact]
    public void BetterParsesExamples()
    {
        Assert.Equal(12, Util.BetterParseCalibrationValue("1abc2"));
        Assert.Equal(38, Util.BetterParseCalibrationValue("pqr3stu8vwx"));
        Assert.Equal(15, Util.BetterParseCalibrationValue("a1b2c3d4e5f"));
        Assert.Equal(77, Util.BetterParseCalibrationValue("treb7uchet"));
    }
    
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

    [Fact]
    public void BetterParsesEdgeCase()
    {
        Assert.Equal(21, Util.BetterParseCalibrationValue("twone"));
    }
}