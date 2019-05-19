using Gamer.Core;
using Gamer.Proxy;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Ultima
{
    public class UltimaProxyHandler : IProxyHandler
    {
        public string Key => "Ultima";
        public Func<Uri, Task<IAssetPack>> AssetPackFunc => async x => await UltimaExtensions2.GetUltimaAssetPackAsync(x);
        public Func<Uri, Task<IDataPack>> DataPackFunc => UltimaExtensions.GetUltimaDataPackAsync;
    }
}
