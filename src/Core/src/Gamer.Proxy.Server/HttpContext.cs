using System.Threading;

namespace Gamer.Proxy.Server
{
    /// <summary>
    /// Class HttpContext.
    /// </summary>
    public class HttpContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpContext"/> class.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="responseChannel">The response channel.</param>
        /// <param name="token">The token.</param>
        public HttpContext(HttpRequest httpRequest, HttpResponseChannel responseChannel, CancellationToken token)
        {
            HttpRequest = httpRequest;
            ResponseChannel = responseChannel;
            Token = token;
        }

        /// <summary>
        /// Gets the HTTP request.
        /// </summary>
        /// <value>The HTTP request.</value>
        public HttpRequest HttpRequest { get; }
        /// <summary>
        /// Gets the response channel.
        /// </summary>
        /// <value>The response channel.</value>
        public HttpResponseChannel ResponseChannel { get; }
        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>The token.</value>
        public CancellationToken Token { get; }
    }
}
