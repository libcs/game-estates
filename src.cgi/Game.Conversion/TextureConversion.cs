using Game.Conversion.Interop;
using Nvidia.TextureTools;
using PVRTexLibNET;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Game.Core
{
    public static class TextureConversion
    {
        public static unsafe bool Compress(this Texture2DSlim tex, TextureFormat format)
        {
            var data = tex.RawData;
            int width = tex.Width, height = tex.Height, pos, outPos;
            switch (format)
            {
                case TextureFormat.Alpha8:
                    {
                        pos = 0; outPos = 0;
                        var out_ = new byte[width * height];
                        fixed (byte* pOut = out_, pData = data)
                            for (var y = 0; y < height; y++)
                                for (var x = 0; x < width; x++)
                                {
                                    var fR = pData[pos];
                                    var fG = pData[pos + 1];
                                    var fB = pData[pos + 2];
                                    var fA = pData[pos + 3];
                                    pOut[outPos] = (byte)(((fR + fB + fG) / 3) & 0XFF);
                                    pos += 4;
                                    outPos += 1;
                                }
                        tex.RawData = out_;
                        return true;
                    }

                case TextureFormat.ARGB4444:
                case TextureFormat.RGB565:
                    {
                        pos = 0; outPos = 0;
                        using (var pvrTexture = PVRTexture.CreateTexture(data, (uint)width, (uint)height, 1, PixelFormat.RGBA8888, true, VariableType.UnsignedByte, ColourSpace.sRGB))
                        {
                            var doDither = true;
                            pvrTexture.Transcode(PixelFormat.RGBA4444, VariableType.UnsignedByte, ColourSpace.sRGB, format == TextureFormat.ARGB4444 ? CompressorQuality.PVRTCNormal : CompressorQuality.ETCMedium, doDither);
                            var texDataSize = pvrTexture.GetTextureDataSize(0);
                            var out_ = new byte[texDataSize];
                            pvrTexture.GetTextureData(out_, texDataSize);
                            if (format == TextureFormat.RGB565)
                            {
                                var data2 = out_;
                                out_ = new byte[texDataSize];
                                fixed (byte* pOut = out_, pData = data2)
                                    for (var y = 0; y < height; y++)
                                        for (var x = 0; x < width; x++)
                                        {
                                            var v0 = pData[pos];
                                            var v1 = pData[pos + 1];
                                            // 4bit little endian {A, B},{G, R}
                                            var sA = v0 & 0xF0 >> 4;
                                            var sB = (v0 & 0xF0) >> 4;
                                            var sG = v1 & 0xF0 >> 4;
                                            var sR = (v1 & 0xF0) >> 4;
                                            // swap to little endian {B, G, R, A }
                                            var fB = sB & 0xf;
                                            var fG = sG & 0xf;
                                            var fR = sR & 0xf;
                                            var fA = sA & 0xf;
                                            pOut[outPos] = (byte)((fG << 4) + fB);
                                            pOut[outPos + 1] = (byte)((fA << 4) + fR);
                                            pos += 2;
                                            outPos += 2;
                                        }
                            }
                            tex.RawData = out_;
                            return true;
                        }
                    }

                case TextureFormat.RGB24:
                    {
                        pos = 0; outPos = 0;
                        var out_ = new byte[width * height * 3];
                        fixed (byte* pOut = out_, pData = data)
                            for (var y = 0; y < height; y++)
                                for (var x = 0; x < width; x++)
                                {
                                    // 4bit little endian {A, B},{G, R}
                                    var fR = pData[pos];
                                    var fG = pData[pos + 1];
                                    var fB = pData[pos + 2];
                                    var fA = pData[pos + 3];
                                    pOut[outPos] = fR;
                                    pOut[outPos + 1] = fG;
                                    pOut[outPos + 2] = fB;
                                    pos += 4;
                                    outPos += 3;
                                }
                        tex.RawData = out_;
                        return true;
                    }

                case TextureFormat.RGBA32:
                    return true;

                case TextureFormat.ARGB32:
                    {
                        pos = 0; outPos = 0;
                        var out_ = new byte[width * height * 4];
                        fixed (byte* pOut = out_, pData = data)
                            for (var y = 0; y < height; y++)
                                for (var x = 0; x < width; x++)
                                {
                                    var fA = pData[pos];
                                    var fR = pData[pos + 1];
                                    var fG = pData[pos + 2];
                                    var fB = pData[pos + 3];
                                    out_[outPos] = fR;
                                    out_[outPos + 1] = fG;
                                    out_[outPos + 2] = fB;
                                    out_[outPos + 3] = fA;
                                    pos += 4;
                                    outPos += 4;
                                }
                        tex.RawData = out_;
                        return true;
                    }

                //case TextureFormat.ATC_RGBA8:
                //case TextureFormat.ATC_RGB4:
                case TextureFormat.ETC2_RGBA8:
                case TextureFormat.ETC_RGB4:
                    TextureConverterWrapper.CompressionFormat format2;
                    switch (format)
                    {
                        //case TextureFormat.ATC_RGBA8: format2 = TextureConverterWrapper.CompressionFormat.AtcRgbaExplicitAlpha; break;
                        //case TextureFormat.ATC_RGB4: format2 = TextureConverterWrapper.CompressionFormat.AtcRgb; break;
                        case TextureFormat.ETC2_RGBA8: format2 = TextureConverterWrapper.CompressionFormat.Etc2Rgba; break;
                        case TextureFormat.ETC_RGB4: format2 = TextureConverterWrapper.CompressionFormat.Etc1; break;
                        default: throw new ArgumentOutOfRangeException(nameof(format), format.ToString());
                    }
                    tex.RawData = TextureConverterWrapper.Compress(data, width, height, format2);
                    return true;

                case TextureFormat.DXT1:
                case TextureFormat.DXT5:
                    {
                        var dxtCompressor = new Compressor();
                        var inputOptions = new InputOptions();
                        inputOptions.SetAlphaMode(format == TextureFormat.DXT1 ? AlphaMode.None : AlphaMode.Premultiplied);
                        inputOptions.SetTextureLayout(TextureType.Texture2D, width, height, 1);
                        fixed (byte* pData = data)
                            for (var x = 0; x < data.Length; x += 4)
                            {
                                pData[x] ^= pData[x + 2];
                                pData[x + 2] ^= pData[x];
                                pData[x] ^= pData[x + 2];
                            }
                        var dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
                        try
                        {
                            var dataPtr = dataHandle.AddrOfPinnedObject();
                            inputOptions.SetMipmapData(dataPtr, width, height, 1, 0, 0);
                            inputOptions.SetMipmapGeneration(false);
                            inputOptions.SetGamma(1.0f, 1.0f);
                            var compressionOptions = new CompressionOptions();
                            compressionOptions.SetFormat(format == TextureFormat.DXT1 ? Nvidia.TextureTools.Format.DXT1 : Nvidia.TextureTools.Format.DXT5);
                            compressionOptions.SetQuality(Quality.Normal);
                            var outputOptions = new OutputOptions();
                            outputOptions.SetOutputHeader(false);
                            var out_ = new byte[0];
                            using (var handler = new DxtDataHandler(out_, outputOptions))
                            {
                                dxtCompressor.Compress(inputOptions, compressionOptions, outputOptions);
                                out_ = handler.dst;
                            }
                            tex.RawData = out_;
                            return true;
                        }
                        finally { dataHandle.Free(); }
                    }

                case TextureFormat.ASTC_RGBA_4x4:
                case TextureFormat.ASTC_RGB_4x4:
                    AstcencWrapper.EncodeASTC(data, width, height, 4, 4, out tex.RawData);
                    return true;

                default: throw new ArgumentOutOfRangeException(nameof(format), format.ToString());
            }
        }

        public static unsafe bool Decompress(this Texture2DSlim tex, TextureFormat format)
        {
            var data = tex.RawData;
            int width = tex.Width, height = tex.Height, pos, outPos;
            switch (format)
            {
                case TextureFormat.Alpha8:
                    {
                        pos = 0; outPos = 0;
                        var out_ = new byte[width * height * 4];
                        fixed (byte* pOut = out_, pData = data)
                            for (var y = 0; y < height; y++)
                                for (var x = 0; x < width; x++)
                                {
                                    var fA = pData[pos];
                                    pOut[outPos] = fA;
                                    pOut[outPos + 1] = fA;
                                    pOut[outPos + 2] = fA;
                                    pOut[outPos + 3] = 0xff;
                                    pos += 1;
                                    outPos += 4;
                                }
                        tex.RawData = out_;
                        return true;
                    }

                case TextureFormat.ARGB4444:
                    {
                        pos = 0; outPos = 0;
                        var out_ = new byte[width * height * 4];
                        fixed (byte* pOut = out_, pData = data)
                            for (var y = 0; y < height; y++)
                                for (var x = 0; x < width; x++)
                                {
                                    var v0 = pData[pos];
                                    var v1 = pData[pos + 1];
                                    // 4bit little endian {B, G}, {R, A }
                                    var fB = v0 & 0xF0 >> 4;
                                    var fG = (v0 & 0xF0) >> 4;
                                    var fR = v1 & 0xF0 >> 4;
                                    var fA = (v1 & 0xF0) >> 4;
                                    fA = (fA * 255 + 7) / 15;
                                    fR = (fR * 255 + 7) / 15;
                                    fG = (fG * 255 + 7) / 15;
                                    fB = (fB * 255 + 7) / 15;
                                    pOut[outPos] = (byte)fR;
                                    pOut[outPos + 1] = (byte)fG;
                                    pOut[outPos + 2] = (byte)fB;
                                    pOut[outPos + 3] = (byte)fA;
                                    pos += 2;
                                    outPos += 4;
                                }
                        tex.RawData = out_;
                        return true;
                    }

                case TextureFormat.RGBA4444:
                    {
                        pos = 0; outPos = 0;
                        var out_ = new byte[width * height * 4];
                        fixed (byte* pOut = out_, pData = data)
                            for (var y = 0; y < height; y++)
                                for (var x = 0; x < width; x++)
                                {
                                    var v0 = pData[pos];
                                    var v1 = pData[pos + 1];
                                    // 4bit little endian {B, G}, {R, A }
                                    var fA = v0 & 0xF0 >> 4;
                                    var fR = (v0 & 0xF0) >> 4;
                                    var fG = v1 & 0xF0 >> 4;
                                    var fB = (v1 & 0xF0) >> 4;
                                    fA = (fA * 255 + 7) / 15;
                                    fR = (fR * 255 + 7) / 15;
                                    fG = (fG * 255 + 7) / 15;
                                    fB = (fB * 255 + 7) / 15;
                                    pOut[outPos] = (byte)fR;
                                    pOut[outPos + 1] = (byte)fG;
                                    pOut[outPos + 2] = (byte)fB;
                                    pOut[outPos + 3] = (byte)fA;
                                    pos += 2;
                                    outPos += 4;
                                }
                        tex.RawData = out_;
                        return true;
                    }

                case TextureFormat.RGBA32:
                    {
                        pos = 0; outPos = 0;
                        var out_ = new byte[width * height * 4];
                        fixed (byte* pOut = out_, pData = data)
                            for (var y = 0; y < height; y++)
                                for (var x = 0; x < width; x++)
                                {
                                    var fR = pData[pos];
                                    var fG = pData[pos + 1];
                                    var fB = pData[pos + 2];
                                    var fA = pData[pos + 3];
                                    pOut[outPos] = fR;
                                    pOut[outPos + 1] = fG;
                                    pOut[outPos + 2] = fB;
                                    pOut[outPos + 3] = fA;
                                    pos += 4;
                                    outPos += 4;
                                }
                        tex.RawData = out_;
                        return true;
                    }

                case TextureFormat.ARGB32:
                    {
                        pos = 0; outPos = 0;
                        var out_ = new byte[width * height * 4];
                        fixed (byte* pOut = out_, pData = data)
                            for (var y = 0; y < height; y++)
                                for (var x = 0; x < width; x++)
                                {
                                    var fA = pData[pos];
                                    var fR = pData[pos + 1];
                                    var fG = pData[pos + 2];
                                    var fB = pData[pos + 3];
                                    pOut[outPos] = fR;
                                    pOut[outPos + 1] = fG;
                                    pOut[outPos + 2] = fB;
                                    pOut[outPos + 3] = fA;
                                    pos += 4;
                                    outPos += 4;
                                }
                        tex.RawData = out_;
                        return true;
                    }

                case TextureFormat.RGB24:
                    {
                        pos = 0; outPos = 0;
                        var out_ = new byte[width * height * 4];
                        fixed (byte* pOut = out_, pData = data)
                            for (var y = 0; y < height; y++)
                                for (var x = 0; x < width; x++)
                                {
                                    var fR = pData[pos];
                                    var fG = pData[pos + 1];
                                    var fB = pData[pos + 2];
                                    pOut[outPos] = fR;
                                    pOut[outPos + 1] = fG;
                                    pOut[outPos + 2] = fB;
                                    pOut[outPos + 3] = 0XFF;
                                    pos += 3;
                                    outPos += 4;
                                }
                        tex.RawData = out_;
                        return true;
                    }

                case TextureFormat.RGB565:
                case TextureFormat.ETC2_RGBA8:
                case TextureFormat.ETC2_RGBA1:
                case TextureFormat.ETC_RGB4:
                    {
                        PixelFormat format2;
                        switch (format)
                        {
                            case TextureFormat.RGB565: format2 = PixelFormat.RGB565; break;
                            case TextureFormat.ETC2_RGBA8: format2 = PixelFormat.ETC2_RGBA; break;
                            case TextureFormat.ETC2_RGBA1: format2 = PixelFormat.ETC2_RGB_A1; break;
                            case TextureFormat.ETC_RGB4: format2 = PixelFormat.ETC1; break;
                            default: throw new ArgumentOutOfRangeException(nameof(format), format.ToString());
                        }
                        using (var pvrTexture = PVRTexture.CreateTexture(data, (uint)width, (uint)height, 1, format2, true, VariableType.UnsignedByte, ColourSpace.sRGB))
                        {
                            pvrTexture.Transcode(PixelFormat.RGBA8888, VariableType.UnsignedByte, ColourSpace.sRGB, CompressorQuality.PVRTCNormal, false);
                            var texDataSize = pvrTexture.GetTextureDataSize(0);
                            var out_ = new byte[texDataSize];
                            pvrTexture.GetTextureData(out_, texDataSize);
                            tex.RawData = out_;
                            return true;
                        }
                    }

                case TextureFormat.DXT1:
                case TextureFormat.DXT5:
                    {
                        using (var pvrTexture = PVRTexture.CreateTexture(data, (uint)width, (uint)height, 1, format == TextureFormat.DXT1 ? PixelFormat.DXT1 : PixelFormat.DXT5, true, VariableType.UnsignedByte, ColourSpace.sRGB))
                        {
                            pvrTexture.Transcode(PixelFormat.RGBA8888, VariableType.UnsignedByte, ColourSpace.sRGB, CompressorQuality.PVRTCNormal, false);
                            var texDataSize = pvrTexture.GetTextureDataSize(0);
                            var out_ = new byte[texDataSize];
                            pvrTexture.GetTextureData(out_, texDataSize);
                            tex.RawData = out_;
                            return true;
                        }
                    }

                case TextureFormat.ASTC_RGB_4x4:
                case TextureFormat.ASTC_RGBA_4x4:
                    AstcencWrapper.DecodeASTC(data, width, height, 4, 4, out tex.RawData);
                    return true;

                default: throw new ArgumentOutOfRangeException(nameof(format), format.ToString());
            }
        }
    }
}
