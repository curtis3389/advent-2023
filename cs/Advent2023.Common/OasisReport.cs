// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

/// <summary>
/// Represents an report from the OASIS device.
/// </summary>
public class OasisReport
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OasisReport"/> class.
    /// </summary>
    /// <param name="lines">The input lines to parse.</param>
    public OasisReport(IList<string> lines)
    {
        this.Histories = lines
            .Select(line => (IList<int>)line
                .Split(' ')
                .Select(s => int.Parse(s))
                .ToList())
            .ToList();
    }

    /// <summary>
    /// Gets the histories of the different values.
    /// </summary>
    public IList<IList<int>> Histories { get; }

    /// <summary>
    /// Extrapolates the previous values for each history.
    /// </summary>
    /// <returns>The extrapolated previous value for each history.</returns>
    public IList<int> ExtrapolatePreviousValues()
    {
        return this.Histories.Select(ExtrapolatePreviousValue).ToList();
    }

    /// <summary>
    /// Extrapolates the next value for each history.
    /// </summary>
    /// <returns>The extrapolated next value for each history.</returns>
    public IList<int> ExtrapolateNextValues()
    {
        return this.Histories.Select(ExtrapolateNextValue).ToList();
    }

    /// <summary>
    /// Extrapolates the next value for the given values.
    /// </summary>
    /// <param name="values">The list of values to extrapolate the next value of.</param>
    /// <returns>The extrapolated next value for the list.</returns>
    private static int ExtrapolateNextValue(IList<int> values)
    {
        if (values.All(v => v == 0))
        {
            return 0;
        }

        return values.Last() + ExtrapolateNextValue(Diffs(values));
    }

    /// <summary>
    /// Extrapolates the previous value for the given values.
    /// </summary>
    /// <param name="values">The list of values to extrapolate the previous value of.</param>
    /// <returns>The extrapolated previous value for the list.</returns>
    private static int ExtrapolatePreviousValue(IList<int> values)
    {
        if (values.All(v => v == 0))
        {
            return 0;
        }

        return values.First() - ExtrapolatePreviousValue(Diffs(values));
    }

    /// <summary>
    /// Gets a list of the differences between the given values.
    /// </summary>
    /// <param name="values">The list of values ot get the differences of.</param>
    /// <returns>A list of the differences of the values.</returns>
    private static IList<int> Diffs(IList<int> values)
    {
        return values
            .Select((value, index) => index != 0 ? value - values[index - 1] : 0)
            .Skip(1)
            .ToList();
    }
}
