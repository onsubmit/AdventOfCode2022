//-----------------------------------------------------------------------
// <copyright file="Day13.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Days
{
    using AdventOfCode2022.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day13 : IDay
    {
        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day13.txt");
            string? line;

            int index = 0;
            int sum = 0;
            PacketData? a;
            PacketData? b;
            while (true)
            {
                line = sr.ReadLine();
                if (line == null)
                {
                    break;
                }

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                a = new(line);

                line = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    throw new InvalidDataException();
                }

                b = new(line);

                index++;
                int compare = PacketDataComparer.Compare(a, b);
                if (compare < 0)
                {
                    sum += index;
                }
            }

            return sum.ToString();
        }
    }
}
