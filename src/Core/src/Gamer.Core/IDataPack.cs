using Gamer.Core.Records;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Gamer.Core
{
    public interface IDataPack : IDisposable
    {
        void SinkDataContains(string path = null);
        Task<byte[]> LoadDataLabelAsync(string label);
        ICellRecord FindCellRecord(Vector3Int cellId);
    }
}