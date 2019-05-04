using Gamer.Base.Core;
using Gamer.Base.Records;
using Gamer.Estate.Tes.FilePack;
using System;
using static System.Diagnostics.Debug;

namespace Gamer.Estate.Tes.Records
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
            var startPosition = r.BaseStream.Position;
            var endPosition = startPosition + Header.DataSize;
            while (r.BaseStream.Position < endPosition)
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
                    r.BaseStream.Position = endPosition;
                    continue;
                }
                var position = r.BaseStream.Position;
                if (!CreateField(r, format, fieldHeader.Type, fieldHeader.DataSize))
                {
                    Print($"Unsupported ESM record type: {Header.Type}:{fieldHeader.Type}");
                    r.BaseStream.Position += fieldHeader.DataSize;
                    continue;
                }
                // check full read
                if (r.BaseStream.Position != position + fieldHeader.DataSize)
                    throw new FormatException($"Failed reading {Header.Type}:{fieldHeader.Type} field data at offset {position} in {filePath} of {r.BaseStream.Position - position - fieldHeader.DataSize}");
            }
            // check full read
            if (r.BaseStream.Position != endPosition)
                throw new FormatException($"Failed reading {Header.Type} record data at offset {startPosition} in {filePath}");
        }
    }
}
