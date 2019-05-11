using System;
using System.Collections.Generic;
using System.IO;
using static Gamer.Core.Debug;

namespace Gamer.Format.Cry.Core
{
    public class ChunkDataStream_800 : ChunkDataStream
    {
        // This includes changes for 2.6 created by Dymek (byte4/1/2hex, and 20 byte per element vertices).  Thank you!
        public static float Byte4HexToFloat(string hexString) => BitConverter.ToSingle(BitConverter.GetBytes(uint.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier)), 0);
        public static int Byte1HexToIntType2(string hexString) => Convert.ToSByte(hexString, 16);

        public static float Byte2HexIntFracToFloat2(string hexString)
        {
            var sintPart = hexString.Substring(0, 2);
            var sfracPart = hexString.Substring(2, 2);
            var intPart = Byte1HexToIntType2(sintPart);
            var intnum = short.Parse(sfracPart, System.Globalization.NumberStyles.AllowHexSpecifier);
            var intbytes = BitConverter.GetBytes(intnum);
            var intbinary = Convert.ToString(intbytes[0], 2).PadLeft(8, '0');
            var binaryIntPart = intbinary;

            var num = short.Parse(sfracPart, System.Globalization.NumberStyles.AllowHexSpecifier);
            var bytes = BitConverter.GetBytes(num);
            var binary = Convert.ToString(bytes[0], 2).PadLeft(8, '0');
            var binaryFracPart = binary;

            //convert Fractional Part
            var dec = 0F;
            for (var i = 0; i < binaryFracPart.Length; i++)
            {
                if (binaryFracPart[i] == '0') continue;
                dec += (float)Math.Pow(2, (i + 1) * (-1));
            }
            var r = 0F;
            r = intPart + dec;
            //if (intPart > 0) r = intPart + dec;
            //if (intPart < 0) r = intPart - dec;
            //if (intPart == 0) r = dec;
            return r;
        }

