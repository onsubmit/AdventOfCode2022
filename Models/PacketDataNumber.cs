//-----------------------------------------------------------------------
// <copyright file="PacketDataNumber.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a regular Packet number.
    /// </summary>
    internal class PacketDataNumber : PacketData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PacketDataNumber"/> class.
        /// </summary>
        /// <param name="raw">The raw string from the input file.</param>
        public PacketDataNumber(string raw)
        {
            this.Value = int.Parse(raw);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketDataNumber"/> class.
        /// </summary>
        /// <param name="value">The value of the number.</param>
        public PacketDataNumber(int value = 0)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the number's value.
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        ///  Converts the string representation of a packet data number.
        /// </summary>
        /// <param name="raw">The raw input.</param>
        /// <param name="number">When this method returns, contains the converted packet data number associated; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the conversion was successful, <c>false</c> otherwise.</returns>
        public static bool TryParse(string raw, [NotNullWhen(true)] out PacketDataNumber? number)
        {
            number = null;

            if (int.TryParse(raw, out int _))
            {
                number = new PacketDataNumber(raw);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a string representation of the number.
        /// </summary>
        /// <returns>A string representation of the number.</returns>
        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
