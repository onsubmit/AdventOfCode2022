//-----------------------------------------------------------------------
// <copyright file="Day15.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Days
{
    using AdventOfCode2022.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day15 : IDay
    {
        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            string[] lines = File.ReadAllLines("input\\Day15.txt");
            TunnelNetwork network = new(lines);

            return 0.ToString();
        }
    }
}
