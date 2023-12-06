// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Day02.Part2;

using Advent2023.Common;

/// <summary>
/// Represents the program to solve part 2 of day 2 of Advent of Code 2023.
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
            .Select(line => new CubeGame(line))
            .Select(game => game.Power())
            .Sum();
        Console.WriteLine(sum);
    }
}
