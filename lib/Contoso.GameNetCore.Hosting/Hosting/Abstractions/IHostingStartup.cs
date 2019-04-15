// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Contoso.GameNetCore.Hosting
{
    /// <summary>
    /// Represents platform specific configuration that will be applied to a <see cref="IGameHostBuilder"/> when building an <see cref="IGameHost"/>.
    /// </summary>
    public interface IHostingStartup
    {
        /// <summary>
        /// Configure the <see cref="IGameHostBuilder"/>.
        /// </summary>
        /// <remarks>
        /// Configure is intended to be called before user code, allowing a user to overwrite any changes made.
        /// </remarks>
        /// <param name="builder"></param>
        void Configure(IGameHostBuilder builder);
    }
}
