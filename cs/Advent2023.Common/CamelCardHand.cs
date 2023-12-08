// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

using System.Text.RegularExpressions;

/// <summary>
/// Represents a hand in the camel card game.
/// </summary>
public class CamelCardHand : IComparable<CamelCardHand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CamelCardHand"/> class.
    /// </summary>
    /// <param name="line">The input line to parse.</param>
    public CamelCardHand(string line)
    {
        var match = Regex.Match(line, "(\\w+)\\s+(\\d+)");
        this.Cards = match.Groups[1].Value
            .Select(c => ToCard(c))
            .ToList();
        this.JokerCards = this.Cards.Select(c => c == CamelCard.Jack ? CamelCard.Joker : c).ToList();
        this.Bid = int.Parse(match.Groups[2].Value);
        this.HandType = GetHandType(this.Cards);
        this.JokerHandType = GetHandType(this.JokerCards);
    }

    /// <summary>
    /// Gets the cards in this hand.
    /// </summary>
    public IList<CamelCard> Cards { get; }

    /// <summary>
    /// Gets the cards, with jokers, in this hand.
    /// </summary>
    public IList<CamelCard> JokerCards { get; }

    /// <summary>
    /// Gets the bid for this hand.
    /// </summary>
    public int Bid { get; }

    /// <summary>
    /// Gets the type of this hand.
    /// </summary>
    public CamelCardHandType HandType { get; }

    /// <summary>
    /// Gets the type of this hand with jokers.
    /// </summary>
    public CamelCardHandType JokerHandType { get; }

    /// <inheritdoc />
    public override string ToString() => $"{string.Join(string.Empty, this.Cards.Select(ToChar))}\t{this.JokerHandType}\t{this.Bid}";

    /// <inheritdoc />
    public int CompareTo(CamelCardHand? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (this.HandType > other.HandType || (this.HandType == other.HandType && HasBiggerCards(this.Cards, other.Cards)))
        {
            return 1;
        }

        if (this.HandType < other.HandType || (this.HandType == other.HandType && HasBiggerCards(other.Cards, this.Cards)))
        {
            return -1;
        }

        return 0;
    }

    /// <summary>
    /// Converts the given character to a camel card.
    /// </summary>
    /// <param name="c">The character to convert to a card.</param>
    /// <returns>A camel card.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If passed an invalid character.</exception>
    private static CamelCard ToCard(char c) => c switch
    {
        '2' => CamelCard.Two,
        '3' => CamelCard.Three,
        '4' => CamelCard.Four,
        '5' => CamelCard.Five,
        '6' => CamelCard.Six,
        '7' => CamelCard.Seven,
        '8' => CamelCard.Eight,
        '9' => CamelCard.Nine,
        'T' => CamelCard.Ten,
        'J' => CamelCard.Jack,
        'Q' => CamelCard.Queen,
        'K' => CamelCard.King,
        'A' => CamelCard.Ace,
        _ => throw new ArgumentOutOfRangeException(nameof(c), c, $"Unknown card: {c}!"),
    };

    /// <summary>
    /// Converts the given card to its equivalent character.
    /// </summary>
    /// <param name="card">The card to convert.</param>
    /// <returns>A character.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If passed an unrecognized card.</exception>
    private static char ToChar(CamelCard card) => card switch
    {
        CamelCard.Joker => 'J',
        CamelCard.Two => '2',
        CamelCard.Three => '3',
        CamelCard.Four => '4',
        CamelCard.Five => '5',
        CamelCard.Six => '6',
        CamelCard.Seven => '7',
        CamelCard.Eight => '8',
        CamelCard.Nine => '9',
        CamelCard.Ten => 'T',
        CamelCard.Jack => 'J',
        CamelCard.Queen => 'Q',
        CamelCard.King => 'K',
        CamelCard.Ace => 'A',
        _ => throw new ArgumentOutOfRangeException(nameof(card), card, $"Unknown card: {card}!"),
    };

    /// <summary>
    /// Checks if a hand of cards has bigger individual cards than another hand.
    /// </summary>
    /// <param name="us">The hand to check if is bigger.</param>
    /// <param name="them">The hand to check against.</param>
    /// <returns>true if the first hand is bigger the the second; false otherwise.</returns>
    private static bool HasBiggerCards(IList<CamelCard> us, IList<CamelCard> them)
    {
        for (var i = 0; i < us.Count; ++i)
        {
            if (us[i] > them[i])
            {
                return true;
            }

            if (us[i] < them[i])
            {
                return false;
            }
        }

        return false;
    }

    /// <summary>
    /// Gets the type of the hand for the given cards.
    /// </summary>
    /// <param name="cards">Hand of cards to get the type of.</param>
    /// <returns>The camel card hand type.</returns>
    private static CamelCardHandType GetHandType(IList<CamelCard> cards)
    {
        var counts = cards
            .GroupBy(card => card)
            .Select(group => (group.Key, group.Count()))
            .ToDictionary(
                pair => pair.Key,
                pair => pair.Item2);

        bool XOfAKind(int x) => counts.Values.Any(count => count == x);
        int NOfXOfAKind(int x) => counts.Values.Count(count => count == x);
        int Jokers() => counts
            .Where(p => p.Key == CamelCard.Joker)
            .Select(p => p.Value)
            .FirstOrDefault(0);
        IList<int> NoJokerCounts() => counts.Where(pair => pair.Key != CamelCard.Joker).Select(pair => pair.Value).ToList();
        bool XOfAKindWithJokers(int x) => NoJokerCounts().Any(count => count >= x - Jokers());
        bool FullHouse() => (XOfAKind(3) && XOfAKind(2)) || (Jokers() == 1 && NOfXOfAKind(2) == 2);

        if (XOfAKindWithJokers(5) || Jokers() == 5)
        {
            return CamelCardHandType.FiveOfAKind;
        }

        if (XOfAKindWithJokers(4))
        {
            return CamelCardHandType.FourOfAKind;
        }

        if (FullHouse())
        {
            return CamelCardHandType.FullHouse;
        }

        if (XOfAKindWithJokers(3))
        {
            return CamelCardHandType.ThreeOfAKind;
        }

        if (NOfXOfAKind(2) == 2 || (NOfXOfAKind(2) == 1 && Jokers() == 1))
        {
            return CamelCardHandType.TwoPair;
        }

        if (NOfXOfAKind(2) == 1 || Jokers() == 1)
        {
            return CamelCardHandType.OnePair;
        }

        return CamelCardHandType.HighCard;
    }

    /// <summary>
    /// Represents a comparer for comparing <see cref="CamelCardHand"/>s based on their cards with jokers.
    /// </summary>
    public class JokerComparer : IComparer<CamelCardHand>
    {
        /// <inheritdoc />
        public int Compare(CamelCardHand? left, CamelCardHand? right)
        {
            if (left == null && right == null)
            {
                return 0;
            }

            if (right == null)
            {
                return 1;
            }

            if (left == null)
            {
                return -1;
            }

            if (left.JokerHandType > right.JokerHandType || (left.JokerHandType == right.JokerHandType && HasBiggerCards(left.JokerCards, right.JokerCards)))
            {
                return 1;
            }

            if (left.JokerHandType < right.JokerHandType || (left.JokerHandType == right.JokerHandType && HasBiggerCards(right.JokerCards, left.JokerCards)))
            {
                return -1;
            }

            return 0;
        }
    }
}
