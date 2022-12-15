//-----------------------------------------------------------------------
// <copyright file="TunnelNetwork.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Models
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents a tunnel network.
    /// </summary>
    internal class TunnelNetwork
    {
        private const int Row = 2000000;
        private static readonly Regex ParseLineRegex = new(@"^Sensor at x=(?<SENSORX>-?\d+), y=(?<SENSORY>-?\d+): closest beacon is at x=(?<BEACONX>-?\d+), y=(?<BEACONY>-?\d+)$");

        private readonly Dictionary<Coordinate, Coordinate> sensorBeaconMap = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="TunnelNetwork"/> class.
        /// </summary>
        /// <param name="lines">Lines from input file.</param>
        public TunnelNetwork(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                Match match = ParseLineRegex.Match(lines[i]);
                if (!match.Success
                    || !int.TryParse(match.Groups["SENSORX"].Value, out int sensorX)
                    || !int.TryParse(match.Groups["SENSORY"].Value, out int sensorY)
                    || !int.TryParse(match.Groups["BEACONX"].Value, out int beaconX)
                    || !int.TryParse(match.Groups["BEACONY"].Value, out int beaconY))
                {
                    throw new InvalidDataException();
                }

                Coordinate sensor = new(sensorX, sensorY);
                Coordinate beacon = new(beaconX, beaconY);

                this.sensorBeaconMap[sensor] = beacon;
            }
        }

        /// <summary>
        /// Gets the number of coordinates that cannot be a beacon for the specific row.
        /// </summary>
        /// <returns>The number of coordinates that cannot be a beacon for the specific row.</returns>
        public int GetNumCoordinatesThatCannotBeABeacon()
        {
            int knownRangeMin = int.MaxValue;
            int knownRangeMax = int.MinValue;
            foreach (Coordinate sensor in this.sensorBeaconMap.Keys)
            {
                Coordinate beacon = this.sensorBeaconMap[sensor];
                int distance = sensor.GetManhattanDistanceTo(beacon);
                int verticalDistanceToRow = Math.Abs(Row - sensor.Y);
                int remainingDistance = distance - verticalDistanceToRow;

                int start = sensor.X - remainingDistance;
                int end = sensor.X + remainingDistance;

                knownRangeMin = Math.Min(knownRangeMin, start);
                knownRangeMax = Math.Max(knownRangeMax, end);
            }

            return knownRangeMax - knownRangeMin;
        }
    }
}