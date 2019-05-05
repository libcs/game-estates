using Gamer.Core;
using Gamer.Estate.Rsi.FilePack;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Rsi
{
    public static class RsiExtensions2
    {
        public static Task<IAssetPack> GetAssetPack(this Uri uri, out PakFile pakFile)
        {
            var game = uri.ToGame(out var path);
            var filePath = FileManager.GetFilePath(path, game);
            if (filePath == null)
                throw new InvalidOperationException($"{game} not available");
            pakFile = new PakFile(filePath);
            return Task.FromResult((IAssetPack)new RsiAssetPack(pakFile));
        }
    }
}