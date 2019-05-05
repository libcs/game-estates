using System;
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
        public void LoadAssetPack(string path)
        {
            var asset = (RsiAssetPack)new Uri(path).GetAssetPack(out var pakFile).Result;
        }
    }
}
