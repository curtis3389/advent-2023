// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

/// <summary>
/// Represents extensions to IList&lt;T&gt;.
/// </summary>
public static class ListExtensions
{
    /// <summary>
    /// Removes the first item from this list.
    /// </summary>
    /// <param name="list">The list to remove the first item from.</param>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    /// <returns>The first item in this list.</returns>
    public static T PopFront<T>(this IList<T> list)
    {
        var first = list[0];
        list.RemoveAt(0);
        return first;
    }

    /// <summary>
    /// Adds the given item to the front of this list.
    /// </summary>
    /// <param name="list">The list to add the item to.</param>
    /// <param name="item">The item to add to the front of this list.</param>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    public static void PushFront<T>(this IList<T> list, T item)
    {
        list.Insert(0, item);
    }

    /// <summary>
    /// Gets the first item of this list or default if there is none.
    /// </summary>
    /// <param name="list">The list to get the first item of.</param>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    /// <returns>The first item of this list or default.</returns>
    public static T? PeekFront<T>(this IList<T> list) => list.FirstOrDefault();
}
