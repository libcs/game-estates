using Gamer.Core;
using Gamer.Estate.Cry.FilePack;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Cry
{
    public static class CryExtensions2
    {
        public static Task<IAssetPack> GetCryAssetPackAsync(this Uri uri, out PakFile pakFile)
        {
            uri.ToCryGame(out var proxySink, out var filePath);
            pakFile = new PakFile(filePath);
            return Task.FromResult((IAssetPack)new CryAssetPack(proxySink, pakFile));
        }
    }
}