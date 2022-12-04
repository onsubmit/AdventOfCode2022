//-----------------------------------------------------------------------
// <copyright file="Day04.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Days
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day04 : IDay
    {
        private static readonly Regex ParseLineRegex = new(@"(?<ELF1_START>\d+)-(?<ELF1_END>\d+),(?<ELF2_START>\d+)-(?<ELF2_END>\d+)", RegexOptions.Compiled);

        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day04.txt");

            int uselessElves = 0;

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                Match match = ParseLineRegex.Match(line);

                if (!match.Success
                    || !int.TryParse(match.Groups["ELF1_START"].Value, out int elf1Start)
                    || !int.TryParse(match.Groups["ELF1_END"].Value, out int elf1End)
                    || !int.TryParse(match.Groups["ELF2_START"].Value, out int elf2Start)
                    || !int.TryParse(match.Groups["ELF2_END"].Value, out int elf2End)
                    || elf1Start > elf1End
                    || elf2Start > elf2End)
                {
                    throw new InvalidDataException();
                }

                if ((elf1Start >= elf2Start && elf1End <= elf2End)
                    || (elf2Start >= elf1Start && elf2End <= elf1End))
                {
                    uselessElves++;
                }
            }

            return uselessElves.ToString();
        }
    }
}
