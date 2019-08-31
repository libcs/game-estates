﻿using Game.Core;
using Game.Core.Records;
using Game.Estate.Tes.FilePack;
using System;
using static Game.Core.Debug;

namespace Game.Estate.Tes.Records
{
    public enum GameFormat { TES3 = 3, TES4, TES5 }

    public abstract class Record : IRecord
    {
        internal Header Header;
        public uint Id => Header.FormId;

        /// <summary>
        /// Return an uninitialized subrecord to deserialize, or null to skip.
        /// </summary>
        /// <returns>Return an uninitialized subrecord to deserialize, or null to skip.</returns>
        public abstract bool CreateField(BinaryFileReader r, GameFormat format, string type, int dataSize);

        public void Read(BinaryFileReader r, string filePath, GameFormat format)
        {
            var startPosition = r.Position;
            var endPosition = startPosition + Header.DataSize;
            while (r.Position < endPosition)
            {
                var fieldHeader = new FieldHeader(r, format);
                if (fieldHeader.Type == "XXXX")
                {
                    if (fieldHeader.DataSize != 4)
                        throw new InvalidOperationException();
                    fieldHeader.DataSize = (int)r.ReadUInt32();
                    continue;
                }
                else if (fieldHeader.Type == "OFST" && Header.Type == "WRLD")
                {
                    r.Position = endPosition;
                    continue;
                }
                var position = r.Position;
                if (!CreateField(r, format, fieldHeader.Type, fieldHeader.DataSize))
                {
                    Log($"Unsupported ESM record type: {Header.Type}:{fieldHeader.Type}");
                    r.Position += fieldHeader.DataSize;
                    continue;
                }
                // check full read
                if (r.Position != position + fieldHeader.DataSize)
                    throw new FormatException($"Failed reading {Header.Type}:{fieldHeader.Type} field data at offset {position} in {filePath} of {r.Position - position - fieldHeader.DataSize}");
            }
            // check full read
            if (r.Position != endPosition)
                throw new FormatException($"Failed reading {Header.Type} record data at offset {startPosition} in {filePath}");
        }
    }
}
