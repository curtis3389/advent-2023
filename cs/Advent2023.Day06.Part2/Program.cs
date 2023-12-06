// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Day06.Part2;

using System.Text.RegularExpressions;
using Advent2023.Common;

/// <summary>
/// Represents the program to solve part 2 of day 6 of Advent of Code 2023.
/// </summary>
public class Program
{
    /// <summary>
    /// Runs the program with given command-line arguments.
    /// </summary>
    /// <param name="args">The command-line arguments to use.</param>
    public static void Main(string[] args)
    {
        var lines = File.ReadAllLines(args[0])
            .Select(line => Regex.Match(line, "(\\w+:)\\s+(.*)"))
            .Select(match => $"{match.Groups[1].Value} {Regex.Replace(match.Groups[2].Value, "\\s+", string.Empty)}")
            .ToList();
        var sheet = new RaceSheet(lines);
        Console.WriteLine(sheet.WaysToWin().First());
    }
}
