// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Contoso.GameNetCore.Hosting.Internal
{
    internal interface ISupportsUseDefaultServiceProvider
    {
        IGameHostBuilder UseDefaultServiceProvider(Action<GameHostBuilderContext, ServiceProviderOptions> configure);
    }
}
