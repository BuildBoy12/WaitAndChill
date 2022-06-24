// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WaitAndChill
{
    using System;
    using System.Collections.Generic;
    using Exiled.API.Features;
    using UnityEngine;
    using WaitAndChill.API;
    using WaitAndChill.Models;

    /// <inheritdoc />
    public class Plugin : Plugin<Config, Translation>
    {
        private readonly List<Subscribable> subscribed = new();

        /// <summary>
        /// Gets an instance of the <see cref="Plugin"/> class.
        /// </summary>
        public static Plugin Instance { get; private set; }

        /// <summary>
        /// Gets or sets the current lobby spawn.
        /// </summary>
        public Vector3 CurrentSpawnPosition { get; set; }

        /// <inheritdoc/>
        public override string Author => "Build";

        /// <inheritdoc/>
        public override string Name => "WaitAndChill";

        /// <inheritdoc/>
        public override string Prefix => "WaitAndChill";

        /// <inheritdoc/>
        public override Version Version { get; } = new(1, 0, 0);

        /// <inheritdoc/>
        public override Version RequiredExiledVersion { get; } = new(5, 2, 1);

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            SpawnPositions.Regenerate();

            Instance = this;
            Subscribe();
            base.OnEnabled();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            Unsubscribe();
            Instance = null;
            base.OnDisabled();
        }

        private void Subscribe()
        {
            foreach (Type type in Assembly.GetTypes())
            {
                if (!type.IsSubclassOf(typeof(Subscribable)))
                    continue;

                Subscribable subscribable = (Subscribable)Activator.CreateInstance(type, args: this);
                subscribable.Subscribe();
                subscribed.Add(subscribable);
            }
        }

        private void Unsubscribe()
        {
            foreach (Subscribable subscribable in subscribed)
                subscribable.Unsubscribe();

            subscribed.Clear();
        }
    }
}