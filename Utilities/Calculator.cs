//-----------------------------------------------------------------------
// <copyright file="Calculator.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Utilities
{
    /// <summary>
    /// Represents a calculator.
    /// </summary>
    internal static class Calculator
    {
        /// <summary>
        /// Gets the least common multiple of the provided set of values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The least common multiple.</returns>
        internal static long GetLeastCommonMultiple(IEnumerable<long> values)
        {
            return values.Aggregate(GetLeastCommonMultiple);
        }

        /// <summary>
        /// Gets the least common multiple of the two values.
        /// </summary>
        /// <param name="x">First value.</param>
        /// <param name="y">Second value.</param>
        /// <returns>The least common multiple.</returns>
        internal static long GetLeastCommonMultiple(long x, long y)
        {
            return Math.Abs(x * y) / GetGreatestCommonDenominator(x, y);
        }

        /// <summary>
        /// Gets the greatest common denominator of the provided set of values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The greatest common denominator.</returns>
        /// <exception cref="InvalidOperationException">Thrown when set is empty.</exception>
        internal static long GetGreatestCommonDenominator(IEnumerable<long> values)
        {
            if (!values.Any())
            {
                throw new InvalidOperationException();
            }

            if (values.Count() == 1)
            {
                return values.First();
            }

            if (values.Count() == 2)
            {
                return GetGreatestCommonDenominator(values.ElementAt(0), values.ElementAt(1));
            }

            return GetGreatestCommonDenominator(values.ElementAt(0), GetGreatestCommonDenominator(values.Skip(1)));
        }

        /// <summary>
        /// Gets the greatest common denominator of the two values.
        /// </summary>
        /// <param name="x">First value.</param>
        /// <param name="y">Second value.</param>
        /// <returns>The greatest common denominator.</returns>
        internal static long GetGreatestCommonDenominator(long x, long y)
        {
            return y == 0 ? x : GetGreatestCommonDenominator(y, x % y);
        }
    }
}
