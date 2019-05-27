using Gamer.Core;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Ultima
{
    public class UltimaEstateHandler : IEstateHandler
    {
        UltimaEstateHandler() { }
        public static readonly IEstateHandler Handler = new UltimaEstateHandler();
        public string Key => "Ultima";
        public Func<Uri, Func<object>, Task<IAssetPack>> AssetPackFunc => async (a, b) => await UltimaExtensions2.GetUltimaAssetPackAsync(a, b);
        public Func<Uri, Func<object>, Task<IDataPack>> DataPackFunc => UltimaExtensions.GetUltimaDataPackAsync;
        public Func<TemporalLoadBalancer, IAssetPack, IDataPack, Func<object>, ICellManager> CellManagerFunc => UltimaExtensions2.GetUltimaCellManager;
    }
}
