using Gamer.Core;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Rsi
{
    public class RsiEstateHandler : IEstateHandler
    {
        public string Key => "Rsi";
        public Func<Uri, Func<object>, Task<IAssetPack>> AssetPackFunc => async (a, b) => await RsiExtensions2.GetRsiAssetPackAsync(a, b);
        public Func<Uri, Func<object>, Task<IDataPack>> DataPackFunc => null;
        public Func<TemporalLoadBalancer, IAssetPack, IDataPack, Func<object>, ICellManager> CellManagerFunc => null;
    }
}
