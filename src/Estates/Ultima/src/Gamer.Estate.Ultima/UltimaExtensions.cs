using Gamer.Core;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Ultima
{
    public static class UltimaExtensions
    {
        public static UltimaGame ToGame(this Uri uri, out string path)
        {
            path = null;
            return UltimaGame.UltimaOnline;
        }

        public static Task<IDataPack> GetDataPackAsync(this Uri uri)
        {
            var game = uri.ToGame(out var path);
            uint.TryParse(path, out var map);
            return Task.FromResult((IDataPack)new UltimaDataPack(map));
        }
    }
}