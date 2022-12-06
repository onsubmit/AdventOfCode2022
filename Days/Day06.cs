//-----------------------------------------------------------------------
// <copyright file="Day06.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Days
{
    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day06 : IDay
    {
        private const int MessageStartIndicatorLength = 14;

        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day06.txt");

            string? line = sr.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                throw new InvalidDataException();
            }

            int messageStartIndex = -1;
            for (int i = 0; i < line.Length - MessageStartIndicatorLength; i++)
            {
                string marker = line.Substring(i, MessageStartIndicatorLength);
                bool isMessageStart = marker.Distinct().Count() == MessageStartIndicatorLength;
                if (isMessageStart)
                {
                    messageStartIndex = i + MessageStartIndicatorLength;
                    break;
                }
            }

            if (messageStartIndex < 0)
            {
                throw new InvalidOperationException();
            }

            return messageStartIndex.ToString();
        }
    }
}
