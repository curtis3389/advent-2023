// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Day10.Part1;

using System;
using Advent2023.Common;

/// <summary>
/// Represents the program to solve part 1 of day 10 of Advent of Code 2023.
/// </summary>
public class Program
{
    /// <summary>
    /// Runs the program with given command-line arguments.
    /// </summary>
    /// <param name="args">The command-line arguments to use.</param>
    public static void Main(string[] args)
    {
        var diagram = new PipeDiagram(File.ReadAllLines(args[0]));
        Console.WriteLine(diagram.GetLongestDistance());
    }
}