        public override void Read(BinaryReader r)
        {
            base.Read(r);
            Flags2 = r.ReadUInt32(); // another filler
            var tmpdataStreamType = r.ReadUInt32();
            DataStreamType = (DataStreamTypeEnum)Enum.ToObject(typeof(DataStreamTypeEnum), tmpdataStreamType);
            NumElements = r.ReadUInt32(); // number of elements in this chunk
            if (_model.FileVersion == FileVersionEnum.CryTek_3_5 || _model.FileVersion == FileVersionEnum.CryTek_3_4)
                BytesPerElement = r.ReadUInt32(); // bytes per element
            if (_model.FileVersion == FileVersionEnum.CryTek_3_6)
            {
                BytesPerElement = (uint)r.ReadInt16();        // Star Citizen 2.0 is using an int16 here now.
                r.ReadInt16();                                // unknown value.   Doesn't look like padding though.
            }
            SkipBytes(r, 8);

            // Now do loops to read for each of the different Data Stream Types.  If vertices, need to populate Vector3s for example.
            switch (DataStreamType)
            {
                case DataStreamTypeEnum.VERTICES:  // Ref is 0x00000000
                    Vertices = new Vector3[NumElements];
                    switch (BytesPerElement)
                    {
                        case 12:
                            for (var i = 0; i < NumElements; i++)
                            {
                                Vertices[i].x = r.ReadSingle();
                                Vertices[i].y = r.ReadSingle();
                                Vertices[i].z = r.ReadSingle();
                            }
                            break;
                        case 8:  // Prey files, and old Star Citizen files
                            for (var i = 0; i < NumElements; i++)
                            {
                                // 2 byte floats.  Use the Half structure from TK.Math
                                //Vertices[i].x = Byte4HexToFloat(r.ReadUInt16().ToString("X8"));
                                //Vertices[i].y = Byte4HexToFloat(r.ReadUInt16().ToString("X8")); r.ReadUInt16();
                                //Vertices[i].z = Byte4HexToFloat(r.ReadUInt16().ToString("X8"));
                                //Vertices[i].w = Byte4HexToFloat(r.ReadUInt16().ToString("X8"));
                                Vertices[i].x = new Half { bits = r.ReadUInt16() }.ToSingle();
                                Vertices[i].y = new Half { bits = r.ReadUInt16() }.ToSingle();
                                Vertices[i].z = new Half { bits = r.ReadUInt16() }.ToSingle();
                                r.ReadUInt16();
                            }
                            break;
                        case 16:
                            //Console.WriteLine("method: (3)");
                            for (var i = 0; i < NumElements; i++)
                            {
                                Vertices[i].x = r.ReadSingle();
                                Vertices[i].y = r.ReadSingle();
                                Vertices[i].z = r.ReadSingle();
                                Vertices[i].w = r.ReadSingle(); // TODO:  Sometimes there's a W to these structures.  Will investigate.
                            }
                            break;
                    }
                    break;
                case DataStreamTypeEnum.INDICES:  // Ref is 
                    Indices = new uint[NumElements];
                    if (BytesPerElement == 2)
                        for (var i = 0; i < NumElements; i++)
                            Indices[i] = (uint)r.ReadUInt16(); //Console.WriteLine(R"Indices {i}: {Indices[i]}");
                    if (BytesPerElement == 4)
                        for (var i = 0; i < NumElements; i++)
                            Indices[i] = r.ReadUInt32();
                    //Log($"Offset is {r.BaseStream.Position:X}");
                    break;
                case DataStreamTypeEnum.NORMALS:
                    Normals = new Vector3[NumElements];
                    for (var i = 0; i < NumElements; i++)
                    {
                        Normals[i].x = r.ReadSingle();
                        Normals[i].y = r.ReadSingle();
                        Normals[i].z = r.ReadSingle();
                    }
                    //Log($"Offset is {r.BaseStream.Position:X}");
                    break;
                case DataStreamTypeEnum.UVS:
                    UVs = new UV[NumElements];
                    for (var i = 0; i < NumElements; i++)
                    {
                        UVs[i].U = r.ReadSingle();
                        UVs[i].V = r.ReadSingle();
                    }
                    //Log($"Offset is {r..BaseStream.Position:X}");
                    break;
                case DataStreamTypeEnum.TANGENTS:
                    Tangents = new Tangent[NumElements, 2];
                    Normals = new Vector3[NumElements];
                    for (var i = 0; i < NumElements; i++)
                        switch (BytesPerElement)
                        {
                            case 0x10:
                                // These have to be divided by 32767 to be used properly (value between 0 and 1)
                                Tangents[i, 0].x = r.ReadInt16();
                                Tangents[i, 0].y = r.ReadInt16();
                                Tangents[i, 0].z = r.ReadInt16();
                                Tangents[i, 0].w = r.ReadInt16();
                                //
                                Tangents[i, 1].x = r.ReadInt16();
                                Tangents[i, 1].y = r.ReadInt16();
                                Tangents[i, 1].z = r.ReadInt16();
                                Tangents[i, 1].w = r.ReadInt16();
                                break;
                            case 0x08:
                                // These have to be divided by 127 to be used properly (value between 0 and 1)
                                // Tangent
                                Tangents[i, 0].w = r.ReadSByte() / 127.0;
                                Tangents[i, 0].x = r.ReadSByte() / 127.0;
                                Tangents[i, 0].y = r.ReadSByte() / 127.0;
                                Tangents[i, 0].z = r.ReadSByte() / 127.0;
                                // Binormal
                                Tangents[i, 1].w = r.ReadSByte() / 127.0;
                                Tangents[i, 1].x = r.ReadSByte() / 127.0;
                                Tangents[i, 1].y = r.ReadSByte() / 127.0;
                                Tangents[i, 1].z = r.ReadSByte() / 127.0;
                                // Calculate the normal based on the cross product of the tangents.
                                //Normals[i].x = (Tangents[i,0].y * Tangents[i,1].z - Tangents[i,0].z * Tangents[i,1].y);
                                //Normals[i].y = 0 - (Tangents[i,0].x * Tangents[i,1].z - Tangents[i,0].z * Tangents[i,1].x); 
                                //Normals[i].z = (Tangents[i,0].x * Tangents[i,1].y - Tangents[i,0].y * Tangents[i,1].x);
                                //Console.WriteLine("Tangent: {0:F6} {1:F6} {2:F6}", Tangents[i,0].x, Tangents[i, 0].y, Tangents[i, 0].z);
                                //Console.WriteLine("Binormal: {0:F6} {1:F6} {2:F6}", Tangents[i, 1].x, Tangents[i, 1].y, Tangents[i, 1].z);
                                //Console.WriteLine("Normal: {0:F6} {1:F6} {2:F6}", Normals[i].x, Normals[i].y, Normals[i].z);
                                break;
                            default: throw new Exception("Need to add new Tangent Size");
                        }
                    //Log($"Offset is {r.BaseStream.Position:X}");
                    break;
                case DataStreamTypeEnum.COLORS:
                    switch (BytesPerElement)
                    {
                        case 3:
                            RGBColors = new IRGB[NumElements];
                            for (var i = 0; i < NumElements; i++)
                            {
                                RGBColors[i].r = r.ReadByte();
                                RGBColors[i].g = r.ReadByte();
                                RGBColors[i].b = r.ReadByte();
                            }
                            break;
                        case 4:
                            RGBAColors = new IRGBA[NumElements];
                            for (var i = 0; i < NumElements; i++)
                            {
                                RGBAColors[i].r = r.ReadByte();
                                RGBAColors[i].g = r.ReadByte();
                                RGBAColors[i].b = r.ReadByte();
                                RGBAColors[i].a = r.ReadByte();
                            }
                            break;
                        default:
                            Log("Unknown Color Depth");
                            for (var i = 0; i < NumElements; i++)
                                SkipBytes(r, BytesPerElement);
                            break;
                    }
                    break;
                case DataStreamTypeEnum.VERTSUVS:  // 3 half floats for verts, 3 half floats for normals, 2 half floats for UVs
                    Vertices = new Vector3[NumElements];
                    Normals = new Vector3[NumElements];
                    RGBColors = new IRGB[NumElements];
                    UVs = new UV[NumElements];
                    switch (BytesPerElement)  // new Star Citizen files
                    {
                        case 20:  // Dymek wrote this.  Used in 2.6 skin files.  3 floats for vertex position, 4 bytes for normals, 2 halfs for UVs.  Normals are calculated from Tangents
                            for (var i = 0; i < NumElements; i++)
                            {
                                Vertices[i].x = r.ReadSingle();
                                Vertices[i].y = r.ReadSingle();
                                Vertices[i].z = r.ReadSingle(); // For some reason, skins are an extra 1 meter in the z direction.
                                // Normals are stored in a signed byte, prob div by 127.
                                Normals[i].x = (float)r.ReadSByte() / 127;
                                Normals[i].y = (float)r.ReadSByte() / 127;
                                Normals[i].z = (float)r.ReadSByte() / 127;
                                r.ReadSByte(); // Should be FF.
                                UVs[i].U = new Half { bits = r.ReadUInt16() }.ToSingle();
                                UVs[i].V = new Half { bits = r.ReadUInt16() }.ToSingle();
                                //UVs[i].U = Byte4HexToFloat(r.ReadUInt16().ToString("X8"));
                                //UVs[i].V = Byte4HexToFloat(r.ReadUInt16().ToString("X8"));
                            }
                            break;
                        case 16:   // Dymek updated this.
                                   //Console.WriteLine("method: (5), 3 half floats for verts, 3 colors, 2 half floats for UVs");
                            for (var i = 0; i < NumElements; i++)
                            {
                                ushort bver = 0;
                                var ver = 0F;
                                Vertices[i].x = Byte2HexIntFracToFloat2(r.ReadUInt16().ToString("X4")) / 127;
                                Vertices[i].y = Byte2HexIntFracToFloat2(r.ReadUInt16().ToString("X4")) / 127;
                                Vertices[i].z = Byte2HexIntFracToFloat2(r.ReadUInt16().ToString("X4")) / 127;
                                Vertices[i].w = Byte2HexIntFracToFloat2(r.ReadUInt16().ToString("X4")) / 127; // Almost always 1
                                // Next structure is Colors, not normals.  For 16 byte elements, normals are calculated from Tangent data.
                                //RGBColors[i].r = r.ReadByte();
                                //RGBColors[i].g = r.ReadByte();
                                //RGBColors[i].b = r.ReadByte();
                                //r.ReadByte();           // additional byte.
                                //
                                //Normals[i].x = (r.ReadByte() - 128.0f) / 127.5f;
                                //Normals[i].y = (r.ReadByte() - 128.0f) / 127.5f;
                                //Normals[i].z = (r.ReadByte() - 128.0f) / 127.5f;
                                //r.ReadByte();           // additional byte.
                                // Read a Quat, convert it to vector3
                                var quat = new Vector4
                                {
                                    x = (r.ReadByte() - 128.0f) / 127.5f,
                                    y = (r.ReadByte() - 128.0f) / 127.5f,
                                    z = (r.ReadByte() - 128.0f) / 127.5f,
                                    w = (r.ReadByte() - 128.0f) / 127.5f
                                };
                                Normals[i].x = (2 * (quat.x * quat.z + quat.y * quat.w));
                                Normals[i].y = (2 * (quat.y * quat.z - quat.x * quat.w));
                                Normals[i].z = (2 * (quat.z * quat.z + quat.w * quat.w)) - 1;

                                // UVs ABSOLUTELY should use the Half structures.
                                UVs[i].U = new Half { bits = r.ReadUInt16() }.ToSingle();
                                UVs[i].V = new Half { bits = r.ReadUInt16() }.ToSingle();

                                //Vertices[i].x = new Half { bits = r.ReadUInt16() }.ToSingle();
                                //Vertices[i].y = new Half { bits = r.ReadUInt16() }.ToSingle();
                                //Vertices[i].z = new Half { bits = r.ReadUInt16() }.ToSingle(); 
                                //Normals[i].x = new Half { bits = r.ReadUInt16() }.ToSingle();
                                //Normals[i].y = new Half { bits = r.ReadUInt16() }.ToSingle();
                                //Normals[i].z = new Half { bits = r.ReadUInt16() }.ToSingle();
                                //UVs[i].U = new Half { bits = r.ReadUInt16() }.ToSingle();
                                //UVs[i].V = new Half { bits = r.ReadUInt16() }.ToSingle();
                            }
                            break;
                        default:
                            Log("Unknown VertUV structure");
                            SkipBytes(r, NumElements * BytesPerElement);
                            break;
                    }
                    break;
                case DataStreamTypeEnum.BONEMAP:
                    var skin = GetSkinningInfo();
                    skin.HasBoneMapDatastream = true;
                    skin.BoneMapping = new List<MeshBoneMapping>();
                    // Bones should have 4 bone IDs (index) and 4 weights.
                    for (var i = 0; i < NumElements; i++)
                    {
                        var tmpMap = new MeshBoneMapping();
                        switch (BytesPerElement)
                        {
                            case 8:
                                tmpMap.BoneIndex = new int[4];
                                tmpMap.Weight = new int[4];
                                for (var j = 0; j < 4; j++)         // read the 4 bone indexes first
                                    tmpMap.BoneIndex[j] = r.ReadByte();
                                for (var j = 0; j < 4; j++)           // read the weights.
                                    tmpMap.Weight[j] = r.ReadByte();
                                skin.BoneMapping.Add(tmpMap);
                                break;
                            case 12:
                                tmpMap.BoneIndex = new int[4];
                                tmpMap.Weight = new int[4];
                                for (var j = 0; j < 4; j++)         // read the 4 bone indexes first
                                    tmpMap.BoneIndex[j] = r.ReadUInt16();
                                for (var j = 0; j < 4; j++)           // read the weights.
                                    tmpMap.Weight[j] = r.ReadByte();
                                skin.BoneMapping.Add(tmpMap);
                                break;
                            default: Log("Unknown BoneMapping structure"); break;
                        }
                    }
                    break;
                case DataStreamTypeEnum.UNKNOWN1:
                    Tangents = new Tangent[NumElements, 2];
                    Normals = new Vector3[NumElements];
                    for (var i = 0; i < NumElements; i++)
                    {
                        Tangents[i, 0].w = r.ReadSByte() / 127.0;
                        Tangents[i, 0].x = r.ReadSByte() / 127.0;
                        Tangents[i, 0].y = r.ReadSByte() / 127.0;
                        Tangents[i, 0].z = r.ReadSByte() / 127.0;
                        // Binormal
                        Tangents[i, 1].w = r.ReadSByte() / 127.0;
                        Tangents[i, 1].x = r.ReadSByte() / 127.0;
                        Tangents[i, 1].y = r.ReadSByte() / 127.0;
                        Tangents[i, 1].z = r.ReadSByte() / 127.0;
                        // Calculate the normal based on the cross product of the tangents.
                        Normals[i].x = (Tangents[i, 0].y * Tangents[i, 1].z - Tangents[i, 0].z * Tangents[i, 1].y);
                        Normals[i].y = 0 - (Tangents[i, 0].x * Tangents[i, 1].z - Tangents[i, 0].z * Tangents[i, 1].x);
                        Normals[i].z = (Tangents[i, 0].x * Tangents[i, 1].y - Tangents[i, 0].y * Tangents[i, 1].x);
                    }
                    break;
                default: Log("***** Unknown DataStream Type *****"); break;
            }
        }
    }
}
