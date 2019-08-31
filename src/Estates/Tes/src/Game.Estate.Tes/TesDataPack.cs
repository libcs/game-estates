using Game.Core;
using Game.Core.Records;
using Game.Estate.Tes.FilePack;
using Game.Core.Netstream;
using System.IO;
using UnityEngine;

namespace Game.Estate.Tes
{
    public class TesDataPack : EsmFile, IDataPack
    {
        public TesDataPack(StreamSink streamSink, string filePath, TesGame game) : base(streamSink, !string.IsNullOrEmpty(filePath) && File.Exists(filePath) ? filePath : null, game) { }

        ICellRecord IDataPack.FindCellRecord(Vector3Int cellId) => FindCELLRecord(cellId);
        //ICellRecord IDataPack.FindInteriorCellRecord(string cellId) { return FindInteriorCellRecord(cellName); }
    }
}
