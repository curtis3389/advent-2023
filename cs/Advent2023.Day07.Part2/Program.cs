// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Day07.Part2;

using Advent2023.Common;

/// <summary>
/// Represents the program to solve part 2 of day 7 of Advent of Code 2023.
/// </summary>
public class Program
{
    /// <summary>
    /// Runs the program with given command-line arguments.
    /// </summary>
    /// <param name="args">The command-line arguments to use.</param>
    public static void Main(string[] args)
    {
        var total = File.ReadAllLines(args[0])
            .Select(line => new CamelCardHand(line))
            .Order(new CamelCardHand.JokerComparer())
            .Select((hand, rank) => (rank + 1) * hand.Bid)
            .Sum();
        Console.WriteLine(total);
    }
}
