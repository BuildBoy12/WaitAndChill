// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WaitAndChill
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Enums;
    using Exiled.API.Interfaces;
    using WaitAndChill.Configs;
    using WaitAndChill.Models;

    /// <inheritdoc />
    public class Config : IConfig
    {
        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the list of effects to apply to players.
        /// </summary>
        [Description("The list of effects to apply to players.")]
        public List<ConfiguredEffect> Effects { get; set; } = new()
        {
            new ConfiguredEffect(EffectType.MovementBoost, 50),
        };

        /// <summary>
        /// Gets or sets the list of items to give to players.
        /// </summary>
        [Description("The list of items to give to players.")]
        public List<string> Inventory { get; set; } = new()
        {
            ItemType.Adrenaline.ToString(),
        };

        /// <summary>
        /// Gets or sets the spawnable positions for players.
        /// </summary>
        public SpawnConfig SpawnablePositions { get; set; } = new();
    }
}