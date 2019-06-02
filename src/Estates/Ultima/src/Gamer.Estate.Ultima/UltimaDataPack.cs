using System.Threading.Tasks;
using Gamer.Core;
using Gamer.Core.Records;
using Gamer.Estate.Ultima.FilePack;
using UnityEngine;

namespace Gamer.Estate.Ultima
{
    public class UltimaDataPack : DataFile, IDataPack
    {
        public UltimaDataPack(uint map) : base(map) { }

        public void SinkDataContains(string path) => throw new System.NotImplementedException();
        public Task<byte[]> LoadDataLabelAsync(string label) => throw new System.NotImplementedException();

        ICellRecord IDataPack.FindCellRecord(Vector3Int cellId) => FindCellRecord(cellId);
    }
}
