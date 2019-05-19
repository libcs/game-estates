using Gamer.Core;
using Gamer.Estate.Rsi.FilePack;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Rsi
{
    public static class RsiExtensions2
    {
        public static Task<IAssetPack> GetRsiAssetPackAsync(this Uri uri)
        {
            uri.ToRsiGame(out var proxySink, out var filePath);
            var pakFile = new PakFile(filePath);
            return Task.FromResult((IAssetPack)new RsiAssetPack(proxySink, pakFile));
        }
    }
}