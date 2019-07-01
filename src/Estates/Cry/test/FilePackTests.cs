//#define LONGTEST
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Gamer.Estate.Cry.Tests
{
    public class FilePackTests
    {
        public FilePackTests(ITestOutputHelper helper) => Core.Debug.LogFunc = x => helper.WriteLine(x.ToString());

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
