//#define LONGTEST
using Game.Estate.Cry.FilePack;
using Game.Format.Cry;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Game.Estate.Cry.Tests
{
    public class SocPackTests
    {
        public SocPackTests(ITestOutputHelper helper) => Core.CoreDebug.LogFunc = x => helper.WriteLine(x.ToString());

        [Theory]
        [InlineData(@"D:\StarCitizen\Data\ObjectContainers\Ships\MISC\Prospector\base_ext_lg.socpak", @"base_ext_lg.soc")]
        public async Task LoadSocPack(string socPath, string modelPath)
        {
            // given
            using (var assetPack = new SocPakFile(socPath))
            {
                // when
                var exist0 = assetPack.ContainsFile(modelPath);
                var data0 = await assetPack.LoadFileDataAsync(modelPath);
                var file0 = new CryFile(Path.Combine(socPath.Replace(".socpak", ""), modelPath));
                file0.LoadFromFile();
                // then
                Assert.True(exist0);
                Assert.NotNull(data0);
            }
        }

        [Theory]
        [InlineData("game:/#StarCitizen", @"Data\ObjectContainers\Ships\MISC\Prospector\base_ext_lg.socpak", @"base_ext_lg.soc")]
        public async Task LoadAssetPack(string path, string socPath, string modelPath)
        {
            // given
            using (var assetPack = await new Uri(path).GetCryAssetPackAsync() as CryAssetPack)
            {
                // when
                var exist0 = assetPack.ContainsFile(socPath);
                var data0 = await assetPack.LoadFileDataAsync(socPath);
                // then
                Assert.True(exist0);
                Assert.NotNull(data0);

                // given
                using (var socPack = await assetPack.GetSocAssetPackAsync(socPath))
                {
                    var exist1 = socPack.ContainsFile(modelPath);
                    var data1 = await socPack.LoadFileDataAsync(modelPath);
                    var model1 = await socPack.LoadObjectInfoAsync(modelPath) as CryFile;
                    // then
                    Assert.True(exist1);
                    Assert.NotNull(data1);
                    Assert.NotNull(model1);
                }
            }
        }
    }
}
