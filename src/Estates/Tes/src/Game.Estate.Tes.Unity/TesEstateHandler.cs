﻿using Game.Core;
using System;
using System.Threading.Tasks;

namespace Game.Estate.Tes
{
    public class TesEstateHandler : IEstateHandler
    {
        TesEstateHandler() { }
        public static readonly IEstateHandler Handler = new TesEstateHandler();
        public string Key => "Tes";
        public Func<Uri, Func<object>, Task<IAssetPack>> AssetPackFunc => async (a, b) => await TesExtensions2.GetTesAssetPackAsync(a, b);
        public Func<Uri, Func<object>, Task<IDataPack>> DataPackFunc => (a, b) => TesExtensions.GetTesDataPackAsync(a, b);
        public Func<TemporalLoadBalancer, IAssetPack, IDataPack, Func<object>, ICellManager> CellManagerFunc => TesExtensions2.GetTesCellManager;
    }
}
