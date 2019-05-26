//#define LONGTEST
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Gamer.Core.Debug;

namespace Gamer.Estate.Cry.Tests
{
    public class FilePackTests
    {
        public FilePackTests(ITestOutputHelper helper) => LogFunc = x => helper.WriteLine(x.ToString());

        [Theory]
        [InlineData("game:/#Abc")]
        public async Task LoadAssetPack(string path)
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
