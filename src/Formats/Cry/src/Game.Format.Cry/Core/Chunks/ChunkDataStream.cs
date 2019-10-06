﻿using static Game.Core.CoreDebug;

namespace Game.Format.Cry.Core
{
    public abstract class ChunkDataStream : Chunk // cccc0016:  Contains data such as vertices, normals, etc.
    {
        public uint Flags; // not used, but looks like the start of the Data Stream chunk
        public uint Flags1; // not used.  UInt32 after Flags that looks like offsets
        public uint Flags2; // not used, looks almost like a filler.
        public DataStreamTypeEnum DataStreamType { get; set; } // type of data (vertices, normals, uv, etc)
        public uint NumElements; // Number of data entries
        public uint BytesPerElement; // Bytes per data entry
        public uint Reserved1;
        public uint Reserved2;
        // Need to be careful with using float for Vertices and normals.  technically it's a floating point of length BytesPerElement.  May need to fix this.
        public Vector3[] Vertices;  // For dataStreamType of 0, length is NumElements. 
        public Vector3[] Normals;   // For dataStreamType of 1, length is NumElements.

        public UV[] UVs;            // for datastreamType of 2, length is NumElements.
        public IRGB[] RGBColors;    // for dataStreamType of 3, length is NumElements.  Bytes per element of 3
        public IRGBA[] RGBAColors;  // for dataStreamType of 4, length is NumElements.  Bytes per element of 4
        public uint[] Indices;    // for dataStreamType of 5, length is NumElements.
        // For Tangents on down, this may be a 2 element array.  See line 846+ in cgf.xml
        public Tangent[,] Tangents;  // for dataStreamType of 6, length is NumElements, 2.  
        public byte[,] ShCoeffs;     // for dataStreamType of 7, length is NumElement,BytesPerElements.
        public byte[,] ShapeDeformation; // for dataStreamType of 8, length is NumElements,BytesPerElement.
        public byte[,] BoneMap;      // for dataStreamType of 9, length is NumElements,BytesPerElement, 2.
        //public MeshBoneMapping[] BoneMap;      // for dataStreamType of 9, length is NumElements,BytesPerElement.
        public byte[,] FaceMap;      // for dataStreamType of 10, length is NumElements,BytesPerElement.
        public byte[,] VertMats;     // for dataStreamType of 11, length is NumElements,BytesPerElement.

        public override void WriteChunk()
        {
            Log($"*** START DATASTREAM ***");
            Log($"    ChunkType:                       {ChunkType}");
            Log($"    Version:                         {Version:X}");
            Log($"    DataStream chunk starting point: {Flags:X}");
            Log($"    Chunk ID:                        {ID:X}");
            Log($"    DataStreamType:                  {DataStreamType}");
            Log($"    Number of Elements:              {NumElements}");
            Log($"    Bytes per Element:               {BytesPerElement}");
            Log($"*** END DATASTREAM ***");
        }
    }
}
