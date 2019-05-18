using System;
using System.Threading;

namespace Gamer.Proxy.Server
{
    /// <summary>
    /// Interface IEventChannel
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IEventChannel : IDisposable
    {
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="token">The token.</param>
        void Send(ServerSentEvent message, CancellationToken token);
    }
}
