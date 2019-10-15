using Game.Core;
using Game.Estate.UltimaIX.FilePack;
using System;
using System.Linq;

namespace Game.Estate.UltimaIX.Records
{
    public class LANDRecord : Record
    {
        [Flags]
        // 00001111 11111111 = 0x0FFF>0
        // 00010000 00000000 = 0x1000
        // 00100000 00000000 = 0x2000
        // 01000000 00000000 = 0x4000
        // 10000000 00000000 = 0x8000
        // 00000000 00011111:16 = 0x001F0000>16
        // 11111111 11000000:16 = 0xFFC00000>21
        public enum Point : uint
        {
            Height_0Mask = 0xFFF,
            Hole = 0x1000,
            Swap = 0x2000,
            Mirror = 0x4000,
            Unknown1 = 0x8000,
            Frame_16Mask = 0x1F,
            Texture_22Mask = 0x3FF,
        }

        public uint Width;
        public uint Height;
        public string Name;
        public uint WaterLevel;
        public uint WaveAmplitude;
        public uint Flags;
        public ushort[] Indices;
        public Point[][] Chunks;

        public static void ReadTerrain(BinaryFileReader r, RecordGroup group)
        {
            var record = new LANDRecord
            {
                Width = r.ReadUInt32(),
                Height = r.ReadUInt32(),
                Name = r.ReadASCIIString(0x80, ASCIIFormat.ZeroTerminated),
                WaterLevel = r.ReadUInt32(),
                WaveAmplitude = r.ReadUInt32(),
                Flags = r.ReadUInt32(),
            };
            var chunkCount = (int)r.ReadUInt32();

            var indexSize = (int)(record.Width * record.Height);
            record.Indices = r.ReadTArray<ushort>(indexSize * 2, indexSize);

            var chunks = record.Chunks = new Point[chunkCount][];
            for (var i = 0; i < chunks.Length; i++)
                chunks[i] = r.ReadTArray<uint>(16 * 16 * 4, 16 * 16).Select(x => (Point)x).ToArray();
            group.Records.Add(record);
        }
    }
}