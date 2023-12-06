namespace Advent2023.Common;

public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
    {
        foreach (var item in list)
        {
            action(item);
        }
    }

    public static IEnumerable<(T, T)> Pairs<T>(this IEnumerable<T> list) => list
        .Select((item, index) => (item, index))
        .GroupBy(t => t.index / 2)
        .Select(group => (group.First().item, group.Last().item));
}
