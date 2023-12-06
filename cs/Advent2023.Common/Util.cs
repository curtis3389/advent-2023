// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

using System.Text.RegularExpressions;

/// <summary>
/// Represents a static collection of helper (utility) functions.
/// </summary>
public static class Util
{
    private static readonly Regex DigitRegex = new("(\\d|one|two|three|four|five|six|seven|eight|nine|zero)");
    private static readonly Regex ReverseDigitRegex = new("(\\d|one|two|three|four|five|six|seven|eight|nine|zero)", RegexOptions.RightToLeft);

    /// <summary>
    /// Parses the calibration value from the given line.
    /// </summary>
    /// <param name="line">The input line to parse the value from.</param>
    /// <returns>The calibration value.</returns>
    public static int ParseCalibrationValue(string line)
    {
        var firstDigit = line.First(char.IsDigit);
        var lastDigit = line.Last(char.IsDigit);
        return int.Parse($"{firstDigit}{lastDigit}");
    }

    /// <summary>
    /// Parses the calibration value as per part 2 from the given line.
    /// </summary>
    /// <param name="line">The line to parse the value from.</param>
    /// <returns>The calibration value.</returns>
    public static int BetterParseCalibrationValue(string line)
    {
        var firstDigit = FirstDigit(line);
        var lastDigit = LastDigit(line);
        return int.Parse($"{firstDigit}{lastDigit}");
    }

    /// <summary>
    /// Gets the first digit (or digit string) from the given line.
    /// </summary>
    /// <param name="line">The line to get the first digit from.</param>
    /// <returns>The first digit in the line, as a char.</returns>
    private static char FirstDigit(string line)
    {
        return ToChar(DigitRegex.Match(line).Groups[0].Captures[0].Value);
    }

    /// <summary>
    /// Gets the last digit (or digit string) from the given line.
    /// </summary>
    /// <param name="line">The line to get the last digit from.</param>
    /// <returns>The last digit in the line, as a char.</returns>
    private static char LastDigit(string line)
    {
        return ToChar(ReverseDigitRegex.Match(line).Groups[0].Captures[0].Value);
    }

    /// <summary>
    /// Converts the given digit string to a digit char.
    /// </summary>
    /// <param name="digit">The digit string (e.g. "1" or "four").</param>
    /// <returns>The digit char.</returns>
    /// <exception cref="Exception">If digit string is invalid.</exception>
    private static char ToChar(string digit)
    {
        return digit switch
        {
            "1" or "one" => '1',
            "2" or "two" => '2',
            "3" or "three" => '3',
            "4" or "four" => '4',
            "5" or "five" => '5',
            "6" or "six" => '6',
            "7" or "seven" => '7',
            "8" or "eight" => '8',
            "9" or "nine" => '9',
            "0" or "zero" => '0',
            _ => throw new Exception($"Unknown digit string: {digit}"),
        };
    }
}
