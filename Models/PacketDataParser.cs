//-----------------------------------------------------------------------
// <copyright file="PacketDataParser.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Models
{
    /// <summary>
    /// A parser for Packet numbers.
    /// </summary>
    internal static class PacketDataParser
    {
        /// <summary>
        /// Parses packet data.
        /// </summary>
        /// <param name="raw">The raw input.</param>
        /// <returns>The new packet data.</returns>
        public static PacketData Parse(string raw)
        {
            if (raw.StartsWith('[') && raw.EndsWith(']'))
            {
                return new PacketData(raw);
            }

            return new PacketDataNumber(raw);
        }
    }
}
