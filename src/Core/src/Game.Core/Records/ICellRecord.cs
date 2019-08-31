using System.Collections;
using UnityEngine;

namespace Game.Core.Records
{
    public interface ICellRecord : IRecord
    {
        bool IsInterior { get; }
        Color? AmbientLight { get; }
    }

    public class InRangeCellInfo
    {
        public GameObject GameObject;
        public GameObject ObjectsContainerGameObject;
        public ICellRecord CellRecord;
        public IEnumerator ObjectsCreationCoroutine;

        public InRangeCellInfo(GameObject gameObject, GameObject objectsContainerGameObject, ICellRecord cellRecord, IEnumerator objectsCreationCoroutine)
        {
            GameObject = gameObject;
            ObjectsContainerGameObject = objectsContainerGameObject;
            CellRecord = cellRecord;
            ObjectsCreationCoroutine = objectsCreationCoroutine;
        }
    }

    public class RefCellObjInfo
    {
        public object RefObj; //: CELLRecord.RefObjDataGroup
        public IRecord ReferencedRecord;
        public string ModelFilePath;
    }
}