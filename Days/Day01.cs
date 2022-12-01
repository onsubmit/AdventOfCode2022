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

            const int NumTopElvesToTrack = 3;
            List<int> top3Calories = new(Enumerable.Range(1, NumTopElvesToTrack).Select(x => int.MinValue));
            int runningSum = 0;

            while (true)
            {
                string? line = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    // Next elf
                    for (int i = 0; i < NumTopElvesToTrack; i++)
                    {
                        if (runningSum > top3Calories[i])
                        {
                            top3Calories.Insert(i, runningSum);

                            if (top3Calories.Count > NumTopElvesToTrack)
                            {
                                top3Calories.RemoveAt(top3Calories.Count - 1);
                            }

                            break;
                        }
                    }

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

            return top3Calories.Sum().ToString();
        }
    }
}
