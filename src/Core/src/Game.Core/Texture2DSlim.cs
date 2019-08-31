//using System;
//using System.IO;
//using System.Text;
//using UnityEngine;
//using UnityEngine.Rendering;

//namespace Game.Core
//{
//    public class Texture2DSlim
//    {
//        public string name;
//        public int masterTextureLimit;
//        public int anisotropicFiltering;
//        public int width;
//        public int height;
//        public int textureSize;
//        public int dataSize;
//        public TextureDimension dimension;
//        public FilterMode filterMode;
//        public int anisoLevel;
//        public TextureWrapMode wrapMode;
//        public float mipMapBias;
//        public bool hasMipmaps;
//        public int mipmapCount;
//        public int texelSizeX;
//        public int texelSizeY;
//        public TextureFormat format;
//        public bool hasResSData;
//        public long dataPos;
//        public string resSName = "";
//        public byte[] textureData;

//        public Texture2DSlim(int width, int height, TextureFormat format, bool hasMipmaps)
//        {
//            this.width = width;
//            this.height = height;
//            this.format = format;
//            this.hasMipmaps = hasMipmaps;
//        }

//        public void LoadRawTextureData(byte[] rawData) => textureData = rawData;

//        public void LoadImage(byte[] data, string resSFilePath = "")
//        {
//            using (var ms = new MemoryStream(data))
//            using (var r = new EndianBinaryReader(ms, Endian.LittleEndian))
//            {
//                var name_length = r.ReadInt32();
//                name = Encoding.UTF8.GetString(r.ReadBytes(name_length));
//                var pos = r.BaseStream.Position;
//                if (pos % 4 != 0)
//                    r.BaseStream.Seek(4 - pos % 4, SeekOrigin.Current);
//                width = r.ReadInt32();
//                height = r.ReadInt32();
//                if (width > 4096 || height > 4096)
//                    throw new InvalidOperationException("Width/Height Error\n Not a unity Texture2D data");
//                textureSize = r.ReadInt32();
//                if (width == 0 || height == 0)
//                    throw new InvalidOperationException("Width/Height Error\n Empty Texture");
//                format = (TextureFormat)r.ReadInt32();
//                mipmapCount = r.ReadInt32();
//                if (mipmapCount > 1)
//                    hasMipmaps = true;
//                var readable = r.ReadInt32();
//                var imageCount = r.ReadInt32();
//                dimension = (TextureDimension)r.ReadInt32();
//                filterMode = (FilterMode)r.ReadInt32();
//                anisoLevel = r.ReadInt32();
//                mipMapBias = r.ReadInt32();
//                wrapMode = (TextureWrapMode)r.ReadInt32();
//                var lightmapFormat = r.ReadInt32();
//                var colorSpace = r.ReadInt32();
//                dataSize = r.ReadInt32();
//                if (dataSize <= 0)
//                    throw new InvalidOperationException("Data Length Error\n Not a unity Texture2D data");
//                if (dataSize == 0)
//                {
//                    dataPos = r.ReadInt32();
//                    dataSize = r.ReadInt32();
//                    var name_len = r.ReadInt32();
//                    if (dataSize <= 0)
//                        throw new InvalidOperationException("Data Length is 0\n Not a unity Texture2D data");
//                    resSName = Encoding.UTF8.GetString(r.ReadBytes(name_len));
//                    if (resSFilePath == "") resSFilePath = "./";
//                    var rname = string.Format("{0}\\{1}", resSFilePath, resSName);
//                    if (File.Exists(rname))
//                    {
//                        hasResSData = true;
//                        using (var fs = File.Open(rname, FileMode.Open, FileAccess.Read))
//                        using (var r2 = new EndianBinaryReader(fs))
//                        {
//                            r2.BaseStream.Seek(dataPos, SeekOrigin.Begin);
//                            textureData = r2.ReadBytes(dataSize);
//                        }
//                    }
//                    else
//                        throw new InvalidOperationException("resS File not found\n can't get Texture2D data");
//                }
//                else
//                {
//                    resSName = name;
//                    dataPos = r.BaseStream.Position;
//                    textureData = r.ReadBytes(dataSize);
//                }
//            }
//        }
//    }
//}
