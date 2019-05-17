using System.Collections.Generic;
using System.Threading;

namespace Gamer.Proxy.Server
{
    /// <summary>
    /// Class MulticastChannel.
    /// </summary>
    /// <seealso cref="Logstream.Server.IEventChannel" />
    public class MulticastChannel : IEventChannel
    {
        readonly IList<IEventChannel> _channels = new List<IEventChannel>();
        readonly object _syncRoot = new object();
        readonly int _replayBufferSize;
        readonly IList<ServerSentEvent> _replayBuffer;
        HttpServer _httpServer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MulticastChannel"/> class.
        /// </summary>
        /// <param name="replayBufferSize">Size of the replay buffer.</param>
        public MulticastChannel(int replayBufferSize = 1)
        {
            _replayBufferSize = replayBufferSize;
            _replayBuffer = new List<ServerSentEvent>(replayBufferSize);
        }

        /// <summary>
        /// Adds the channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="token">The token.</param>
        public void AddChannel(IEventChannel channel, CancellationToken token)
        {
            lock (_syncRoot)
            {
                _channels.Add(channel);
                foreach (var message in _replayBuffer)
                    try { channel.Send(message, token); }
                    catch { _channels.Remove(channel); }
            }
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="token">The token.</param>
        public void Send(ServerSentEvent message, CancellationToken token)
        {
            lock (_syncRoot)
            {
                var closeChannels = new List<IEventChannel>();
                foreach (var channel in _channels)
                    try { channel.Send(message, token); }
                    catch { closeChannels.Add(channel); }
                foreach (var channel in closeChannels)
                    _channels.Remove(channel);
                while (_replayBuffer.Count >= _replayBufferSize)
                    _replayBuffer.RemoveAt(0);
                _replayBuffer.Add(message);
            }
        }

        /// <summary>
        /// Attaches the server.
        /// </summary>
        /// <param name="httpServer">The HTTP server.</param>
        public void AttachServer(HttpServer httpServer) => _httpServer = httpServer;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_httpServer != null)
                _httpServer.Dispose();
            foreach (var channel in _channels)
                channel.Dispose();
        }
    }
}
