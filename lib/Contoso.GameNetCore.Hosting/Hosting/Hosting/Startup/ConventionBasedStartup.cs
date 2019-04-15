// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Contoso.GameNetCore.Hosting;
using Contoso.GameNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Microsoft.AspNetCore.Hosting.Internal
{
    public class ConventionBasedStartup : IStartup
    {
        readonly StartupMethods _methods;

        public ConventionBasedStartup(StartupMethods methods) =>
            _methods = methods;

        public void Configure(IApplicationBuilder app)
        {
            try
            {
                _methods.ConfigureDelegate(app);
            }
            catch (Exception ex)
            {
                if (ex is TargetInvocationException)
                {
                    ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                }

                throw;
            }
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            try
            {
                return _methods.ConfigureServicesDelegate(services);
            }
            catch (Exception ex)
            {
                if (ex is TargetInvocationException)
                {
                    ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                }

                throw;
            }
        }
    }
}
