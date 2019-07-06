using Gamer.Core;
using System;
using UnityEngine;
using Gamer.Estate.Cry;
using static Gamer.Core.Debug;

namespace Gamer.Estate.Cry.Loader
{
    public static class LoadAsset
    {
        static IAssetUnityPack AssetPack;

        public static void Awake() { }

        public static void Start()
        {
            var assetUri = new Uri("game:/#StarCitizen");

            AssetPack = assetUri.GetCryAssetPackAsync().Result();

            //MakeObject(@"Data\Objects\animals\fish\CleanerFish_clean_prop_animal_01.chr");
            MakeObject(@"Data\Objects\animals\sandWorm\sandWorm.chr");
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
