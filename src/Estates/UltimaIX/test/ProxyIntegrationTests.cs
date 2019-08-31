//using Game.Stream;
//using Game.Stream.Server;
//using System;
//using System.Threading.Tasks;
//using Xunit;
//using Xunit.Abstractions;

//namespace Game.Estate.Cry.Tests
//{
//    public class TestsFixture : IDisposable
//    {
//        public readonly StreamTarget Target = new StreamTarget
//        {
//            Host = "127.0.0.1",
//            Port = HttpServer.FindFreeTcpPort(),
//        };

//        public TestsFixture() => Target.Initialize(CryEstateHandler.Handler);
//        public void Dispose() => Target.Dispose();
//    }

//    public class StreamIntegrationTests : IClassFixture<TestsFixture>
//    {
//        readonly TestsFixture _fixture;
//        public StreamIntegrationTests(TestsFixture fixture, ITestOutputHelper helper) { _fixture = fixture; Core.Debug.LogFunc = x => helper.WriteLine(x.ToString()); }

//        [Theory]
//        [InlineData("game://localhost:{0}/#StarCitizen", "xxx")]
//        public async Task LoadAssetPack(string path, string modelPath)
//        {
//            // given
//            var uri = new Uri(string.Format(path, _fixture.Target.Port));
//            using (var assetPack = await uri.GetCryAssetPackAsync())
//            {
//                // when
//                var exists = assetPack.ContainsFile(modelPath);
//                // then
//                Assert.True(exists);
//            }
//        }
//    }
//}
