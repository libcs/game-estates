// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Builder;

namespace Contoso.GameNetCore.Hosting.Internal
{
    internal interface ISupportsStartup
    {
        IGameHostBuilder Configure(Action<GameHostBuilderContext, IApplicationBuilder> configure);
        IGameHostBuilder UseStartup(Type startupType);
    }
}
