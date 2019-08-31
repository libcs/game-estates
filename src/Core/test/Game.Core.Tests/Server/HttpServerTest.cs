using NFluent;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Core.Netstream.Server
{
    public class HttpServerTest
    {
        [Test]
        public async Task Should_call_handler_on_request()
        {
            // given
            var port = HttpServer.FindFreeTcpPort();
            var server = BuildServer(port, "content");
            // when
            var task = Task.Run(() => server.Run());

            // then
            var httpClient = new HttpClient();
            var urlPrefix = $"http://localhost:{port}/";
            var content = await httpClient.GetStringAsync(urlPrefix);
            Check.That(content).Contains("content");
        }

        [Test]
        public async Task Should_stop_handling_requests_after_dispose()
        {
            // given
            var port = HttpServer.FindFreeTcpPort();
            var server = BuildServer(port, "content");
            // when
            var serverTask = Task.Run(() => server.Run());
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            server.Dispose();

            // then
            var httpClient = new HttpClient();
            var urlPrefix = $"http://localhost:{port}/";

            Check.ThatCode(() => httpClient.GetStringAsync(urlPrefix).Wait()).ThrowsAny();
        }

        [Test]
        public void Should_throw_exception_running_a_disposed_server()
        {
            // given
            var port = HttpServer.FindFreeTcpPort();
            var server = BuildServer(port, "content");
            // when
            server.Dispose();

            // then
            var serverTask = Task.Run(() => server.Run());
            Check.ThatCode(() => server.Run()).Throws<InvalidOperationException>();
        }

        static HttpServer BuildServer(int port, string content) =>
            new HttpServer("127.0.0.1", port, ctx =>
            {
                var response = new HttpResponse(200, "OK");
                response.Headers.Add("Content-Type", "text/html; charset=utf-8");
                response.Headers.Add("Connection", "close");
                response.Headers.Add("Date", DateTime.UtcNow.ToString("r"));
                response.Content = content;
                ctx.ResponseChannel.Send(response, CancellationToken.None);
                return null;
            });
    }
}
