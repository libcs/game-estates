// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.ServiceProcess;
using Microsoft.Extensions.DependencyInjection;

namespace Contoso.GameNetCore.Hosting.WindowsServices
{
    /// <summary>
    ///     Provides an implementation of a Windows service that hosts GAME.NET Core.
    /// </summary>
    public class GameHostService : ServiceBase
    {
        readonly IGameHost _host;
        bool _stopRequestedByWindows;

        /// <summary>
        /// Creates an instance of <c>GameHostService</c> which hosts the specified game application.
        /// </summary>
        /// <param name="host">The configured game host containing the game application to host in the Windows service.</param>
        public GameHostService(IGameHost host)
        {
            _host = host ?? throw new ArgumentNullException(nameof(host));
        }

        /// <summary>
        /// This method is not intended for direct use. Its sole purpose is to allow
        /// the service to be started by the tests.
        /// </summary>
        internal void Start() => OnStart(Array.Empty<string>());

        protected sealed override void OnStart(string[] args)
        {
            OnStarting(args);

            _host.Start();

            OnStarted();

            // Register callback for application stopping after we've
            // started the service, because otherwise we might introduce unwanted
            // race conditions.
            _host
                .Services
                //.GetRequiredService<IHostApplicationLifetime>()
                .GetRequiredService<IApplicationLifetime>()
                .ApplicationStopping
                .Register(() =>
                {
                    if (!_stopRequestedByWindows)
                        Stop();
                });
        }

        protected sealed override void OnStop()
        {
            _stopRequestedByWindows = true;
            OnStopping();
            try
            {
                _host.StopAsync().GetAwaiter().GetResult();
            }
            finally
            {
                _host.Dispose();
                OnStopped();
            }
        }

        /// <summary>
        /// Executes before GAME.NET Core starts.
        /// </summary>
        /// <param name="args">The command line arguments passed to the service.</param>
        protected virtual void OnStarting(string[] args) { }

        /// <summary>
        /// Executes after GAME.NET Core starts.
        /// </summary>
        protected virtual void OnStarted() { }

        /// <summary>
        /// Executes before GAME.NET Core shuts down.
        /// </summary>
        protected virtual void OnStopping() { }

        /// <summary>
        /// Executes after GAME.NET Core shuts down.
        /// </summary>
        protected virtual void OnStopped() { }
    }
}