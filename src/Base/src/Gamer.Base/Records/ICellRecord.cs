using UnityEngine;

namespace Gamer.Base.Records
{
    public interface ICellRecord : IRecord
    {
        bool IsInterior { get; }
        Color? AmbientLight { get; }
    }
}