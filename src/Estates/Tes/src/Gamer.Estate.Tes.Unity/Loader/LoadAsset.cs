using Gamer.Core;
using System;

namespace Gamer.Estate.Tes.Loader
{
    public static class LoadAsset
    {
        static IAssetUnityPack AssetPack;

        public static void Awake() { }

        //var assetUri = "file:///C:/Program%20Files%20(x86)/Steam/steamapps/common/Morrowind/Data%20Files/Morrowind.*";
        //var file2Uri = "file://192.168.1.3/User/_ASSETS/Fallout4/Textures1";
        //var file4Uri = "http://192.168.1.3/assets/Morrowind/Morrowind.bsa";
        //var file4Uri = "http://192.168.1.3/assets/Morrowind/Morrowind.bsa";
        public static void Start()
        {
            var assetUri = new Uri("game://Morrowind/Morrowind.bsa");
            //var assetUri = new Uri("game://SkyrimVR/Skyrim*");
            //var assetUri = new Uri("game://Fallout4VR/Fallout4*");

            AssetPack = assetUri.GetTesAssetPackAsync().Result;

            // Morrowind
            MakeObject("meshes/i/in_dae_room_l_floor_01.nif");
            MakeObject("meshes/w/w_arrow01.nif");
            MakeObject("meshes/x/ex_common_balcony_01.nif");

            // Skyrim
            //var nifFileLoadingTask = Asset.LoadObjectInfoAsync("meshes/actors/alduin/alduin.nif").Result;
            //MakeObject("meshes/markerx.nif");
            //MakeObject("meshes/w/w_arrow01.nif");
            //MakeObject("meshes/x/ex_common_balcony_01.nif");
        }

        static void MakeObject(string path) => AssetPack.CreateObject(path);

        public static void OnDestroy() => AssetPack.Dispose();
        public static void Update() { }
    }
}
