using Gamer.Core;
using System;
using UnityEngine;
using static Gamer.Core.Debug;

namespace Gamer.Estate.Ultima.Loader
{
    public static class LoadData
    {
        static IDataPack DataPack;

        public static void Awake() { }

        public static void Start()
        {
            var dataUri = new Uri("game://Zero/");

            DataPack = dataUri.GetUltimaDataPackAsync().Result;

            //TestLoadCell(new Vector3(((-2 << 5) + 1) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, ((-1 << 5) + 1) * ConvertUtils.ExteriorCellSideLengthInMeters));
            //TestLoadCell(new Vector3((-1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, (-1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters));
            TestLoadCell(new Vector3(0 * ConvertUtils.ExteriorCellSideLengthInMeters, 0, 0 * ConvertUtils.ExteriorCellSideLengthInMeters));
            //TestLoadCell(new Vector3((1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, (1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters));
            //TestLoadCell(new Vector3((1 << 5) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, (1 << 5) * ConvertUtils.ExteriorCellSideLengthInMeters));
            //TestAllCells();
        }

        public static Vector3Int GetCellId(Vector3 point, int world) => new Vector3Int(Mathf.FloorToInt(point.x / ConvertUtils.ExteriorCellSideLengthInMeters), Mathf.FloorToInt(point.z / ConvertUtils.ExteriorCellSideLengthInMeters), world);

        static void TestLoadCell(Vector3 position)
        {
            var cellId = GetCellId(position, 60);
            var cell = DataPack.FindCellRecord(cellId);
            var land = ((UltimaDataPack)DataPack).FindLANDRecord(cellId);
            Log($"LAND #{land?.GridId}");
        }

        //static void TestAllCells()
        //{
        //    var cells = ((UltimaDataPack)DataPack).Groups["CELL"].Records;
        //    Log($"CELLS: {cells.Count}");
        //    foreach (var record in cells.Cast<CELLRecord>())
        //        Log(record.EDID.Value);
        //}

        public static void OnDestroy() { DataPack?.Dispose(); DataPack = null; }
        public static void Update() { }
    }
}
