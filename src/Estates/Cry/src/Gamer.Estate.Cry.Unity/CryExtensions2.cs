using Gamer.Core;
using Gamer.Estate.Cry.FilePack;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Cry
{
    public static class CryExtensions2
    {
        public static Task<IAssetPack> GetAssetPackAsync(this Uri uri, out PakFile pakFile)
        {
            var game = uri.ToGame(out var path);
            var filePath = FileManager.GetFilePath(path, game);
            if (filePath == null)
                throw new InvalidOperationException($"{game} not available");
            pakFile = new PakFile(filePath);
            return Task.FromResult((IAssetPack)new CryAssetPack(pakFile));
        }
    }
}