//-----------------------------------------------------------------------
// <copyright file="Day10.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Days
{
    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day10 : IDay
    {
        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day10.txt");

            int x = 1;
            int cycle = 1;
            int sum = 0;

            void IncrementCycle()
            {
                cycle++;
                if ((cycle + 20) % 40 == 0)
                {
                    int signalStrength = cycle * x;
                    sum += signalStrength;
                }
            }

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "noop")
                {
                    IncrementCycle();
                    continue;
                }

                if (line.Contains("addx"))
                {
                    if (!int.TryParse(line[5..], out int value))
                    {
                        throw new InvalidDataException();
                    }

                    IncrementCycle();
                    x += value;
                    IncrementCycle();
                    continue;
                }

                throw new InvalidDataException();
            }

            return sum.ToString();
        }
    }
}
