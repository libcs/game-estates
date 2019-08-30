using Gamer.Core;
using Gamer.Core.Records;
using Gamer.Estate.UltimaIX.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Gamer.Estate.UltimaIX.FilePack
{
    partial class DatFile
    {
        Dictionary<uint, (WRLDRecord wrld, RecordGroup group)> _WRLDById;

        void Process()
        {
        }

        public LTEXRecord FindLTEXRecord(int index)
        {
            //_LTEXsById.TryGetValue(index, out var ltex);
            //return ltex;
            return null;
        }

        public LANDRecord FindLANDRecord(Vector3Int cellId)
        {
            var world = _WRLDById[(uint)cellId.z];
            for (var wrld = world.group; wrld.Next != null; wrld = wrld.Next)
                for (var cellBlock = wrld.EnsureWrldAndCell(cellId); cellBlock.Next != null; cellBlock = cellBlock.Next)
                    if (cellBlock.LANDsById.TryGetValue(cellId, out var land))
                        return land;
            return null;
        }

        public CELLRecord FindCELLRecord(Vector3Int cellId)
        {
            var world = _WRLDById[(uint)cellId.z];
            for (var wrld = world.group; wrld.Next != null; wrld = wrld.Next)
                for (var cellBlock = wrld.EnsureWrldAndCell(cellId); cellBlock.Next != null; cellBlock = cellBlock.Next)
                    if (cellBlock.CELLsById.TryGetValue(cellId, out var cell))
                        return cell;
            return null;
        }
    }

    partial class RecordGroup
    {
        internal HashSet<byte[]> _ensureCELLsByLabel;
        internal Dictionary<Vector3Int, CELLRecord> CELLsById;
        internal Dictionary<Vector3Int, LANDRecord> LANDsById;

        public RecordGroup EnsureWrldAndCell(Vector3Int cellId)
        {
            var cellBlockX = (short)(cellId.x >> 5);
            var cellBlockY = (short)(cellId.y >> 5);
            var cellBlockId = new byte[4];
            Buffer.BlockCopy(BitConverter.GetBytes(cellBlockY), 0, cellBlockId, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(cellBlockX), 0, cellBlockId, 2, 2);
            return null;
            //Load();
            //return GroupByLabel.TryGetValue(ToLabel(false, cellBlockId), out var cellBlock) ? cellBlock.EnsureCell(cellId) : null;
        }
    }
}
