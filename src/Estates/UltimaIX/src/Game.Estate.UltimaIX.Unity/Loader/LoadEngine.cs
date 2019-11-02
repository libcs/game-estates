using Game.Core;
using System;
using UnityEngine;

namespace Game.Estate.UltimaIX.Loader
{
    public static class LoadEngine
    {
        static SimpleEngine Engine;
        static GameObject PlayerPrefab;

        public static void Awake() => PlayerPrefab = GameObject.Find("Player00");

        public static void Start()
        {
            var assetUri = new Uri("game:/#UltimaIX");
            var dataUri = new Uri("game:/#UltimaIX");

            Engine = new SimpleEngine(UltimaIXEstateHandler.Handler, assetUri, dataUri);

            // engine (14 - Avatar House Earth)
            // engine (09 - Britania)
            Engine.CurrentWorld = 09;
            Engine.SpawnPlayer(PlayerPrefab, new Vector3(100f, 100f, 0));
        }

        public static void OnDestroy() => Engine?.Dispose();
        public static void Update() => Engine?.Update();
    }
}
