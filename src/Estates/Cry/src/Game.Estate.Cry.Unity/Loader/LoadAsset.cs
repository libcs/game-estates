using Game.Core;
using System;
using UnityEngine;
using Game.Estate.Cry;
using static Game.Core.Debug;

namespace Game.Estate.Cry.Loader
{
    public static class LoadAsset
    {
        static IAssetUnityPack AssetPack;

        public static void Awake() { }

        public static void Start()
        {
            var assetUri = new Uri("game:/#StarCitizen");

            AssetPack = assetUri.GetCryAssetPackAsync().Result();

            //MakeObject(@"Data\Objects\DFM\DFM_NewHorizonSpeedway\architecture\race_ring\holo_chevron.cgf");
            //MakeObject(@"Data\Objects\DFM\DFM_NewHorizonSpeedway\architecture\race_ring\holo_chevron_set_curved_left.cgf");
            //MakeObject(@"Data\Objects\DFM\DFM_NewHorizonSpeedway\architecture\race_ring\holo_chevron_set_curved_right.cgf");
            //MakeObject(@"Data\Objects\DFM\DFM_NewHorizonSpeedway\architecture\race_ring\holo_finish.cgf");
            //MakeObject(@"Data\Objects\DFM\DFM_NewHorizonSpeedway\architecture\race_ring\holo_lapindicator.cgf");
            //MakeObject(@"Data\Objects\DFM\DFM_NewHorizonSpeedway\architecture\race_ring\holo_start.cgf");
            //MakeObject(@"Data\Objects\DFM\DFM_NewHorizonSpeedway\architecture\race_ring\race_ring_holo_center.cgf");
            //MakeObject(@"Data\Objects\DFM\DFM_NewHorizonSpeedway\architecture\race_ring\race_ring_holo_chevron.cgf");


            MakeObject(@"Data\Objects\Spaceships\Ships\ARGO\MPUV_Utility_Vehicle\ARGO_MPUV_lod1.cga");
            //MakeObject(@"Data\Objects\Spaceships\Ships\DRAK\Caterpillar\exteriors\DRAK_Caterpillar.cga");
            //MakeObject(@"Data\Objects\Spaceships\Ships\DRAK\Caterpillar\exteriors\DRAK_Caterpillar_lod1.cga");
            //MakeObject(@"Data\Objects\Spaceships\Ships\DRAK\Caterpillar\exteriors\DRAK_Caterpillar_lod5.cga");
            //MakeObject(@"Data\Objects\Spaceships\Ships\MISC\Prospector\MISC_Prospector_lod1.cga");
            //MakeObject(@"Data\Objects\Spaceships\Ships\MISC\Prospector\MISC_Prospector_lod5.cga");
            //MakeObject(@"Data\Objects\animals\fish\CleanerFish_clean_prop_animal_01.chr");
            //MakeObject(@"Data\Objects\animals\sandWorm\sandWorm.chr");

            //MakeObject(@"Data\Objects\test\Dummytest.cgf");
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
