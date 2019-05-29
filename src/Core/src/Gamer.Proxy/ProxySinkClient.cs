using Gamer.Core;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Gamer.Core.Debug;

namespace Gamer.Proxy
{
    public class ProxySinkClient : ProxySink
    {
        readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        readonly HttpClient _hc = new HttpClient();
        readonly string _platform;
        readonly bool _schemeGame;

        public ProxySinkClient(Uri address, string platform, string estate)
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
            //Log($"query: {ToPathAndQueryString(path, nvc)}");
            var r = await _hc.GetAsync(ToPathAndQueryString(path, nvc)).ConfigureAwait(false);
            if (!r.IsSuccessStatusCode)
                throw new InvalidOperationException(r.ReasonPhrase);
            var data = await r.Content.ReadAsByteArrayAsync();
            return ProxyUtils.FromBytes<T>(_schemeGame, data);
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
             _cache.GetOrCreate("d/.set", x => CallAsync<byte[]>((string)x.Key).Result());
        public override async Task<byte[]> LoadDataLabelAsync(byte[] label, Func<Task<byte[]>> action) =>
            await _cache.GetOrCreateAsync($"d/{Encoding.ASCII.GetString(label)}.dat", async x => await CallAsync<byte[]>((string)x.Key));
    }
}
