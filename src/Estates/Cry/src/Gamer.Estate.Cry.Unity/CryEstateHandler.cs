using Gamer.Core;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Cry
{
    public class CryEstateHandler : IEstateHandler
    {
        CryEstateHandler() { }
        public static readonly IEstateHandler Handler = new CryEstateHandler();
        public string Key => "Cry";
        public Func<Uri, Func<object>, Task<IAssetPack>> AssetPackFunc => async (a, b) => await CryExtensions2.GetCryAssetPackAsync(a, b);
        public Func<Uri, Func<object>, Task<IDataPack>> DataPackFunc => null;
        public Func<TemporalLoadBalancer, IAssetPack, IDataPack, Func<object>, ICellManager> CellManagerFunc => null;
    }
}
