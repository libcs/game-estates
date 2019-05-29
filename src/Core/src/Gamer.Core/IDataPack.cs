using Gamer.Core.Records;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Gamer.Core
{
    public interface IDataPack : IDisposable
    {
        void SinkDataContains();
        Task<byte[]> LoadDataLabelAsync(byte[] label);
        ICellRecord FindCellRecord(Vector3Int cellId);
    }
}