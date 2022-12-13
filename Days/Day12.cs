//-----------------------------------------------------------------------
// <copyright file="Day12.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Days
{
    using AdventOfCode2022.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day12 : IDay
    {
        private static readonly Dictionary<Coordinate, int> TotalDistances = new();
        private static readonly Dictionary<Coordinate, Coordinate> Previous = new();
        private static readonly PriorityQueue<Coordinate, int> Queue = new();

        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            HeightMap heightMap = new(File.ReadAllLines("input\\Day12.txt"));

            Setup(heightMap);
            ProcessQueue(heightMap);

            List<Coordinate> path = new();
            for (Coordinate target = heightMap.End; Previous.ContainsKey(target); target = Previous[target])
            {
                path.Add(target);
            }

            return path.Count.ToString();
        }

        private static void Setup(HeightMap heightMap)
        {
            for (int r = 0; r < heightMap.Rows; r++)
            {
                for (int c = 0; c < heightMap.Columns; c++)
                {
                    Coordinate coordinate = new(r, c);
                    int distance = coordinate == heightMap.Start ? 0 : int.MaxValue;
                    TotalDistances[coordinate] = distance;
                    Queue.Enqueue(coordinate, distance);
                }
            }
        }

        private static void ProcessQueue(HeightMap heightMap)
        {
            Coordinate min = new(0, 0);
            Coordinate max = new(heightMap.Rows - 1, heightMap.Columns - 1);

            while (Queue.TryDequeue(out Coordinate? coordinate, out int _) && coordinate != heightMap.End)
            {
                foreach (Coordinate direction in Directions.Cardinal)
                {
                    Coordinate neighbor = coordinate + direction;

                    if (!neighbor.IsInRange(min, max))
                    {
                        // Out of bounds.
                        continue;
                    }

                    if (heightMap[neighbor] > heightMap[coordinate] + 1)
                    {
                        // To avoid needing to get out your climbing gear,
                        // the elevation of the destination square can be
                        // at most one higher than the elevation of your current square.
                        continue;
                    }

                    int distance = TotalDistances[coordinate] + 1;
                    if (distance < TotalDistances[neighbor])
                    {
                        TotalDistances[neighbor] = distance;
                        Previous[neighbor] = coordinate;
                        Queue.Enqueue(neighbor, distance);
                    }
                }
            }
        }
    }
}
