//-----------------------------------------------------------------------
// <copyright file="Day01.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Days
{
    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day01 : IDay
    {
        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day01.txt");

            int maxCalories = int.MinValue;
            int runningSum = 0;

            while (true)
            {
                string? line = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    // Next elf
                    maxCalories = Math.Max(maxCalories, runningSum);
                    runningSum = 0;

                    if (line == null)
                    {
                        break;
                    }

                    continue;
                }

                if (!int.TryParse(line, out int calories))
                {
                    throw new InvalidDataException();
                }

                runningSum += calories;
            }

            return maxCalories.ToString();
        }
    }
}
