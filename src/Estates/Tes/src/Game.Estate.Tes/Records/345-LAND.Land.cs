﻿using Game.Core;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Game.Estate.Tes.Records
{
    public class LANDRecord : Record
    {
        // TESX
        public struct VNMLField
        {
            public Byte3[] Vertexs; // XYZ 8 bit floats

            public VNMLField(BinaryFileReader r, int dataSize)
            {
                Vertexs = r.ReadTArray<Byte3>(dataSize, dataSize / 3);
                //Vertexs = new Vector3Int8[dataSize / 3];
                //for (var i = 0; i < Vertexs.Length; i++) Vertexs[i] = new Vector3Int8 { X = r.ReadByte(), Y = r.ReadByte(), Z = r.ReadByte() };
            }
        }

        public struct VHGTField
        {
            public float ReferenceHeight; // A height offset for the entire cell. Decreasing this value will shift the entire cell land down.
            public sbyte[] HeightData; // HeightData

            public VHGTField(BinaryFileReader r, int dataSize)
            {
                ReferenceHeight = r.ReadSingle();
                var count = dataSize - 4 - 3;
                HeightData = r.ReadTArray<sbyte>(count, count);
                //HeightData = new sbyte[count];
                //for (var i = 0; i < HeightData.Length; i++) HeightData[i] = r.ReadSByte();
                r.Skip(3); // Unused
            }
        }

        public struct VCLRField
        {
            public ColorRef3[] Colors; // 24-bit RGB

            public VCLRField(BinaryFileReader r, int dataSize) => Colors = r.ReadTArray<ColorRef3>(dataSize, dataSize / 24);
        }

        public struct VTEXField
        {
            public ushort[] TextureIndicesT3;
            public uint[] TextureIndicesT4;

            public VTEXField(BinaryFileReader r, int dataSize, GameFormat format)
            {
                if (format == GameFormat.TES3)
                {
                    TextureIndicesT3 = r.ReadTArray<ushort>(dataSize, dataSize >> 1);
                    TextureIndicesT4 = null;
                    return;
                }
                TextureIndicesT3 = null;
                TextureIndicesT4 = r.ReadTArray<uint>(dataSize, dataSize >> 2);
            }
        }

        // TES3
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CORDField
        {
            public int CellX;
            public int CellY;
            public override string ToString() => $"{CellX},{CellY}";
        }

        public struct WNAMField
        {
            // Low-LOD heightmap (signed chars)
            public WNAMField(BinaryFileReader r, int dataSize)
            {
                r.Skip(dataSize);
                //var heightCount = dataSize;
                //for (var i = 0; i < heightCount; i++) { var height = r.ReadByte(); }
            }
        }

        // TES4
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BTXTField
        {
            public uint Texture;
            public byte Quadrant;
            public byte Pad01;
            public short Layer;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct VTXTField
        {
            public ushort Position;
            public ushort Pad01;
            public float Opacity;
        }

        public class ATXTGroup
        {
            public BTXTField ATXT;
            public VTXTField[] VTXTs;
        }

        public override string ToString() => $"LAND: {INTV}";
        public IN32Field DATA; // Unknown (default of 0x09) Changing this value makes the land 'disappear' in the editor.
        // A RGB color map 65x65 pixels in size representing the land normal vectors.
        // The signed value of the 'color' represents the vector's component. Blue
        // is vertical(Z), Red the X direction and Green the Y direction.Note that
        // the y-direction of the data is from the bottom up.
        public VNMLField VNML;
        public VHGTField VHGT; // Height data
        public VNMLField? VCLR; // Vertex color array, looks like another RBG image 65x65 pixels in size. (Optional)
        public VTEXField? VTEX; // A 16x16 array of short texture indices. (Optional)
        // TES3
        public CORDField INTV; // The cell coordinates of the cell
        public WNAMField WNAM; // Unknown byte data.
        // TES4
        public BTXTField[] BTXTs = new BTXTField[4]; // Base Layer
        public ATXTGroup[] ATXTs; // Alpha Layer
        ATXTGroup _lastATXT;

        public Vector3Int GridId; // => new Vector3Int(INTV.CellX, INTV.CellY, 0);

        public override bool CreateField(BinaryFileReader r, GameFormat format, string type, int dataSize)
        {
            switch (type)
            {
                case "DATA": DATA = r.ReadT<IN32Field>(dataSize); return true;
                case "VNML": VNML = new VNMLField(r, dataSize); return true;
                case "VHGT": VHGT = new VHGTField(r, dataSize); return true;
                case "VCLR": VCLR = new VNMLField(r, dataSize); return true;
                case "VTEX": VTEX = new VTEXField(r, dataSize, format); return true;
                // TES3
                case "INTV": INTV = r.ReadT<CORDField>(dataSize); return true;
                case "WNAM": WNAM = new WNAMField(r, dataSize); return true;
                // TES4
                case "BTXT": var btxt = r.ReadT<BTXTField>(dataSize); BTXTs[btxt.Quadrant] = btxt; return true;
                case "ATXT":
                    if (ATXTs == null) ATXTs = new ATXTGroup[4];
                    var atxt = r.ReadT<BTXTField>(dataSize); _lastATXT = ATXTs[atxt.Quadrant] = new ATXTGroup { ATXT = atxt }; return true;
                case "VTXT": _lastATXT.VTXTs = r.ReadTArray<VTXTField>(dataSize, dataSize >> 3); return true;
                default: return false;
            }
        }
    }
}