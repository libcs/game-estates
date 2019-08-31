using System;
using UnityEngine;

namespace Game.Format.Cry
{
    public static class ConvertUtils
    {
        const int yardInUnits = 64;
        const float meterInYards = 1.09361f;
        public const float MeterInUnits = meterInYards * yardInUnits;

        const int exteriorCellSideLengthInUnits = 128 * yardInUnits;
        public const float ExteriorCellSideLengthInMeters = exteriorCellSideLengthInUnits / MeterInUnits;

        public static Quaternion RotationMatrixToQuaternion(Matrix4x4 matrix) => throw new NotImplementedException(); //Quaternion.LookRotation(matrix.GetColumn(2), matrix.GetColumn(1));
    }
}