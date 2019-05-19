using Gamer.Core;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Ultima
{
    public static class UltimaExtensions2
    {
        public static Task<IAssetUnityPack> GetUltimaAssetPackAsync(this Uri uri, Func<object> func = null)
        {
            uri.ToUltimaGame(func, out var proxySink, out var filePaths);
            return Task.FromResult((IAssetUnityPack)new UltimaAssetPack(proxySink));
        }

        public static ICellManager GetUltimaCellManager(this TemporalLoadBalancer loadBalancer, IAssetPack assetPack, IDataPack dataPack, Func<object> func = null) => new UltimaCellManager(loadBalancer, (UltimaAssetPack)assetPack, (UltimaDataPack)dataPack);
    }
}