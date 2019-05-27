using NFluent;
using NUnit.Framework;
using System;
using System.Linq;

namespace Gamer.Proxy.Server
{
    public class HttpResponseTest
    {
        [Test]
        public void Should_build_response_with_status_code()
        {
            // given
            var response = new HttpResponse(404, "NOT FOUND");
            // when
            var textResponse = response.ToString();
            // then
            Check.That(textResponse).StartsWith("HTTP/1.1 404");
        }

        [Test]
        public void Should_build_response_with_headers()
        {
            // given
            var response = new HttpResponse(404, "NOT FOUND");
            response.Headers.Add("header", "value");
            // when
            var textResponse = response.ToString();
            // then
            Check.That(textResponse).Contains("header: value");
        }

    }
}
