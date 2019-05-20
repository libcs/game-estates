using Gamer.Estate.Tes;
using Gamer.Proxy;
using NLog;
using System;
using System.Threading;

namespace Gamer.ProxyService
{
    internal class ProxyService : IDisposable
    {
        const int ExceptionMax = 2;

        public static readonly ManualResetEventSlim[] Ready = { new ManualResetEventSlim(false), new ManualResetEventSlim(false) };
        readonly ILogger _log;
        readonly ProxyTarget _target;
        DateTime _lastHeartBeat;

        public ProxyService(ILogger log)
        {
            _log = log;
            _target = new ProxyTarget
            {
                Host = Config.ProxyHost
            };
        }

        public void Dispose()
        {
            Ready[0].Dispose();
            Ready[1].Dispose();
        }

        public bool IsStopping { get; set; }

        public void Watch(CancellationToken cancellationToken)
        {
            try
            {
                ProxyWatch(cancellationToken);
                Ready[0].Wait(cancellationToken);
            }
            catch (Exception e)
            {
                _log.Error(e);
                if (IsStopping && e is OperationCanceledException)
                {
                    Ready[1].Set();
                    _log.Info("Exit");
                    return;
                }
            }
        }

        public void Start()
        {
            _log.Info("ProxyService");
            _target.Initialize(new TesEstateHandler());
        }

        public void ProxyWatch(CancellationToken cancellationToken)
        {
            Console.WriteLine("Watching...");
            while (!cancellationToken.IsCancellationRequested)
                try
                {
                    Heartbeat();
                }
                catch (Exception e)
                {
                    Console.Write("*");
                    if (OnException(e)) throw;
                    else _log.Error(e);
                    Thread.Sleep(5000);
                }
        }

        static string _lastException;
        static int _lastExceptionCount;
        static bool OnException(Exception e)
        {
            if (e.InnerException is NotImplementedException)
                return true;
            if (e.InnerException is AggregateException)
                e = e.InnerException;
            var lastException = e.Message + e.StackTrace;
            if (_lastException != lastException)
            {
                _lastException = lastException;
                _lastExceptionCount = 1;
                return false;
            }
            return ++_lastExceptionCount >= ExceptionMax;
        }

        void Heartbeat()
        {
            if (DateTime.Now < _lastHeartBeat.AddHours(1))
                return;
            _log.Info("Heartbeat...");
            _lastHeartBeat = DateTime.Now;
            return;
        }
    }
}
