//using Gamer.Core;
//using System;

//namespace Gamer.Estate.Rsi.Loader
//{
//    public static class LoadObject
//    {
//        static IAssetPack Asset;

//        public static void Awake()
//        {
//        }

//        public static void Start()
//        {
//            var assetUri = new Uri("game://Morrowind/Morrowind.bsa");

//            Asset = assetUri.GetAssetPack();
//            //Data = dataUri.GetDataPack();

//            // Morrowind
//            MakeObject("meshes/i/in_dae_room_l_floor_01.nif");
//            //MakeObject("meshes/w/w_arrow01.nif");
//            //MakeObject("meshes/x/ex_common_balcony_01.nif");

//            // Skyrim
//            //var nifFileLoadingTask = Asset.LoadObjectInfoAsync("meshes/actors/alduin/alduin.nif");
//            //MakeObject("meshes/markerx.nif");
//            //MakeObject("meshes/w/w_arrow01.nif");
//            //MakeObject("meshes/x/ex_common_balcony_01.nif");

//        }

//        static void MakeObject(string path) => Asset.CreateObject(path);

//        public static void OnDestroy()
//        {
//            Asset?.Dispose();
//            Asset = null;
//        }

//        public static void Update()
//        {
//        }
//    }
//}
