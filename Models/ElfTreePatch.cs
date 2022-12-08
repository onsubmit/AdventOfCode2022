//-----------------------------------------------------------------------
// <copyright file="ElfTreePatch.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Models
{
    /// <summary>
    /// Represts a patch of trees.
    /// </summary>
    internal class ElfTreePatch
    {
        private readonly int[][] trees;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElfTreePatch"/> class.
        /// </summary>
        /// <param name="lines">Lines from input file.</param>
        public ElfTreePatch(string[] lines)
        {
            this.trees = lines.Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
        }

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        public int Rows => this.trees.Length;

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        public int Columns => this.trees[0]?.Length ?? 0;

        /// <summary>
        /// Gets a value indicating how many trees are visible.
        /// </summary>
        public int NumTreesVisible
        {
            get
            {
                int num = 0;
                for (int i = 0; i < this.Rows; i++)
                {
                    for (int j = 0; j < this.Columns; j++)
                    {
                        if (this.IsTreeVisible(i, j))
                        {
                            num++;
                        }
                    }
                }

                return num;
            }
        }

        private bool IsTreeVisible(int row, int column)
        {
            int height = this.trees[row][column];

            if (row == 0
                || column == 0
                || row == this.Rows - 1
                || column == this.Columns - 1)
            {
                return true;
            }

            List<int> heightsFromLeft = new();
            List<int> heightsFromRight = new();
            for (int c = 0; c < this.Columns; c++)
            {
                if (c < column)
                {
                    heightsFromLeft.Add(this.trees[row][c]);
                }

                if (c > column)
                {
                    heightsFromRight.Add(this.trees[row][c]);
                }
            }

            if (height > heightsFromLeft.Max() || height > heightsFromRight.Max())
            {
                return true;
            }

            List<int> heightsFromTop = new();
            List<int> heightsFromBottom = new();
            for (int r = 0; r < this.Rows; r++)
            {
                if (r < row)
                {
                    heightsFromTop.Add(this.trees[r][column]);
                }

                if (r > row)
                {
                    heightsFromBottom.Add(this.trees[r][column]);
                }
            }

            if (height > heightsFromTop.Max() || height > heightsFromBottom.Max())
            {
                return true;
            }

            return false;
        }
    }
}
