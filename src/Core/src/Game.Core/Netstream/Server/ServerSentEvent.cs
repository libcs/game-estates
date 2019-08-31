using System;
using System.Linq;
using System.Text;

namespace Game.Core.Netstream.Server
{
    /// <summary>
    /// Class ServerSentEvent.
    /// </summary>
    public class ServerSentEvent
    {
        readonly static string[] LogLevels = { "DEBUG", "INFO", "WARN", "ERROR" };
        readonly string _type;
        readonly string _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerSentEvent"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="data">The data.</param>
        public ServerSentEvent(string type, string data)
        {
            _type = type;
            _data = data;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var lines = _data.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var b = new StringBuilder();
            if (LogLevels.Contains(_type))
                b.Append("event: " + _type + "\r\n");
            foreach (var line in lines)
                b.Append("data: " + line + "\r\n");
            b.Append("\r\n");
            return b.ToString();
        }
    }
}
