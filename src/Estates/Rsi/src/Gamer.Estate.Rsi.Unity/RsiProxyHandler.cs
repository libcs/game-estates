using Gamer.Core;
using Gamer.Proxy;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Rsi
{
    public class RsiProxyHandler : IProxyHandler
    {
        public string Key => "Rsi";
        public Func<Uri, Task<IAssetPack>> AssetPackFunc => async x => await RsiExtensions2.GetRsiAssetPackAsync(x);
        public Func<Uri, Task<IDataPack>> DataPackFunc => null;
    }
}
