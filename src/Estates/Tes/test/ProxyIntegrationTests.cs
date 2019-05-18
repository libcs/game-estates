using Gamer.Proxy;
using Gamer.Proxy.Server;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Gamer.Estate.Tes.Tests
{
    public class TestsFixture : IDisposable
    {
        public readonly ProxyTarget Target = new ProxyTarget
        {
            Host = "127.0.0.1",
            Port = HttpServer.FindFreeTcpPort(),
        };

        public TestsFixture() => Target.Initialize();
        public void Dispose() => Target.Dispose();
    }

    public class ProxyIntegrationTests : IClassFixture<TestsFixture>
    {
        public ProxyIntegrationTests(ITestOutputHelper helper) => Core.Debug.LogFunc = x => helper.WriteLine(x.ToString());

        TestsFixture _fixture;
        public void SetFixture(TestsFixture fixture) => _fixture = fixture;

        [Theory]
        [InlineData("http://localhost:{0}/Morrowind/Morrowind.bsa", "meshes/i/in_dae_room_l_floor_01.nif")]
        public async Task LoadAssetPack(string path, string modelPath)
        {
            // given
            var uri = new Uri(string.Format(path, _fixture.Target.Port));
            var asset = await uri.GetTesAssetPackAsync() as TesAssetPack;
            // when
            var exists = asset.ContainsFile(modelPath);
            // then
            Assert.True(exists);
        }
    }
}
