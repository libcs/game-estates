using Gamer.Core;
using System;
using UnityEngine;

namespace Gamer.Estate.Tes.Loader
{
    public static class LoadEngine
    {
        static SimpleEngine Engine;
        static GameObject PlayerPrefab;

        public static void Awake() => PlayerPrefab = GameObject.Find("Player00");

        public static void Start()
        {
            var assetUri = new Uri("game://Morrowind/Morrowind.bsa");
            var dataUri = new Uri("game://Morrowind/Morrowind.esm");

            //var assetUri = new Uri("game://Oblivion/Oblivion*");
            //var dataUri = new Uri("game://Oblivion/Oblivion.esm");

            Engine = new SimpleEngine(new TesEstateHandler(), assetUri, dataUri);

            // engine
            Engine.SpawnPlayer(PlayerPrefab, new Vector3(-137.94f, 2.30f, -1037.6f)); // new Vector3Int(-2, -9)

            // engine - oblivion
            //Engine.SpawnPlayer(PlayerPrefab, new Vector3Int(0, 0, 60), new Vector3(0, 0, 0));
        }

        public static void OnDestroy() => Engine.Dispose();
        public static void Update() => Engine.Update();
    }
}
