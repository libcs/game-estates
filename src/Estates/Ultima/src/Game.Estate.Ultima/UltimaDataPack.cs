using System.Threading.Tasks;
using Game.Core;
using Game.Core.Records;
using Game.Estate.Ultima.FilePack;
using UnityEngine;

namespace Game.Estate.Ultima
{
    public class UltimaDataPack : DataFile, IDataPack
    {
        public UltimaDataPack(uint map) : base(map) { }

        public void SinkDataContains(string path) => throw new System.NotImplementedException();
        public Task<byte[]> LoadDataLabelAsync(string label) => throw new System.NotImplementedException();

        ICellRecord IDataPack.FindCellRecord(Vector3Int cellId) => FindCellRecord(cellId);
    }
}
