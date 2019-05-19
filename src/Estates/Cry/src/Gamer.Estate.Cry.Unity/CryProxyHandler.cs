using Gamer.Core;
using Gamer.Proxy;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Cry
{
    public class CryProxyHandler : IProxyHandler
    {
        public string Key => "Cry";
        public Func<Uri, Task<IAssetPack>> AssetPackFunc => async x => await CryExtensions2.GetCryAssetPackAsync(x);
        public Func<Uri, Task<IDataPack>> DataPackFunc => null;
    }
}
