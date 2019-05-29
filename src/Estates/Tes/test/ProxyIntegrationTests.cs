using Gamer.Format.Nif;
using Gamer.Proxy;
using Gamer.Proxy.Server;
using System;
using System.Threading.Tasks;
using UnityEngine;
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

        public TestsFixture() => Target.Initialize(TesEstateHandler.Handler);
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

        public static Vector3Int GetCellId(Vector3 point, int world) => new Vector3Int(Mathf.FloorToInt(point.x / ConvertUtils.ExteriorCellSideLengthInMeters), Mathf.FloorToInt(point.z / ConvertUtils.ExteriorCellSideLengthInMeters), world);

        [Theory]
        //[InlineData("game:/Morrowind.esm#Morrowind", -137.94f, 2.30f, -1037.6f, 0)]
        //[InlineData("game://localhost:{0}/Morrowind.esm#Morrowind", -137.94f, 2.30f, -1037.6f, 0)]
        [InlineData("game://localhost:{0}/Oblivion.esm#Oblivion", -137.94f, 2.30f, -1037.6f, 0)]
        public async Task LoadDataPack(string path, float x, float y, float z, int world)
        {
            // given
            var cellId = GetCellId(new Vector3(x, y, z), world);
            var uri = new Uri(string.Format(path, _fixture.Target.Port));
            using (var dataPack = await uri.GetTesDataPackAsync())
            {
                // when
                var cell0 = dataPack.FindCellRecord(cellId);
                // then
                Assert.NotNull(cell0);
            }
        }
    }
}
