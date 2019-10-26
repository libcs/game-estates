using Game.Core;
using Game.Estate.UltimaIX.FilePack;
using System;

namespace Game.Estate.UltimaIX.Records
{
    public class DYNARecord : Record
    {
        public struct Page
        {
            public uint NextPage; // Offset of the next page in this chunk, relative to the end of the header, or 0 for none
            public uint EndEntityOffset;
            public uint EndTriggerOffset;
            public uint BaseX; // Base X coordinate of the chunk.
            public uint BaseY; // Base Y coordinate of the chunk.
            public uint EntityCount; // Number of entities in the chunk.
            public uint TriggerCount; // Number of triggers in the chunk.
        }

        public struct Entity
        {
            public ushort Id;
            public ushort Unknown1;
            public ushort OffsetX; // X offset of the entity relative to the chunk's baseX value.
            public ushort OffsetY; // Y offset of the entity relative to the chunk's baseY value.
            public ushort Z; // Z position of the entity; the elevation.
            public ushort TypeIndex; // Type index.
            public short Rotation1; public short Rotation2; public short Rotation3; public short Rotation4; // Rotation of the entity expressed as an 0.16 quaternion(divide integer values by 32767).
            public uint Flags; // Entity flags.
            public ushort MeshIndex; // The mesh index to render for this entity.
            public ushort TriggerId;
            public uint TriggerDataOffset; // Offset of the trigger data, relative to the end of the file header.
        }

        public uint Width;
        public uint Height;
        public Page[] Pages;

        public static void ReadNonfixed(BinaryFileReader r, Header header, RecordGroup group)
        {
            var world = int.Parse(header.Label);
            // header
            r.Skip(4 * 5);
            var width = r.ReadUInt32();
            var height = r.ReadUInt32();
            r.Skip(4);
            var indexSize = (int)(width * height);
            var indices = r.ReadTArray<uint>(indexSize * 4, indexSize);
            r.Skip(4);
            // pages
            var page = r.ReadT<Page>(28); r.Skip(4 * 7);
            var entities = new Entity[page.EntityCount];
            for (var i = 0; i < entities.Length; i++)
                entities[i] = r.ReadT<Entity>(64);
        }
    }
}