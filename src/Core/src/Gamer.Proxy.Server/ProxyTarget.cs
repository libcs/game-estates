using Gamer.Proxy.Server;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Gamer.Proxy
{
    public class ProxyTarget : IDisposable
    {
        readonly ChannelFactory _channelFactory = new ChannelFactory();
        IEventChannel _channel;

        public bool Initialized { get; set; }
        public bool Active { get; set; } = true;
        public string Host { get; set; }
        public int Port { get; set; } = 3365;
        public string Certificate { get; set; }
        public int ReplayBufferSize { get; set; } = 10;

        public ProxyTarget() { }
        public ProxyTarget(ChannelFactory channelFactory) => _channelFactory = channelFactory;

        public IDictionary<string, object> Estates => _channelFactory.Estates;

        public void Initialize(IDictionary<string, object> estates = null)
        {
            if (estates != null)
                foreach (var estate in estates)
                    _channelFactory.Estates.Add(estate);
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