// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Contoso.GameNetCore.Hosting.Builder;
using Contoso.GameNetCore.Hosting.Server;
using Contoso.GameNetCore.Hosting.Server.Features;
using Contoso.GameNetCore.Hosting.Views;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
#if !NET3
using IHostEnvironment = Contoso.GameNetCore.Hosting.IHostingEnvironment;
using IHostApplicationLifetime = Contoso.GameNetCore.Hosting.IApplicationLifetime;
using IAsyncDisposable = System.IDisposable;
#endif

namespace Contoso.GameNetCore.Hosting.Internal
{
    internal class GameHost : IGameHost, IAsyncDisposable
    {
        static readonly string DeprecatedServerUrlsKey = "server.urls";

        readonly IServiceCollection _applicationServiceCollection;
        IStartup _startup;
        ApplicationLifetime _applicationLifetime;
        HostedServiceExecutor _hostedServiceExecutor;

        readonly IServiceProvider _hostingServiceProvider;
        readonly IConfiguration _config;
        readonly AggregateException _hostingStartupErrors;
        ExceptionDispatchInfo _applicationServicesException;
        ILogger<GameHost> _logger;

        bool _stopped;

        // Used for testing only
        internal GameHostOptions Options { get; }

        IServer Server { get; set; }

        public GameHost(
            IServiceCollection appServices,
            IServiceProvider hostingServiceProvider,
            GameHostOptions options,
            IConfiguration config,
            AggregateException hostingStartupErrors)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _hostingStartupErrors = hostingStartupErrors;
            Options = options;
            _applicationServiceCollection = appServices ?? throw new ArgumentNullException(nameof(appServices));
            _hostingServiceProvider = hostingServiceProvider ?? throw new ArgumentNullException(nameof(hostingServiceProvider));
            _applicationServiceCollection.AddSingleton<ApplicationLifetime>();
            // There's no way to to register multiple service types per definition. See https://github.com/aspnet/DependencyInjection/issues/360
#pragma warning disable CS0618 // Type or member is obsolete
            _applicationServiceCollection.AddSingleton(services
                => services.GetService<ApplicationLifetime>() as IHostApplicationLifetime);
            _applicationServiceCollection.AddSingleton(services
                => services.GetService<ApplicationLifetime>() as GameNetCore.Hosting.IApplicationLifetime);
#if NET3
            _applicationServiceCollection.AddSingleton(services
                => services.GetService<ApplicationLifetime>() as Extensions.Hosting.IApplicationLifetime);
#endif
#pragma warning restore CS0618 // Type or member is obsolete
            _applicationServiceCollection.AddSingleton<HostedServiceExecutor>();
        }

        public IServiceProvider Services { get; private set; }

        public IFeatureCollection ServerFeatures
        {
            get
            {
                EnsureServer();
                return Server?.Features;
            }
        }

        // Called immediately after the constructor so the properties can rely on it.
        public void Initialize()
        {
            try
            {
                EnsureApplicationServices();
            }
            catch (Exception ex)
            {
                // EnsureApplicationServices may have failed due to a missing or throwing Startup class.
                if (Services == null)
                    Services = _applicationServiceCollection.BuildServiceProvider();

                if (!Options.CaptureStartupErrors)
                    throw;

                _applicationServicesException = ExceptionDispatchInfo.Capture(ex);
            }
        }

        public void Start() =>
            StartAsync().GetAwaiter().GetResult();

        public virtual async Task StartAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            HostingEventSource.Log.HostStart();
            _logger = Services.GetRequiredService<ILogger<GameHost>>();
            _logger.Starting();

            var application = BuildApplication();

            _applicationLifetime = Services.GetRequiredService<ApplicationLifetime>();
            _hostedServiceExecutor = Services.GetRequiredService<HostedServiceExecutor>();
            var diagnosticSource = Services.GetRequiredService<DiagnosticListener>();
            var httpContextFactory = Services.GetRequiredService<IHttpContextFactory>();
            var hostingApp = new HostingApplication(application, _logger, diagnosticSource, httpContextFactory);
            await Server.StartAsync(hostingApp, cancellationToken).ConfigureAwait(false);

            // Fire IApplicationLifetime.Started
            _applicationLifetime?.NotifyStarted();

            // Fire IHostedService.Start
            await _hostedServiceExecutor.StartAsync(cancellationToken).ConfigureAwait(false);

            _logger.Started();

            // Log the fact that we did load hosting startup assemblies.
            if (_logger.IsEnabled(LogLevel.Debug))
                foreach (var assembly in Options.GetFinalHostingStartupAssemblies())
                    _logger.LogDebug($"Loaded hosting startup assembly {assembly}");

