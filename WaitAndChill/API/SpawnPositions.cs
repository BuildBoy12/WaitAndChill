// -----------------------------------------------------------------------
// <copyright file="SpawnPositions.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WaitAndChill.API
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Exiled.API.Enums;
    using Exiled.API.Extensions;
    using Exiled.API.Features;
    using UnityEngine;

    /// <summary>
    /// Handles the generation of spawn positions available for players in the lobby.
    /// </summary>
    public static class SpawnPositions
    {
        private static readonly List<Vector3> SpawnPositionsValue = new();

        private static readonly Dictionary<string, Vector3> CustomPositions = new(StringComparer.OrdinalIgnoreCase)
        {
            { "TOWER1", new Vector3(54.81f, 1019.41f, -44.906f) },
            { "TOWER2", new Vector3(148.6951f, 1019.447f, -19.06371f) },
            { "TOWER3", new Vector3(223.1443f, 1026.775f, -18.15129f) },
            { "TOWER4", new Vector3(-21.81f, 1019.89f, -43.45f) },
        };

        /// <summary>
        /// Gets all available spawn positions.
        /// </summary>
        public static ReadOnlyCollection<Vector3> Positions => SpawnPositionsValue.AsReadOnly();

        /// <summary>
        /// Regenerates the collection of positions. Uses the values of <see cref="CustomPositions"/> as a fallback if no positions are found in the config.
        /// </summary>
        public static void Regenerate()
        {
            SpawnPositionsValue.Clear();
            if (Plugin.Instance.Config.SpawnablePositions is null)
            {
                SpawnPositionsValue.AddRange(CustomPositions.Values);
                return;
            }

            RegenerateCustomPositions();
            RegenerateRoomPositions();
            RegenerateRolePositions();
            RegenerateManualPositions();

            if (SpawnPositionsValue.Count == 0)
                SpawnPositionsValue.AddRange(CustomPositions.Values);
        }

        private static void RegenerateCustomPositions()
        {
            if (Plugin.Instance.Config.SpawnablePositions.CustomRooms is not List<string> customPositions)
                return;

            foreach (string name in customPositions)
            {
                if (CustomPositions.TryGetValue(name, out Vector3 position))
                    SpawnPositionsValue.Add(position);
            }
        }

        private static void RegenerateRoomPositions()
        {
            if (Plugin.Instance.Config.SpawnablePositions.Rooms is not List<RoomType> roomTypes)
                return;

            foreach (RoomType roomType in roomTypes)
            {
                Room room = Room.Get(roomType);
                if (room is null)
                    continue;

                Vector3 roomPosition = room.Position;
                SpawnPositionsValue.Add(new Vector3(roomPosition.x, roomPosition.y + 2f, roomPosition.z));
            }
        }

        private static void RegenerateRolePositions()
        {
            if (Plugin.Instance.Config.SpawnablePositions.RoleRooms is not List<RoleType> roleTypes)
                return;

            foreach (RoleType roleType in roleTypes)
                SpawnPositionsValue.Add(roleType.GetRandomSpawnProperties().Item1);
        }

        private static void RegenerateManualPositions()
        {
            if (Plugin.Instance.Config.SpawnablePositions.ManualPositions is List<Vector3> positions)
                SpawnPositionsValue.AddRange(positions);
        }
    }
}