// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

/// <summary>
/// Represents the possible type (i.e. evaluations) of a hand in the camel card game.
/// </summary>
public enum CamelCardHandType
{
    /// <summary>
    /// The hand has all different cards.
    /// </summary>
    HighCard = 0,

    /// <summary>
    /// The hand has two of the same card.
    /// </summary>
    OnePair = 1,

    /// <summary>
    /// The hand has two of two different cards.
    /// </summary>
    TwoPair = 2,

    /// <summary>
    /// The hand has three of the same card.
    /// </summary>
    ThreeOfAKind = 3,

    /// <summary>
    /// The hand has three of the same card and a pair of another.
    /// </summary>
    FullHouse = 4,

    /// <summary>
    /// The hand has four of the same card.
    /// </summary>
    FourOfAKind = 5,

    /// <summary>
    /// The hand has five of the same card.
    /// </summary>
    FiveOfAKind = 6,
}
