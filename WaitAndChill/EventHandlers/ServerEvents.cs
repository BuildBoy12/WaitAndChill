// -----------------------------------------------------------------------
// <copyright file="ServerEvents.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WaitAndChill.EventHandlers
{
    using Exiled.API.Features;
    using UnityEngine;
    using WaitAndChill.API;
    using WaitAndChill.Models;
    using ServerHandlers = Exiled.Events.Handlers.Server;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers.Server"/>.
    /// </summary>
    public class ServerEvents : Subscribable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerEvents"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="WaitAndChill.Plugin"/> class.</param>
        public ServerEvents(Plugin plugin)
            : base(plugin)
        {
        }

        /// <inheritdoc/>
        public override void Subscribe()
        {
            ServerHandlers.ReloadedConfigs += OnReloadedConfigs;
            ServerHandlers.RoundStarted += OnRoundStarted;
            ServerHandlers.WaitingForPlayers += OnWaitingForPlayers;
        }

        /// <inheritdoc />
        public override void Unsubscribe()
        {
            ServerHandlers.ReloadedConfigs -= OnReloadedConfigs;
            ServerHandlers.RoundStarted -= OnRoundStarted;
            ServerHandlers.WaitingForPlayers -= OnWaitingForPlayers;
        }

        private void OnReloadedConfigs()
        {
            SpawnPositions.Regenerate();
        }

        private void OnRoundStarted()
        {
            foreach (Player player in Player.List)
            {
                player.ClearInventory();
                player.Role.Type = RoleType.Spectator;
            }
        }

        private void OnWaitingForPlayers()
        {
            GameObject.Find("StartRound").transform.localScale = Vector3.zero;
            Plugin.CurrentSpawnPosition = SpawnPositions.Positions[Exiled.Loader.Loader.Random.Next(0, SpawnPositions.Positions.Count)];
        }
    }
}