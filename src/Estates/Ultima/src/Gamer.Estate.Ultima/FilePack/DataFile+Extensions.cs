using Gamer.Estate.Ultima.Records;
using UnityEngine;

namespace Gamer.Estate.Ultima.FilePack
{
    partial class DataFile
    {
        public LANDRecord FindLANDRecord(Vector3Int cellId) => _LANDsById.TryGetValue(cellId, out var land) ? land : land;
        public CELLRecord FindCellRecord(Vector3Int cellId) => _CELLsById.TryGetValue(cellId, out var cell) ? cell : cell;
        public CELLRecord FindCellRecordByName(int world, int cellId, string cellName) => null; // _CELLsByName.TryGetValue(cellName, out var cell) ? cell : cell;
    }
}