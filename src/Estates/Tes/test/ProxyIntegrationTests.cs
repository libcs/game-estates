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

        public TestsFixture() => Target.Initialize(new TesEstateHandler());
        public void Dispose() => Target.Dispose();
    }

    public class ProxyIntegrationTests : IClassFixture<TestsFixture>
    {
        readonly TestsFixture _fixture;
        public ProxyIntegrationTests(TestsFixture fixture, ITestOutputHelper helper) { _fixture = fixture; Core.Debug.LogFunc = x => helper.WriteLine(x.ToString()); }

        [Theory]
        //[InlineData("game://localhost:{0}/Morrowind.bsa#Morrowind", "meshes/i/in_dae_room_l_floor_01.nif")]
        [InlineData("game://localhost:{0}/Morrowind.bsa#Morrowind", "textures/Tx_BC_moss.dds")]
        public async Task LoadAssetPack(string path, string modelPath)
        {
            // given
            var uri = new Uri(string.Format(path, _fixture.Target.Port));
            using (var assetPack = await uri.GetTesAssetPackAsync())
            {
                // when
                var exist0 = assetPack.ContainsFile(modelPath);
                var data0 = await assetPack.LoadFileDataAsync(modelPath);
                // then
                Assert.True(exist0);
                Assert.NotNull(data0);
            }
        }
    }
}
