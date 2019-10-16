using Game.Core;
using Game.Estate.UltimaIX.FilePack;
using System;

namespace Game.Estate.UltimaIX.Records
{
    public class STATRecord : Record
    {
        public class Page
        {
            public uint BaseX; // The base X coordinate to add to the X coordinate of all objects in the page. This is divisible by 4096.
            public uint BaseY; // The base Y coordinate to add to the Y coordinate of all objects in the page. This is divisible by 4096.
            public FixedObject[] FixedObjects; // The list of objects in the page.
        }

        [Flags]
        public enum FixedFlag : ushort
        {
            Required = 0x1, // Crashes the game if this is not set.
            CollideWithPlayer = 0x2, // Collide with the player, and maybe other NPCs.
            Semitransparent = 0x1000, // Render the object as semi-transparent.
        }

        public struct FixedObject
        {
            public uint Reference; // A byte offset from the start of the file to another object, or 0. Some objects have invalid references, and some are circular.
            public ushort Position1; public ushort Position2; public ushort Position3; // The location of the object within the tile, between 0 and 4095. This means that for each terrain quad there are 128 discrete positions.
            public ushort Type; // The type index.
            public short Angle1; public short Angle2; public short Angle3; // Angle of the fixed stored in 0.16 fixed point as a conjugated normalized quaternion. This means that to convert to a regular quaternion, if we consider the input values x, y, z, and w, then you compute the angle as Quaternion(-x / 32767.0, -y / 32767.0, -z / 32767.0, w / 32767.0
            public FixedFlag Flags; // Flags for the object.
            public uint Unknown;
        }

        public uint Width;
        public uint Height;
        public uint[] Indices;
        public Page[] Pages;

        struct Header
        {
            public uint Unknown1;
            public uint Unknown2;
            public uint PageSize; // The size in bytes of all the pages.
            public uint Unknown3;
            public uint Width; // The number of tiles the region is wide. This is the same as the terrain height map's width divided by two.
            public uint Height; // The number of tiles the region is tall. This is the same as the terrain height map's height divided by two.
            public uint Unknown4;
            public uint Unknown5;
        }

        public static void ReadFixed(BinaryFileReader r, RecordGroup group)
        {
            var header = r.ReadT<Header>(32);
            var indexSize = (int)(header.Width * header.Height);
            // These are either 0 or a number in the form nnnn001h, where nnnn is a number that may be a page index. This may be in [x + y * width] order, but that doesn't seem to corroborate with where objects are located in the maps
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