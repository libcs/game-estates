using Gamer.Core;
using Gamer.Proxy;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Tes
{
    public class TesProxyHandler : IProxyHandler
    {
        public string Key => "Tes";
        public Func<Uri, Task<IAssetPack>> AssetPackFunc => async x => await TesExtensions2.GetTesAssetPackAsync(x);
        public Func<Uri, Task<IDataPack>> DataPackFunc => TesExtensions.GetTesDataPackAsync;
    }
}
