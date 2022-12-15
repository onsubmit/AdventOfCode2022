//-----------------------------------------------------------------------
// <copyright file="Day14.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Days
{
    using AdventOfCode2022.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day14 : IDay
    {
        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            string[] lines = File.ReadAllLines("input\\Day14.txt");
            RockStructure structure = new(lines);

            Coordinate source = new(500, 0);

            int sandUnits = 0;
            while (structure.PlaceSand(source))
            {
                sandUnits++;
            }

            return sandUnits.ToString();
        }
    }
}
