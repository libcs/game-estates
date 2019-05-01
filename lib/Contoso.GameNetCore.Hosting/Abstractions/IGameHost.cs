// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http.Features;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contoso.GameNetCore.Hosting
{
    /// <summary>
    /// Represents a configured game host.
    /// </summary>
    public interface IGameHost : IDisposable
    {
        /// <summary>
        /// The <see cref="IFeatureCollection"/> exposed by the configured server.
        /// </summary>
        IFeatureCollection ServerFeatures { get; }

        /// <summary>
        /// The <see cref="IServiceProvider"/> for the host.
        /// </summary>
        IServiceProvider Services { get; }

        /// <summary>
        /// Starts listening on the configured addresses.
        /// </summary>
        void Start();

        /// <summary>
        /// Starts listening on the configured addresses.
        /// </summary>
        Task StartAsync(CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        /// Attempt to gracefully stop the host.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StopAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}