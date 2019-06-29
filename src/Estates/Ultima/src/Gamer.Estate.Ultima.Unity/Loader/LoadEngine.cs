using Gamer.Core;
using System;
using UnityEngine;

namespace Gamer.Estate.Ultima.Loader
{
    public static class LoadEngine
    {
        static SimpleEngine Engine;
        static GameObject PlayerPrefab;

        public static void Awake() => PlayerPrefab = GameObject.Find("Player00");

        public static void Start()
        {
            var assetUri = new Uri("game:/#Zero");
            var dataUri = new Uri("game:/#Zero");

            Engine = new SimpleEngine(UltimaEstateHandler.Handler, assetUri, dataUri);

            // engine
            var scale = ConvertUtils.ExteriorCellSideLengthInMeters;
            //Engine.SpawnPlayerAndUpdate(PlayerPrefab, new Vector3(4 * scale, 20, 25 * scale));
            //Engine.SpawnPlayerAndUpdate(PlayerPrefab, new Vector3(15 * scale, 20, 25 * scale));
            Engine.SpawnPlayerAndUpdate(PlayerPrefab, new Vector3(11 * scale, 10, 29 * scale));
        }

        public static void OnDestroy() => Engine.Dispose();
        public static void Update() => Engine.Update();
    }
}
