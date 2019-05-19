using Gamer.Core;
using System;
using System.Threading.Tasks;

namespace Gamer.Proxy
{
    public interface IProxyHandler
    {
        string Key { get; }
        Func<Uri, Task<IAssetPack>> AssetPackFunc { get; }
        Func<Uri, Task<IDataPack>> DataPackFunc { get; }
    }
}
