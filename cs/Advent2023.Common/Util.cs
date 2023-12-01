using System.Text.RegularExpressions;

namespace Advent2023.Common;

public static class Util
{
    private static readonly Regex digitRegex = new Regex("(\\d|one|two|three|four|five|six|seven|eight|nine|zero)");
    private static readonly Regex reverseDigitRegex = new Regex("(\\d|one|two|three|four|five|six|seven|eight|nine|zero)", RegexOptions.RightToLeft);
    
    public static int ParseCalibrationValue(string line)
    {
        var firstDigit = line.First(char.IsDigit);
        var lastDigit = line.Last(char.IsDigit);
        return int.Parse($"{firstDigit}{lastDigit}");
    }

    public static int BetterParseCalibrationValue(string line)
    {
        var firstDigit = FirstDigit(line);
        var lastDigit = LastDigit(line);
        return int.Parse($"{firstDigit}{lastDigit}");
    }

    private static char FirstDigit(string line)
    {
        return ToChar(digitRegex.Match(line).Groups[0].Captures[0].Value);
    }
    
    private static char LastDigit(string line)
    {
        return ToChar(reverseDigitRegex.Match(line).Groups[0].Captures[0].Value);
    }

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
            _ => throw new Exception($"Unknown digit string: {digit}")
        };
    }
}
