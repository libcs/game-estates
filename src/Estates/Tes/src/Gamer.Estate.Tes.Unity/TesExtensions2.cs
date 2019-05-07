using Gamer.Core;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Tes
{
    public static class TesExtensions2
    {
        public static Task<IAssetPack> GetAssetPackAsync(this Uri uri)
        {
            var game = uri.ToGame(out var path);
            var filePaths = FileManager.GetFilePaths(path, game);
            if (filePaths == null)
                throw new InvalidOperationException($"{game} not available");
            return Task.FromResult((IAssetPack)new TesAssetPack(filePaths));
        }
    }
}