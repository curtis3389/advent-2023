// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

/// <summary>
/// Represents extensions to IEnumerable&lt;T&gt;.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Executes the given action for each item in this enumerable.
    /// </summary>
    /// <param name="list">The enumerable to execute the actions for.</param>
    /// <param name="action">The action to execute for each item.</param>
    /// <typeparam name="T">The type of the items of the enumeration.</typeparam>
    public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
    {
        foreach (var item in list)
        {
            action(item);
        }
    }

    /// <summary>
    /// Pairs up adjacent elements in this enumeration.
    /// </summary>
    /// <param name="list">The enumeration to pair up.</param>
    /// <typeparam name="T">The type of the items in the enumeration.</typeparam>
    /// <returns>The pairs in this enumeration.</returns>
    public static IEnumerable<(T First, T Second)> Pairs<T>(this IEnumerable<T> list) => list
        .Select((item, index) => (item, index))
        .GroupBy(t => t.index / 2)
        .Select(group => (group.First().item, group.Last().item));
}
