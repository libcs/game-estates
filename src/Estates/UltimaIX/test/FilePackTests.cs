//#define LONGTEST
using System;
using System.Threading.Tasks;
using UnityEngine;
using Xunit;
using Xunit.Abstractions;

namespace Game.Estate.UltimaIX.Tests
{
    public class FilePackTests
    {
        public FilePackTests(ITestOutputHelper helper) => Core.Debug.LogFunc = x => helper.WriteLine(x.ToString());

        [Theory]
        [InlineData("game:/static/activity.flx#UltimaIX", @"1")]
        public async Task LoadAssetPack(string path, string modelPath)
        {
            // given
            using (var assetPack = await new Uri(path).GetUltimaIXAssetPackAsync())
            {
                // when
                var exist0 = assetPack.ContainsFile(modelPath);
                var data0 = await assetPack.LoadFileDataAsync(modelPath);
                var model0 = await assetPack.LoadObjectInfoAsync(modelPath) as object; // UltimaIXFile;
                // then
                Assert.True(exist0);
                Assert.NotNull(data0);
                //Assert.NotNull(model0);
            }
        }

        //[Theory]
        //[InlineData("game:/#StarCitizen", @"Data\Objects\animals\fish\textures\fish_cleanerfish_cleanl_unique_uee_01_diff.dds")]
        ////[InlineData("game:/#StarCitizen", @"Data\Objects\animals\fish\textures\fish_cleanerfish_cleanl_unique_uee_01_ddna.dds")]
        //public async Task LoadTexture(string path, string modelPath)
        //{
        //    // given
        //    using (var assetPack = await new Uri(path).GetCryAssetPackAsync())
        //    {
        //        // when
        //        var exist0 = assetPack.ContainsFile(modelPath);
        //        var data0 = await assetPack.LoadTextureInfoAsync(modelPath);
        //        // then
        //        Assert.True(exist0);
        //        Assert.NotNull(data0);
        //    }
        //}

        [Theory]
        [InlineData("game:/#UltimaIX")]
        public async Task LoadDataPack(string path)
        {
            using (var dataPack = (UltimaIXDataPack)await new Uri(path).GetUltimaIXDataPackAsync())
            {
                //var x = await dataPack.LoadDataLabelAsync("test");
                TestLoadCell(dataPack, new Vector3(0, 0, 0));
                //TestAllCells(dataPack);
            }
        }

        public static Vector3Int GetCellId(Vector3 point, int world) => new Vector3Int(Mathf.FloorToInt(point.x / 1), Mathf.FloorToInt(point.z / 1), world);

        static void TestLoadCell(UltimaIXDataPack data, Vector3 position)
        {
            var cellId = GetCellId(position, 60);
            var cell = data.FindCELLRecord(cellId);
            var land = data.FindLANDRecord(cellId);
        }
    }
}
