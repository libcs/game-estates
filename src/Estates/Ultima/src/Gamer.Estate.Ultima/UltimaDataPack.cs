using Gamer.Core;
using Gamer.Core.Records;
using Gamer.Estate.Ultima.FilePack;
using UnityEngine;

namespace Gamer.Estate.Ultima
{
    public class UltimaDataPack : DataFile, IDataPack
    {
        public UltimaDataPack(uint map) : base(map) { }

        ICellRecord IDataPack.FindCellRecord(Vector3Int cellId) => FindCellRecord(cellId);
    }
}
