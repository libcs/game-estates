using Gamer.Core;
using Gamer.Core.Records;
using Gamer.Estate.Tes.FilePack;
using Gamer.Proxy;
using System.IO;
using UnityEngine;

namespace Gamer.Estate.Tes
{
    public class TesDataPack : EsmFile, IDataPack
    {
        public TesDataPack(ProxySink proxySink, string filePath, TesGame game) : base(proxySink, !string.IsNullOrEmpty(filePath) && File.Exists(filePath) ? filePath : null, game) { }

        ICellRecord IDataPack.FindCellRecord(Vector3Int cellId) => FindCELLRecord(cellId);
        //ICellRecord IDataPack.FindInteriorCellRecord(string cellId) { return FindInteriorCellRecord(cellName); }
    }
}
