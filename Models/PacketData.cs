//-----------------------------------------------------------------------
// <copyright file="PacketData.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdventOfCode2022.Models
{
    /// <summary>
    /// Represents packet data.
    /// </summary>
    internal class PacketData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PacketData"/> class.
        /// </summary>
        public PacketData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketData"/> class.
        /// </summary>
        /// <param name="raw">The raw string from the input file.</param>
        public PacketData(string raw)
        {
            this.Raw = raw;

            string inner = raw.StartsWith('[') && raw.EndsWith(']') ? raw[1..^1] : raw;

            if (string.IsNullOrWhiteSpace(inner))
            {
                return;
            }

            if (!inner.Contains('['))
            {
                // All integers, no lists.
                this.Values.AddRange(inner.Split(',').Select(n => new PacketDataNumber(n)));
                return;
            }

            Stack<int> stack = new();
            int numberStart = 0;
            for (int i = 0; i < inner.Length; i++)
            {
                char c = inner[i];
                if (c == '[')
                {
                    stack.Push(i);
                    continue;
                }
                else if (c == ']')
                {
                    numberStart = stack.Pop();
                }

                if (!stack.Any())
                {
                    string? value = null;
                    if (i == inner.Length - 1)
                    {
                        value = inner[numberStart..];
                    }
                    else if (c == ',')
                    {
                        value = inner[numberStart..i];
                    }
                    else
                    {
                        continue;
                    }

                    this.Values.Add(PacketDataParser.Parse(value));
                    numberStart = i + 1;
                }
            }
        }

        /// <summary>
        /// Gets the raw input.
        /// </summary>
        public string? Raw { get; private set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        public List<PacketData> Values { get; set; } = new();

        /// <summary>
        /// Generates a string representation of the packet data.
        /// </summary>
        /// <returns>A string representation of the packet data.</returns>
        public override string ToString()
        {
            return $"[{string.Join(",", this.Values)}]";
        }
    }
}
