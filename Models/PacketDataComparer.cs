//-----------------------------------------------------------------------
// <copyright file="PacketDataComparer.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Models
{
    /// <summary>
    /// Compares two <see cref="PacketData"/> instances.
    /// </summary>
    internal static class PacketDataComparer
    {
        /// <summary>
        /// Compares two <see cref="PacketData"/> instances.
        /// </summary>
        /// <param name="a">1st instance.</param>
        /// <param name="b">2nd instance.</param>
        /// <returns>-1 if the 1st precedes the 2nd. 0 if they occur in the same position in the sort order. 1 if the 1st follows the 2nd.</returns>
        public static int Compare(PacketData a, PacketData b)
        {
            if (a == null || b == null)
            {
                return 0;
            }

            PacketDataNumber? numberA = a as PacketDataNumber;
            PacketDataNumber? numberB = b as PacketDataNumber;

            if (numberA != null && numberB != null)
            {
                // If both values are integers, the lower integer should come first.
                return numberA.Value.CompareTo(numberB.Value);
            }

            if (numberA != null)
            {
                // First valueis a number.
                return Compare(new() { numberA }, b.Values);
            }

            if (numberB != null)
            {
                // Second value is a number.
                return Compare(a.Values, new() { numberB });
            }

            // Both packets are lists.
            return Compare(a.Values, b.Values);
        }

        private static int Compare(List<PacketData> a, List<PacketData> b)
        {
            for (int i = 0; i < a.Count; i++)
            {
                if (b.Count == i)
                {
                    // If the right list runs out of items first, the inputs are not in the right order.
                    return 1;
                }

                int compare = Compare(a[i], b[i]);
                if (compare != 0)
                {
                    return compare;
                }
            }

            if (a.Count < b.Count)
            {
                // If the left list runs out of items first, the inputs are in the right order.
                return -1;
            }

            // If the lists are the same length and no comparison makes a decision about the order, continue checking the next part of the input.
            return 0;
        }
    }
}
