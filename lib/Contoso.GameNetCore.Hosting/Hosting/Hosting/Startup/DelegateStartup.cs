// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Contoso.GameNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Contoso.GameNetCore.Hosting
{
    public class DelegateStartup : StartupBase<IServiceCollection>
    {
        private Action<IApplicationBuilder> _configureApp;

        public DelegateStartup(IServiceProviderFactory<IServiceCollection> factory, Action<IApplicationBuilder> configureApp) : base(factory)
        {
            _configureApp = configureApp;
        }

        public override void Configure(IApplicationBuilder app) => _configureApp(app);
    }
}