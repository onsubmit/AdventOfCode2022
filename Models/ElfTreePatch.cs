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
        /// Gets how many trees are visible.
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

        /// <summary>
        /// Gets the highest scenic score.
        /// </summary>
        public int HighestScenicScore
        {
            get
            {
                int highestScenicScore = int.MinValue;
                for (int i = 0; i < this.Rows; i++)
                {
                    for (int j = 0; j < this.Columns; j++)
                    {
                        highestScenicScore = Math.Max(highestScenicScore, this.GetScenicScore(i, j));
                    }
                }

                return highestScenicScore;
            }
        }

        private bool IsTreeVisible(int row, int column)
        {
            if (row == 0
                || column == 0
                || row == this.Rows - 1
                || column == this.Columns - 1)
            {
                // Edge
                return true;
            }

            int height = this.trees[row][column];

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

        private int GetScenicScore(int row, int column)
        {
            if (row == 0
                || column == 0
                || row == this.Rows - 1
                || column == this.Columns - 1)
            {
                // Edge
                return 0;
            }

            int height = this.trees[row][column];

            int lookUpCount = 0;
            int lookDownCount = 0;
            int lookLeftCount = 0;
            int lookRightCount = 0;
            for (int r = row - 1; r >= 0; r--)
            {
                lookUpCount++;
                if (this.trees[r][column] >= height)
                {
                    break;
                }
            }

            for (int r = row + 1; r < this.Rows; r++)
            {
                lookDownCount++;
                if (this.trees[r][column] >= height)
                {
                    break;
                }
            }

            for (int c = column - 1; c >= 0; c--)
            {
                lookLeftCount++;
                if (this.trees[row][c] >= height)
                {
                    break;
                }
            }

            for (int c = column + 1; c < this.Columns; c++)
            {
                lookRightCount++;
                if (this.trees[row][c] >= height)
                {
                    break;
                }
            }

            return lookUpCount * lookDownCount * lookLeftCount * lookRightCount;
        }
    }
}
