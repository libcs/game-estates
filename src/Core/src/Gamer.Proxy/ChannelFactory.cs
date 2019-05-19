using Gamer.Proxy.Server;
using Microsoft.Extensions.Caching.Memory;
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
        readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

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
                var req = ctx.HttpRequest;
                if (req.Uri == "/")
                {
                    var res = new HttpResponse(200, "OK");
                    res.Headers.Add("Content-Type", "text/html");
                    res.Headers.Add("Connection", "close");
                    res.Content = "OK";
                    await ctx.ResponseChannel.Send(res, ctx.Token)
                         .ContinueWith(t => ctx.ResponseChannel.Close());
                }
                else if (req.Uri == ".stream")
                {
                    var res = new HttpResponse(200, "OK");
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
                // ESTATE
                else if (!req.Headers.TryGetValue("Estate", out var estateName) || !Estates.TryGetValue(estateName, out var estate))
                {
                    var res = new HttpResponse(404, "Not Found");
                    res.Headers.Add("Content-Type", "text/html");
                    res.Headers.Add("Connection", "close");
                    await ctx.ResponseChannel.Send(res, ctx.Token)
                         .ContinueWith(t => ctx.ResponseChannel.Close());
                }
                else await HandleEstate(ctx, req, estate);
            }
            var httpServer = new HttpServer(host, port, handler);
            channel.AttachServer(httpServer);
            httpServer.Run(certificate);
            return channel;
        }

        async Task HandleEstate(HttpContext ctx, HttpRequest req, IProxyHandler estate)
        {
            req.Headers.TryGetValue("Pack", out var pack);
            if (req.Uri.StartsWith("/asset/"))
            {
                var res = new HttpResponse(200, "OK");
                var val = req.Uri.Substring(7);
                var asset = await _cache.GetOrCreateAsync($"a:{pack}", async x => await estate.AssetPackFunc(new Uri(pack), () => res));
                if (val == ".set") asset.GetContainsSet();
                else await asset.LoadFileDataAsync(val);
                res.Headers.Add("Content-Type", "text/html");
                res.Headers.Add("Cache-Control", "no-cache");
                res.Headers.Add("Connection", "close");
                await ctx.ResponseChannel.Send(res, ctx.Token)
                    .ContinueWith(t => ctx.ResponseChannel.Close());
            }
            else if (req.Uri.StartsWith("/data/"))
            {
                var res = new HttpResponse(200, "OK");
                var val = req.Uri.Substring(6);
                var data = await _cache.GetOrCreateAsync($"d:{pack}", async x => await estate.DataPackFunc(new Uri(pack), () => res));
                res.Headers.Add("Content-Type", "text/html");
                res.Headers.Add("Cache-Control", "no-cache");
                res.Headers.Add("Connection", "close");
                await ctx.ResponseChannel.Send(res, ctx.Token)
                    .ContinueWith(t => ctx.ResponseChannel.Close());
            }
            else
            {
                var res = new HttpResponse(404, "Not Found");
                res.Headers.Add("Content-Type", "text/html");
                res.Headers.Add("Connection", "close");
                await ctx.ResponseChannel.Send(res, ctx.Token)
                     .ContinueWith(t => ctx.ResponseChannel.Close());
            }
        }

        static string GetContent(Stream stream)
        {
            using (var sr = new StreamReader(stream))
                return sr.ReadToEnd();
        }
    }
}
