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

            Coordinate head = new(0, 0);
            Coordinate tail = new(0, 0);
            HashSet<Coordinate> tailPath = new() { tail };

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

                    if (head == tail || tail.IsAdjacentTo(head))
                    {
                        // Tail is close enough.
                        continue;
                    }

                    int distanceX = head.X - tail.X;
                    int distanceY = head.Y - tail.Y;

                    int adjustX = distanceX > 0 ? 1 : -1;
                    int adjustY = distanceY > 0 ? 1 : -1;

                    if (distanceX == 0)
                    {
                        // Tail is in same column.
                        tail.Y += distanceY - adjustY;
                        tailPath.Add(new Coordinate(tail));
                        continue;
                    }

                    if (distanceY == 0)
                    {
                        // Tail is in same row.
                        tail.X += distanceX - adjustX;
                        tailPath.Add(new Coordinate(tail));
                        continue;
                    }

                    // Head and tail are not in the same row or column.
                    tail.X += adjustX;
                    tail.Y += adjustY;
                    tailPath.Add(new Coordinate(tail));
                }
            }

            return tailPath.Count.ToString();
        }
    }
}
