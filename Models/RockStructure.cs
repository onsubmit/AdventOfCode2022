//-----------------------------------------------------------------------
// <copyright file="RockStructure.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Models
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    /// <summary>
    /// Represents a rock structure.
    /// </summary>
    internal class RockStructure
    {
        private static readonly Dictionary<Material, string> MaterialStrings = new()
        {
            { Material.Empty, " " },
            { Material.Rock, "#" },
            { Material.Sand, "o" },
        };

        private readonly Material[][] materials;

        private readonly int numRows;
        private readonly int numCols;
        private readonly int maxX;

        /// <summary>
        /// Initializes a new instance of the <see cref="RockStructure"/> class.
        /// </summary>
        /// <param name="lines">Lines from input file.</param>
        public RockStructure(string[] lines)
        {
            Coordinate min = new(int.MaxValue, 0);
            Coordinate max = new(int.MinValue, int.MinValue);

            List<List<Coordinate>> paths = new();
            for (int i = 0; i < lines.Length; i++)
            {
                List<Coordinate> path = new();

                string[] split = lines[i].Split("->");
                foreach (string c in split)
                {
                    if (!Coordinate.TryParse(c.Trim(), out Coordinate? coordinate))
                    {
                        throw new InvalidDataException();
                    }

                    min.X = Math.Min(min.X, coordinate.X);
                    min.Y = Math.Min(min.Y, coordinate.Y);
                    max.X = Math.Max(max.X, coordinate.X);
                    max.Y = Math.Max(max.Y, coordinate.Y);

                    path.Add(coordinate);
                }

                paths.Add(path);
            }

            int hackySolutionToExtendWidthAmount = 200;
            min.X -= hackySolutionToExtendWidthAmount;
            max.X += hackySolutionToExtendWidthAmount;
            max.Y += 2;
            paths.Add(new() { new(min.X, max.Y), new(max.X, max.Y) });

            this.numRows = max.Y - min.Y + 1;
            this.numCols = max.X - min.X + 1;
            this.maxX = max.X;

            this.materials = Enumerable.Range(0, this.numRows).Select(i => Enumerable.Range(0, this.numCols).Select(i => Material.Empty).ToArray()).ToArray();

            this.SetRockPositions(paths);
        }

        private Material this[int row, int column]
        {
            get
            {
                return this.materials[row][this.MapColumn(column)];
            }

            set
            {
                this.materials[row][this.MapColumn(column)] = value;
            }
        }

        private Material this[Coordinate c]
        {
            get
            {
                return this[c.Y, c.X];
            }

            set
            {
                this[c.Y, c.X] = value;
            }
        }

        /// <summary>
        /// Places sand at the given coordinate.
        /// </summary>
        /// <param name="c">The coordinate.</param>
        /// <returns><c>true</c> if the sand finds a resting place, <c>false</c> otherwise.</returns>
        public bool PlaceSand(Coordinate c)
        {
            while (true)
            {
                if (this[c] == Material.Sand)
                {
                    return false;
                }

                (bool canFallFurther, Coordinate nextCoordinate) = this.CanFallFurther(c);

                if (!canFallFurther)
                {
                    // Sand came to a resting point.
                    this[c] = Material.Sand;
                    return true;
                }

                c = nextCoordinate;
            }
        }

        /// <summary>
        /// Generates a string representation of the rock structure.
        /// </summary>
        /// <returns>A string representation of the rock structure.</returns>
        public override string ToString()
        {
            StringBuilder sb = new();
            for (int row = 0; row < this.numRows; row++)
            {
                sb.AppendLine(string.Join(string.Empty, this.materials[row].Select(m => MaterialStrings[m])));
            }

            return sb.ToString();
        }

        private void SetRockPositions(List<List<Coordinate>> paths)
        {
            foreach (List<Coordinate> path in paths)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Coordinate a = path[i];
                    Coordinate b = path[i + 1];

                    if (a.X == b.X)
                    {
                        // Vertical line.
                        int minRow = Math.Min(a.Y, b.Y);
                        int maxRow = Math.Max(a.Y, b.Y);
                        for (int row = minRow; row <= maxRow; row++)
                        {
                            this[row, a.X] = Material.Rock;
                        }

                        continue;
                    }

                    if (a.Y == b.Y)
                    {
                        // Horizontal line.
                        int minCol = Math.Min(a.X, b.X);
                        int maxCol = Math.Max(a.X, b.X);
                        for (int col = minCol; col <= maxCol; col++)
                        {
                            this[a.Y, col] = Material.Rock;
                        }

                        continue;
                    }

                    throw new InvalidDataException();
                }
            }
        }

        private (bool CanFallFurther, Coordinate Next) CanFallFurther(Coordinate coordinate)
        {
            Coordinate down = coordinate + new Coordinate(0, 1);
            Coordinate downLeft = coordinate + new Coordinate(-1, 1);
            Coordinate downRight = coordinate + new Coordinate(1, 1);

            foreach (Coordinate next in new[] { down, downLeft, downRight })
            {
                if (!this.IsWithinBounds(next))
                {
                    throw new InvalidOperationException();
                }

                if (this[next] == Material.Empty)
                {
                    return (CanFallFurther: true, Next: next);
                }
            }

            // Cannot fall further.
            return (CanFallFurther: false, Next: coordinate);
        }

        private bool IsWithinBounds(Coordinate c)
        {
            int row = c.Y;
            if (row < 0 || row >= this.numRows)
            {
                return false;
            }

            int column = this.MapColumn(c.X);
            if (column < 0 || column >= this.numCols)
            {
                return false;
            }

            return true;
        }

        private int MapColumn(int column)
        {
            return this.numCols - 1 - (this.maxX - column);
        }
    }
}
