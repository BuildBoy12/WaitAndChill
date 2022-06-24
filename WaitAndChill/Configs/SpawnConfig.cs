// -----------------------------------------------------------------------
// <copyright file="SpawnConfig.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WaitAndChill.Configs
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Enums;
    using UnityEngine;

    /// <summary>
    /// Represents configs that deal with the available spawns for players in the lobby.
    /// </summary>
    public class SpawnConfig
    {
        /// <summary>
        /// Gets or sets the custom room names that are considered as valid spawn positions.
        /// </summary>
        [Description("The custom room names that are considered as valid spawn positions.")]
        public List<string> CustomRooms { get; set; } = new()
        {
            "Tower1",
            "Tower2",
            "Tower3",
            "Tower4",
        };

        /// <summary>
        /// Gets or sets the rooms that are considered as valid spawn positions.
        /// </summary>
        [Description("The rooms that are considered as valid spawn positions.")]
        public List<RoomType> Rooms { get; set; } = new()
        {
            RoomType.EzGateA,
            RoomType.EzGateB,
        };

        /// <summary>
        /// Gets or sets the roles that are considered as valid spawn positions.
        /// </summary>
        [Description("The roles that are considered as valid spawn positions.")]
        public List<RoleType> RoleRooms { get; set; } = new()
        {
            RoleType.Scp049,
            RoleType.Scp106,
            RoleType.Scp173,
            RoleType.Scp93953,
        };

        /// <summary>
        /// Gets or sets the manual spawn positions.
        /// </summary>
        [Description("The manual spawn positions.")]
        public List<Vector3> ManualPositions { get; set; } = null;
    }
}