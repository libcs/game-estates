using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;

namespace Gamer.ProxyService
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
            var proxyService = Services.GetRequiredService<ProxyService>();
            var source = new CancellationTokenSource();
            HostFactory.Run(x =>
                x.Service<ProxyService>(s =>
                {
                    s.ConstructUsing(tc => proxyService);
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Dispose());
                    s.AfterStartingService(() =>
                        Task.Run(() => proxyService.Watch(source.Token), source.Token)
                    );
                    s.BeforeStoppingService(() =>
                    {
                        proxyService.IsStopping = true;
                        source.Cancel();
                    });
                }).OnException((ex) =>
                    log.Error(ex.ToString())
                )
            );
        }
    }
}
