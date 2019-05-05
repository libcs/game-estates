using Gamer.Estate.Ultima.FilePack;
using UnityEngine;

namespace Gamer.Estate.Ultima
{
    public static class ConvertUtils
    {
        public const float MeterInUnits = 10;
        public const float ExteriorCellSideLengthInMeters = 8F * DataFile.CELL_PACK;

        public static Quaternion RotationMatrixToQuaternion(Matrix4x4 matrix) => Quaternion.LookRotation(matrix.GetColumn(2), matrix.GetColumn(1));
    }
}