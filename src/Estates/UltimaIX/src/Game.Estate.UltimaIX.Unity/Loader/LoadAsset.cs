using Game.Core;
using System;
using UnityEngine;
//using Game.Estate.UltimaIX;
using static Game.Core.CoreDebug;

namespace Game.Estate.UltimaIX.Loader
{
    public static class LoadAsset
    {
        static IAssetUnityPack AssetPack;

        public static void Awake() { }

        public static void Start()
        {
            var assetUri = new Uri("game:/static/bitmap16.flx#UltimaIX");
            //var assetUri = new Uri("game:/static/Texture8.9#UltimaIX");
            //var assetUri = new Uri("game:/static/Texture16.9#UltimaIX");

            AssetPack = assetUri.GetUltimaIXAssetPackAsync().Result();

            //MakeTexture($"bitmap/1056", 0);

            MakeImage($"bitmap/1139", 0);
            MakeImage($"bitmap/6499", 5);
            MakeImage($"bitmap/6498", 10);

            //for (var i = 1087; i < 3000; i++)
            //    if (AssetPack.ContainsFile($"texture/{i}"))
            //        MakeTexture($"texture/{i}", 1087 - i);

            //for (var i = 0; i < 10; i++)
            //    if (AssetPack.ContainsFile($"bitmap/{i}"))
            //        MakeTexture($"bitmap/{i}", i);

            //MakeTexture("1087", 1);
            //MakeTexture("1211", 2);
            //MakeTexture("1360", 3);
            //MakeObject("0");
        }

        static GameObject MakeObject(string path) => AssetPack.CreateObject(path);

        static GameObject MakeImage(string path, int idx)
        {
            var tex = AssetPack.LoadTexture(path);
            var obj = GameObject.CreatePrimitive(PrimitiveType.Quad);
            obj.name = path.Replace("/", "");
            obj.transform.localScale = new Vector3(tex.width, tex.height, 0);
            //obj.transform.localPosition += new Vector3(10 * (idx % 10), 0, 10 * (idx / 10));
            var mat = obj.GetComponent<MeshRenderer>().material;
            mat.shader = Shader.Find("Unlit/Texture");
            mat.mainTexture = tex;
            return obj;
        }

        static GameObject MakeTexture(string path, int idx)
        {
            var textureManager = new TextureManager(AssetPack);
            var materialManager = new MaterialManager(textureManager);
            var obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            obj.name = path.Replace("/", "");
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
