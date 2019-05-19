using Gamer.Proxy.Server;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Gamer.Proxy
{
    /// <summary>
    /// Class ChannelFactory.
    /// </summary>
    public class ChannelFactory
    {
        /// <summary>
        /// Gets the estates.
        /// </summary>
        /// <value>
        /// The estates.
        /// </value>
        public IDictionary<string, object> Estates { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Creates the specified host.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <param name="certificate">The certificate.</param>
        /// <param name="replayBufferSize">Size of the replay buffer.</param>
        /// <returns>IEventChannel.</returns>
        public virtual IEventChannel Create(string host, int port = 3365, X509Certificate certificate = null, int replayBufferSize = 1)
        {
            var channel = new MulticastChannel(replayBufferSize);
            void handler(HttpContext ctx)
            {
                var req = ctx.HttpRequest; var res = new HttpResponse(200, "OK");
                if (req.Uri == "/")
                {
                    res.Headers.Add("Content-Type", "text/html");
                    res.Headers.Add("Connection", "close");
                    res.Content = "OK";
                    ctx.ResponseChannel.Send(res, ctx.Token)
                        .ContinueWith(t => ctx.ResponseChannel.Close());
                }
                else if (!req.Headers.TryGetValue("Est", out var est)) throw new InvalidDataException("Est");
                else if (req.Uri == ".stream")
                {
                    res.Headers.Add("Content-Type", "text/event-stream");
                    res.Headers.Add("Cache-Control", "no-cache");
                    res.Headers.Add("Connection", "keep-alive");
                    res.Headers.Add("Access-Control-Allow-Origin", "*");
                    ctx.ResponseChannel.Send(res, ctx.Token)
                        .ContinueWith(t =>
                        {
                            ctx.ResponseChannel.Send(new ServerSentEvent("INFO", $"Connected successfully on LOG stream from {host}:{port}"), ctx.Token);
                            channel.AddChannel(ctx.ResponseChannel, ctx.Token);
                        });
                }
                else if (Estates.TryGetValue(est, out var estate))
                {
                    res.Headers.Add("Content-Type", "text/html");
                    res.Headers.Add("Cache-Control", "no-cache");
                    res.Headers.Add("Connection", "close");
                    res.Content = "OK";
                    ctx.ResponseChannel.Send(res, ctx.Token)
                        .ContinueWith(t => ctx.ResponseChannel.Close());
                }
            }
            var httpServer = new HttpServer(host, port, handler);
            channel.AttachServer(httpServer);
            httpServer.Run(certificate);
            return channel;
        }

        static string GetContent(Stream stream)
        {
            using (var sr = new StreamReader(stream))
                return sr.ReadToEnd();
        }
    }
}
