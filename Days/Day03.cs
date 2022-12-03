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
            const int GroupSize = 3;

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Any(c => !char.IsLetter(c)))
                {
                    throw new InvalidDataException();
                }

                List<string> group = new(GroupSize) { line };
                for (int i = 0; i < GroupSize - 1; i++)
                {
                    line = sr.ReadLine();
                    if (line == null)
                    {
                        throw new InvalidDataException();
                    }

                    group.Add(line);
                }

                IEnumerable<char> common = group[0];
                for (int i = 1; i < group.Count; i++)
                {
                    common = common.Intersect(group[i]);
                }

                char sharedItem = common.Single();
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
