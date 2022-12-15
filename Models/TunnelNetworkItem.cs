//-----------------------------------------------------------------------
// <copyright file="TunnelNetworkItem.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Models
{
    /// <summary>
    ///  Represents an item in a tunnel network.
    /// </summary>
    internal enum TunnelNetworkItem
    {
        /// <summary>
        /// Unknown what occupies the space, but it's not a beacon.
        /// </summary>
        UnknownButNotABeacon,

        /// <summary>
        /// A sensor occupies the space.
        /// </summary>
        Sensor,

        /// <summary>
        /// A beacon occupies the space.
        /// </summary>
        Beacon,
    }
}
