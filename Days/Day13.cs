//-----------------------------------------------------------------------
// <copyright file="Day13.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Days
{
    using AdventOfCode2022.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day13 : IDay
    {
        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day13.txt");
            string? line;

            string[] initial = new[] { "[[2]]", "[[6]]" };
            List<PacketData> packets = initial.Select(p => new PacketData(p)).ToList();

            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                packets.Add(new(line));
            }

            packets.Sort(new PacketDataComparer());

            int found = 0;
            int product = 1;
            for (int i = 0; i < packets.Count; i++)
            {
                PacketData packet = packets[i];
                if (initial.Contains(packet.Raw))
                {
                    product *= i + 1;
                    found++;
                }

                if (found == 2)
                {
                    break;
                }
            }

            return product.ToString();
        }
    }
}
