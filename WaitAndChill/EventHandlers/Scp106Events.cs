// -----------------------------------------------------------------------
// <copyright file="Scp106Events.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WaitAndChill.EventHandlers
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using WaitAndChill.Models;
    using Scp106Handlers = Exiled.Events.Handlers.Scp106;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers.Scp106"/>.
    /// </summary>
    public class Scp106Events : Subscribable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Scp106Events"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="WaitAndChill.Plugin"/> class.</param>
        public Scp106Events(Plugin plugin)
            : base(plugin)
        {
        }

        /// <inheritdoc />
        public override void Subscribe()
        {
            Scp106Handlers.CreatingPortal += OnCreatingPortal;
            Scp106Handlers.Teleporting += OnTeleporting;
        }

        /// <inheritdoc />
        public override void Unsubscribe()
        {
            Scp106Handlers.CreatingPortal -= OnCreatingPortal;
            Scp106Handlers.Teleporting -= OnTeleporting;
        }

        private void OnCreatingPortal(CreatingPortalEventArgs ev)
        {
            if (Round.IsLobby)
                ev.IsAllowed = false;
        }

        private void OnTeleporting(TeleportingEventArgs ev)
        {
            if (Round.IsLobby)
                ev.IsAllowed = false;
        }
    }
}