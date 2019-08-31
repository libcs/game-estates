using Game.Core;
using Game.Core.Netstream;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Estate.Tes
{
    public static class TesExtensions
    {
        public static TesGame ToTesGame(this Uri uri, Func<object> func, out StreamSink streamSink, out string[] filePaths) => StreamUtils.ToGame<TesGame>(uri, func, out streamSink, out filePaths, "Tes", (path, game) => TesFileManager.GetFilePaths(Path.GetExtension(path) == ".bsa" || Path.GetExtension(path) == ".ba2", path, game));

        public static Task<IDataPack> GetTesDataPackAsync(this Uri uri, Func<object> func = null)
        {
            var game = uri.ToTesGame(func, out var streamSink, out var filePaths);
            return Task.FromResult((IDataPack)new TesDataPack(streamSink, filePaths.Single(), game));
        }
    }
}