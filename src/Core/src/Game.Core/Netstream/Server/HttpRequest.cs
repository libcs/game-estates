using System.Collections.Generic;
using System.Linq;

namespace Game.Core.Netstream.Server
{
    /// <summary>
    /// Class HttpRequest.
    /// </summary>
    public class HttpRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequest"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="method">The method.</param>
        /// <param name="headers">The headers.</param>
        public HttpRequest(string uri, string method, IDictionary<string, string> headers)
        {
            Uri = uri;
            Method = method;
            Headers = headers;
        }

        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <value>The URI.</value>
        public string Uri { get; }
        /// <summary>
        /// Gets the method.
        /// </summary>
        /// <value>The method.</value>
        public string Method { get; }
        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <value>The headers.</value>
        public IDictionary<string, string> Headers { get; }

        /// <summary>
        /// Parses the specified lines.
        /// </summary>
        /// <param name="lines">The lines.</param>
        /// <returns>HttpRequest.</returns>
        public static HttpRequest Parse(IEnumerable<string> lines)
        {
            var firstLine = lines.FirstOrDefault();
            if (firstLine == null)
                return null;
            var spaceIndex = firstLine.IndexOf(' ');
            var lastSpaceIndex = firstLine.LastIndexOf(' ');
            var method = firstLine.Substring(0, spaceIndex).Trim().ToUpper();
            var uri = firstLine.Substring(spaceIndex, lastSpaceIndex - spaceIndex).Trim();
            var headers = new Dictionary<string, string>();
            foreach (var line in lines.Skip(1))
            {
                var separatorIndex = line.IndexOf(':');
                var headerName = line.Substring(0, separatorIndex).Trim();
                var headerValue = line.Substring(separatorIndex + 1).Trim();
                headers.Add(headerName, headerValue);
            }
            return new HttpRequest(uri, method, headers);
        }
    }
}
