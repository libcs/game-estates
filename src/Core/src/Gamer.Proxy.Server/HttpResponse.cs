using System.Collections.Generic;
using System.Text;

namespace Gamer.Proxy.Server
{
    /// <summary>
    /// Class HttpResponse.
    /// </summary>
    public class HttpResponse
    {
        readonly int _statusCode;
        readonly string _statusDescription;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponse"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="statusDescription">The status description.</param>
        public HttpResponse(int statusCode = 0, string statusDescription = null)
        {
            _statusCode = statusCode;
            _statusDescription = statusDescription;
            Headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <value>The headers.</value>
        public IDictionary<string, string> Headers { get; }
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var b = new StringBuilder("HTTP/1.1 " + _statusCode + " " + _statusDescription + "\r\n");
            if (Content != null)
            {
                var byteCount = Encoding.UTF8.GetByteCount(Content);
                Headers.Add("Content-Length", byteCount.ToString());
            }
            foreach (var header in Headers)
                b.Append(header.Key + ": " + header.Value + "\r\n");
            b.Append("\r\n");
            if (Content != null)
                b.Append(Content);
            return b.ToString();
        }
    }
}
