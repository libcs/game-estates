using Game.Core;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

namespace Game.Core.Netstream
{
    public class StreamSinkClient : StreamSink
    {
        readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions { });
        readonly HttpClient _hc = new HttpClient { Timeout = TimeSpan.FromMinutes(30) };
        readonly string _platform;
        readonly bool _schemeGame;

        public StreamSinkClient(Uri address, string platform, string estate)
        {
            _platform = platform;
            _schemeGame = address.Scheme == UriSchemeGame || address.Scheme == UriSchemeGames;
            _hc.BaseAddress = _schemeGame
                ? new UriBuilder(address) { Scheme = address.Scheme == UriSchemeGame ? Uri.UriSchemeHttp : Uri.UriSchemeHttps }.Uri
                : new UriBuilder(address) { Path = address.LocalPath.EnsureEndsWith("/"), Fragment = null }.Uri;
            _hc.DefaultRequestHeaders.Accept.Clear();
            _hc.DefaultRequestHeaders.Add("Platform", platform);
            _hc.DefaultRequestHeaders.Add("Estate", estate);
            if (_schemeGame)
                _hc.DefaultRequestHeaders.Add("Pack", new UriBuilder(address) { Host = "serv", Port = -1 }.ToString());
        }

        public async Task<T> CallAsync<T>(string path, NameValueCollection nvc = null)
        {
            if (nvc == null)
                nvc = new NameValueCollection { { "p", _platform } }; //, { "t", DateTime.Now.Ticks.ToString() } };
            var requestUri = ToPathAndQueryString(path, nvc);
            //Core.Debug.Log($"query: {requestUri}");
            var r = await _hc.GetAsync(requestUri).ConfigureAwait(false);
            if (!r.IsSuccessStatusCode)
                throw new InvalidOperationException(r.ReasonPhrase);
            var data = await r.Content.ReadAsByteArrayAsync();
            return StreamUtils.FromBytes<T>(_schemeGame, data);
        }

        // ASSET
        public override HashSet<string> GetContainsSet(Func<HashSet<string>> action) => throw new NotSupportedException();
        public override bool ContainsFile(string filePath, Func<bool> action) =>
            _cache.GetOrCreate(".set", x => CallAsync<HashSet<string>>((string)x.Key).Result())
            .Contains(filePath.Replace('\\', '/'));
        public override async Task<byte[]> LoadFileDataAsync(string filePath, Func<Task<byte[]>> action) =>
            await _cache.GetOrCreateAsync(filePath.Replace('\\', '/'), async x => await CallAsync<byte[]>((string)x.Key));

        // DATA
        public override byte[] GetDataContains(Func<byte[]> action) =>
            //_cache.GetOrCreate("d/.set", x => CallAsync<byte[]>((string)x.Key).Result());
            CallAsync<byte[]>("d/.set").Result();
        public override async Task<byte[]> LoadDataLabelAsync(string filePath, Func<Task<byte[]>> action) =>
            //await _cache.GetOrCreateAsync($"d/{filePath}", async x => await CallAsync<byte[]>((string)x.Key));
            await CallAsync<byte[]>($"d/{filePath}");
    }
}
