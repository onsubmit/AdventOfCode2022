//-----------------------------------------------------------------------
// <copyright file="Day06.cs" company="Andy Young">
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
    internal class Day06 : IDay
    {
        private const int PacketStartIndicatorLength = 4;

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

            int packetStartIndex = -1;
            for (int i = 0; i < line.Length - PacketStartIndicatorLength; i++)
            {
                string marker = line.Substring(i, PacketStartIndicatorLength);
                bool isPacketStart = marker.Distinct().Count() == PacketStartIndicatorLength;
                if (isPacketStart)
                {
                    packetStartIndex = i + PacketStartIndicatorLength;
                    break;
                }
            }

            if (packetStartIndex < 0)
            {
                throw new InvalidOperationException();
            }

            return packetStartIndex.ToString();
        }
    }
}
