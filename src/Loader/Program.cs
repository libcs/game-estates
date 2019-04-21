//using Test = Asset.Tes.ObjectTestObject;

using Contoso.GameNetCore;
using Contoso.GameNetCore.Hosting;

namespace Loader
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateGameHostBuilder(args).Build().Run();

            //BaseSettings.Game.MaterialType = MaterialType.None;
            //Test.Start();
        }

        public static IGameHostBuilder CreateGameHostBuilder(string[] args) =>
            GameHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}