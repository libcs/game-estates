using Game.Core;
using System;
using System.Threading.Tasks;

namespace Game.Estate.Ultima
{
    public static class UltimaExtensions2
    {
        public static Task<IAssetUnityPack> GetUltimaAssetPackAsync(this Uri uri, Func<object> func = null)
        {
            uri.ToUltimaGame(func, out var streamSink, out var filePaths);
            return Task.FromResult((IAssetUnityPack)new UltimaAssetPack(streamSink));
        }

        public static ICellManager GetUltimaCellManager(this TemporalLoadBalancer loadBalancer, IAssetPack assetPack, IDataPack dataPack, Func<object> func = null) => new UltimaCellManager(loadBalancer, (UltimaAssetPack)assetPack, (UltimaDataPack)dataPack);
    }
}