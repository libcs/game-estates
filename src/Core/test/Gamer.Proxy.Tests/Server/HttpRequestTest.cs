using NFluent;
using NUnit.Framework;

namespace Gamer.Proxy.Server
{
    public class HttpRequestTest
    {
        [Test]
        public void Should_parse_request()
        {
            // given
            var rawRequest = new[]
            {
                "GET / HTTP/1.1",
                "Host: localhost:8080"
            };
            // when
            var httpRequest = HttpRequest.Parse(rawRequest);
            // then
            Check.That(httpRequest).IsNotNull();
            Check.That(httpRequest.Method).IsEqualTo("GET");
            Check.That(httpRequest.Uri).IsEqualTo("/");
            Check.That(httpRequest.Headers).HasSize(1);
            Check.That(httpRequest.Headers["Host"]).IsEqualTo("localhost:8080");
        }
    }
}
