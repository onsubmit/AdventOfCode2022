//-----------------------------------------------------------------------
// <copyright file="Day05.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Days
{
    using System.Text.RegularExpressions;
    using AdventOfCode2022.Extensions;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day05 : IDay
    {
        private const int CrateStringWidth = 4;

        private static readonly Regex ParseLineColumnsRegex = new(@"\[[A-Z]\]", RegexOptions.Compiled);
        private static readonly Regex ParseLineColumFootersRegex = new(@"^(\s+?\d+\s+?)+$", RegexOptions.Compiled);
        private static readonly Regex ParseLineMoveRegex = new(@"^move (?<AMOUNT>\d+) from (?<COLUMN_START>\d+) to (?<COLUMN_END>\d+)$", RegexOptions.Compiled);

        private readonly Dictionary<Regex, Action<Match, string>> actionMap;

        private int? lineLength = null;
        private int? numColumns = null;
        private List<Stack<char>> columns = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Day05"/> class.
        /// </summary>
        public Day05()
        {
            this.actionMap = new()
            {
                {
                    // Line containing the column footers e.g. " 1   2   3 ".
                    ParseLineColumFootersRegex,
                    this.ReverseStacks
                },
                {
                    // Line containing crates e.g. "[Z] [M] [P]"
                    ParseLineColumnsRegex,
                    this.BuildStacks
                },
                {
                    // Line containing rearrangement procedure e.g. "move 3 from 1 to 3"
                    ParseLineMoveRegex,
                    this.RearrangeStacks
                },
            };
        }

        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day05.txt");

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    // Blank line between inputs
                    continue;
                }

                bool lineParsed = false;
                foreach (var kvp in this.actionMap)
                {
                    Match match = kvp.Key.Match(line);
                    if (match.Success)
                    {
                        kvp.Value(match, line);
                        lineParsed = true;
                        break;
                    }
                }

                if (!lineParsed)
                {
                    throw new InvalidDataException();
                }
            }

            return string.Join(string.Empty, this.columns.Select(c => c.Pop()));
        }

        private void ReverseStacks(Match match, string line)
        {
            // Reverse the stacks.
            for (int i = 0; i < this.columns.Count; i++)
            {
                this.columns[i] = this.columns[i].ReverseStack();
            }
        }

        private void BuildStacks(Match match, string line)
        {
            if (!this.lineLength.HasValue)
            {
                // Initialize data format.
                this.lineLength = line.Length;
                if ((this.lineLength + 1) % CrateStringWidth != 0)
                {
                    throw new InvalidDataException($"Line length must be a multiple of {CrateStringWidth}.");
                }

                this.numColumns = (this.lineLength + 1) / CrateStringWidth;
                this.columns = new(this.numColumns.Value);
                for (int i = 0; i < this.numColumns; i++)
                {
                    this.columns.Add(new());
                }
            }
            else if (line.Length != this.lineLength)
            {
                throw new InvalidDataException("All lines must be the same length.");
            }

            // Find all crates in the line.
            int index = line.IndexOf('[');
            while (index >= 0)
            {
                int column = index / CrateStringWidth;
                this.columns[column].Push(line[index + 1]);
                index = line.IndexOf('[', index + 1);
            }
        }

        private void RearrangeStacks(Match match, string line)
        {
            if (!int.TryParse(match.Groups["AMOUNT"].Value, out int amount)
                || !int.TryParse(match.Groups["COLUMN_START"].Value, out int columnStart)
                || !int.TryParse(match.Groups["COLUMN_END"].Value, out int columnEnd)
                || amount < 0
                || columnStart <= 0
                || columnEnd <= 0
                || columnStart == columnEnd)
            {
                throw new InvalidDataException();
            }

            // Move 'amount' from start column to end column.
            List<char> cratesToMove = new(amount);
            while (amount-- > 0)
            {
                char value = this.columns[columnStart - 1].Pop();
                cratesToMove.Add(value);
            }

            for (int i = cratesToMove.Count - 1; i >= 0; i--)
            {
                this.columns[columnEnd - 1].Push(cratesToMove[i]);
            }
        }
    }
}
