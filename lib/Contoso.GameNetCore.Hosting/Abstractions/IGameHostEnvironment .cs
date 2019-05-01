// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.FileProviders;
#if !NET3
using IHostEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;
#endif

namespace Contoso.GameNetCore.Hosting
{
    /// <summary>
    /// Provides information about the game hosting environment an application is running in.
    /// </summary>
    public interface IGameHostEnvironment : IHostEnvironment
    {
        /// <summary>
        /// Gets or sets the absolute path to the directory that contains the game-servable application content files.
        /// </summary>
        string GameRootPath { get; set; }

        /// <summary>
        /// Gets or sets an <see cref="IFileProvider"/> pointing at <see cref="GameRootPath"/>.
        /// </summary>
        IFileProvider GameRootFileProvider { get; set; }
    }
}