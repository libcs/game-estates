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
        [InlineData("game://Abc/")]
        public async Task LoadAssetPack(string path)
        {
            var asset = await new Uri(path).GetAssetPackAsync(out var pakFile) as CryAssetPack;
//#if LONGTEST
//            asset.TestLoadFileData(int.MaxValue);
//#else
//            asset.TestLoadFileData(100);
//#endif
        }
    }
}
