using Game.Core;
using System;
using UnityEngine;

namespace Game.Estate.Tes.Loader
{
    public static class LoadAsset
    {
        static IAssetUnityPack AssetPack;

        public static void Awake() { }

        public static void Start()
        {
            //var assetUri = new Uri("game:/Morrowind.bsa#Morrowind");
            var assetUri = new Uri("http://192.168.1.3/ASSETS/Morrowind/Morrowind.bsa#Morrowind");

            //var assetUri = new Uri("game:/Skyrim*#SkyrimVR");
            //var assetUri = new Uri("game:/Fallout4*#Fallout4VR");

            AssetPack = assetUri.GetTesAssetPackAsync().Result();

            // Morrowind
            //MakeObject("meshes/i/in_dae_room_l_floor_01.nif");
            //MakeObject("meshes/w/w_arrow01.nif");
            MakeObject("meshes/x/ex_common_balcony_01.nif");
            //MakeTexture("meshes/x/ex_common_balcony_01.nif");

            // Skyrim
            //var nifFileLoadingTask = await Asset.LoadObjectInfoAsync("meshes/actors/alduin/alduin.nif");
            //MakeObject("meshes/markerx.nif");
            //MakeObject("meshes/w/w_arrow01.nif");
            //MakeObject("meshes/x/ex_common_balcony_01.nif");
        }

        static GameObject MakeObject(string path) => AssetPack.CreateObject(path);
        static GameObject MakeTexture(string path)
        {
            var textureManager = new TextureManager(AssetPack);
            var materialManager = new MaterialManager(textureManager);
            var obj = GameObject.CreatePrimitive(PrimitiveType.Cube); // GameObject.Find("Cube"); // CreatePrimitive(PrimitiveType.Cube);
            var meshRenderer = obj.GetComponent<MeshRenderer>();
            var materialProps = new MaterialProps
            {
                Textures = new MaterialTextures { MainFilePath = path },
            };
            meshRenderer.material = materialManager.BuildMaterialFromProperties(materialProps);
            return obj;
        }
        static void MakeCursor(string path) => Cursor.SetCursor(AssetPack.LoadTexture(path), Vector2.zero, CursorMode.Auto);

        public static void OnDestroy() => AssetPack.Dispose();
        public static void Update() { }
    }
}