            if (_hostingStartupErrors != null)
                foreach (var exception in _hostingStartupErrors.InnerExceptions)
                    _logger.HostingStartupAssemblyError(exception);
        }

        void EnsureApplicationServices()
        {
            if (Services == null)
            {
                EnsureStartup();
                Services = _startup.ConfigureServices(_applicationServiceCollection);
            }
        }

        void EnsureStartup()
        {
            if (_startup != null)
                return;

            _startup = _hostingServiceProvider.GetService<IStartup>();

            if (_startup == null)
                throw new InvalidOperationException($"No application configured. Please specify startup via IGameHostBuilder.UseStartup, IGameHostBuilder.Configure, injecting {nameof(IStartup)} or specifying the startup assembly via {nameof(GameHostDefaults.StartupAssemblyKey)} in the game host configuration.");
        }

        RequestDelegate BuildApplication()
        {
            try
            {
                _applicationServicesException?.Throw();
                EnsureServer();

                var builderFactory = Services.GetRequiredService<IApplicationBuilderFactory>();
                var builder = builderFactory.CreateBuilder(Server.Features);
                builder.ApplicationServices = Services;

                var startupFilters = Services.GetService<IEnumerable<IStartupFilter>>();
                Action<IApplicationBuilder> configure = _startup.Configure;
                foreach (var filter in startupFilters.Reverse())
                    configure = filter.Configure(configure);

                configure(builder);

                return builder.Build();
            }
            catch (Exception ex)
            {
                if (!Options.SuppressStatusMessages)
                    // Write errors to standard out so they can be retrieved when not in development mode.
                    Console.WriteLine("Application startup exception: " + ex.ToString());
                var logger = Services.GetRequiredService<ILogger<GameHost>>();
                logger.ApplicationError(ex);

                if (!Options.CaptureStartupErrors)
                    throw;

                EnsureServer();

                // Generate an HTML error page.
                var hostingEnv = Services.GetRequiredService<IHostEnvironment>();
                var showDetailedErrors = hostingEnv.IsDevelopment() || Options.DetailedErrors;

                var model = new ErrorPageModel
                {
                    RuntimeDisplayName = RuntimeInformation.FrameworkDescription
                };
                var systemRuntimeAssembly = typeof(System.ComponentModel.DefaultValueAttribute).GetTypeInfo().Assembly;
                var assemblyVersion = new AssemblyName(systemRuntimeAssembly.FullName).Version.ToString();
                var clrVersion = assemblyVersion;
                model.RuntimeArchitecture = RuntimeInformation.ProcessArchitecture.ToString();
                var currentAssembly = typeof(ErrorPage).GetTypeInfo().Assembly;
                model.CurrentAssemblyVesion = currentAssembly
                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                    .InformationalVersion;
                model.ClrVersion = clrVersion;
                model.OperatingSystemDescription = RuntimeInformation.OSDescription;

                //if (showDetailedErrors)
                //{
                //    var exceptionDetailProvider = new ExceptionDetailsProvider(
                //        hostingEnv.ContentRootFileProvider,
                //        sourceCodeLineCount: 6);

                //    model.ErrorDetails = exceptionDetailProvider.GetDetails(ex);
                //}
                //else
                //    model.ErrorDetails = new ExceptionDetails[0];

                var errorPage = new ErrorPage(model);
                return context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.Headers["Cache-Control"] = "no-cache";
                    return errorPage.ExecuteAsync(context);
                };
            }
        }

        void EnsureServer()
        {
            if (Server == null)
            {
                Server = Services.GetRequiredService<IServer>();

                var serverAddressesFeature = Server.Features?.Get<IServerAddressesFeature>();
                var addresses = serverAddressesFeature?.Addresses;
                if (addresses != null && !addresses.IsReadOnly && addresses.Count == 0)
                {
                    var urls = _config[GameHostDefaults.ServerUrlsKey] ?? _config[DeprecatedServerUrlsKey];
                    if (!string.IsNullOrEmpty(urls))
                    {
                        serverAddressesFeature.PreferHostingUrls = GameHostUtilities.ParseBool(_config, GameHostDefaults.PreferHostingUrlsKey);

                        foreach (var value in urls.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                            addresses.Add(value);
                    }
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_stopped)
                return;
            _stopped = true;

            _logger?.Shutdown();

            var timeoutToken = new CancellationTokenSource(Options.ShutdownTimeout).Token;
            if (!cancellationToken.CanBeCanceled)
                cancellationToken = timeoutToken;
            else
                cancellationToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutToken).Token;

            // Fire IApplicationLifetime.Stopping
            _applicationLifetime?.StopApplication();

            if (Server != null)
                await Server.StopAsync(cancellationToken).ConfigureAwait(false);

            // Fire the IHostedService.Stop
            if (_hostedServiceExecutor != null)
                await _hostedServiceExecutor.StopAsync(cancellationToken).ConfigureAwait(false);

            // Fire IApplicationLifetime.Stopped
            _applicationLifetime?.NotifyStopped();

            HostingEventSource.Log.HostStop();
        }

        public void Dispose() =>
            DisposeAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        public async ValueTask DisposeAsync()
        {
            if (!_stopped)
                try
                {
                    await StopAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger?.ServerShutdownException(ex);
                }

            await DisposeServiceProviderAsync(Services).ConfigureAwait(false);
            await DisposeServiceProviderAsync(_hostingServiceProvider).ConfigureAwait(false);
        }

        async ValueTask DisposeServiceProviderAsync(IServiceProvider serviceProvider)
        {
            switch (serviceProvider)
            {
#if NET3
                case IAsyncDisposable asyncDisposable: await asyncDisposable.DisposeAsync(); break;
#endif
                case IDisposable disposable: disposable.Dispose(); break;
            }
        }
    }
}
