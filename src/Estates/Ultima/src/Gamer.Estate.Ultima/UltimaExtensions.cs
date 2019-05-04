using System;

namespace Gamer.Estate.Ultima
{
    public static class UltimaExtensions
    {
        public static UltimaGame ToGame(this Uri uri, out string path)
        {
            path = null;
            return UltimaGame.UltimaOnline;
        }
    }
}