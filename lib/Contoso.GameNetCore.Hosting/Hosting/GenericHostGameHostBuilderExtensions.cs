// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Contoso.GameNetCore.Hosting;
using Contoso.GameNetCore.Hosting.Internal;
using Microsoft.Extensions.Hosting;
using System;

namespace Contoso.Extensions.Hosting
{
    public static class GenericHostGameHostBuilderExtensions
    {
        public static IHostBuilder ConfigureGameHost(this IHostBuilder builder, Action<IGameHostBuilder> configure)
        {
            var gamehostBuilder = new GenericGameHostBuilder(builder);
            configure(gamehostBuilder);
            return builder;
        }
    }
}
