 using Gamer.Core;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Tes
{
    public static class TesExtensions2
    {
        public static Task<IAssetUnityPack> GetTesAssetPackAsync(this Uri uri, Func<object> func = null)
        {
            uri.ToTesGame(func, out var proxySink, out var filePaths);
            return Task.FromResult((IAssetUnityPack)new TesAssetPack(proxySink, filePaths));
        }

        public static ICellManager GetTesCellManager(this TemporalLoadBalancer loadBalancer, IAssetPack assetPack, IDataPack dataPack, Func<object> func = null) => new TesCellManager(loadBalancer, (TesAssetPack)assetPack, (TesDataPack)dataPack);
    }
}
