// Copyright (c) Curtis Hollibaugh. All rights reserved.

namespace Advent2023.Common;

/// <summary>
/// Represents an equality comparer that uses lambda functions.
/// </summary>
/// <typeparam name="T">The type of the values to compare.</typeparam>
public class LambdaEqualityComparer<T> : IEqualityComparer<T>
{
    private readonly Func<T?, T?, bool> lambda;
    private readonly Func<T, int> hashLambda;

    /// <summary>
    /// Initializes a new instance of the <see cref="LambdaEqualityComparer{T}"/> class.
    /// </summary>
    /// <param name="lambda">The lambda to use for comparison. Return true if equal.</param>
    /// <param name="hashLambda">The lambda to use for hashing. Should mirror the comparison.</param>
    public LambdaEqualityComparer(Func<T?, T?, bool> lambda, Func<T, int> hashLambda)
    {
        this.lambda = lambda;
        this.hashLambda = hashLambda;
    }

    /// <inheritdoc />
    public bool Equals(T? x, T? y)
    {
        var result = this.lambda(x, y);
        return result;
    }

    /// <inheritdoc />
    public int GetHashCode(T obj)
    {
        return this.hashLambda(obj);
    }
}
