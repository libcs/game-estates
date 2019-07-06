using Gamer.Format.Cry.Core;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using static Gamer.Core.Debug;

namespace Gamer.Estate.Cry.Tests
{
    public class ModelTests
    {
        const string AssetRoot = @"D:\StarCitizen\data";

        public ModelTests(ITestOutputHelper helper) => LogFunc = x => helper.WriteLine(x.ToString());

        [Theory]
        [InlineData(@"Objects\animals\crab\props\crab_thorshu_prop_01.chr")]
        [InlineData(@"Objects\animals\fish\CleanerFish_clean_prop_animal_01.chr")]
        [InlineData(@"Objects\animals\sandWorm\sandWorm.chr")]
        [InlineData(@"Objects\buildingsets\human\hightech\prop\hydroponic\hydroponic_machine_1_incubator_01x01x02_a.cgf")]
        public void LoadModel(string path)
        {
            path = Path.Combine(AssetRoot, path);
            var file = (path, File.Open(path, FileMode.Open));
            var model = new Model(file);
        }
    }
}
