using Contoso.GameNetCore;
using Contoso.GameNetCore.Hosting;
using Test = Game.Asset.Tes.Loader.LoadObject;

namespace Loader
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Test.Start();
            //CreateGameHostBuilder(args).Build().Run();
        }

        public static IGameHostBuilder CreateGameHostBuilder(string[] args) =>
            GameHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}