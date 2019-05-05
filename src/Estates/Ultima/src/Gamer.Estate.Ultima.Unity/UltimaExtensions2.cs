using Gamer.Core;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Ultima
{
    public static class UltimaExtensions2
    {
        public static Task<IAssetPack> GetAssetPack(this Uri uri)
        {
            var game = uri.ToGame(out var path);
            return Task.FromResult((IAssetPack)new UltimaAssetPack());
        }
    }
}