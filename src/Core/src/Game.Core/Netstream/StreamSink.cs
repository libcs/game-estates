using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Game.Core.Netstream
{
    public class StreamSink
    {
        public static readonly string UriSchemeGame = "game";
        public static readonly string UriSchemeGames = "games";
        public static StreamSink DefaultStreamSink = new StreamSink();

        readonly WebClient _wc = new WebClient();

        public class Message
        {
            public string username { get; set; }
            public string text { get; set; }
            public string dt { get; set; }
        }

        public class DataInfo
        {
            enum Record : byte { Group, EnterGroup, LeaveGroup };
            readonly MemoryStream _s;
            readonly BinaryReader _r;
            readonly BinaryWriter _w;

            public DataInfo() { _s = new MemoryStream(); _w = new BinaryWriter(_s); }
            public DataInfo(byte[] bytes) { _s = new MemoryStream(bytes); _r = new BinaryReader(_s); }

            public void AddGroup(string label, byte[] headerBytes)
            {
                _w.Write((byte)Record.Group);
                _w.Write(label.Length); if (label.Length != 0) _w.Write(label);
                _w.Write(headerBytes.Length); if (headerBytes.Length != 0) _w.Write(headerBytes);
            }
            public void EnterGroup() => _w.Write((byte)Record.EnterGroup);
            public void LeaveGroup() => _w.Write((byte)Record.LeaveGroup);

            public Action<byte[]> Data { get; set; }

            public void Decoder(Action<string, byte[]> group = null, Action enterGroup = null, Action leaveGroup = null)
            {
                int length;
                var endPosition = _r.BaseStream.Length;
                while (_r.BaseStream.Position != endPosition)
                    switch ((Record)_r.ReadByte())
                    {
                        case Record.Group:
                            group(
                                (length = _r.ReadInt32()) != 0 ? _r.ReadString() : string.Empty,
                                (length = _r.ReadInt32()) != 0 ? _r.ReadBytes(length) : null); break;
                        case Record.EnterGroup: enterGroup(); break;
                        case Record.LeaveGroup: leaveGroup(); break;
                        default: throw new ArgumentOutOfRangeException(nameof(_r));
                    }
            }

            public byte[] ToArray() => _s.ToArray();
        }

        internal StreamSink() => _wc.OpenReadCompleted += OnOpenReadCompleted;

        // When SSE (Server side event) occurs this fires
        void OnOpenReadCompleted(object sender, OpenReadCompletedEventArgs args)
        {
            using (var r = new StreamReader(args.Result, Encoding.UTF8))
            {
                var cometPayload = r.ReadLine();
                var jsonPayload = cometPayload.Substring(5);
                var message = JsonConvert.DeserializeObject<Message>(jsonPayload);
                Console.WriteLine("Message received: {0} {1} {2}", message.dt, message.username, message.text);
            }
        }

        protected static string ToPathAndQueryString(string path, NameValueCollection nvc)
        {
            if (nvc == null)
                return path;
            var array = (
                from key in nvc.AllKeys
                from value in nvc.GetValues(key)
                select !string.IsNullOrEmpty(value) ? string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)) : null)
                .Where(x => x != null).ToArray();
            return path + (array.Length > 0 ? "?" + string.Join("&", array) : string.Empty);
        }

        public void OpenSse(Uri address) => _wc.OpenReadAsync(address);

        // ASSET
        public virtual HashSet<string> GetContainsSet(Func<HashSet<string>> action) => action();
        public virtual bool ContainsFile(string filePath, Func<bool> action) => action();
        public virtual Task<byte[]> LoadFileDataAsync(string filePath, Func<Task<byte[]>> action) => action();

        // DATA
        public virtual byte[] GetDataContains(Func<byte[]> action) => action();
        public virtual Task<byte[]> LoadDataLabelAsync(string filePath, Func<Task<byte[]>> action) => action();
    }
}
