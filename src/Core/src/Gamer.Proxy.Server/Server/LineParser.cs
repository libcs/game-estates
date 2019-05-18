using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gamer.Proxy.Server
{
    /// <summary>
    /// Class LineParser.
    /// </summary>
    public class LineParser
    {
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IEnumerable&lt;System.String&gt;&gt;.</returns>
        public async Task<IEnumerable<string>> Parse(Stream input, CancellationToken cancellationToken)
        {
            var buf = new byte[0x1000];
            var r = new List<string>();
            var b = new StringBuilder();
            var reading = true;
            while (reading && !cancellationToken.IsCancellationRequested)
            {
                var bytesRead = await input.ReadAsync(buf, 0, buf.Length, cancellationToken);
                var chars = Encoding.ASCII.GetChars(buf, 0, bytesRead);
                var charsRead = chars.Length;
                for (var i = 0; i < charsRead; i++)
                {
                    var c = chars[i];
                    if (c == '\n')
                    {
                        var line = b.ToString();
                        b.Clear();
                        if (line == null || line.Length == 0) reading = false;
                        else r.Add(line);
                    }
                    else if (c == '\r') { } // ignore
                    else b.Append(c);
                }
            }
            return r;
        }
    }
}
