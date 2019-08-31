using Game.Core.Records;
using UnityEngine;

namespace Game.Core
{
    public interface ICellManager
    {
        Vector3Int GetCellId(Vector3 point, int world);
        InRangeCellInfo StartCreatingCell(Vector3Int cellId);
        InRangeCellInfo StartCreatingCellByName(int world, int cellId, string cellName);
        void UpdateCells(Vector3 currentPosition, int world, bool immediate = false, int cellRadiusOverride = -1);
        void DestroyAllCells();
    }
}