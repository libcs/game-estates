using Game.Core.Netstream.Server;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Game.Core.Netstream
{
    public class StreamTarget : IDisposable
    {
        readonly ChannelFactory _channelFactory = new ChannelFactory();
        IEventChannel _channel;

        public bool Initialized { get; set; }
        public bool Active { get; set; } = true;
        public string Host { get; set; }
        public int Port { get; set; } = 3365;
        public string Certificate { get; set; }
        public int ReplayBufferSize { get; set; } = 10;

        public StreamTarget() { }
        public StreamTarget(ChannelFactory channelFactory) => _channelFactory = channelFactory;

        public IDictionary<string, IEstateHandler> Estates => _channelFactory.Estates;

        public void Initialize(params IEstateHandler[] estates)
        {
            if (estates != null)
                foreach (var estate in estates)
                    _channelFactory.Estates.Add(estate.Key, estate);
            Initialized = true;
            _channel = Active ? _channelFactory.Create(Host, Port, HttpServer.FindCertificate(Certificate), ReplayBufferSize) : null;
        }

        public void Write(string type, string data) => _channel.Send(new ServerSentEvent(type, data), new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token);

        public void Emit(string type, string data)
        {
            if (!Initialized)
                Initialize();
            if (!Active)
                return;
            _channel.Send(new ServerSentEvent(type, data), new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token);
        }

        public void Dispose()
        {
            if (!Initialized)
                Initialize();
            _channel?.Dispose();
        }
    }
}