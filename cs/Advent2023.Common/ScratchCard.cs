using System.Text.RegularExpressions;

namespace Advent2023.Common;

public class ScratchCard
{
    private static readonly Regex lineRegex = new ("Card\\s+(\\d+):\\s+(.*)\\|\\s+(.*)");

    public ScratchCard(string line)
    {
        var match = lineRegex.Match(line);
        this.Id = int.Parse(match.Groups[1].Value);
        this.WinningNumbers = match.Groups[2].Value.Split(' ').Where(s => s != string.Empty).Select(int.Parse).ToList();
        this.Numbers = match.Groups[3].Value.Split(' ').Where(s => s != string.Empty).Select(int.Parse).ToList();
    }

    public static int WinningTotal(IList<ScratchCard> pile)
    {
        ScratchCard Card(int id) => pile.Single(card => card.Id == id);
        var counts = new Dictionary<int, int>(pile.Select(card => new KeyValuePair<int, int>(card.Id, 1)));
        counts.Keys
            .Order()
            .ForEach(key => Enumerable.Range(key + 1, Card(key).Matches)
                .ForEach(rewardKey => counts[rewardKey] += counts[key]));
        return counts.Values.Sum();
    }

    public int Id { get; }
    public IList<int> Numbers { get; }
    public IList<int> WinningNumbers { get; }
    public int Matches => this.Numbers.Intersect(this.WinningNumbers).Count();
    public int Points => (int)Math.Pow(2, this.Matches - 1);
}
