//#define LONGTEST
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Gamer.Core.Debug;

namespace Gamer.Estate.Rsi.Tests
{
    public class FilePackTests
    {
        public FilePackTests(ITestOutputHelper helper) => LogFunc = x => helper.WriteLine(x.ToString());

        [Theory]
        [InlineData("game://StarCitizen/")]
        public async Task LoadAssetPack(string path)
        {
            var assetPack = await new Uri(path).GetRsiAssetPackAsync() as RsiAssetPack;
            //#if LONGTEST
            //            assetPack.TestLoadFileData(int.MaxValue);
            //#else
            //            assetPack.TestLoadFileData(100);
            //#endif
        }
    }
}
