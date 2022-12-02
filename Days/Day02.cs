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
        private static readonly Dictionary<RockPaperScissors, int> Scores = new()
        {
            { RockPaperScissors.Rock, 1 },
            { RockPaperScissors.Paper, 2 },
            { RockPaperScissors.Scissors, 3 },
        };

        private static readonly Dictionary<string, RockPaperScissors> GuideMap = new()
        {
            { "A", RockPaperScissors.Rock },
            { "B", RockPaperScissors.Paper },
            { "C", RockPaperScissors.Scissors },
        };

        private static readonly Dictionary<RockPaperScissors, RockPaperScissors> WinningPlay = new()
        {
            { RockPaperScissors.Rock, RockPaperScissors.Paper },
            { RockPaperScissors.Paper, RockPaperScissors.Scissors },
            { RockPaperScissors.Scissors, RockPaperScissors.Rock },
        };

        private static readonly Dictionary<RockPaperScissors, RockPaperScissors> LosingPlay = WinningPlay.ToDictionary((p) => p.Value, (p) => p.Key);

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
                if (!GuideMap.TryGetValue(plays[0], out RockPaperScissors elf))
                {
                    throw new InvalidDataException();
                }

                RockPaperScissors me = plays[1] switch
                {
                    "X" => LosingPlay[elf],
                    "Y" => elf,
                    "Z" => WinningPlay[elf],
                    _ => throw new InvalidOperationException("Invalid strategy"),
                };

                score += Scores[me] + GetOutcomeScore(elf, me);
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

            if (WinningPlay[elf] == me)
            {
                // Win
                return 6;
            }

            // Loss
            return 0;
        }
    }
}
