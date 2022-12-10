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
        private const int ScreenRows = 6;
        private const int ScreenColumns = 40;
        private readonly char[][] screen = Enumerable.Range(0, ScreenRows).Select(i => Enumerable.Range(0, ScreenColumns).Select(i => '.').ToArray()).ToArray();

        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day10.txt");

            int x = 1;
            int row = 0;
            int column = 0;

            void Draw()
            {
                if (Math.Abs(x - column) <= 1)
                {
                    this.screen[row][column] = '#';
                }

                column++;
                if (column % ScreenColumns == 0)
                {
                    column = 0;
                    row++;
                }
            }

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                Draw();
                if (line == "noop")
                {
                    continue;
                }

                if (line.Contains("addx"))
                {
                    if (!int.TryParse(line[5..], out int value))
                    {
                        throw new InvalidDataException();
                    }

                    Draw();
                    x += value;
                    continue;
                }

                throw new InvalidDataException();
            }

            string screenContents = string.Join(string.Empty, this.screen.Select(l => $"{string.Join(string.Empty, l)}{Environment.NewLine}"));
            return $"{Environment.NewLine}{screenContents}";
        }
    }
}
