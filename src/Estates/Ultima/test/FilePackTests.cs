//#define LONGTEST

using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Gamer.Core.Debug;

namespace Gamer.Estate.Ultima.Tests
{
    public class FilePackTests
    {
        public FilePackTests(ITestOutputHelper helper) => LogFunc = x => helper.WriteLine(x.ToString());

        [Theory]
        [InlineData("game://UltimaOnline/*")]
        public async Task LoadAssetPack(string path)
        {
            var asset = await new Uri(path).GetAssetPackAsync() as UltimaAssetPack;
            //foreach (var pack in asset.Packs)
            //{
            //    pack.TestContainsFile();
            //    pack.TestLoadFileData();
            //}
        }

        [Theory]
        [InlineData("game://UltimaOnline/*")]
        public async Task LoadDataPack(string path)
        {
            var data = await new Uri(path).GetDataPackAsync() as UltimaDataPack;
            //TestLoadCell(data, new Vector3(0, 0, 0));
            //TestAllCells(data);
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
