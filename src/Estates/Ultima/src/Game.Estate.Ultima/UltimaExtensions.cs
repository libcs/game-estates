using Game.Core;
using Game.Core.Netstream;
using System;
using System.Threading.Tasks;

namespace Game.Estate.Ultima
{
    public static class UltimaExtensions
    {
        public static UltimaGame ToUltimaGame(this Uri uri, Func<object> func, out StreamSink streamSink, out string[] filePaths) => StreamUtils.ToGame<UltimaGame>(uri, func, out streamSink, out filePaths, "Ultima", (path, game) => new string[0]);

        public static Task<IDataPack> GetUltimaDataPackAsync(this Uri uri, Func<object> func = null)
        {
            var game = uri.ToUltimaGame(func, out var streamSink, out var filePaths);
            return Task.FromResult((IDataPack)new UltimaDataPack((uint)game));
        }
    }
}