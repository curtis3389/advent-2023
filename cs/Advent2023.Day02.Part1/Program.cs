// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Day02.Part1;

using Advent2023.Common;

/// <summary>
/// Represents the program to solve part 1 of day 2 of Advent of Code 2023.
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
            .Where(game => game.Possible(12, 13, 14))
            .Select(game => game.Id)
            .Sum();
        Console.WriteLine(sum);
    }
}
