// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Day01.Part2;

using Advent2023.Common;

/// <summary>
/// Represents the program to solve part 2 of day 1 of Advent of Code 2023.
/// </summary>
public class Program
{
    /// <summary>
    /// Runs the program with given command-line arguments.
    /// </summary>
    /// <param name="args">The command-line arguments to use.</param>
    public static void Main(string[] args)
    {
        var sum = File.ReadAllLines(args.First())
            .Select(Util.BetterParseCalibrationValue)
            .Sum();
        Console.WriteLine(sum);
    }
}
