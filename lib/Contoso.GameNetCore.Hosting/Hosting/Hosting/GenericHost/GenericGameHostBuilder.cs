// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Contoso.GameNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
#if !NET3
using IHostEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;
#endif

namespace Contoso.GameNetCore.Hosting.Internal
{
    internal class GenericGameHostBuilder : IGameHostBuilder, ISupportsStartup, ISupportsUseDefaultServiceProvider
    {
        readonly IHostBuilder _builder;
        readonly IConfiguration _config;
        readonly object _startupKey = new object();

        AggregateException _hostingStartupErrors;
        HostingStartupGameHostBuilder _hostingStartupGameHostBuilder;

        public GenericGameHostBuilder(IHostBuilder builder)
        {
            _builder = builder;

            _config = new ConfigurationBuilder()
                .AddEnvironmentVariables(prefix: "GAMENETCORE_")
                .Build();

            _builder.ConfigureHostConfiguration(config =>
            {
                config.AddConfiguration(_config);

                // We do this super early but still late enough that we can process the configuration
                // wired up by calls to UseSetting
                ExecuteHostingStartups();
            });

            // IHostingStartup needs to be executed before any direct methods on the builder
            // so register these callbacks first
            _builder.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                if (_hostingStartupGameHostBuilder != null)
                {
                    var gamehostContext = GetGameHostBuilderContext(context);
                    _hostingStartupGameHostBuilder.ConfigureAppConfiguration(gamehostContext, configurationBuilder);
                }
            });

            _builder.ConfigureServices((context, services) =>
            {
                if (_hostingStartupGameHostBuilder != null)
                {
                    var gamehostContext = GetGameHostBuilderContext(context);
                    _hostingStartupGameHostBuilder.ConfigureServices(gamehostContext, services);
                }
            });

