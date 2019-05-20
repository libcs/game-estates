using Gamer.Core;
using System;
using UnityEngine;

namespace Gamer.Estate.Ultima.Loader
{
    public static class LoadAsset
    {
        static IAssetUnityPack AssetPack;

        public static void Awake() { }

        public static void Start()
        {
            var assetUri = new Uri("game://Zero/");

            AssetPack = assetUri.GetUltimaAssetPackAsync().Result;

            //MakeObject("sta010").transform.Translate(Vector3.left * 1 + Vector3.up);
            //MakeObject("sta069").transform.Translate(Vector3.left * 1 + Vector3.down);

            //MakeObject("lnd001").transform.Translate(Vector3.right * 1);
            //MakeObject("lnd002").transform.Translate(Vector3.right * 1 + Vector3.up);
            //MakeObject("lnd516").transform.Translate(Vector3.right * 1 + Vector3.down);

            //MakeObject("gmp065").transform.Translate(Vector3.back * 5);

            MakeTexture("sta010");
            //MakeTexture("sta069");

            //MakeTexture("lnd001");
            //MakeTexture("lnd002");
            //MakeTexture("lnd516");
            //MakeTexture("lnd1137");

            //MakeTexture("gmp065");

            //MakeTexture("tex789");
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
