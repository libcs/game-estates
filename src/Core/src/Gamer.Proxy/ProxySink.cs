using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gamer.Proxy
{
    public class ProxySink
    {
        public static readonly string UriSchemeGame = "game";
        public static readonly string UriSchemeGames = "games";

        readonly WebClient _wc = new WebClient();

        public class Message
        {
            public string username { get; set; }
            public string text { get; set; }
            public string dt { get; set; }
        }

        public ProxySink() => _wc.OpenReadCompleted += OnOpenReadCompleted;

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

        public void OpenSse(Uri address) => _wc.OpenReadAsync(address);

        public virtual HashSet<string> GetContainsSet(Func<HashSet<string>> action) => action();

        public virtual bool ContainsFile(string filePath, Func<bool> action) => action();

        public virtual Task<byte[]> LoadFileDataAsync(string filePath, Func<Task<byte[]>> action) => action();
    }
}
