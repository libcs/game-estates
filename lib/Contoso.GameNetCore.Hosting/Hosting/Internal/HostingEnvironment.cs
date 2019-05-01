// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.FileProviders;

namespace Contoso.GameNetCore.Hosting.Internal
{
#pragma warning disable CS0618 // Type or member is obsolete
#if !NET3
    public class HostingEnvironment : IHostingEnvironment, IGameHostEnvironment
#else
    public class HostingEnvironment : IHostingEnvironment, Extensions.Hosting.IHostingEnvironment, IGameHostEnvironment
#endif
#pragma warning restore CS0618 // Type or member is obsolete
    {

        public string EnvironmentName { get; set; }
#if !NET3
            = "Production";
#else
            = Extensions.Hosting.Environments.Production;
#endif
        public string ApplicationName { get; set; }

        public string GameRootPath { get; set; }

        public IFileProvider GameRootFileProvider { get; set; }

        public string ContentRootPath { get; set; }

        public IFileProvider ContentRootFileProvider { get; set; }
    }
}
