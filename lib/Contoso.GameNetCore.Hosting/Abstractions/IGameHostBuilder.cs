// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Contoso.GameNetCore.Hosting
{
    /// <summary>
    /// A builder for <see cref="IGameHost"/>.
    /// </summary>
    public interface IGameHostBuilder
    {
        /// <summary>
        /// Builds an <see cref="IGameHost"/> which hosts a game application.
        /// </summary>
        IGameHost Build();

        /// <summary>
        /// Adds a delegate for configuring the <see cref="IConfigurationBuilder"/> that will construct an <see cref="IConfiguration"/>.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="IConfigurationBuilder" /> that will be used to construct an <see cref="IConfiguration" />.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        /// <remarks>
        /// The <see cref="IConfiguration"/> and <see cref="ILoggerFactory"/> on the <see cref="GameHostBuilderContext"/> are uninitialized at this stage.
        /// The <see cref="IConfigurationBuilder"/> is pre-populated with the settings of the <see cref="IGameHostBuilder"/>.
        /// </remarks>
        IGameHostBuilder ConfigureAppConfiguration(Action<GameHostBuilderContext, IConfigurationBuilder> configureDelegate);

        /// <summary>
        /// Adds a delegate for configuring additional services for the host or game application. This may be called
        /// multiple times.
        /// </summary>
        /// <param name="configureServices">A delegate for configuring the <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        IGameHostBuilder ConfigureServices(Action<IServiceCollection> configureServices);

        /// <summary>
        /// Adds a delegate for configuring additional services for the host or game application. This may be called
        /// multiple times.
        /// </summary>
        /// <param name="configureServices">A delegate for configuring the <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        IGameHostBuilder ConfigureServices(Action<GameHostBuilderContext, IServiceCollection> configureServices);

        /// <summary>
        /// Get the setting value from the configuration.
        /// </summary>
        /// <param name="key">The key of the setting to look up.</param>
        /// <returns>The value the setting currently contains.</returns>
        string GetSetting(string key);

        /// <summary>
        /// Add or replace a setting in the configuration.
        /// </summary>
        /// <param name="key">The key of the setting to add or replace.</param>
        /// <param name="value">The value of the setting to add or replace.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        IGameHostBuilder UseSetting(string key, string value);
    }
}