using Game.Core;
using Game.Estate.Cry.FilePack;
using System;
using System.Threading.Tasks;

namespace Game.Estate.Cry
{
    public static class CryExtensions2
    {
        public static Task<IAssetUnityPack> GetCryAssetPackAsync(this Uri uri, Func<object> func = null)
        {
            uri.ToCryGame(func, out var streamSink, out var filePaths);
            var pakFile = new PakFile(filePaths[0]);
            return Task.FromResult((IAssetUnityPack)new CryAssetPack(streamSink, pakFile));
        }
    }
}