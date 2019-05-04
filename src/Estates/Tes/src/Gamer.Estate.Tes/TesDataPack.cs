using Gamer.Base;
using Gamer.Base.Records;
using Gamer.Estate.Tes.FilePack;
using System.IO;
using UnityEngine;

namespace Gamer.Estate.Tes
{
    public class TesDataPack : EsmFile, IDataPack
    {
        public TesDataPack(string filePath, TesGame game) : base(filePath != null && File.Exists(filePath) ? filePath : null, game) { }

        ICellRecord IDataPack.FindCellRecord(Vector3Int cellId) => FindCellRecord(cellId);
        //ICellRecord IDataPack.FindInteriorCellRecord(string cellId) { return FindInteriorCellRecord(cellName); }
    }
}
