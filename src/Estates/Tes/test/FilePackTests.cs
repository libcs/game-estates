//#define LONGTEST
using Gamer.Estate.Tes.Records;
using Gamer.Format.Nif;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Xunit;
using Xunit.Abstractions;

namespace Gamer.Estate.Tes.Tests
{
    public class FilePackTests
    {
        public FilePackTests(ITestOutputHelper helper) => Core.Debug.LogFunc = x => helper.WriteLine(x.ToString());

        [Theory]
        [InlineData("game:/Morrowind.bsa#Morrowind", "textures/Tx_BC_moss.dds")]
        //[InlineData("file:///C:/Program%20Files%20(x86)/Steam/steamapps/common/Morrowind/Data%20Files/Morrowind.*#Morrowind", "textures/Tx_BC_moss.dds")]
        [InlineData("file:///C:/Program%20Files%20(x86)/Steam/steamapps/common/Morrowind/Data%20Files/Morrowind.bsa#Morrowind", "textures/Tx_BC_moss.dds")]
        [InlineData("http://192.168.1.3/ASSETS/Morrowind/Morrowind.bsa#Morrowind", "textures/Tx_BC_moss.dds")]
        //[InlineData("file://192.168.1.3/User/_ASSETS/Fallout4/Textures1";
        public async Task LoadAssetPack(string path, string modelPath)
        {
            // given
            using (var assetPack = await new Uri(path).GetTesAssetPackAsync())
            {
                // when
                var exist0 = assetPack.ContainsFile(modelPath);
                var data0 = await assetPack.LoadFileDataAsync(modelPath);
                // then
                Assert.True(exist0);
                Assert.NotNull(data0);
            }
        }

        [Theory]
#if LONGTEST
        [InlineData("game:/Morrowind.bsa#Morrowind")]
        [InlineData("game:/Oblivion*#Oblivion")]
        [InlineData("game:/Skyrim*#SkyrimVR")]
        [InlineData("game:/Fallout4*#Fallout4")]
        [InlineData("game:/Fallout4*#Fallout4VR")]
#else
        //[InlineData("game:/Fallout4 - Materials.ba2#Fallout4VR")]
        [InlineData("game:/Morrowind.bsa#Morrowind")]
        [InlineData("file:///C:/Program%20Files%20(x86)/Steam/steamapps/common/Morrowind/Data%20Files/Morrowind.bsa#Morrowind")]
        //[InlineData("http://192.168.1.3/ASSETS/Morrowind/Morrowind.bsa#Morrowind")]
#endif
        public async Task LoadAssetPackAll(string path)
        {
            using (var assetPack = await new Uri(path).GetTesAssetPackAsync() as TesAssetPack)
                foreach (var pack in assetPack.Packs)
                {
                    pack.TestContainsFile();
#if LONGTEST
                    pack.TestLoadFileData(int.MaxValue);
#else
                    pack.TestLoadFileData(100);
#endif
                }
        }

        [Theory]
#if LONGTEST
        [InlineData("game:/Morrowind.esm#Morrowind")]
        [InlineData("game:/Bloodmoon.esm#Morrowind")]
        [InlineData("game:/Tribunal.esm#Morrowind")]
        [InlineData("game:/Oblivion.esm#Oblivion")]
        [InlineData("game:/Skyrim.esm#SkyrimVR")]
        [InlineData("game:/Fallout4.esm#Fallout4")]
        [InlineData("game:/Fallout4.esm#Fallout4VR")]
#else
        [InlineData("game:/Morrowind.esm#Morrowind")]
        [InlineData("file:///C:/Program%20Files%20(x86)/Steam/steamapps/common/Morrowind/Data%20Files/Morrowind.esm#Morrowind")]
        //[InlineData("http://192.168.1.3/ASSETS/Morrowind/Morrowind.esm#Morrowind")]
#endif
        public async Task LoadDataPack(string path)
        {
            using (var dataPack = (TesDataPack)await new Uri(path).GetTesDataPackAsync())
            {
                TestLoadCell(dataPack, new Vector3(0, 0, 0));
                TestAllCells(dataPack);
            }

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
                if (!string.IsNullOrEmpty(record.EDID.Value))
                    Core.Debug.Log(record.EDID.Value);
        }
    }
}
