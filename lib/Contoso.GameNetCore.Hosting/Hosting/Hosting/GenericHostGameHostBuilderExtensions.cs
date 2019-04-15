// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Contoso.GameNetCore.Hosting;
using Contoso.GameNetCore.Hosting.Internal;

namespace Contoso.Extensions.Hosting
{
    public static class GenericHostGameHostBuilderExtensions
    {
        public static IHostBuilder ConfigureWebHost(this IHostBuilder builder, Action<IWebHostBuilder> configure)
        {
            var webhostBuilder = new GenericGameHostBuilder(builder);
            configure(webhostBuilder);
            return builder;
        }
    }
}
