﻿//-----------------------------------------------------------------------
// <copyright file="Coordinate.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Models
{
    /// <summary>
    /// Represents an x/y coordinate.
    /// </summary>
    internal struct Coordinate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> struct.
        /// </summary>
        /// <param name="x">x value.</param>
        /// <param name="y">y value.</param>
        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> struct.
        /// </summary>
        /// <param name="c">Coordinate to clone.</param>
        public Coordinate(Coordinate c)
        {
            this.X = c.X;
            this.Y = c.Y;
        }

        /// <summary>
        /// Gets or sets the x value.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y value.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Determines if two coordinates are equal.
        /// </summary>
        /// <param name="a">First coordinate.</param>
        /// <param name="b">Second coordinate.</param>
        /// <returns><c>true</c> if the coordinates are equal, <c>false</c> otherwise.</returns>
        public static bool operator ==(Coordinate a, Coordinate b) => a.Equals(b);

        /// <summary>
        /// Determines if two coordinates are not equal.
        /// </summary>
        /// <param name="a">First coordinate.</param>
        /// <param name="b">Second coordinate.</param>
        /// <returns><c>true</c> if the coordinates are not equal, <c>false</c> otherwise.</returns>
        public static bool operator !=(Coordinate a, Coordinate b) => !a.Equals(b);

        /// <summary>
        /// Determines if this coordinate is adjacent to the provided coordinate.
        /// </summary>
        /// <param name="c">The coordinate to check.</param>
        /// <returns><c>true</c> if this coordinate is adjacent to the provided coordinate, <c>false</c> otherwise.</returns>
        public bool IsAdjacentTo(Coordinate c)
        {
            if (this.X == c.X)
            {
                return Math.Abs(this.Y - c.Y) == 1;
            }

            if (this.Y == c.Y)
            {
                return Math.Abs(this.X - c.X) == 1;
            }

            return Math.Abs(this.X - c.X) == 1 && Math.Abs(this.Y - c.Y) == 1;
        }

        /// <summary>
        /// Determines if this coordinate is equal to the provided object.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns><c>true</c> if the object is an equal coordinate, <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not Coordinate)
            {
                return false;
            }

            Coordinate c = (Coordinate)obj;

            return this.X == c.X && this.Y == c.Y;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y);
        }

        /// <summary>
        /// Generates a string-representation of the coordinate.
        /// </summary>
        /// <returns>A string-representation of the coordinate.</returns>
        public override string ToString() => $"({this.X}, {this.Y})";
    }
}
