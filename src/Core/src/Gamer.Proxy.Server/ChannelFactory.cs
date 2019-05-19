using Gamer.Proxy.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

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
        public IDictionary<string, IProxyHandler> Estates { get; } = new Dictionary<string, IProxyHandler>();

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
            async Task handler(HttpContext ctx)
            {
                var req = ctx.HttpRequest; var res = new HttpResponse(200, "OK");
                if (req.Uri == "/")
                {
                    res.Headers.Add("Content-Type", "text/html");
                    res.Headers.Add("Connection", "close");
                    res.Content = "OK";
                    await ctx.ResponseChannel.Send(res, ctx.Token)
                         .ContinueWith(t => ctx.ResponseChannel.Close());
                }
                else if (!req.Headers.TryGetValue("Est", out var est)) throw new InvalidDataException("Est");
                else if (req.Uri == ".stream")
                {
                    res.Headers.Add("Content-Type", "text/event-stream");
                    res.Headers.Add("Cache-Control", "no-cache");
                    res.Headers.Add("Connection", "keep-alive");
                    res.Headers.Add("Access-Control-Allow-Origin", "*");
                    await ctx.ResponseChannel.Send(res, ctx.Token)
                        .ContinueWith(t =>
                        {
                            ctx.ResponseChannel.Send(new ServerSentEvent("INFO", $"Connected successfully on LOG stream from {host}:{port}"), ctx.Token);
                            channel.AddChannel(ctx.ResponseChannel, ctx.Token);
                        });
                }
                else if (Estates.TryGetValue(est, out var estate))
                    await HandleEstate(ctx, req, res, estate);
            }
            var httpServer = new HttpServer(host, port, handler);
            channel.AttachServer(httpServer);
            httpServer.Run(certificate);
            return channel;
        }

        static async Task HandleEstate(HttpContext ctx, HttpRequest req, HttpResponse res, IProxyHandler estate)
        {
            //req.Uri
            string content;
            req.Headers.TryGetValue("Val", out var val);
            switch (req.Headers.TryGetValue("Obj", out var obj) ? obj : null)
            {
                case "Asset":
                    var asset = await estate.AssetPackFunc(new Uri(""));
                    asset.ContainsFile(val);
                    await asset.LoadFileDataAsync(val);
                    content = "";
                    break;
                case "Data":
                    var data = await estate.DataPackFunc(new Uri(""));
                    content = "";
                    break;
                default: throw new ArgumentOutOfRangeException("Obj", obj);
            }
            res.Headers.Add("Content-Type", "text/html");
            res.Headers.Add("Cache-Control", "no-cache");
            res.Headers.Add("Connection", "close");
            res.Content = content;
            await ctx.ResponseChannel.Send(res, ctx.Token)
                .ContinueWith(t => ctx.ResponseChannel.Close());
        }

        static string GetContent(Stream stream)
        {
            using (var sr = new StreamReader(stream))
                return sr.ReadToEnd();
        }
    }
}
