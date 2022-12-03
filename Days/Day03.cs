//-----------------------------------------------------------------------
// <copyright file="Day03.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Days
{
    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day03 : IDay
    {
        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day03.txt");

            int sum = 0;

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Length % 2 != 0 || line.Any(c => !char.IsLetter(c)))
                {
                    throw new InvalidDataException();
                }

                int midpoint = line.Length / 2;
                string rucksack1 = line[..midpoint];
                string rucksack2 = line[midpoint..];

                char sharedItem = rucksack1.Intersect(rucksack2).Single();
                int priority = sharedItem switch
                {
                    <= 'Z' => sharedItem - 'A' + 27,
                    _ => sharedItem - 'a' + 1,
                };

                sum += priority;
            }

            return sum.ToString();
        }
    }
}
