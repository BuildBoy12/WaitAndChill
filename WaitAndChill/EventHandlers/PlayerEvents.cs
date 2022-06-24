// -----------------------------------------------------------------------
// <copyright file="PlayerEvents.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using Exiled.API.Features.Items;

namespace WaitAndChill.EventHandlers
{
    using System;
    using Exiled.API.Extensions;
    using Exiled.API.Features;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs;
    using WaitAndChill.Models;
    using PlayerHandlers = Exiled.Events.Handlers.Player;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers.Player"/>.
    /// </summary>
    public class PlayerEvents : Subscribable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerEvents"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="WaitAndChill.Plugin"/> class.</param>
        public PlayerEvents(Plugin plugin)
            : base(plugin)
        {
        }

        /// <inheritdoc />
        public override void Subscribe()
        {
            PlayerHandlers.SearchingPickup += OnSearchingPickup;
            PlayerHandlers.IntercomSpeaking += OnIntercomSpeaking;
            PlayerHandlers.SpawningRagdoll += OnSpawningRagdoll;
            PlayerHandlers.DroppingItem += OnDroppingItem;
            PlayerHandlers.DroppingAmmo += OnDroppingAmmo;
            PlayerHandlers.InteractingDoor += OnInteractingDoor;
            PlayerHandlers.InteractingElevator += OnInteractingElevator;
            PlayerHandlers.InteractingLocker += OnInteractingLocker;
            PlayerHandlers.Dying += OnDying;
            PlayerHandlers.Spawned += OnSpawned;
        }

        /// <inheritdoc />
        public override void Unsubscribe()
        {
            PlayerHandlers.SearchingPickup -= OnSearchingPickup;
            PlayerHandlers.IntercomSpeaking -= OnIntercomSpeaking;
            PlayerHandlers.SpawningRagdoll -= OnSpawningRagdoll;
            PlayerHandlers.DroppingItem -= OnDroppingItem;
            PlayerHandlers.DroppingAmmo -= OnDroppingAmmo;
            PlayerHandlers.InteractingDoor -= OnInteractingDoor;
            PlayerHandlers.InteractingElevator -= OnInteractingElevator;
            PlayerHandlers.InteractingLocker -= OnInteractingLocker;
            PlayerHandlers.Dying -= OnDying;
            PlayerHandlers.Spawned -= OnSpawned;
        }

        private void OnSearchingPickup(SearchingPickupEventArgs ev)
        {
            if (Round.IsLobby)
                ev.IsAllowed = false;
        }

        private void OnIntercomSpeaking(IntercomSpeakingEventArgs ev)
        {
            if (Round.IsLobby)
                ev.IsAllowed = false;
        }

        private void OnSpawningRagdoll(SpawningRagdollEventArgs ev)
        {
            if (Round.IsLobby)
                ev.IsAllowed = false;
        }

        private void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (Round.IsLobby)
                ev.IsAllowed = false;
        }

        private void OnDroppingAmmo(DroppingAmmoEventArgs ev)
        {
            if (Round.IsLobby)
                ev.IsAllowed = false;
        }

        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (Round.IsLobby)
                ev.IsAllowed = false;
        }

        private void OnInteractingElevator(InteractingElevatorEventArgs ev)
        {
            if (Round.IsLobby)
                ev.IsAllowed = false;
        }

        private void OnInteractingLocker(InteractingLockerEventArgs ev)
        {
            if (Round.IsLobby)
                ev.IsAllowed = false;
        }

        private void OnDying(DyingEventArgs ev)
        {
            if (Round.IsLobby)
                ev.Target.ClearInventory();
        }

        private void OnSpawned(SpawnedEventArgs ev)
        {
            if (!Round.IsLobby)
                return;

            foreach (ConfiguredEffect effect in Plugin.Config.Effects)
                effect.Apply(ev.Player);

            foreach (string item in Plugin.Config.Inventory)
                AddItem(ev.Player, item);

            ev.Player.Position = Plugin.CurrentSpawnPosition;
        }

        private void AddItem(Player player, string itemName)
        {
            if (CustomItem.TryGet(itemName, out CustomItem customItem))
            {
                customItem.Give(player);
                return;
            }

            if (Enum.TryParse(itemName, true, out ItemType type))
            {
                if (type.IsAmmo())
                    player.Ammo[type] = 100;
                else
                    player.AddItem(type);

                return;
            }

            Log.Warn($"{nameof(AddItem)}: {itemName} is not a valid ItemType or Custom Item name.");
        }
    }
}