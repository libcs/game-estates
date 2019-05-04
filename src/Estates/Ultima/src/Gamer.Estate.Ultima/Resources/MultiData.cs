using Gamer.Base.Core;
using Gamer.Estate.Ultima.Resources.IO;
using System.Linq;
using UnityEngine;

namespace Gamer.Estate.Ultima.Resources
{
    public class MultiData
    {
        public static MultiComponentList[] Cache { get; } = new MultiComponentList[0x4000];
        public static AFileIndex FileIndex { get; } = FileManager.CreateFileIndex("Multi.idx", "Multi.mul", 0x4000, 14);

        public static MultiComponentList GetComponents(int index)
        {
            MultiComponentList mcl;
            index &= FileManager.ItemIDMask;
            if (index >= 0 && index < Cache.Length)
            {
                mcl = Cache[index];
                if (mcl == null)
                    Cache[index] = mcl = Load(index);
            }
            else mcl = MultiComponentList.Empty;
            return mcl;
        }

        public static MultiComponentList Load(int index)
        {
            try
            {
                var r = FileIndex.Seek(index, out int length, out int extra, out bool patched);
                return r == null ? MultiComponentList.Empty : new MultiComponentList(r, length / 12);
            }
            catch { return MultiComponentList.Empty; }
        }
    }

    public class MultiComponentList
    {
        Vector2Int _min, _max;
        public static readonly MultiComponentList Empty = new MultiComponentList();

        public Vector2Int Min { get { return _min; } }
        public Vector2Int Max { get { return _max; } }
        public Vector2Int Center { get; }
        public int Width { get; }
        public int Height { get; }

        public MultiItem[] Items { get; private set; }

        public struct MultiItem
        {
            public short ItemId;
            public short OffsetX, OffsetY, OffsetZ;
            public int Flags;

            public override string ToString() => $"{ItemId:X4} {OffsetX} {OffsetY} {OffsetZ} {Flags:X4}";
        }

        MultiComponentList() => Items = new MultiItem[0];
        public MultiComponentList(BinaryFileReader r, int count)
        {
            var metrics_dataread_start = (int)r.Position;
            _min = _max = Vector2Int.zero;
            Items = new MultiItem[count];
            for (var i = 0; i < count; ++i)
            {
                Items[i].ItemId = r.ReadInt16();
                Items[i].OffsetX = r.ReadInt16();
                Items[i].OffsetY = r.ReadInt16();
                Items[i].OffsetZ = r.ReadInt16();
                Items[i].Flags = r.ReadInt32();
                if (Items[i].OffsetX < _min.x)
                    _min.x = Items[i].OffsetX;
                if (Items[i].OffsetY < _min.y)
                    _min.y = Items[i].OffsetY;
                if (Items[i].OffsetX > _max.x)
                    _max.x = Items[i].OffsetX;
                if (Items[i].OffsetY > _max.y)
                    _max.y = Items[i].OffsetY;
            }
            Center = new Vector2Int(-_min.x, -_min.y);
            Width = (_max.x - _min.x) + 1;
            Height = (_max.y - _min.y) + 1;
            // SortMultiComponentList();
            Metrics.ReportDataRead((int)r.Position - metrics_dataread_start);
        }

        void SortMultiComponentList() => Items = Items.OrderBy(a => a.OffsetY).ThenBy(a => a.OffsetX).ToArray();
    }
}