using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Gamer.Proxy
{
    public class ProxySinkClient : ProxySink
    {
        readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        readonly HttpClient _hc = new HttpClient();

        public ProxySinkClient(Uri address, string estate)
        {
            _hc.BaseAddress = address;
            _hc.DefaultRequestHeaders.Accept.Clear();
            _hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _hc.DefaultRequestHeaders.Add("Estate", estate);
            _hc.DefaultRequestHeaders.Add("Pack", new UriBuilder(address) { Scheme = "serv", Host = address.Fragment.Substring(1), Port = -1, Fragment = null }.ToString());
        }

        public async Task<T> CallAsync<T>(string method)
        {
            var r = await _hc.GetAsync(method);
            if (!r.IsSuccessStatusCode)
                throw new InvalidOperationException(r.ReasonPhrase);
            var data = await r.Content.ReadAsByteArrayAsync();
            return FromBytes<T>(data);
        }

        public override bool ContainsFile(string filePath, Func<bool> action) =>
            _cache.GetOrCreateAsync("/asset/.set", async x => await CallAsync<HashSet<string>>("/asset/.set")).Result.Contains(filePath.Replace('\\', '/'));

        public override async Task<byte[]> LoadFileDataAsync(string filePath, Func<Task<byte[]>> action) =>
            await _cache.GetOrCreateAsync($"/asset/{filePath.Replace('\\', '/')}", async x => await CallAsync<byte[]>($"/asset/{filePath.Replace('\\', '/')}"));
    }
}
