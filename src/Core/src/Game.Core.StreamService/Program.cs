using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;

namespace Game.NetstreamService
{
    class Program
    {
        public static IServiceProvider Services { get; set; }

        static Program()
        {
            var services = new ServiceCollection();
            Startup.ConfigureServices(services);
            Services = services.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            var log = Services.GetRequiredService<ILogger>();
            var streamService = Services.GetRequiredService<NetstreamService>();
            var source = new CancellationTokenSource();
            HostFactory.Run(x =>
                x.Service<NetstreamService>(s =>
                {
                    s.ConstructUsing(tc => streamService);
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Dispose());
                    s.AfterStartingService(() =>
                        Task.Run(() => streamService.Watch(source.Token), source.Token)
                    );
                    s.BeforeStoppingService(() =>
                    {
                        streamService.IsStopping = true;
                        source.Cancel();
                    });
                }).OnException((ex) =>
                    log.Error(ex.ToString())
                )
            );
        }
    }
}
