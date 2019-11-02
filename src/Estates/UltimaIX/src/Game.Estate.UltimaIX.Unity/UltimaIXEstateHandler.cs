using Game.Core;
using System;
using System.Threading.Tasks;

namespace Game.Estate.UltimaIX
{
    public class UltimaIXEstateHandler : IEstateHandler
    {
        UltimaIXEstateHandler() { }
        public static readonly IEstateHandler Handler = new UltimaIXEstateHandler();
        public string Key => "UltimaIX";
        public Func<Uri, Func<object>, Task<IAssetPack>> AssetPackFunc => async (a, b) => await UltimaIXExtensions2.GetUltimaIXAssetPackAsync(a, b);
        public Func<Uri, Func<object>, Task<IDataPack>> DataPackFunc => (a, b) => UltimaIXExtensions.GetUltimaIXDataPackAsync(a, b);
        public Func<TemporalLoadBalancer, IAssetPack, IDataPack, Func<object>, ICellManager> CellManagerFunc => UltimaIXExtensions2.GetUltimaIXCellManager;
    }
}
