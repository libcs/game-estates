using Game.Core.Netstream;
using System;

namespace Game.Estate.Cry
{
    public static class CryExtensions
    {
        public static CryGame ToCryGame(this Uri uri, Func<object> func, out StreamSink streamSink, out string[] filePaths) => StreamUtils.ToGame<CryGame>(uri, func, out streamSink, out filePaths, "Cry", (path, game) => CryFileManager.GetFilePaths("Data.p4k", game));
    }
}