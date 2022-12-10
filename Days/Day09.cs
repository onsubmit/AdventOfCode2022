//-----------------------------------------------------------------------
// <copyright file="Day09.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Days
{
    using System.Text.RegularExpressions;
    using AdventOfCode2022.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day09 : IDay
    {
        private static readonly Regex ParseLineRegex = new(@"(?<DIRECTION>[UDLR]{1}) (?<DISTANCE>\d+)", RegexOptions.Compiled);

        private static readonly Dictionary<string, int> VerticalMovements = new()
        {
            { "U", 1 },
            { "D", -1 },
        };

        private static readonly Dictionary<string, int> HorizontalMovements = new()
        {
            { "R", 1 },
            { "L", -1 },
        };

        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day09.txt");

            const int NumKnots = 10;

            List<Coordinate> rope = Enumerable.Range(0, NumKnots).Select(x => new Coordinate(0, 0)).ToList();
            Coordinate head = rope.First();
            HashSet<Coordinate> tailPath = new() { rope.Last() };

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                Match match = ParseLineRegex.Match(line);
                if (!match.Success)
                {
                    throw new InvalidDataException();
                }

                string direction = match.Groups["DIRECTION"].Value;
                if (!int.TryParse(match.Groups["DISTANCE"].Value, out int distance))
                {
                    throw new InvalidDataException();
                }

                for (int i = 0; i < distance; i++)
                {
                    if (HorizontalMovements.ContainsKey(direction))
                    {
                        head.X += HorizontalMovements[direction];
                    }
                    else if (VerticalMovements.ContainsKey(direction))
                    {
                        head.Y += VerticalMovements[direction];
                    }

                    for (int knotIndex = 0; knotIndex < rope.Count - 1; knotIndex++)
                    {
                        Coordinate knot1 = rope[knotIndex];
                        Coordinate knot2 = rope[knotIndex + 1];
                        if (knot1 == knot2 || knot2.IsAdjacentTo(knot1))
                        {
                            // Knot is close enough.
                            break;
                        }

                        int distanceX = knot1.X - knot2.X;
                        int distanceY = knot1.Y - knot2.Y;

                        int adjustX = distanceX > 0 ? 1 : -1;
                        int adjustY = distanceY > 0 ? 1 : -1;

                        if (distanceX == 0)
                        {
                            // Knot is in same column.
                            knot2.Y += distanceY - adjustY;
                            continue;
                        }

                        if (distanceY == 0)
                        {
                            // Knot is in same row.
                            knot2.X += distanceX - adjustX;
                            continue;
                        }

                        // Knots are not in the same row or column.
                        knot2.X += adjustX;
                        knot2.Y += adjustY;
                    }

                    tailPath.Add(rope.Last());
                }
            }

            return tailPath.Count.ToString();
        }
    }
}
