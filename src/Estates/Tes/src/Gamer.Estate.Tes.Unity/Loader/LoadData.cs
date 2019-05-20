using Gamer.Core;
using Gamer.Core.Records;
using Gamer.Estate.Tes.Records;
using Gamer.Format.Nif;
using System;
using System.Linq;
using UnityEngine;
using static Gamer.Core.Debug;

namespace Gamer.Estate.Tes.Loader
{
    public static class LoadData
    {
        static IDataPack DataPack;

        public static void Awake() { }

        public static void Start()
        {
            var dataUri = new Uri("game://Morrowind/Morrowind.esm");
            //var dataUri = new Uri("game://Morrowind/Bloodmoon.esm");
            //var dataUri = new Uri("game://Morrowind/Tribunal.esm");

            //var dataUri = new Uri("game://Oblivion/Oblivion.esm");

            //var dataUri = new Uri("game://SkyrimVR/Skyrim.esm");

            //var dataUri = new Uri("game://Fallout4/Fallout4.esm");

            //var dataUri = new Uri("game://Fallout4VR/Fallout4.esm");

            DataPack = dataUri.GetTesDataPackAsync().Result;

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
            var land = ((TesDataPack)DataPack).FindLANDRecord(cellId);
            Log($"LAND #{land?.Id}");
        }

        static void TestAllCells()
        {
            var cells = ((TesDataPack)DataPack).Groups["CELL"].Records;
            Log($"CELLS: {cells.Count}");
            foreach (var record in cells.Cast<CELLRecord>())
                Log(record.EDID.Value);
        }

        public static void OnDestroy() { DataPack?.Dispose(); DataPack = null; }

        public static void Update() { }
    }
}
