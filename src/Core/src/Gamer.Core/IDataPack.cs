using Gamer.Core.Records;
using System;
using UnityEngine;

namespace Gamer.Core
{
    public interface IDataPack : IDisposable
    {
        ICellRecord FindCellRecord(Vector3Int cellId);
        void SplitToFiles(string splitPath);
    }
}