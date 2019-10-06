using Game.Format.Cry.Core;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using static Game.Core.CoreDebug;

namespace Game.Estate.Cry.Tests
{
    public class MaterialTests
    {
        const string AssetRoot = @"D:\StarCitizen";

        public MaterialTests(ITestOutputHelper helper) => LogFunc = x => helper.WriteLine(x.ToString());

        [Theory]
        [InlineData(@"Data\Objects\animals\crab\props\crab_thorshu_prop_01.mtl")]
        public void LoadMaterial(string path)
        {
            path = Path.Combine(AssetRoot, path);
            var file = (path, File.Open(path, FileMode.Open));
            var material = Material.FromFile(file);
        }
    }
}
