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
        private static readonly Regex ParseLineRegex = new(@"^Sensor at x=(?<SENSORX>-?\d+), y=(?<SENSORY>-?\d+): closest beacon is at x=(?<BEACONX>-?\d+), y=(?<BEACONY>-?\d+)$");

        private readonly Dictionary<Coordinate, Coordinate> sensorBeaconMap = new();
        private readonly Dictionary<Coordinate, int> sensorBeaconDistances = new();

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
                this.sensorBeaconDistances[sensor] = sensor.GetManhattanDistanceTo(beacon);
            }
        }

        /// <summary>
        /// Gets the distinct distress beacon.
        /// </summary>
        /// <returns>The distress beacon.</returns>
        /// <exception cref="InvalidOperationException">Thrown if if the distress beacon cannot be identified.</exception>
        public Coordinate GetDistressBeacon()
        {
            Coordinate min = new(0, 0);
            Coordinate max = new(4000000, 4000000);

            foreach (Coordinate sensor in this.sensorBeaconMap.Keys)
            {
                Coordinate beacon = this.sensorBeaconMap[sensor];
                int distance = this.sensorBeaconDistances[sensor];
                IEnumerable<Coordinate> border = GetBorder(sensor, distance, min, max);
                foreach (Coordinate b in border)
                {
                    if (b == beacon)
                    {
                        continue;
                    }

                    bool foundCandidate = true;
                    foreach (Coordinate otherSensor in this.sensorBeaconMap.Keys)
                    {
                        if (otherSensor == sensor)
                        {
                            continue;
                        }

                        if (b.GetManhattanDistanceTo(otherSensor) <= this.sensorBeaconDistances[otherSensor])
                        {
                            // Potential coordinate is already covered by another sensor.
                            foundCandidate = false;
                            break;
                        }
                    }

                    if (foundCandidate)
                    {
                        return b;
                    }
                }
            }

            throw new InvalidOperationException();
        }

        private static IEnumerable<Coordinate> GetBorder(Coordinate c, int radius, Coordinate min, Coordinate max)
        {
            int r = radius + 1;

            for (int i = 0; i <= r; i++)
            {
                HashSet<Coordinate> coordinates = new()
                {
                    new(c.X + i, c.Y + r - i),
                    new(c.X + i, c.Y - r + i),
                    new(c.X - i, c.Y + r - i),
                    new(c.X - i, c.Y - r + i),
                };

                foreach (Coordinate b in coordinates)
                {
                    if (!b.IsInRange(min, max))
                    {
                        continue;
                    }

                    yield return b;
                }
            }
        }
    }
}