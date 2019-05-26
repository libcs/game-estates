using Gamer.Core;
using Gamer.Estate.Rsi.FilePack;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Rsi
{
    public static class RsiExtensions2
    {
        public static Task<IAssetUnityPack> GetRsiAssetPackAsync(this Uri uri, Func<object> func = null)
        {
            uri.ToRsiGame(func, out var proxySink, out var filePaths);
            var pakFile = new PakFile(filePaths[0]);
            return Task.FromResult((IAssetUnityPack)new RsiAssetPack(proxySink, pakFile));
        }
    }
}