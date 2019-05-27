using Gamer.Core;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gamer.Proxy
{
    public class ProxySinkClient : ProxySink
    {
        readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        readonly HttpClient _hc = new HttpClient();
        readonly bool _schemeGame;

        public ProxySinkClient(Uri address, string estate)
        {
            _schemeGame = address.Scheme == UriSchemeGame || address.Scheme == UriSchemeGames;
            _hc.BaseAddress = _schemeGame
                ? new UriBuilder(address) { Scheme = address.Scheme == UriSchemeGame ? Uri.UriSchemeHttp : Uri.UriSchemeHttps }.Uri
                : new UriBuilder(address) { Path = address.LocalPath.EnsureEndsWith("/"), Fragment = null }.Uri;
            _hc.DefaultRequestHeaders.Accept.Clear();
            if (_schemeGame)
            {
                _hc.DefaultRequestHeaders.Add("Estate", estate);
                _hc.DefaultRequestHeaders.Add("Pack", new UriBuilder(address) { Host = "serv", Port = -1 }.ToString());
            }
        }

        public async Task<T> CallAsync<T>(string method)
        {
            var r = await _hc.GetAsync(method);
            if (!r.IsSuccessStatusCode)
                throw new InvalidOperationException(r.ReasonPhrase);
            var data = await r.Content.ReadAsByteArrayAsync();
            return ProxyUtils.FromBytes<T>(_schemeGame, data);
        }

        public override HashSet<string> GetContainsSet(Func<HashSet<string>> action) => throw new NotSupportedException();

        public override bool ContainsFile(string filePath, Func<bool> action) =>
            _cache.GetOrCreateAsync(".set", async x => await CallAsync<HashSet<string>>((string)x.Key))
                .Result().Contains(filePath.Replace('\\', '/'));

        public override async Task<byte[]> LoadFileDataAsync(string filePath, Func<Task<byte[]>> action) =>
            await _cache.GetOrCreateAsync(filePath.Replace('\\', '/'), async x => await CallAsync<byte[]>((string)x.Key));
    }
}
