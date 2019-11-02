using Game.Core;
using Game.Core.Records;
using Game.Estate.UltimaIX.FilePack;
using Game.Core.Netstream;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Estate.UltimaIX
{
    public class UltimaIXDataPack : DatFile, IDataPack
    {
        public UltimaIXDataPack(StreamSink streamSink, string filePath, UltimaIXGame game) : base(streamSink, !string.IsNullOrEmpty(filePath) && Directory.Exists(filePath) ? filePath : null, game) { }

        public Task<byte[]> LoadDataLabelAsync(string label)
        {
            throw new NotImplementedException();
        }

        public void SinkDataContains(string path = null)
        {
            throw new NotImplementedException();
        }

        ICellRecord IDataPack.FindCellRecord(Vector3Int cellId) => FindCELLRecord(cellId);
    }
}
