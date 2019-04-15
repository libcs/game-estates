// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.Configuration;

namespace Contoso.GameNetCore.Hosting
{
    /// <summary>
    /// Context containing the common services on the <see cref="IGameHost" />. Some properties may be null until set by the <see cref="IGameHost" />.
    /// </summary>
    public class GameHostBuilderContext
    {
        /// <summary>
        /// The <see cref="IGameHostEnvironment" /> initialized by the <see cref="IGameHost" />.
        /// </summary>
        public IGameHostEnvironment HostingEnvironment { get; set; }

        /// <summary>
        /// The <see cref="IConfiguration" /> containing the merged configuration of the application and the <see cref="IGameHost" />.
        /// </summary>
        public IConfiguration Configuration { get; set; }
    }
}
