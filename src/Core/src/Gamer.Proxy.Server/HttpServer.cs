using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Gamer.Proxy.Server
{
    /// <summary>
    /// Class HttpServer.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class HttpServer : IDisposable
    {
        readonly int _port;
        readonly IPAddress _host;
        readonly Action<HttpContext> _handler;
        volatile bool _running;
        volatile bool _disposed;
        X509Certificate _serverCertificate;
        TcpListener _server;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpServer"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <param name="handler">The handler.</param>
        public HttpServer(string host, int port, Action<HttpContext> handler)
        {
            _port = port;
            _handler = handler;
            _host = host != null ? IPAddress.Parse(host) : null;
        }

        /// <summary>
        /// Runs the specified certificate.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        /// <param name="delay">The delay.</param>
        /// <exception cref="InvalidOperationException">Cannot run on a disposed server</exception>
        public void Run(X509Certificate certificate = null, TimeSpan? delay = null)
        {
            if (_disposed)
                throw new InvalidOperationException("Cannot run on a disposed server");
            _serverCertificate = certificate;
            _server = new TcpListener(_host ?? IPAddress.Any, _port);
            _server.Start();
            _running = true;
            Task.Run(async () =>
            {
                while (_running)
                {
                    var client = await _server.AcceptTcpClientAsync();
                    var cancelTokenSource = new CancellationTokenSource(delay ?? TimeSpan.FromSeconds(5)); // todo configuration
                    var task = Task.Run(async () => await ProcessClientAsync(client, cancelTokenSource), cancelTokenSource.Token);
                }
            });
        }

        async Task ProcessClientAsync(TcpClient client, CancellationTokenSource cancelTokenSource)
        {
            var stream = (Stream)client.GetStream();
            var sslStream = _serverCertificate != null ? new SslStream(stream, false) : null;
            try
            {
                if (sslStream != null)
                {
                    sslStream.AuthenticateAsServer(_serverCertificate, false, SslProtocols.Tls12, true);
                    //DisplaySslStream(sslStream);
                    //sslStream.ReadTimeout = 5000;
                    //sslStream.WriteTimeout = 5000;
                    stream = sslStream;
                }
                var lines = await new LineParser().Parse(stream, cancelTokenSource.Token);
                var httpRequest = HttpRequest.Parse(lines);
                if (httpRequest == null)
                    return;
                var responseChannel = new HttpResponseChannel(client, stream);
                var httpContext = new HttpContext(httpRequest, responseChannel, cancelTokenSource.Token);
                _handler(httpContext);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
                if (e.InnerException != null)
                    Console.WriteLine($"Inner exception: {e.InnerException.Message}");
                stream.Close();
                client.Close();
                Console.WriteLine("Connection closed.");
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _disposed = true;
            _running = false;
            if (_server != null)
                _server.Stop();
        }

        static void DisplaySslStream(SslStream stream)
        {
            Console.WriteLine($"Cipher: {stream.CipherAlgorithm} strength {stream.CipherStrength}");
            Console.WriteLine($"Hash: {stream.HashAlgorithm} strength {stream.HashStrength}");
            Console.WriteLine($"Key exchange: {stream.KeyExchangeAlgorithm} strength {stream.KeyExchangeStrength}");
            Console.WriteLine($"Protocol: {stream.SslProtocol}");
            Console.WriteLine($"Is authenticated: {stream.IsAuthenticated} as server? {stream.IsServer}");
            Console.WriteLine($"IsSigned: {stream.IsSigned}");
            Console.WriteLine($"Is Encrypted: {stream.IsEncrypted}");
            Console.WriteLine($"Can read: {stream.CanRead}, write {stream.CanWrite}");
            Console.WriteLine($"Can timeout: {stream.CanTimeout}");
            Console.WriteLine($"Certificate revocation list checked: {stream.CheckCertRevocationStatus}");
            var lc = stream.LocalCertificate;
            Console.WriteLine(stream.LocalCertificate != null ? $"Local cert was issued to {lc.Subject} and is valid from {lc.GetEffectiveDateString()} until {lc.GetExpirationDateString()}." : "Local certificate is null.");
            var rc = stream.RemoteCertificate;
            Console.WriteLine(stream.RemoteCertificate != null ? $"Remote cert was issued to {rc.Subject} and is valid from {rc.GetEffectiveDateString()} until {rc.GetExpirationDateString()}." : "Remote certificate is null.");
        }

        /// <summary>
        /// Finds the local ip.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <exception cref="Exception">No network adapters with an IPv4 address in the system!</exception>
        public static string FindLocalIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        /// <summary>
        /// Finds the certificate.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="findType">Type of the find.</param>
        /// <param name="storeName">Name of the store.</param>
        /// <param name="location">The location.</param>
        /// <returns>X509Certificate2.</returns>
        public static X509Certificate2 FindCertificate(object value, X509FindType findType = X509FindType.FindBySubjectName, string storeName = null, StoreLocation location = StoreLocation.LocalMachine)
        {
            if (value == null || (value is string valueAsString && valueAsString.Length == 0))
                return null;
            using (var store = new X509Store(storeName ?? "MY", location))
            {
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                return store.Certificates.Find(findType, value, false).Cast<X509Certificate2>()
                    .Where(x => x.NotBefore <= DateTime.Now)
                    .OrderBy(x => x.NotAfter)
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Finds the free TCP port.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public static int FindFreeTcpPort()
        {
            var l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            var port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }
    }
}
