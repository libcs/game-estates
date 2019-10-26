using Game.Core;
using Game.Estate.UltimaIX.FilePack;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Estate.UltimaIX.Records
{
    public class LANDRecord : Record
    {
        //[Flags]
        //enum Point : uint
        //{
        //    Height_0Mask = 0xFFF,           // 00001111 11111111 = 0x0FFF>0
        //    Hole = 0x1000,                  // 00010000 00000000 = 0x1000
        //    Swap = 0x2000,                  // 00100000 00000000 = 0x2000
        //    Mirror = 0x4000,                // 01000000 00000000 = 0x4000
        //    Unknown1 = 0x8000,              // 10000000 00000000 = 0x8000
        //    Frame_16Mask = 0x1F,            // 00000000 00011111:16 = 0x001F0000>16
        //    Texture_22Mask = 0x3FF,         // 11111111 11000000:16 = 0xFFC00000>21
        //}

        class Chunk
        {
            public ushort[] VHGT;
            public ushort[] VTEX;
            public byte[] VTEXF;
        }

        class Data
        {
            public uint Width;
            public uint Height;
            public string Name;
            public uint WaterLevel;
            public uint WaveAmplitude;
            public uint Flags;
            public ushort[] Indices;
            public Chunk[] Chunks;
        }

        public ushort[] VHGT;
        public byte[] VTEXF;
        public ushort[] VTEX;
        public Vector3Int GridId;

        public static void ReadTerrain(BinaryFileReader r, Header header, RecordGroup group)
        {
            var world = int.Parse(header.Label);
            var data = new Data
            {
                Width = r.ReadUInt32(),
                Height = r.ReadUInt32(),
                Name = r.ReadASCIIString(0x80, ASCIIFormat.ZeroTerminated),
                WaterLevel = r.ReadUInt32(),
                WaveAmplitude = r.ReadUInt32(),
                Flags = r.ReadUInt32(),
            };
            var chunkCount = (int)r.ReadUInt32();
            var indexSize = (int)(data.Width * data.Height);
            var indices = data.Indices = r.ReadTArray<ushort>(indexSize * 2, indexSize);
            var chunks = data.Chunks = new Chunk[chunkCount];
            const int POINT_STRIDE = 16;
            for (var i = 0; i < chunks.Length; i++)
            {
                var points = r.ReadTArray<uint>(POINT_STRIDE * POINT_STRIDE * 4, POINT_STRIDE * POINT_STRIDE); //.Select(x => (Point)x).ToArray();
                chunks[i] = new Chunk
                {
                    VHGT = points.Select(x => (ushort)(x & 0xFFF)).ToArray(),
                    VTEXF = points.Select(x => (byte)((x >> 16) & 0x1F)).ToArray(),
                    VTEX = points.Select(x => (ushort)((x >> 22) & 0x3FF)).ToArray(), //: sky, >> 21?
                };
            }
            group.Tag = data;

            // transform
            const int LAND_STRIDE = 64;
            var records = new List<Record>();
            for (var y = 0; y < data.Height; y += 4)
                for (var x = 0; x < data.Width; x += 4)
                {
                    var vhgt = new ushort[LAND_STRIDE * LAND_STRIDE];
                    var vtex = new ushort[LAND_STRIDE * LAND_STRIDE];
                    var vtexf = new byte[LAND_STRIDE * LAND_STRIDE];
                    for (var sy = 0; sy < 4; sy++)
                        for (var sx = 0; sx < 4; sx++)
                        {
                            var index = indices[x + sx + ((y + sy) * data.Width)];
                            if (index >= chunks.Length)
                                continue;
                            var chunk = chunks[index];
                            var offset = (sx * POINT_STRIDE) + (sy * POINT_STRIDE * LAND_STRIDE);
                            for (var i = 0; i < POINT_STRIDE; i++)
                            {
                                Buffer.BlockCopy(chunk.VHGT, i * POINT_STRIDE * 2, vhgt, offset + (i * LAND_STRIDE * 2), POINT_STRIDE * 2);
                                Buffer.BlockCopy(chunk.VTEX, i * POINT_STRIDE * 2, vtex, offset + (i * LAND_STRIDE * 2), POINT_STRIDE * 2);
                                Buffer.BlockCopy(chunk.VTEXF, i * POINT_STRIDE, vtexf, offset + (i * LAND_STRIDE), POINT_STRIDE);
                            }
                        }
                    records.Add(new LANDRecord
                    {
                        VHGT = vhgt,
                        VTEX = vtex,
                        VTEXF = vtexf,
                        GridId = new Vector3Int(x / 4, y / 4, world),
                    });
                }

            // insert
            group.LANDsById = records.ToDictionary(x => ((LANDRecord)x).GridId, x => (LANDRecord)x);
            group.Records.AddRange(records);
        }
    }
}