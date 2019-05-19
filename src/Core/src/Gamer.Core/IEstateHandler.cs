using System;
using System.Threading.Tasks;

namespace Gamer.Core
{
    public interface IEstateHandler
    {
        string Key { get; }
        Func<Uri, Func<object>, Task<IAssetPack>> AssetPackFunc { get; }
        Func<Uri, Func<object>, Task<IDataPack>> DataPackFunc { get; }
        Func<TemporalLoadBalancer, IAssetPack, IDataPack, Func<object>, ICellManager> CellManagerFunc { get; }
    }
}
