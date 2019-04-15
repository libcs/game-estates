// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ServiceProcess;

namespace Contoso.GameNetCore.Hosting.WindowsServices
{
    /// <summary>
    ///     Extensions to <see cref="IGameHost"/> for hosting inside a Windows service.
    /// </summary>
    public static class GameHostWindowsServiceExtensions
    {
        /// <summary>
        ///     Runs the specified game application inside a Windows service and blocks until the service is stopped.
        /// </summary>
        /// <param name="host">An instance of the <see cref="IGameHost"/> to host in the Windows service.</param>
        /// <example>
        ///     This example shows how to use <see cref="RunAsService"/>.
        ///     <code>
        ///         public class Program
        ///         {
        ///             public static void Main(string[] args)
        ///             {
        ///                 var config = GameHostConfiguration.GetDefault(args);
        ///                 
        ///                 var host = new GameHostBuilder()
        ///                     .UseConfiguration(config)
        ///                     .Build();
        ///          
        ///                 // This call will block until the service is stopped.
        ///                 host.RunAsService();
        ///             }
        ///         }
        ///     </code>
        /// </example>
        public static void RunAsService(this IGameHost host)
        {
            var gameHostService = new GameHostService(host);
            ServiceBase.Run(gameHostService);
        }
    }
}