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
        public TesDataPack(ProxySink client, string filePath, TesGame game) : base(client, filePath != null && File.Exists(filePath) ? filePath : null, game) { }

        ICellRecord IDataPack.FindCellRecord(Vector3Int cellId) => FindCellRecord(cellId);
        //ICellRecord IDataPack.FindInteriorCellRecord(string cellId) { return FindInteriorCellRecord(cellName); }
    }
}
