// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace Contoso.GameNetCore.Hosting.Internal
{
    public static class HostingEnvironmentExtensions
    {
#pragma warning disable CS0618 // Type or member is obsolete
        public static void Initialize(this IHostingEnvironment hostingEnvironment, string contentRootPath, GameHostOptions options)
#pragma warning restore CS0618 // Type or member is obsolete
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (string.IsNullOrEmpty(contentRootPath))
                throw new ArgumentException("A valid non-empty content root must be provided.", nameof(contentRootPath));
            if (!Directory.Exists(contentRootPath))
                throw new ArgumentException($"The content root '{contentRootPath}' does not exist.", nameof(contentRootPath));

            hostingEnvironment.ApplicationName = options.ApplicationName;
            hostingEnvironment.ContentRootPath = contentRootPath;
            hostingEnvironment.ContentRootFileProvider = new PhysicalFileProvider(hostingEnvironment.ContentRootPath);

            var gameRoot = options.GameRoot;
            if (gameRoot == null)
            {
                // Default to /wwwroot if it exists.
                var gameroot = Path.Combine(hostingEnvironment.ContentRootPath, "gameroot");
                if (Directory.Exists(gameroot))
                    hostingEnvironment.GameRootPath = gameroot;
            }
            else
                hostingEnvironment.GameRootPath = Path.Combine(hostingEnvironment.ContentRootPath, gameRoot);

            if (!string.IsNullOrEmpty(hostingEnvironment.GameRootPath))
            {
                hostingEnvironment.GameRootPath = Path.GetFullPath(hostingEnvironment.GameRootPath);
                if (!Directory.Exists(hostingEnvironment.GameRootPath))
                    Directory.CreateDirectory(hostingEnvironment.GameRootPath);
                hostingEnvironment.GameRootFileProvider = new PhysicalFileProvider(hostingEnvironment.GameRootPath);
            }
            else
                hostingEnvironment.GameRootFileProvider = new NullFileProvider();

            hostingEnvironment.EnvironmentName =
                options.Environment ??
                hostingEnvironment.EnvironmentName;
        }

        public static void Initialize(this IGameHostEnvironment hostingEnvironment, string contentRootPath, GameHostOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (string.IsNullOrEmpty(contentRootPath))
                throw new ArgumentException("A valid non-empty content root must be provided.", nameof(contentRootPath));
            if (!Directory.Exists(contentRootPath))
                throw new ArgumentException($"The content root '{contentRootPath}' does not exist.", nameof(contentRootPath));

            hostingEnvironment.ApplicationName = options.ApplicationName;
            hostingEnvironment.ContentRootPath = contentRootPath;
            hostingEnvironment.ContentRootFileProvider = new PhysicalFileProvider(hostingEnvironment.ContentRootPath);

            var webRoot = options.GameRoot;
            if (webRoot == null)
            {
                // Default to /wwwroot if it exists.
                var gameroot = Path.Combine(hostingEnvironment.ContentRootPath, "gameroot");
                if (Directory.Exists(gameroot))
                    hostingEnvironment.GameRootPath = gameroot;
            }
            else
                hostingEnvironment.GameRootPath = Path.Combine(hostingEnvironment.ContentRootPath, webRoot);

            if (!string.IsNullOrEmpty(hostingEnvironment.GameRootPath))
            {
                hostingEnvironment.GameRootPath = Path.GetFullPath(hostingEnvironment.GameRootPath);
                if (!Directory.Exists(hostingEnvironment.GameRootPath))
                    Directory.CreateDirectory(hostingEnvironment.GameRootPath);
                hostingEnvironment.GameRootFileProvider = new PhysicalFileProvider(hostingEnvironment.GameRootPath);
            }
            else
                hostingEnvironment.GameRootFileProvider = new NullFileProvider();

            hostingEnvironment.EnvironmentName =
                options.Environment ??
                hostingEnvironment.EnvironmentName;
        }
    }
}
