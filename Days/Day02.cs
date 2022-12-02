//-----------------------------------------------------------------------
// <copyright file="Day02.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Days
{
    using AdventOfCode2022.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day02 : IDay
    {
        private Dictionary<RockPaperScissors, int> scores = new()
        {
            { RockPaperScissors.Rock, 1 },
            { RockPaperScissors.Paper, 2 },
            { RockPaperScissors.Scissors, 3 },
        };

        private Dictionary<string, RockPaperScissors> guideMap = new()
        {
            { "A", RockPaperScissors.Rock },
            { "B", RockPaperScissors.Paper },
            { "C", RockPaperScissors.Scissors },
            { "X", RockPaperScissors.Rock },
            { "Y", RockPaperScissors.Paper },
            { "Z", RockPaperScissors.Scissors },
        };

        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day02.txt");

            int score = 0;

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] plays = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (!this.guideMap.TryGetValue(plays[0], out RockPaperScissors elf))
                {
                    throw new InvalidDataException();
                }

                if (!this.guideMap.TryGetValue(plays[1], out RockPaperScissors me))
                {
                    throw new InvalidDataException();
                }

                score += this.scores[me] + GetOutcomeScore(elf, me);
            }

            return score.ToString();
        }

        private static int GetOutcomeScore(RockPaperScissors elf, RockPaperScissors me)
        {
            if (elf == me)
            {
                // Draw
                return 3;
            }

            if ((elf == RockPaperScissors.Rock && me == RockPaperScissors.Paper)
                || (elf == RockPaperScissors.Paper && me == RockPaperScissors.Scissors)
                || (elf == RockPaperScissors.Scissors && me == RockPaperScissors.Rock))
            {
                // Win
                return 6;
            }

            // Loss
            return 0;
        }
    }
}
