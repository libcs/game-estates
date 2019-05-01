// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.FileProviders;

namespace Contoso.GameNetCore.Hosting
{
    /// <summary>
    /// Provides information about the game hosting environment an application is running in.
    /// <para>
    ///  This type is obsolete and will be removed in a future version.
    ///  The recommended alternative is Contoso.GameNetCore.Hosting.IGameHostEnvironment.
    /// </para>
    /// </summary>
    [System.Obsolete("This type is obsolete and will be removed in a future version. The recommended alternative is Contoso.GameNetCore.Hosting.IGameHostEnvironment.", error: false)]
    public interface IHostingEnvironment
    {
        /// <summary>
        /// Gets or sets the name of the environment. The host automatically sets this property to the value
        /// of the "GAMENETCORE_ENVIRONMENT" environment variable, or "environment" as specified in any other configuration source.
        /// </summary>
        string EnvironmentName { get; set; }

        /// <summary>
        /// Gets or sets the name of the application. This property is automatically set by the host to the assembly containing
        /// the application entry point.
        /// </summary>
        string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the absolute path to the directory that contains the game-servable application content files.
        /// </summary>
        string GameRootPath { get; set; }

        /// <summary>
        /// Gets or sets an <see cref="IFileProvider"/> pointing at <see cref="GameRootPath"/>.
        /// </summary>
        IFileProvider ContentRootFileProvider { get; set; }

        /// <summary>
        /// Gets or sets the absolute path to the directory that contains the application content files.
        /// </summary>
        string ContentRootPath { get; set; }

        /// <summary>
        /// Gets or sets an <see cref="IFileProvider"/> pointing at <see cref="ContentRootPath"/>.
        /// </summary>
        IFileProvider GameRootFileProvider { get; set; }
    }
}
