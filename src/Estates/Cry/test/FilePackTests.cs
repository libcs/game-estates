//#define LONGTEST
using Game.Format.Cry;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Game.Estate.Cry.Tests
{
    public class FilePackTests
    {
        public FilePackTests(ITestOutputHelper helper) => Core.CoreDebug.LogFunc = x => helper.WriteLine(x.ToString());

        [Theory]
        [InlineData("game:/#StarCitizen", @"Data\Objects\animals\fish\CleanerFish_clean_prop_animal_01.chr")]
        public async Task LoadAssetPack(string path, string modelPath)
        {
            // given
            using (var assetPack = await new Uri(path).GetCryAssetPackAsync())
            {
                // when
                var exist0 = assetPack.ContainsFile(modelPath);
                var data0 = await assetPack.LoadFileDataAsync(modelPath);
                var model0 = await assetPack.LoadObjectInfoAsync(modelPath) as CryFile;
                // then
                Assert.True(exist0);
                Assert.NotNull(data0);
                Assert.NotNull(model0);
            }
        }

        [Theory]
        [InlineData("game:/#StarCitizen", @"Data\Objects\animals\fish\textures\fish_cleanerfish_cleanl_unique_uee_01_diff.dds")]
        //[InlineData("game:/#StarCitizen", @"Data\Objects\animals\fish\textures\fish_cleanerfish_cleanl_unique_uee_01_ddna.dds")]
        public async Task LoadTexture(string path, string modelPath)
        {
            // given
            using (var assetPack = await new Uri(path).GetCryAssetPackAsync())
            {
                // when
                var exist0 = assetPack.ContainsFile(modelPath);
                var data0 = await assetPack.LoadTextureInfoAsync(modelPath);
                // then
                Assert.True(exist0);
                Assert.NotNull(data0);
            }
        }

        [Theory]
        [InlineData("game:/#StarCitizen")]
        public async Task LoadAssetPackAll(string path)
        {
            using (var assetPack = await new Uri(path).GetCryAssetPackAsync() as CryAssetPack)
            {
                //#if LONGTEST
                //            assetPack.TestLoadFileData(int.MaxValue);
                //#else
                //            assetPack.TestLoadFileData(100);
                //#endif
            }
        }
    }
}
