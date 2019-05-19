using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gamer.Proxy.Server
{
    /// <summary>
    /// Class HttpResponseChannel.
    /// </summary>
    /// <seealso cref="Logstream.Server.IEventChannel" />
    public class HttpResponseChannel : IEventChannel
    {
        readonly TcpClient _tcpClient;
        readonly Stream _stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseChannel"/> class.
        /// </summary>
        /// <param name="tcpClient">The TCP client.</param>
        /// <param name="stream">The stream.</param>
        public HttpResponseChannel(TcpClient tcpClient, Stream stream)
        {
            _tcpClient = tcpClient;
            _stream = stream;
        }

        /// <summary>
        /// Sends the specified sse.
        /// </summary>
        /// <param name="sse">The sse.</param>
        /// <param name="token">The token.</param>
        public void Send(ServerSentEvent sse, CancellationToken token) => Send((object)sse, token);
        /// <summary>
        /// Sends the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task.</returns>
        public Task Send(object obj, CancellationToken token)
        {
            var content = Encoding.UTF8.GetBytes(obj.ToString());
            var task = _stream.WriteAsync(content, 0, content.Length, token);
            if (obj is HttpResponse res && res.ContentBytes != null)
            {
                var contentBytes = res.ContentBytes; res.ContentBytes = null;
                task = task.ContinueWith(t => _stream.WriteAsync(contentBytes, 0, contentBytes.Length, token));
            }
            return task.ContinueWith(t => _stream.FlushAsync(token), token);
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            _stream.Close();
            _tcpClient.Close();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => Close();
    }
}
