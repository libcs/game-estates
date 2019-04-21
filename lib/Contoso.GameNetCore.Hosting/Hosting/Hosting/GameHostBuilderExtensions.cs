// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Reflection;
using Contoso.GameNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
#if !NET3
using IHostEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;
#endif

namespace Contoso.GameNetCore.Hosting
{
    public static class GameHostBuilderExtensions
    {
        /// <summary>
        /// Specify the startup method to be used to configure the web application.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="configureApp">The delegate that configures the <see cref="IApplicationBuilder"/>.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder Configure(this IGameHostBuilder hostBuilder, Action<IApplicationBuilder> configureApp) =>
            hostBuilder.Configure((_, app) => configureApp(app), configureApp.GetMethodInfo().DeclaringType.GetTypeInfo().Assembly.GetName().Name);

        /// <summary>
        /// Specify the startup method to be used to configure the web application.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="configureApp">The delegate that configures the <see cref="IApplicationBuilder"/>.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder Configure(this IGameHostBuilder hostBuilder, Action<GameHostBuilderContext, IApplicationBuilder> configureApp) =>
            hostBuilder.Configure(configureApp, configureApp.GetMethodInfo().DeclaringType.GetTypeInfo().Assembly.GetName().Name);

        static IGameHostBuilder Configure(this IGameHostBuilder hostBuilder, Action<GameHostBuilderContext, IApplicationBuilder> configureApp, string startupAssemblyName)
        {
            if (configureApp == null)
                throw new ArgumentNullException(nameof(configureApp));

            hostBuilder.UseSetting(GameHostDefaults.ApplicationKey, startupAssemblyName);

            // Light up the ISupportsStartup implementation
            if (hostBuilder is ISupportsStartup supportsStartup)
                return supportsStartup.Configure(configureApp);

            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IStartup>(sp =>
                    new DelegateStartup(sp.GetRequiredService<IServiceProviderFactory<IServiceCollection>>(), app => configureApp(context, app)));
            });
        }

        /// <summary>
        /// Specify the startup type to be used by the game host.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="startupType">The <see cref="Type"/> to be used.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder UseStartup(this IGameHostBuilder hostBuilder, Type startupType)
        {
            var startupAssemblyName = startupType.GetTypeInfo().Assembly.GetName().Name;

            hostBuilder.UseSetting(GameHostDefaults.ApplicationKey, startupAssemblyName);

            // Light up the GenericWebHostBuilder implementation
            if (hostBuilder is ISupportsStartup supportsStartup)
                return supportsStartup.UseStartup(startupType);

            return hostBuilder
                .ConfigureServices(services =>
                {
                    if (typeof(IStartup).GetTypeInfo().IsAssignableFrom(startupType.GetTypeInfo()))
                        services.AddSingleton(typeof(IStartup), startupType);
                    else
                        services.AddSingleton(typeof(IStartup), sp =>
                        {
                            var hostingEnvironment = sp.GetRequiredService<IHostEnvironment>();
                            return new ConventionBasedStartup(StartupLoader.LoadMethods(sp, startupType, hostingEnvironment.EnvironmentName));
                        });
                });
        }

        /// <summary>
        /// Specify the startup type to be used by the game host.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <typeparam name ="TStartup">The type containing the startup methods for the application.</typeparam>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder UseStartup<TStartup>(this IGameHostBuilder hostBuilder) where TStartup : class =>
            hostBuilder.UseStartup(typeof(TStartup));

        /// <summary>
        /// Configures the default service provider
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="configure">A callback used to configure the <see cref="ServiceProviderOptions"/> for the default <see cref="IServiceProvider"/>.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder UseDefaultServiceProvider(this IGameHostBuilder hostBuilder, Action<ServiceProviderOptions> configure) =>
            hostBuilder.UseDefaultServiceProvider((context, options) => configure(options));

        /// <summary>
        /// Configures the default service provider
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="configure">A callback used to configure the <see cref="ServiceProviderOptions"/> for the default <see cref="IServiceProvider"/>.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder UseDefaultServiceProvider(this IGameHostBuilder hostBuilder, Action<GameHostBuilderContext, ServiceProviderOptions> configure)
        {
            // Light up the GenericWebHostBuilder implementation
            if (hostBuilder is ISupportsUseDefaultServiceProvider supportsDefaultServiceProvider)
                return supportsDefaultServiceProvider.UseDefaultServiceProvider(configure);

            return hostBuilder.ConfigureServices((context, services) =>
            {
                var options = new ServiceProviderOptions();
                configure(context, options);
                services.Replace(ServiceDescriptor.Singleton<IServiceProviderFactory<IServiceCollection>>(new DefaultServiceProviderFactory(options)));
            });
        }

        /// <summary>
        /// Adds a delegate for configuring the <see cref="IConfigurationBuilder"/> that will construct an <see cref="IConfiguration"/>.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="IConfigurationBuilder" /> that will be used to construct an <see cref="IConfiguration" />.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        /// <remarks>
        /// The <see cref="IConfiguration"/> and <see cref="ILoggerFactory"/> on the <see cref="GameHostBuilderContext"/> are uninitialized at this stage.
        /// The <see cref="IConfigurationBuilder"/> is pre-populated with the settings of the <see cref="IGameHostBuilder"/>.
        /// </remarks>
        public static IGameHostBuilder ConfigureAppConfiguration(this IGameHostBuilder hostBuilder, Action<IConfigurationBuilder> configureDelegate) =>
            hostBuilder.ConfigureAppConfiguration((context, builder) => configureDelegate(builder));

        /// <summary>
        /// Adds a delegate for configuring the provided <see cref="ILoggingBuilder"/>. This may be called multiple times.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder" /> to configure.</param>
        /// <param name="configureLogging">The delegate that configures the <see cref="ILoggingBuilder"/>.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder ConfigureLogging(this IGameHostBuilder hostBuilder, Action<ILoggingBuilder> configureLogging) =>
            hostBuilder.ConfigureServices(collection => collection.AddLogging(configureLogging));

        /// <summary>
        /// Adds a delegate for configuring the provided <see cref="LoggerFactory"/>. This may be called multiple times.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder" /> to configure.</param>
        /// <param name="configureLogging">The delegate that configures the <see cref="LoggerFactory"/>.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder ConfigureLogging(this IGameHostBuilder hostBuilder, Action<GameHostBuilderContext, ILoggingBuilder> configureLogging) =>
            hostBuilder.ConfigureServices((context, collection) => collection.AddLogging(builder => configureLogging(context, builder)));
    }
}
