using Game.Core;
using System;
using UnityEngine;
//using Game.Estate.UltimaIX;
//using static Game.Core.Debug;

namespace Game.Estate.UltimaIX.Loader
{
    public static class LoadAsset
    {
        static IAssetUnityPack AssetPack;

        public static void Awake() { }

        public static void Start()
        {
            //var assetUri = new Uri("game:/static/Texture8.9#UltimaIX");
            var assetUri = new Uri("game:/static/Texture16.9#UltimaIX");
            AssetPack = assetUri.GetUltimaIXAssetPackAsync().Result();
            for (var i = 1087; i < 3000; i++)
                if (AssetPack.ContainsFile($"{i}"))
                    MakeTexture($"{i}", 1087 - i);
            //MakeTexture("1087", 1);
            //MakeTexture("1211", 2);
            //MakeTexture("1360", 3);
            //MakeObject("0");
        }

        static GameObject MakeObject(string path) => AssetPack.CreateObject(path);
        static GameObject MakeTexture(string path, int idx)
        {
            var textureManager = new TextureManager(AssetPack);
            var materialManager = new MaterialManager(textureManager);
            var obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            obj.name = "tex" + path;
            obj.transform.localPosition += new Vector3(10 * (idx % 10), 0, 10 * (idx / 10));
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
