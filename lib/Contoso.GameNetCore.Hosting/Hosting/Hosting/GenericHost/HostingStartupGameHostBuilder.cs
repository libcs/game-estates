// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Contoso.GameNetCore.Hosting.Internal
{
    // We use this type to capture calls to the IGameHostBuilder so the we can properly order calls to 
    // to GenericHostGameHostBuilder.
    internal class HostingStartupGameHostBuilder : IGameHostBuilder, ISupportsStartup, ISupportsUseDefaultServiceProvider
    {
        readonly GenericGameHostBuilder _builder;
        Action<GameHostBuilderContext, IConfigurationBuilder> _configureConfiguration;
        Action<GameHostBuilderContext, IServiceCollection> _configureServices;

        public HostingStartupGameHostBuilder(GenericGameHostBuilder builder) =>
            _builder = builder;

        public IGameHost Build() =>
            throw new NotSupportedException($"Building this implementation of {nameof(IGameHostBuilder)} is not supported.");

        public IGameHostBuilder ConfigureAppConfiguration(Action<GameHostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            _configureConfiguration += configureDelegate;
            return this;
        }

        public IGameHostBuilder ConfigureServices(Action<IServiceCollection> configureServices) =>
            ConfigureServices((context, services) => configureServices(services));

        public IGameHostBuilder ConfigureServices(Action<GameHostBuilderContext, IServiceCollection> configureServices)
        {
            _configureServices += configureServices;
            return this;
        }

        public string GetSetting(string key) => _builder.GetSetting(key);

        public IGameHostBuilder UseSetting(string key, string value)
        {
            _builder.UseSetting(key, value);
            return this;
        }

        public void ConfigureServices(GameHostBuilderContext context, IServiceCollection services) =>
            _configureServices?.Invoke(context, services);

        public void ConfigureAppConfiguration(GameHostBuilderContext context, IConfigurationBuilder builder) =>
            _configureConfiguration?.Invoke(context, builder);

        public IGameHostBuilder UseDefaultServiceProvider(Action<GameHostBuilderContext, ServiceProviderOptions> configure) =>
            _builder.UseDefaultServiceProvider(configure);

        public IGameHostBuilder Configure(Action<GameHostBuilderContext, IApplicationBuilder> configure) =>
            _builder.Configure(configure);

        public IGameHostBuilder UseStartup(Type startupType) =>
            _builder.UseStartup(startupType);
    }
}
