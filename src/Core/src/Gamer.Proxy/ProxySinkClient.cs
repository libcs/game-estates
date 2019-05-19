using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Gamer.Proxy
{
    public class ProxySinkClient : ProxySink
    {
        public class Message
        {
            public string username { get; set; }
            public string text { get; set; }
            public string dt { get; set; }
        }

        readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        readonly WebClient _wc = new WebClient();
        readonly HttpClient _hc = new HttpClient();
        readonly Uri _address;

        public ProxySinkClient(Uri address)
        {
            _address = address;
            _wc.OpenReadCompleted += OnOpenReadCompleted;
            _hc.BaseAddress = _address;
            _hc.DefaultRequestHeaders.Accept.Clear();
            _hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public override bool ContainsFile(string filePath, Func<bool> action) =>
            _cache.GetOrCreateAsync<Dictionary<string, string>>(".list", async x =>
            {
                await CallAsync(".list");
                return null;
            }).Result.ContainsKey(filePath);

        public override async Task<byte[]> LoadFileDataAsync(string filePath, Func<Task<byte[]>> action) =>
            await _cache.GetOrCreateAsync<byte[]>(filePath, x =>
            {
                return null;
            });


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

        public void OpenSse() => _wc.OpenReadAsync(_address);

        public async Task CallAsync(string method)
        {
            var r = await _hc.GetAsync("api/Department/1");
            if (r.IsSuccessStatusCode)
            {
                //var department = await r.Content.ReadAsAsync<Department>();
                //Console.WriteLine("Id:{0}\tName:{1}", department.DepartmentId, department.DepartmentName);
                //Console.WriteLine("No of Employee in Department: {0}", department.Employees.Count);
            }
            else
                Console.WriteLine("Internal server Error");
        }

    }
}
