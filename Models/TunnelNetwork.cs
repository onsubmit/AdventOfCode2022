//-----------------------------------------------------------------------
// <copyright file="TunnelNetwork.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Models
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents a tunnel network.
    /// </summary>
    internal class TunnelNetwork
    {
        private static readonly Regex ParseLineRegex = new(@"^Sensor at x=(?<SENSORX>-?\d+), y=(?<SENSORY>-?\d+): closest beacon is at x=(?<BEACONX>-?\d+), y=(?<BEACONY>-?\d+)$");

        private readonly Dictionary<Coordinate, TunnelNetworkItem> network = new();

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

                this.network[sensor] = TunnelNetworkItem.Sensor;
                this.network[beacon] = TunnelNetworkItem.Beacon;
            }
        }
    }
}
