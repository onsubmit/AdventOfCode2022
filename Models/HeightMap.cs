//-----------------------------------------------------------------------
// <copyright file="HeightMap.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Models
{
    /// <summary>
    /// Represents a height map.
    /// </summary>
    internal class HeightMap
    {
        private readonly int[][] heights;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeightMap"/> class.
        /// </summary>
        /// <param name="lines">Lines from input file.</param>
        public HeightMap(string[] lines)
        {
            this.heights = new int[lines.Length][];

            for (int r = 0; r < lines.Length; r++)
            {
                this.heights[r] = new int[lines[r].Length];

                for (int c = 0; c < lines[r].Length; c++)
                {
                    char value = lines[r][c];
                    if (value == 'S')
                    {
                        this.Start = new Coordinate(r, c);
                        value = 'a';
                    }
                    else if (value == 'E')
                    {
                        this.End = new Coordinate(r, c);
                        value = 'z';
                    }

                    this.heights[r][c] = value - 'a';
                }
            }
        }

        /// <summary>
        /// Gets the starting coordinate.
        /// </summary>
        public Coordinate Start { get; set; } = new(0, 0);

        /// <summary>
        /// Gets the ending coordinate.
        /// </summary>
        public Coordinate End { get; private set; } = new(0, 0);

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        public int Rows => this.heights.Length;

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        public int Columns => this.heights[0]?.Length ?? 0;

        /// <summary>
        /// Gets the value at the given coordinate.
        /// </summary>
        /// <param name="c">The coordinate.</param>
        /// <returns>The value at the given coordinate.</returns>
        public int this[Coordinate c] => this.heights[c.X][c.Y];
    }
}