            _builder.ConfigureServices((context, services) =>
            {
                var gamehostContext = GetGameHostBuilderContext(context);
                var gameHostOptions = (GameHostOptions)context.Properties[typeof(GameHostOptions)];

                // Add the IHostingEnvironment and IApplicationLifetime from Microsoft.AspNetCore.Hosting
                services.AddSingleton(gamehostContext.HostingEnvironment);
#pragma warning disable CS0618 // Type or member is obsolete
                services.AddSingleton((GameNetCore.Hosting.IHostingEnvironment)gamehostContext.HostingEnvironment);
                services.AddSingleton<IApplicationLifetime, GenericGameHostApplicationLifetime>();
#pragma warning restore CS0618 // Type or member is obsolete

                services.Configure<GenericGameHostServiceOptions>(options =>
                {
                    // Set the options
                    options.GameHostOptions = gameHostOptions;
                    // Store and forward any startup errors
                    options.HostingStartupExceptions = _hostingStartupErrors;
                });

                services.AddHostedService<GenericGameHostService>();

                // REVIEW: This is bad since we don't own this type. Anybody could add one of these and it would mess things up
                // We need to flow this differently
                var listener = new DiagnosticListener("Contoso.GameNetCore");
                services.TryAddSingleton<DiagnosticListener>(listener);
                services.TryAddSingleton<DiagnosticSource>(listener);

                //services.TryAddSingleton<IHttpContextFactory, DefaultHttpContextFactory>();
                //services.TryAddScoped<IMiddlewareFactory, MiddlewareFactory>();
                services.TryAddSingleton<IApplicationBuilderFactory, ApplicationBuilderFactory>();

                // Support UseStartup(assemblyName)
                if (!string.IsNullOrEmpty(gameHostOptions.StartupAssembly))
                    try
                    {
                        var startupType = StartupLoader.FindStartupType(gameHostOptions.StartupAssembly, gamehostContext.HostingEnvironment.EnvironmentName);
                        UseStartup(startupType, context, services);
                    }
                    catch (Exception ex) when (gameHostOptions.CaptureStartupErrors)
                    {
                        var capture = ExceptionDispatchInfo.Capture(ex);

                        services.Configure<GenericGameHostServiceOptions>(options =>
                        {
                            options.ConfigureApplication = app =>
                            {
                                // Throw if there was any errors initializing startup
                                capture.Throw();
                            };
                        });
                    }
            });
        }

        void ExecuteHostingStartups()
        {
            var gameHostOptions = new GameHostOptions(_config, Assembly.GetEntryAssembly()?.GetName().Name);

            if (gameHostOptions.PreventHostingStartup)
                return;

            var exceptions = new List<Exception>();
            _hostingStartupGameHostBuilder = new HostingStartupGameHostBuilder(this);

            // Execute the hosting startup assemblies
            foreach (var assemblyName in gameHostOptions.GetFinalHostingStartupAssemblies().Distinct(StringComparer.OrdinalIgnoreCase))
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(assemblyName));

                    foreach (var attribute in assembly.GetCustomAttributes<HostingStartupAttribute>())
                    {
                        var hostingStartup = (IHostingStartup)Activator.CreateInstance(attribute.HostingStartupType);
                        hostingStartup.Configure(_hostingStartupGameHostBuilder);
                    }
                }
                catch (Exception ex)
                {
                    // Capture any errors that happen during startup
                    exceptions.Add(new InvalidOperationException($"Startup assembly {assemblyName} failed to execute. See the inner exception for more details.", ex));
                }
            }

            if (exceptions.Count > 0)
                _hostingStartupErrors = new AggregateException(exceptions);
        }

        public IGameHost Build() =>
            throw new NotSupportedException($"Building this implementation of {nameof(IGameHostBuilder)} is not supported.");

        public IGameHostBuilder ConfigureAppConfiguration(Action<GameHostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            _builder.ConfigureAppConfiguration((context, builder) =>
            {
                var gamehostBuilderContext = GetGameHostBuilderContext(context);
                configureDelegate(gamehostBuilderContext, builder);
            });

            return this;
        }

        public IGameHostBuilder ConfigureServices(Action<IServiceCollection> configureServices) =>
            ConfigureServices((context, services) => configureServices(services));

        public IGameHostBuilder ConfigureServices(Action<GameHostBuilderContext, IServiceCollection> configureServices)
        {
            _builder.ConfigureServices((context, builder) =>
            {
                var gamehostBuilderContext = GetGameHostBuilderContext(context);
                configureServices(gamehostBuilderContext, builder);
            });

            return this;
        }

        public IGameHostBuilder UseDefaultServiceProvider(Action<GameHostBuilderContext, ServiceProviderOptions> configure)
        {
            _builder.UseServiceProviderFactory(context =>
            {
                var gameHostBuilderContext = GetGameHostBuilderContext(context);
                var options = new ServiceProviderOptions();
                configure(gameHostBuilderContext, options);
                return new DefaultServiceProviderFactory(options);
            });

            return this;
        }

        public IGameHostBuilder UseStartup(Type startupType)
        {
            // UseStartup can be called multiple times. Only run the last one.
            _builder.Properties["UseStartup.StartupType"] = startupType;
            _builder.ConfigureServices((context, services) =>
            {
                if (_builder.Properties.TryGetValue("UseStartup.StartupType", out var cachedType) && (Type)cachedType == startupType)
                    UseStartup(startupType, context, services);
            });

            return this;
        }

        private void UseStartup(Type startupType, HostBuilderContext context, IServiceCollection services)
        {
            var gameHostBuilderContext = GetGameHostBuilderContext(context);
            var gameHostOptions = (GameHostOptions)context.Properties[typeof(GameHostOptions)];

            ExceptionDispatchInfo startupError = null;
            object instance = null;
            ConfigureBuilder configureBuilder = null;

            try
            {
                // We cannot support methods that return IServiceProvider as that is terminal and we need ConfigureServices to compose
                if (typeof(IStartup).IsAssignableFrom(startupType))
                    throw new NotSupportedException($"{typeof(IStartup)} isn't supported");
                if (StartupLoader.HasConfigureServicesIServiceProviderDelegate(startupType, context.HostingEnvironment.EnvironmentName))
                    throw new NotSupportedException($"ConfigureServices returning an {typeof(IServiceProvider)} isn't supported.");

                instance = ActivatorUtilities.CreateInstance(new HostServiceProvider(gameHostBuilderContext), startupType);
                context.Properties[_startupKey] = instance;

                // Startup.ConfigureServices
                var configureServicesBuilder = StartupLoader.FindConfigureServicesDelegate(startupType, context.HostingEnvironment.EnvironmentName);
                var configureServices = configureServicesBuilder.Build(instance);

                configureServices(services);

                // REVIEW: We're doing this in the callback so that we have access to the hosting environment
                // Startup.ConfigureContainer
                var configureContainerBuilder = StartupLoader.FindConfigureContainerDelegate(startupType, context.HostingEnvironment.EnvironmentName);
                if (configureContainerBuilder.MethodInfo != null)
                {
                    var containerType = configureContainerBuilder.GetContainerType();
                    // Store the builder in the property bag
                    _builder.Properties[typeof(ConfigureContainerBuilder)] = configureContainerBuilder;

                    var actionType = typeof(Action<,>).MakeGenericType(typeof(HostBuilderContext), containerType);

                    // Get the private ConfigureContainer method on this type then close over the container type
                    var configureCallback = GetType().GetMethod(nameof(ConfigureContainer), BindingFlags.NonPublic | BindingFlags.Instance)
                                                     .MakeGenericMethod(containerType)
                                                     .CreateDelegate(actionType, this);

                    // _builder.ConfigureContainer<T>(ConfigureContainer);
                    typeof(IHostBuilder).GetMethods().First(m => m.Name == nameof(IHostBuilder.ConfigureContainer))
                        .MakeGenericMethod(containerType)
                        .InvokeWithoutWrappingExceptions(_builder, new object[] { configureCallback });
                }

                // Resolve Configure after calling ConfigureServices and ConfigureContainer
                configureBuilder = StartupLoader.FindConfigureDelegate(startupType, context.HostingEnvironment.EnvironmentName);
            }
            catch (Exception ex) when (gameHostOptions.CaptureStartupErrors)
            {
                startupError = ExceptionDispatchInfo.Capture(ex);
            }

            // Startup.Configure
            services.Configure<GenericGameHostServiceOptions>(options =>
            {
                options.ConfigureApplication = app =>
                {
                    // Throw if there was any errors initializing startup
                    startupError?.Throw();

                    // Execute Startup.Configure
                    if (instance != null && configureBuilder != null)
                        configureBuilder.Build(instance)(app);
                };
            });
        }

        void ConfigureContainer<TContainer>(HostBuilderContext context, TContainer container)
        {
            var instance = context.Properties[_startupKey];
            var builder = (ConfigureContainerBuilder)context.Properties[typeof(ConfigureContainerBuilder)];
            builder.Build(instance)(container);
        }

        public IGameHostBuilder Configure(Action<GameHostBuilderContext, IApplicationBuilder> configure)
        {
            _builder.ConfigureServices((context, services) =>
            {
                services.Configure<GenericGameHostServiceOptions>(options =>
                {
                    var gamehostBuilderContext = GetGameHostBuilderContext(context);
                    options.ConfigureApplication = app => configure(gamehostBuilderContext, app);
                });
            });

            return this;
        }

        GameHostBuilderContext GetGameHostBuilderContext(HostBuilderContext context)
        {
            if (!context.Properties.TryGetValue(typeof(GameHostBuilderContext), out var contextVal))
            {
                var options = new GameHostOptions(context.Configuration, Assembly.GetEntryAssembly()?.GetName().Name);
                var gameHostBuilderContext = new GameHostBuilderContext
                {
                    Configuration = context.Configuration,
                    HostingEnvironment = new HostingEnvironment(),
                };
                gameHostBuilderContext.HostingEnvironment.Initialize(context.HostingEnvironment.ContentRootPath, options);
                context.Properties[typeof(GameHostBuilderContext)] = gameHostBuilderContext;
                context.Properties[typeof(GameHostOptions)] = options;
                return gameHostBuilderContext;
            }

            // Refresh config, it's periodically updated/replaced
            var gameHostContext = (GameHostBuilderContext)contextVal;
            gameHostContext.Configuration = context.Configuration;
            return gameHostContext;
        }

        public string GetSetting(string key) =>
            _config[key];

        public IGameHostBuilder UseSetting(string key, string value)
        {
            _config[key] = value;
            return this;
        }

        // This exists just so that we can use ActivatorUtilities.CreateInstance on the Startup class
        class HostServiceProvider : IServiceProvider
        {
            readonly GameHostBuilderContext _context;

            public HostServiceProvider(GameHostBuilderContext context) =>
                _context = context;

            public object GetService(Type serviceType)
            {
                // The implementation of the HostingEnvironment supports both interfaces
#pragma warning disable CS0618 // Type or member is obsolete
                if (serviceType == typeof(Microsoft.Extensions.Hosting.IHostingEnvironment)
                    || serviceType == typeof(Contoso.GameNetCore.Hosting.IHostingEnvironment)
#pragma warning restore CS0618 // Type or member is obsolete
                    || serviceType == typeof(IGameHostEnvironment)
                    || serviceType == typeof(IHostEnvironment)
                    )
                    return _context.HostingEnvironment;

                if (serviceType == typeof(IConfiguration))
                    return _context.Configuration;

                return null;
            }
        }
    }
}
