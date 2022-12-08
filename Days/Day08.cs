//-----------------------------------------------------------------------
// <copyright file="Day08.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2022.Days
{
    using System.Linq;
    using AdventOfCode2022.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day08 : IDay
    {
        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            ElfTreePatch trees = new(File.ReadAllLines("input\\Day08.txt"));
            return trees.NumTreesVisible.ToString();
        }
    }
}
