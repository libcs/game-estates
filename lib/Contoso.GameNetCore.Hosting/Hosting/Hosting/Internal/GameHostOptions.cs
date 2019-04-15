// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Contoso.GameNetCore.Hosting.Internal
{
    public class GameHostOptions
    {
        public GameHostOptions() { }

        public GameHostOptions(IConfiguration configuration)
            : this(configuration, string.Empty) { }

        public GameHostOptions(IConfiguration configuration, string applicationNameFallback)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            ApplicationName = configuration[GameHostDefaults.ApplicationKey] ?? applicationNameFallback;
            StartupAssembly = configuration[GameHostDefaults.StartupAssemblyKey];
            DetailedErrors = GameHostUtilities.ParseBool(configuration, GameHostDefaults.DetailedErrorsKey);
            CaptureStartupErrors = GameHostUtilities.ParseBool(configuration, GameHostDefaults.CaptureStartupErrorsKey);
            Environment = configuration[GameHostDefaults.EnvironmentKey];
            GameRoot = configuration[GameHostDefaults.GameRootKey];
            ContentRootPath = configuration[GameHostDefaults.ContentRootKey];
            PreventHostingStartup = GameHostUtilities.ParseBool(configuration, GameHostDefaults.PreventHostingStartupKey);
            SuppressStatusMessages = GameHostUtilities.ParseBool(configuration, GameHostDefaults.SuppressStatusMessagesKey);

            // Search the primary assembly and configured assemblies.
            HostingStartupAssemblies = Split($"{ApplicationName};{configuration[GameHostDefaults.HostingStartupAssembliesKey]}");
            HostingStartupExcludeAssemblies = Split(configuration[GameHostDefaults.HostingStartupExcludeAssembliesKey]);

            var timeout = configuration[GameHostDefaults.ShutdownTimeoutKey];
            if (!string.IsNullOrEmpty(timeout)
                && int.TryParse(timeout, NumberStyles.None, CultureInfo.InvariantCulture, out var seconds))
            {
                ShutdownTimeout = TimeSpan.FromSeconds(seconds);
            }
        }

        public string ApplicationName { get; set; }

        public bool PreventHostingStartup { get; set; }

        public bool SuppressStatusMessages { get; set; }

        public IReadOnlyList<string> HostingStartupAssemblies { get; set; }

        public IReadOnlyList<string> HostingStartupExcludeAssemblies { get; set; }

        public bool DetailedErrors { get; set; }

        public bool CaptureStartupErrors { get; set; }

        public string Environment { get; set; }

        public string StartupAssembly { get; set; }

        public string GameRoot { get; set; }

        public string ContentRootPath { get; set; }

        public TimeSpan ShutdownTimeout { get; set; } = TimeSpan.FromSeconds(5);

        public IEnumerable<string> GetFinalHostingStartupAssemblies() =>
            HostingStartupAssemblies.Except(HostingStartupExcludeAssemblies, StringComparer.OrdinalIgnoreCase);

        IReadOnlyList<string> Split(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Array.Empty<string>();

            var list = new List<string>();
            foreach (var part in value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var trimmedPart = part;
                if (!string.IsNullOrEmpty(trimmedPart))
                    list.Add(trimmedPart);
            }
            return list;
        }
    }
}