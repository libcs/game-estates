using Game.Core;
using Game.Core.Records;
using Game.Estate.UltimaIX.FilePack;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Estate.UltimaIX.Records
{
    public class CELLRecord : Record, ICellRecord
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

        public class Data
        {
            public uint Width;
            public uint Height;
            public uint[] Indices;
            public Page[] Pages;
        }

        struct Head
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

        public Vector3Int GridId; // => new Vector3Int(XCLC.Value.GridX, XCLC.Value.GridY, !IsInterior ? 0 : -1);

        public bool IsInterior => false;
        public Color? AmbientLight => null;

        public static void ReadFixed(BinaryFileReader r, Header header, RecordGroup group)
        {
            var world = int.Parse(header.Label);
            var head = r.ReadT<Head>(32);
            var indexSize = (int)(head.Width * head.Height);
            // These are either 0 or a number in the form nnnn001h, where nnnn is a number that may be a page index. This may be in [x + y * width] order, but that doesn't seem to corroborate with where objects are located in the maps
            var indices = r.ReadTArray<uint>(indexSize * 4, indexSize);
            const int POINT_STRIDE = 32;
            var pages = new Page[head.PageSize / 4096];
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
            var data = new Data
            {
                Width = head.Width,
                Height = head.Height,
                Indices = indices,
                Pages = pages,
            };
            group.Tag = data;

            // transform
            const int CELL_STRIDE = 64;
            var records = new List<Record>();
            for (var y = 0; y < data.Height; y += 2)
                for (var x = 0; x < data.Width; x += 2)
                {
                    //var vhgt = new ushort[CELL_STRIDE * CELL_STRIDE];
                    //var vtex = new ushort[CELL_STRIDE * CELL_STRIDE];
                    //var vtexf = new byte[CELL_STRIDE * CELL_STRIDE];
                    //for (var sy = 0; sy < 2; sy++)
                    //    for (var sx = 0; sx < 2; sx++)
                    //    {
                    //        var index = indices[x + sx + ((y + sy) * data.Width)];
                    //        if (index >= chunks.Length)
                    //            continue;
                    //        var chunk = chunks[index];
                    //        var offset = (sx * POINT_STRIDE) + (sy * POINT_STRIDE * CELL_STRIDE);
                    //        for (var i = 0; i < POINT_STRIDE; i++)
                    //        {
                    //            Buffer.BlockCopy(chunk.VHGT, i * POINT_STRIDE * 2, vhgt, offset + (i * CELL_STRIDE * 2), POINT_STRIDE * 2);
                    //            Buffer.BlockCopy(chunk.VTEX, i * POINT_STRIDE * 2, vtex, offset + (i * CELL_STRIDE * 2), POINT_STRIDE * 2);
                    //            Buffer.BlockCopy(chunk.VTEXF, i * POINT_STRIDE, vtexf, offset + (i * CELL_STRIDE), POINT_STRIDE);
                    //        }
                    //    }
                    records.Add(new CELLRecord
                    {
                        GridId = new Vector3Int(x / 2, y / 2, world),
                    });
                }

            // insert
            group.CELLsById = records.ToDictionary(x => ((CELLRecord)x).GridId, x => (CELLRecord)x);
            group.Records.AddRange(records);
        }
    }
}