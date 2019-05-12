using Gamer.Format.Cry.Core;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using static Gamer.Core.Debug;

namespace Gamer.Estate.Cry.Tests
{
    public class MaterialTests
    {
        const string AssetRoot = @"D:\StarCitizen\data";

        public MaterialTests(ITestOutputHelper helper) => LogFunc = x => helper.WriteLine(x.ToString());

        [Theory]
        [InlineData(@"Objects\animals\crab\props\crab_thorshu_prop_01.mtl")]
        public void LoadMaterial(string path)
        {
            var material = Material.FromFile(new FileInfo(Path.Combine(AssetRoot, path)));
        }
    }
}
