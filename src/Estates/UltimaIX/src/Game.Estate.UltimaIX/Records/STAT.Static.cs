using Game.Core;
using Game.Estate.UltimaIX.FilePack;
using System;

namespace Game.Estate.UltimaIX.Records
{
    public class STATRecord : Record
    {
        public struct Header
        {
            public uint Unknown1;
            public uint Unknown2;
            public uint PageSize;
            public uint Unknown3;
            public uint Width;
            public uint Height;
            public uint Unknown4;
            public uint Unknown5;
        }

        public class Page
        {
            public uint BaseX;
            public uint BaseY;
            public FixedObject[] FixedObjects;
        }

        [Flags]
        public enum FixedFlag : ushort
        {
            Required = 0x1,
            CollideWithPlayer = 0x2,
            Semitransparent = 0x1000,
        }

        public struct FixedObject
        {
            public uint Reference;
            public ushort Position1;
            public ushort Position2;
            public ushort Position3;
            public ushort Type;
            public short Angle1;
            public short Angle2;
            public short Angle3;
            public FixedFlag Flags;
            public uint Unknown;
        }

        public uint Width;
        public uint Height;
        public uint[] Indices;
        public Page[] Pages;

        public static void ReadFixed(BinaryFileReader r, RecordGroup group)
        {
            var header = r.ReadT<Header>(32);
            var indexSize = (int)(header.Width * header.Height);
            var indices = r.ReadTArray<uint>(indexSize * 4, indexSize);
            var pages = new Page[header.PageSize / 4096];
            for (var i = 0; i < pages.Length; i++)
            {
                var page = pages[i] = new Page();
                r.Skip(4 * 0x3);
                page.BaseX = r.ReadUInt32();
                page.BaseY = r.ReadUInt32();
                r.Skip(4 * 0x13);
                page.FixedObjects = r.ReadTArray<FixedObject>(0x18 * 166, 166);
                r.Skip(0x10);
            }
            group.Records.Add(new STATRecord
            {
                Width = header.Width,
                Height = header.Height,
                Indices = indices,
                Pages = pages,
            });
        }
    }
}