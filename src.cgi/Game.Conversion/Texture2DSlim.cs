using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// Stores information about a 2D texture.
    /// </summary>
    public class Texture2DSlim
    {
        public enum RawDataFormat : byte { Img, Raw, DXT }

        public RawDataFormat DataFormat;
        public int Width, Height;
        public TextureFormat Format;
        public bool HasMipmaps;
        public byte[] RawData;

        const int DDS_HEADER_SIZE = 128;
        public Texture2DSlim(RawDataFormat dataFormat, TextureFormat format, byte[] rawData)
        {
            DataFormat = dataFormat;
            switch (dataFormat)
            {
                case RawDataFormat.DXT:
                    if (format != TextureFormat.DXT1 && format != TextureFormat.DXT5)
                        throw new ArgumentOutOfRangeException(nameof(format), "Invalid TextureFormat. Only DXT1 and DXT5 formats are supported by this method.");
                    Format = format;
                    var ddsSizeCheck = rawData[4];
                    if (ddsSizeCheck != 124)
                        throw new ArgumentOutOfRangeException(nameof(rawData), "Invalid DDS DXTn texture. Unable to read"); // this header byte should be 124 for DDS image files
                    Height = (rawData[13] << 8) | rawData[12];
                    Width = (rawData[17] << 8) | rawData[16];
                    var dxtBytes = new byte[rawData.Length - DDS_HEADER_SIZE];
                    Buffer.BlockCopy(dxtBytes, DDS_HEADER_SIZE, dxtBytes, 0, rawData.Length - DDS_HEADER_SIZE);
                    RawData = dxtBytes;
                    return;
                case RawDataFormat.Raw:
                    RawData = rawData;
                    return;
                default: throw new NotImplementedException($"{dataFormat}");
            }
        }

        public Texture2DSlim(int width, int height, TextureFormat format, bool hasMipmaps, byte[] rawData)
        {
            DataFormat = RawDataFormat.Raw;
            Width = width;
            Height = height;
            Format = format;
            HasMipmaps = hasMipmaps;
            RawData = rawData;
        }

        /// <summary>
        /// Creates a Unity Texture2D from this Texture2DInfo.
        /// </summary>
        public Texture2D ToTexture2D()
        {
            var tex = new Texture2D(Width, Height, Format, HasMipmaps);
            if (RawData != null)
            {
                tex.LoadRawTextureData(RawData);
                tex.Apply();
                tex.Compress(true);
            }
            return tex;
        }
    }
}