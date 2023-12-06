namespace Advent2023.Common;

public static class ListExtensions
{
    public static T Dequeue<T>(this IList<T> list)
    {
        var first = list[0];
        list.RemoveAt(0);
        return first;
    }

    public static void Enqueue<T>(this IList<T> list, T item)
    {
        list.Insert(0, item);
    }

    public static T? Next<T>(this IList<T> list) => list.FirstOrDefault();
}
