//-----------------------------------------------------------------------
// <copyright file="RockStructure.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Models
{
    /// <summary>
    /// Represents a rock structure.
    /// </summary>
    internal class RockStructure
    {
        private readonly Material[][] materials;

        /// <summary>
        /// Initializes a new instance of the <see cref="RockStructure"/> class.
        /// </summary>
        /// <param name="lines">Lines from input file.</param>
        public RockStructure(string[] lines)
        {
            Coordinate min = new(int.MaxValue, 0);
            Coordinate max = new(int.MinValue, int.MinValue);

            List<List<Coordinate>> rockPaths = new();
            for (int i = 0; i < lines.Length; i++)
            {
                string[] split = lines[i].Split("->");
                List<Coordinate> path = split.Select(c =>
                {
                    if (!Coordinate.TryParse(c.Trim(), out Coordinate? coordinate))
                    {
                        throw new InvalidDataException();
                    }

                    return coordinate;
                }).ToList();

                rockPaths.Add(path);

                int minX = path.MinBy(c => c.X)?.X ?? int.MaxValue;
                int minY = path.MinBy(c => c.Y)?.Y ?? int.MaxValue;
                int maxX = path.MaxBy(c => c.X)?.X ?? int.MinValue;
                int maxY = path.MaxBy(c => c.Y)?.Y ?? int.MinValue;

                min.X = Math.Min(min.X, minX);
                min.Y = Math.Min(min.Y, minY);
                max.X = Math.Max(max.X, maxX);
                max.Y = Math.Max(max.Y, maxY);
            }

            int numRows = max.Y - min.Y + 1;
            int numCols = max.X - min.X + 1;

            this.materials = Enumerable.Range(0, numRows).Select(i => Enumerable.Range(0, numCols).Select(i => Material.Empty).ToArray()).ToArray();

            foreach (List<Coordinate> path in rockPaths)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Coordinate a = path[i];
                    Coordinate b = path[i + 1];

                    if (a.X == b.X)
                    {
                        // Vertical
                        for (int row = a.Y; row <= b.Y; row++)
                        {
                            this.materials[row][numCols - 1 - (max.X - a.X)] = Material.Rock;
                        }
                    }
                    else if (a.Y == b.Y)
                    {
                        // Horizontal
                        int minCol = Math.Min(a.X, b.X);
                        int maxCol = Math.Max(a.X, b.X);
                        for (int col = minCol; col <= maxCol; col++)
                        {
                            this.materials[a.Y][numCols - 1 - (max.X - col)] = Material.Rock;
                        }
                    }
                    else
                    {
                        throw new InvalidDataException();
                    }
                }
            }
        }
    }
}
