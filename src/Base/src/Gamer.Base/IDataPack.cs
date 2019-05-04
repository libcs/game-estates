using Gamer.Base.Records;
using System;
using UnityEngine;

namespace Gamer.Base
{
    public interface IDataPack : IDisposable
    {
        ICellRecord FindCellRecord(Vector3Int cellId);
    }
}