// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Day11.Part1;

using System;
using Advent2023.Common;

/// <summary>
/// Represents the program to solve part 1 of day 11 of Advent of Code 2023.
/// </summary>
public class Program
{
    /// <summary>
    /// Runs the program with given command-line arguments.
    /// </summary>
    /// <param name="args">The command-line arguments to use.</param>
    public static void Main(string[] args)
    {
        var map = new GalaxyMap(File.ReadAllLines(args[0]));
        Console.WriteLine(map.GetShortestPathLengths().Sum());
    }
}
