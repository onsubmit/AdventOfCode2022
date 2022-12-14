//-----------------------------------------------------------------------
// <copyright file="Monkey.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Models
{
    /// <summary>
    ///  Represents a monkey.
    /// </summary>
    internal class Monkey
    {
        /// <summary>
        /// Gets or sets the queue of items inspected by the monkey.
        /// </summary>
        public Queue<long> Items { get; set; } = new();

        /// <summary>
        /// Gets or sets the divisor.
        /// </summary>
        public long Divisor { get; set; }

        /// <summary>
        /// Gets or sets a function that describes how your worry level changes as that monkey inspects an item.
        /// </summary>
        public Func<long, long> Operation { get; set; } = (worryLevel) => 0;

        /// <summary>
        /// Gets or sets the index of the destination monkey when <see cref="Test"/> is <c>true</c>.
        /// </summary>
        public int TestTrueMonkey { get; set; }

        /// <summary>
        /// Gets or sets the index of the destination monkey when <see cref="Test"/> is <c>false</c>.
        /// </summary>
        public int TestFalseMonkey { get; set; }

        /// <summary>
        /// Describes how the monkey uses your worry level to decide where to throw an item next.
        /// </summary>
        /// <param name="worryLevel">The current worry level.</param>
        /// <returns>The monkey's decision.</returns>
        public bool Test(long worryLevel) => worryLevel % this.Divisor == 0;
    }
}
