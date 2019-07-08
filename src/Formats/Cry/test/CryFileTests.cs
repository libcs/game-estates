using Gamer.Format.Cry;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using static Gamer.Core.Debug;

namespace Gamer.Estate.Cry.Tests
{
    public class CryFileTests
    {
        const string AssetRoot = @"D:\StarCitizen";

        public CryFileTests(ITestOutputHelper helper) => LogFunc = x => helper.WriteLine(x.ToString());

        [Theory]
        [InlineData(@"Data\Objects\animals\crab\props\crab_thorshu_prop_01.chr")]
        [InlineData(@"Data\Objects\animals\fish\CleanerFish_clean_prop_animal_01.chr")]
        [InlineData(@"Data\Objects\animals\sandWorm\sandWorm.chr")]
        [InlineData(@"Data\Objects\buildingsets\human\hightech\prop\hydroponic\hydroponic_machine_1_incubator_01x01x02_a.cgf")]
        public void LoadModel(string path)
        {
            var cryFile = new CryFile(Path.Combine(AssetRoot, path));
            cryFile.LoadFromFile();
        }

        [Theory]
        [InlineData(@"Data\Prefabs\empty_landingpad.xml")]
        [InlineData(@"Data\Prefabs\drak_caterpillar.xml")]
        public void LoadXml(string path)
        {
            var xml = CryXmlSerializer.ReadFile(Path.Combine(AssetRoot, path));
        }
    }
}
