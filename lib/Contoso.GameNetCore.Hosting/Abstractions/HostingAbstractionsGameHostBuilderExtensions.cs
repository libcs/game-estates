// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Contoso.GameNetCore.Hosting.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Threading;

namespace Contoso.GameNetCore.Hosting
{
    public static class HostingAbstractionsGameHostBuilderExtensions
    {
        static readonly string ServerUrlsSeparator = ";";

        /// <summary>
        /// Use the given configuration settings on the game host.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> containing settings to be used.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder UseConfiguration(this IGameHostBuilder hostBuilder, IConfiguration configuration)
        {
            foreach (var setting in configuration.AsEnumerable(makePathsRelative: true))
                hostBuilder.UseSetting(setting.Key, setting.Value);
            return hostBuilder;
        }

        /// <summary>
        /// Set whether startup errors should be captured in the configuration settings of the game host.
        /// When enabled, startup exceptions will be caught and an error page will be returned. If disabled, startup exceptions will be propagated.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="captureStartupErrors"><c>true</c> to use startup error page; otherwise <c>false</c>.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder CaptureStartupErrors(this IGameHostBuilder hostBuilder, bool captureStartupErrors) =>
            hostBuilder.UseSetting(GameHostDefaults.CaptureStartupErrorsKey, captureStartupErrors ? "true" : "false");

        /// <summary>
        /// Specify the assembly containing the startup type to be used by the game host.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="startupAssemblyName">The name of the assembly containing the startup type.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder UseStartup(this IGameHostBuilder hostBuilder, string startupAssemblyName)
        {
            if (startupAssemblyName == null)
                throw new ArgumentNullException(nameof(startupAssemblyName));
            return hostBuilder
                .UseSetting(GameHostDefaults.ApplicationKey, startupAssemblyName)
                .UseSetting(GameHostDefaults.StartupAssemblyKey, startupAssemblyName);
        }

        /// <summary>
        /// Specify the server to be used by the game host.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="server">The <see cref="IServer"/> to be used.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder UseServer(this IGameHostBuilder hostBuilder, IServer server)
        {
            if (server == null)
                throw new ArgumentNullException(nameof(server));
            return hostBuilder.ConfigureServices(services => services.AddSingleton(server));
        }

        /// <summary>
        /// Specify the environment to be used by the game host.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="environment">The environment to host the application in.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder UseEnvironment(this IGameHostBuilder hostBuilder, string environment)
        {
            if (environment == null)
                throw new ArgumentNullException(nameof(environment));
            return hostBuilder.UseSetting(GameHostDefaults.EnvironmentKey, environment);
        }

        /// <summary>
        /// Specify the content root directory to be used by the game host.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="contentRoot">Path to root directory of the application.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder UseContentRoot(this IGameHostBuilder hostBuilder, string contentRoot)
        {
            if (contentRoot == null)
                throw new ArgumentNullException(nameof(contentRoot));
            return hostBuilder.UseSetting(GameHostDefaults.ContentRootKey, contentRoot);
        }

        /// <summary>
        /// Specify the gameroot directory to be used by the game host.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="gameRoot">Path to the root directory used by the game server.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder UseGameRoot(this IGameHostBuilder hostBuilder, string gameRoot)
        {
            if (gameRoot == null)
                throw new ArgumentNullException(nameof(gameRoot));
            return hostBuilder.UseSetting(GameHostDefaults.GameRootKey, gameRoot);
        }

        /// <summary>
        /// Specify the urls the game host will listen on.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="urls">The urls the hosted application will listen on.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder UseUrls(this IGameHostBuilder hostBuilder, params string[] urls)
        {
            if (urls == null)
                throw new ArgumentNullException(nameof(urls));
            return hostBuilder.UseSetting(GameHostDefaults.ServerUrlsKey, string.Join(ServerUrlsSeparator, urls));
        }

        /// <summary>
        /// Indicate whether the host should listen on the URLs configured on the <see cref="IGameHostBuilder"/>
        /// instead of those configured on the <see cref="IServer"/>.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="preferHostingUrls"><c>true</c> to prefer URLs configured on the <see cref="IGameHostBuilder"/>; otherwise <c>false</c>.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder PreferHostingUrls(this IGameHostBuilder hostBuilder, bool preferHostingUrls) =>
            hostBuilder.UseSetting(GameHostDefaults.PreferHostingUrlsKey, preferHostingUrls ? "true" : "false");

        /// <summary>
        /// Specify if startup status messages should be suppressed.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="suppressStatusMessages"><c>true</c> to suppress writing of hosting startup status messages; otherwise <c>false</c>.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder SuppressStatusMessages(this IGameHostBuilder hostBuilder, bool suppressStatusMessages) =>
            hostBuilder.UseSetting(GameHostDefaults.SuppressStatusMessagesKey, suppressStatusMessages ? "true" : "false");

        /// <summary>
        /// Specify the amount of time to wait for the game host to shutdown.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="timeout">The amount of time to wait for server shutdown.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder UseShutdownTimeout(this IGameHostBuilder hostBuilder, TimeSpan timeout) =>
            hostBuilder.UseSetting(GameHostDefaults.ShutdownTimeoutKey, ((int)timeout.TotalSeconds).ToString(CultureInfo.InvariantCulture));

        /// <summary>
        /// Start the game host and listen on the specified urls.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to start.</param>
        /// <param name="urls">The urls the hosted application will listen on.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHost Start(this IGameHostBuilder hostBuilder, params string[] urls)
        {
            var host = hostBuilder.UseUrls(urls).Build();
            host.StartAsync(CancellationToken.None).GetAwaiter().GetResult();
            return host;
        }
    }
}
