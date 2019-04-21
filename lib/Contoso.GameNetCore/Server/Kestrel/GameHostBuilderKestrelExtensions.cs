// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Contoso.GameNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Contoso.GameNetCore.Hosting
{
    public static class GameHostBuilderKestrelExtensions
    {
        /// <summary>
        /// Specify Kestrel as the server to be used by the game host.
        /// </summary>
        /// <param name="hostBuilder">
        /// The Contoso.GameNetCore.Hosting.IWebHostBuilder to configure.
        /// </param>
        /// <returns>
        /// The Contoso.GameNetCore.Hosting.IGameHostBuilder.
        /// </returns>
        public static IGameHostBuilder UseKestrel(this IGameHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                // Don't override an already-configured transport
                services.TryAddSingleton<ITransportFactory, SocketTransportFactory>();

                services.AddTransient<IConfigureOptions<KestrelServerOptions>, KestrelServerOptionsSetup>();
                services.AddSingleton<IServer, KestrelServer>();
            });
        }

        /// <summary>
        /// Specify Kestrel as the server to be used by the game host.
        /// </summary>
        /// <param name="hostBuilder">
        /// The Contoso.GameNetCore.Hosting.IGameHostBuilder to configure.
        /// </param>
        /// <param name="options">
        /// A callback to configure Kestrel options.
        /// </param>
        /// <returns>
        /// The Contoso.GameNetCore.Hosting.IGameHostBuilder.
        /// </returns>
        public static IGameHostBuilder UseKestrel(this IGameHostBuilder hostBuilder, Action<KestrelServerOptions> options) => hostBuilder.UseKestrel().ConfigureKestrel(options);

        /// <summary>
        /// Configures Kestrel options but does not register an IServer. See <see cref="UseKestrel(IGameHostBuilder)"/>.
        /// </summary>
        /// <param name="hostBuilder">
        /// The Contoso.GameNetCore.Hosting.IGameHostBuilder to configure.
        /// </param>
        /// <param name="options">
        /// A callback to configure Kestrel options.
        /// </param>
        /// <returns>
        /// The Contoso.GameNetCore.Hosting.IGameHostBuilder.
        /// </returns>
        public static IGameHostBuilder ConfigureKestrel(this IGameHostBuilder hostBuilder, Action<KestrelServerOptions> options) => 
            hostBuilder.ConfigureServices(services =>
            {
                services.Configure(options);
            });

        /// <summary>
        /// Specify Kestrel as the server to be used by the web host.
        /// </summary>
        /// <param name="hostBuilder">
        /// The Contoso.GameNetCore.Hosting.IGameHostBuilder to configure.
        /// </param>
        /// <param name="configureOptions">A callback to configure Kestrel options.</param>
        /// <returns>
        /// The Contoso.GameNetCore.Hosting.IGameHostBuilder.
        /// </returns>
        public static IGameHostBuilder UseKestrel(this IGameHostBuilder hostBuilder, Action<GameHostBuilderContext, KestrelServerOptions> configureOptions) =>
            hostBuilder.UseKestrel().ConfigureKestrel(configureOptions);

        /// <summary>
        /// Configures Kestrel options but does not register an IServer. See <see cref="UseKestrel(IGameHostBuilder)"/>.
        /// </summary>
        /// <param name="hostBuilder">
        /// The Contoso.GameNetCore.Hosting.IGameHostBuilder to configure.
        /// </param>
        /// <param name="configureOptions">A callback to configure Kestrel options.</param>
        /// <returns>
        /// The Contoso.GameNetCore.Hosting.IGameHostBuilder.
        /// </returns>
        public static IGameHostBuilder ConfigureKestrel(this IGameHostBuilder hostBuilder, Action<GameHostBuilderContext, KestrelServerOptions> configureOptions)
        {
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.Configure<KestrelServerOptions>(options =>
                {
                    configureOptions(context, options);
                });
            });
        }
    }
}
