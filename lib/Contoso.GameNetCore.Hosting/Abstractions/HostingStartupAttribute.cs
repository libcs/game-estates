// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Reflection;

namespace Contoso.GameNetCore.Hosting
{
    /// <summary>
    /// Marker attribute indicating an implementation of <see cref="IHostingStartup"/> that will be loaded and executed when building an <see cref="IGameHost"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
    public sealed class HostingStartupAttribute : Attribute
    {
        /// <summary>
        /// Constructs the <see cref="HostingStartupAttribute"/> with the specified type.
        /// </summary>
        /// <param name="hostingStartupType">A type that implements <see cref="IHostingStartup"/>.</param>
        public HostingStartupAttribute(Type hostingStartupType)
        {
            if (hostingStartupType == null)
                throw new ArgumentNullException(nameof(hostingStartupType));
            if (!IntrospectionExtensions.GetTypeInfo(typeof(IHostingStartup)).IsAssignableFrom(IntrospectionExtensions.GetTypeInfo(hostingStartupType)))
                throw new ArgumentException($"\"{hostingStartupType}\" does not implement {typeof(IHostingStartup)}.", nameof(hostingStartupType));
            HostingStartupType = hostingStartupType;
        }

        /// <summary>
        /// The implementation of <see cref="IHostingStartup"/> that should be loaded when 
        /// starting an application.
        /// </summary>
        public Type HostingStartupType { get; }
    }
}
