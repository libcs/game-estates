using Gamer.Core;
using Gamer.Core.Records;
using Gamer.Estate.UltimaIX.FilePack;
using Gamer.Proxy;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Gamer.Estate.UltimaIX
{
    public class UltimaIXDataPack : DatFile, IDataPack
    {
        public UltimaIXDataPack(ProxySink proxySink, string filePath, UltimaIXGame game) : base(proxySink, !string.IsNullOrEmpty(filePath) && File.Exists(filePath) ? filePath : null, game) { }

        public Task<byte[]> LoadDataLabelAsync(string label)
        {
            throw new NotImplementedException();
        }

        public void SinkDataContains(string path = null)
        {
            throw new NotImplementedException();
        }

        ICellRecord IDataPack.FindCellRecord(Vector3Int cellId) => throw new NotImplementedException(); // FindCellRecord(cellId);
    }
}
