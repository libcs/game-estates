using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Game.Core.Debug;

namespace Game.Estate.Ultima.Tests
{
    public class FilePackTests
    {
        public FilePackTests(ITestOutputHelper helper) => LogFunc = x => helper.WriteLine(x.ToString());

        [Theory]
        [InlineData("game:/#Zero")]
        public async Task LoadAssetPack(string path)
        {
            using (var assetPack = await new Uri(path).GetUltimaAssetPackAsync() as UltimaAssetPack)
            {
                //foreach (var pack in assetPack.Packs)
                //{
                //    pack.TestContainsFile();
                //    pack.TestLoadFileData();
                //}
            }
        }

        [Theory]
        [InlineData("game:/#Zero")]
        public async Task LoadDataPack(string path)
        {
            using (var dataPack = await new Uri(path).GetUltimaDataPackAsync() as UltimaDataPack)
            {
                //TestLoadCell(dataPack, new Vector3(0, 0, 0));
                //TestAllCells(dataPack);
            }
        }

        //public static Vector3Int GetCellId(Vector3 point, int world) => new Vector3Int(Mathf.FloorToInt(point.x / ConvertUtils.ExteriorCellSideLengthInMeters), Mathf.FloorToInt(point.z / ConvertUtils.ExteriorCellSideLengthInMeters), world);

        //static void TestLoadCell(TesDataPack data, Vector3 position)
        //{
        //    var cellId = GetCellId(position, 60);
        //    var cell = data.FindCellRecord(cellId);
        //    var land = data.FindLANDRecord(cellId);
        //}

        //static void TestAllCells(TesDataPack data)
        //{
        //    foreach (var record in data.Groups["CELL"].Records.Cast<CELLRecord>())
        //        Log(record.EDID.Value);
        //}
    }
}
