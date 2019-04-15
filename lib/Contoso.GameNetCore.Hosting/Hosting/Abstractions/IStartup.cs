// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Contoso.GameNetCore.Hosting
{
    public interface IStartup
    {
        IServiceProvider ConfigureServices(IServiceCollection services);

        void Configure(IApplicationBuilder app);
    }
}