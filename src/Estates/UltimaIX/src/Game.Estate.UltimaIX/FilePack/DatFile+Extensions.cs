using Game.Estate.UltimaIX.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Estate.UltimaIX.FilePack
{
    partial class DatFile
    {
        Dictionary<int, Func<RecordGroup>> _WRLDById;

        void Process()
        {
            _WRLDById = Headers.ToDictionary(x => int.Parse(x.Label), x => (Func<RecordGroup>)(() =>
            {
                if (x.State == 0) { x.State = 1; Load(); }
                return GroupByLabel[x.Label];
            }));
        }

        public LTEXRecord FindLTEXRecord(int index)
        {
            return null;
        }

        public LANDRecord FindLANDRecord(Vector3Int cellId)
        {
            var wrld = _WRLDById[cellId.z]();
            var cellBlock = wrld.EnsureCell(cellId);
            return cellBlock.LANDsById.TryGetValue(cellId, out var land) ? land : null;
        }

        public CELLRecord FindCELLRecord(Vector3Int cellId)
        {
            var wrld = _WRLDById[cellId.z]();
            var cellBlock = wrld.EnsureCell(cellId);
            return cellBlock.CELLsById.TryGetValue(cellId, out var cell) ? cell : null;
        }
    }

    partial class RecordGroup
    {
        internal Dictionary<Vector3Int, CELLRecord> CELLsById;
        internal Dictionary<Vector3Int, LANDRecord> LANDsById;

        public RecordGroup EnsureCell(Vector3Int cellId) => this;
    }
}
