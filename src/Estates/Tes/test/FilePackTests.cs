using Gamer.Estate.Nif;
using Gamer.Estate.Tes.Records;
using System;
using System.Linq;
using UnityEngine;
using Xunit;
using Xunit.Abstractions;
using static Gamer.Core.Debug;

namespace Gamer.Estate.Tes.Tests
{
    public class FilePackTests
    {
        public FilePackTests(ITestOutputHelper helper) => LogFunc = x => helper.WriteLine(x.ToString());

        [Theory]
        [InlineData("game://Morrowind/Morrowind.bsa")]
        [InlineData("game://Oblivion/Oblivion*")]
        [InlineData("game://Fallout4VR/Fallout4 - Materials.ba2")]
        //[InlineData("game://SkyrimVR/Skyrim*")]
        //[InlineData("game://Fallout4/Fallout4*")]
        //[InlineData("game://Fallout4VR/Fallout4*")]
        public void LoadAssetPack(string path)
        {
            var asset = (TesAssetPack)new Uri(path).GetAssetPack().Result;
            foreach (var pack in asset.Packs)
            {
                pack.TestContainsFile();
                pack.TestLoadFileData();
            }
        }

        [Theory]
        [InlineData("game://Morrowind/Morrowind.esm")]
        [InlineData("game://Morrowind/Bloodmoon.esm")]
        [InlineData("game://Morrowind/Tribunal.esm")]
        [InlineData("game://Oblivion/Oblivion.esm")]
        [InlineData("game://SkyrimVR/Skyrim.esm")]
        [InlineData("game://Fallout4/Fallout4.esm")]
        [InlineData("game://Fallout4VR/Fallout4.esm")]
        public void LoadDataPack(string path)
        {
            var data = (TesDataPack)new Uri(path).GetDataPack().Result;
            TestLoadCell(data, new Vector3(0, 0, 0));
            TestAllCells(data);


            ////TestLoadCell(new Vector3(((-2 << 5) + 1) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, ((-1 << 5) + 1) * ConvertUtils.ExteriorCellSideLengthInMeters));
            ////TestLoadCell(new Vector3((-1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, (-1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters));
            //TestLoadCell(new Vector3(0 * ConvertUtils.ExteriorCellSideLengthInMeters, 0, 0 * ConvertUtils.ExteriorCellSideLengthInMeters));
            ////TestLoadCell(new Vector3((1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, (1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters));
            ////TestLoadCell(new Vector3((1 << 5) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, (1 << 5) * ConvertUtils.ExteriorCellSideLengthInMeters));
            ////TestAllCells();

        }

        public static Vector3Int GetCellId(Vector3 point, int world) => new Vector3Int(Mathf.FloorToInt(point.x / ConvertUtils.ExteriorCellSideLengthInMeters), Mathf.FloorToInt(point.z / ConvertUtils.ExteriorCellSideLengthInMeters), world);

        static void TestLoadCell(TesDataPack data, Vector3 position)
        {
            var cellId = GetCellId(position, 60);
            var cell = data.FindCellRecord(cellId);
            var land = data.FindLANDRecord(cellId);
        }

        static void TestAllCells(TesDataPack data)
        {
            foreach (var record in data.Groups["CELL"].Records.Cast<CELLRecord>())
                Log(record.EDID.Value);
        }
    }
}
