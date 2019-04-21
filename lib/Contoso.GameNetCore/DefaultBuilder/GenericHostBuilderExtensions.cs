using Contoso.GameNetCore;
using Contoso.GameNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace Contoso.Extensions.Hosting
{
    /// <summary>
    /// Extension methods for configuring the IWebHostBuilder.
    /// </summary>
    public static class GenericHostBuilderExtensions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IGameHostBuilder"/> class with pre-configured defaults.
        /// </summary>
        /// <remarks>
        ///   The following defaults are applied to the <see cref="IGameHostBuilder"/>:
        ///     use Kestrel as the game server and configure it using the application's configuration providers.
        /// </remarks>
        /// <param name="builder">The <see cref="IHostBuilder" /> instance to configure</param>
        /// <param name="configure">The configure callback</param>
        /// <returns></returns>
        public static IHostBuilder ConfigureGameHostDefaults(this IHostBuilder builder, Action<IGameHostBuilder> configure)
        {
            return builder.ConfigureGameHost(gameHostBuilder =>
            {
                GameHost.ConfigureGameDefaults(gameHostBuilder);

                configure(gameHostBuilder);
            });
        }
    }
}
