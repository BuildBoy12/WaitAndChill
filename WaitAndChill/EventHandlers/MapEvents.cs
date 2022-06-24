// -----------------------------------------------------------------------
// <copyright file="MapEvents.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WaitAndChill.EventHandlers
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using WaitAndChill.Models;
    using MapHandlers = Exiled.Events.Handlers.Map;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers.Map"/>.
    /// </summary>
    public class MapEvents : Subscribable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapEvents"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="WaitAndChill.Plugin"/> class.</param>
        public MapEvents(Plugin plugin)
            : base(plugin)
        {
        }

        /// <inheritdoc />
        public override void Subscribe()
        {
            MapHandlers.ChangingIntoGrenade += OnChangingIntoGrenade;
            MapHandlers.PlacingBlood += OnPlacingBlood;
        }

        /// <inheritdoc />
        public override void Unsubscribe()
        {
            MapHandlers.ChangingIntoGrenade -= OnChangingIntoGrenade;
            MapHandlers.PlacingBlood -= OnPlacingBlood;
        }

        private void OnChangingIntoGrenade(ChangingIntoGrenadeEventArgs ev)
        {
            if (Round.IsLobby)
                ev.IsAllowed = false;
        }

        private void OnPlacingBlood(PlacingBloodEventArgs ev)
        {
            if (Round.IsLobby)
                ev.IsAllowed = false;
        }
    }
}