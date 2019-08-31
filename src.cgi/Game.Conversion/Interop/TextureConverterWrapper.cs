using System;
using System.Runtime.InteropServices;

namespace Game.Conversion.Interop
{
    /// <summary>
    /// TQonvertImage.nFormat: Image format
    /// Paletted formats are no longer supported and are included for legacy purposes only
    /// </summary>
    internal enum QFormat : uint
    {
        // General formats
        Q_FORMAT_RGBA_8UI = 1,
        Q_FORMAT_RGBA_8I,
        Q_FORMAT_RGB5_A1UI,
        Q_FORMAT_RGBA_4444,
        Q_FORMAT_RGBA_16UI,
        Q_FORMAT_RGBA_16I,
        Q_FORMAT_RGBA_32UI,
        Q_FORMAT_RGBA_32I,

        Q_FORMAT_PALETTE_8_RGBA_8888,
        Q_FORMAT_PALETTE_8_RGBA_5551,
        Q_FORMAT_PALETTE_8_RGBA_4444,
        Q_FORMAT_PALETTE_4_RGBA_8888,
        Q_FORMAT_PALETTE_4_RGBA_5551,
        Q_FORMAT_PALETTE_4_RGBA_4444,
        Q_FORMAT_PALETTE_1_RGBA_8888,
        Q_FORMAT_PALETTE_8_RGB_888,
        Q_FORMAT_PALETTE_8_RGB_565,
        Q_FORMAT_PALETTE_4_RGB_888,
        Q_FORMAT_PALETTE_4_RGB_565,

        Q_FORMAT_R2_GBA10UI,
        Q_FORMAT_RGB10_A2UI,
        Q_FORMAT_RGB10_A2I,
        Q_FORMAT_RGBA_F,
        Q_FORMAT_RGBA_HF,

        Q_FORMAT_RGB9_E5,   // Last five bits are exponent bits (Read following section in GLES3 spec: "3.8.17 Shared Exponent Texture Color Conversion")
        Q_FORMAT_RGB_8UI,
        Q_FORMAT_RGB_8I,
        Q_FORMAT_RGB_565,
        Q_FORMAT_RGB_16UI,
        Q_FORMAT_RGB_16I,
        Q_FORMAT_RGB_32UI,
        Q_FORMAT_RGB_32I,

        Q_FORMAT_RGB_F,
        Q_FORMAT_RGB_HF,
        Q_FORMAT_RGB_11_11_10_F,

        Q_FORMAT_RG_F,
        Q_FORMAT_RG_HF,
        Q_FORMAT_RG_32UI,
        Q_FORMAT_RG_32I,
        Q_FORMAT_RG_16I,
        Q_FORMAT_RG_16UI,
        Q_FORMAT_RG_8I,
        Q_FORMAT_RG_8UI,
        Q_FORMAT_RG_S88,

        Q_FORMAT_R_32UI,
        Q_FORMAT_R_32I,
        Q_FORMAT_R_F,
        Q_FORMAT_R_16F,
        Q_FORMAT_R_16I,
        Q_FORMAT_R_16UI,
        Q_FORMAT_R_8I,
        Q_FORMAT_R_8UI,

        Q_FORMAT_LUMINANCE_ALPHA_88,
        Q_FORMAT_LUMINANCE_8,
        Q_FORMAT_ALPHA_8,

        Q_FORMAT_LUMINANCE_ALPHA_F,
        Q_FORMAT_LUMINANCE_F,
        Q_FORMAT_ALPHA_F,
        Q_FORMAT_LUMINANCE_ALPHA_HF,
        Q_FORMAT_LUMINANCE_HF,
        Q_FORMAT_ALPHA_HF,
        Q_FORMAT_DEPTH_16,
        Q_FORMAT_DEPTH_24,
        Q_FORMAT_DEPTH_24_STENCIL_8,
        Q_FORMAT_DEPTH_32,

        Q_FORMAT_BGR_565,
        Q_FORMAT_BGRA_8888,
        Q_FORMAT_BGRA_5551,
        Q_FORMAT_BGRX_8888,
        Q_FORMAT_BGRA_4444,
        // Compressed formats
        Q_FORMAT_ATITC_RGBA,
        Q_FORMAT_ATC_RGBA_EXPLICIT_ALPHA = Q_FORMAT_ATITC_RGBA,
        Q_FORMAT_ATITC_RGB,
        Q_FORMAT_ATC_RGB = Q_FORMAT_ATITC_RGB,
        Q_FORMAT_ATC_RGBA_INTERPOLATED_ALPHA,
        Q_FORMAT_ETC1_RGB8,
        Q_FORMAT_3DC_X,
        Q_FORMAT_3DC_XY,

        Q_FORMAT_ETC2_RGB8,
        Q_FORMAT_ETC2_RGBA8,
        Q_FORMAT_ETC2_RGB8_PUNCHTHROUGH_ALPHA1,
        Q_FORMAT_ETC2_SRGB8,
        Q_FORMAT_ETC2_SRGB8_ALPHA8,
        Q_FORMAT_ETC2_SRGB8_PUNCHTHROUGH_ALPHA1,
        Q_FORMAT_EAC_R_SIGNED,
        Q_FORMAT_EAC_R_UNSIGNED,
        Q_FORMAT_EAC_RG_SIGNED,
        Q_FORMAT_EAC_RG_UNSIGNED,

        Q_FORMAT_S3TC_DXT1_RGB,
        Q_FORMAT_S3TC_DXT1_RGBA,
        Q_FORMAT_S3TC_DXT3_RGBA,
        Q_FORMAT_S3TC_DXT5_RGBA,

        // YUV formats
        Q_FORMAT_AYUV_32,
        Q_FORMAT_I444_24,
        Q_FORMAT_YUYV_16,
        Q_FORMAT_UYVY_16,
        Q_FORMAT_I420_12,
        Q_FORMAT_YV12_12,
        Q_FORMAT_NV21_12,
        Q_FORMAT_NV12_12,

        // ASTC Format
        Q_FORMAT_ASTC_8,
        Q_FORMAT_ASTC_16,
    }

    /// <summary>
    /// TFormatFlags.nEncodeFlag: Image encoding flags 
    /// </summary>
    internal enum TEncodeFlag
    {
        Q_FLAG_ENCODE_NONE = 0,
        Q_FLAG_ENCODE_DEFAULT = 0,
        Q_FLAG_ENCODE_ATITC_FAST,
    }

    /// <summary>
    /// TFormatFlags.nScaleFilter: Scaling option to use when creating scaled output (for example for mipmap generation)
    /// </summary>
    internal enum TScaleFilterFlag
    {
        Q_FLAG_SCALEFILTER_DEFAULT = 0,
        Q_FLAG_SCALEFILTER_NEAREST,
        Q_FLAG_SCALEFILTER_MEAN,
        Q_FLAG_SCALEFILTER_BILINEAR,
        Q_FLAG_SCALEFILTER_BICUBIC,
        Q_FLAG_SCALEFILTER_KAISER,
    }

    /// <summary>
    /// TFormatFlags.nNormalMap: Describes the algorithm to use for creating normal maps (for bumpmapping)
    /// </summary>
    internal enum TNormalMapFlag
    {
        Q_FLAG_NORMALMAP_NONE = 0,
        Q_FLAG_NORMALMAP_ROBERTSCROSS,
        Q_FLAG_NORMALMAP_SOBEL,
        Q_FLAG_NORMALMAP_PREWITTGRADIENT,
    }


    /// <summary>
    /// TFormatFlags.nDebugFlag: Debug options
    /// </summary>
    internal enum TDebugFlags
    {
        Q_FLAG_DEBUG_DEFAULT = 0,
        Q_FLAG_DEBUG_DISABLE_VERBOSE = 0x00000001,  // Disable verbose error output to stderr
        Q_FLAG_DEBUG_VERSION = 0x00000002,  // Return the library version (MAJOR<<16 | MINOR) as an unsigned int in pOutput->pData
    }

    /// <summary>
    /// Qonvert function Success and Error return values
    /// </summary>
    internal enum TReturnCode
    {
        Q_SUCCESS = 0,
        Q_ERROR_UNSUPPORTED_DIMENSIONS,
        Q_ERROR_UNSUPPORTED_SRC_FORMAT,
        Q_ERROR_UNSUPPORTED_DST_FORMAT,
        Q_ERROR_UNSUPPORTED_SRC_FORMAT_FLAG,
        Q_ERROR_UNSUPPORTED_DST_FORMAT_FLAG,
        Q_ERROR_INCORRECT_SRC_PARAMETER,
        Q_ERROR_INCORRECT_DST_PARAMETER,
        Q_ERROR_INCORRECT_DATASIZE,
        Q_ERROR_OTHER,
    }

    //=============================================================================
    // TYPEDEFS 
    //=============================================================================

    /// <summary>
    /// Additional format flags, leaving any value to 0 means the library will use a proper default value 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct TFormatFlags
    {
        /// Specify if the image stride is different then the default (bpp*width)
        public uint nStride;
        /// Specify which of the bits in a color are red, can be used for swizzled color modes like (BGR instead of RGB)
        public uint nMaskRed;
        /// Specify which of the bits in a color are green, can be used for swizzled color modes like (BGR instead of RGB)
        public uint nMaskGreen;
        /// Specify which of the bits in a color are blue, can be used for swizzled color modes like (BGR instead of RGB)
        public uint nMaskBlue;
        /// Specify which of the bits in a color are alpha, can be used for swizzled color modes like (BGR instead of RGB)
        public uint nMaskAlpha;
        /// Flip image horizontally
        public uint nFlipX;
        /// Flip image vertically
        public uint nFlipY;
        /// Specifying image encoding options (see TEncodeFlag above)
        public uint nEncodeFlag;
        /// TScaleFilterFlag specifying the scale filter used when scaling the source image to the destination image (for mipmapping)
        public uint nScaleFilterFlag;
        /// TNormalMapFlag specify if a normal map should be created (for destination images only) 
        public uint nNormalMapFlag;
        /// Specify the scale when creating a normalmap (0 = no scaling)
        public uint nNormalMapScale;
        /// Specify if wrapping is enabled when creating a normalmap (0 = clamp to border, 1 = wrap)
        public uint nNormalMapWrap;
        /// Debug flags (logical OR TDebugFlags)
        public uint nDebugFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TQonvertImage
    {
        /// Image width
        public uint nWidth;
        /// Image height
        public uint nHeight;
        /// Image format, see Q_FORMAT_xxx enums above.
        public uint nFormat;
        /// Points to additional image format flags, can be NULL (will assume default values).
        public IntPtr pFormatFlags;
        /// Size of the pData buffer
        public uint nDataSize;
        /// Image data
        //[MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public IntPtr pData;
        /// void*
        public IntPtr compressionOptions;
    }

    internal enum CompressionOptionsType
    {
        CompType_None = 0,
        CompType_ASTC,
        CompType_ASTC_16,
        CompType_Count
    }

    internal enum ASTCCompressionOptionsSpeed
    {
        ASTC_EXHAUSTIVE = 0,
        ASTC_THOROUGH,
        ASTC_MEDIUM,
        ASTC_FAST,
        ASTC_VERY_FAST
    }

    internal enum ASTCCompressionOptionsMode
    {
        ASTC_HDR = 0,
        ASTC_SRGB,
        ASTC_LINEAR
    }

    internal enum ASTCCompressionOptionsDefaultOrCustom
    {
        DEFAULT = 0,
        CUSTOM
    }

    internal enum ASTCCompressionOptionsUseBitRate
    {
        USE_BITRATE = 0,
        USE_BLOCK_DIMENSIONS
    }

    internal enum ASTCCompressionOptionsCommand
    {
        COMPRESS = 0,
        DECOMPRESS
    }

    internal enum ASTC_BIT_FIDELITY
    {
        ASTC_8_BIT = 1,
        ASTC_16_BIT,
        ASTC_32_BIT,
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ASTCOptions
    {
        // These variables are ASTC specific. 
        public int CompressionSpeed;
        public int CompressionMode;
        public int UseBitRate;
        public float BitRate;
        public int xDimension;
        public int yDimension;

        public int UseBuiltInOptimizations;
        public int ApplyPSNROptimization;
        public int ApplyPerceptualOptimization;
        public int ApplyMaskOptimization;
        public int ApplyAlphaOptimization;
        public int ApplyHDROptimization;
        public int ApplyHDRWithAlphaChannelOptimization;
        public int ApplyHDRForLogErrorOptimization;
        public int ApplyHDRWithAlphaChannelForLogErrorOptimization;
        public int BitFidelity;
        public int ASTCCompressionMode;

        /// unsigned char*
        [MarshalAs(UnmanagedType.LPStr)]
        public string astc_pData;

        /// TQonvertImage*
        public IntPtr astc_pSrc;

        /// TQonvertImage*
        public IntPtr astc_pDest;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct StandardOptions
    {
        /// int
        public int ImageType; //Is this a CompressedImage or a DifferenceImage (cacheing only)
        /// int
        public int CompressionType; //For help with typecasting the pointer below.
        /// void*
        public IntPtr CompressionOptions; //The pointer to the compression Options.
    }

    internal class NativeMethods
    {
        /// Return Type: unsigned short
        /// nByte: unsigned char
        [DllImport("TextureConverter.dll", EntryPoint = "UByteToUInt16")]
        public static extern ushort UByteToUInt16(byte nByte);

        /// Return Type: short
        /// nByte: unsigned char
        [DllImport("TextureConverter.dll", EntryPoint = "UByteToInt16")]
        public static extern short UByteToInt16(byte nByte);

        /// Return Type: _TQonvertImage*
        /// param0: _TQonvertImage*
        [DllImport("TextureConverter.dll", EntryPoint = "NewTQonvertImage")]
        public static extern IntPtr NewTQonvertImage(ref TQonvertImage param0);

        //=============================================================================
        // FUNCTION PROTOTYPES
        //=============================================================================

        //-----------------------------------------------------------------------------
        // Qonvert
        //
        // Convert the input texture to the output texture format. Typically used to convert
        // simple image formats into OpenGL ES supported compression formats, like ATITC
        // supported on the Conversion Platform MSM series chips.
        //
        // If pOutput->pData is set to NULL, the pOutput->nDataSize will be calculated based 
        // on the pOutput format information. With this information the application is expected
        // to create a memory block for pOutput->pData. 
        // 
        // Return: 
        //   0     == SUCCESS
        //   other == ERROR CODE
        //-----------------------------------------------------------------------------
        /// Return Type: unsigned int
        /// pSrcImg: TQonvertImage*
        /// pDstImg: TQonvertImage*
        /// options: void*
        [DllImport("TextureConverter.dll", EntryPoint = "Qonvert")]
        public static extern uint Qonvert(ref TQonvertImage pSrcImg, ref TQonvertImage pDstImg, IntPtr options);

        /// Return Type: _ASTCOptions*
        [DllImport("TextureConverter.dll", EntryPoint = "NewASTCOptions")]
        public static extern IntPtr NewASTCOptions();

        /// Return Type: _ASTCOptions*
        /// param0: _ASTCOptions*
        [DllImport("TextureConverter.dll", EntryPoint = "NewASTCOptionsCopy")]
        public static extern IntPtr NewASTCOptionsCopy(ref ASTCOptions param0);

        /// Return Type: boolean
        /// me: _ASTCOptions*
        ///other: _ASTCOptions*
        [DllImport("TextureConverter.dll", EntryPoint = "ASTCOptionsEqual")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool ASTCOptionsEqual(ref ASTCOptions me, ref ASTCOptions other);

        /// Return Type: _StandardOptions*
        /// param0: int
        /// param1: int
        /// param2: void*
        [DllImport("TextureConverter.dll", EntryPoint = "NewStandardOptions")]
        public static extern IntPtr NewStandardOptions(int param0, int param1, IntPtr param2);

        /// Return Type: _StandardOptions*
        /// options: _StandardOptions*
        [DllImport("TextureConverter.dll", EntryPoint = "NewStandardOptionsCopy")]
        public static extern IntPtr NewStandardOptionsCopy(ref StandardOptions options);

        static T Alloc<T>()
        {
            var typeSize = Marshal.SizeOf(typeof(T));
            unsafe
            {
                var ptr = Marshal.AllocHGlobal(typeSize);
                var basePtr = (byte*)ptr.ToPointer();
                for (var idx = 0; idx < typeSize; idx++)
                    *basePtr++ = 0;
                return (T)Marshal.PtrToStructure(ptr, typeof(T));
            }
        }

        public static TQonvertImage CreateEmptyQonvertImage() => Alloc<TQonvertImage>();
        public static TFormatFlags CreateFormatFlags() => Alloc<TFormatFlags>();
    }

    public class TextureConverterWrapper
    {
        public enum CompressionFormat : uint
        {
            AtcRgbaExplicitAlpha = QFormat.Q_FORMAT_ATC_RGBA_EXPLICIT_ALPHA,
            AtcRgbaInterpolatedAlpha = QFormat.Q_FORMAT_ATC_RGBA_INTERPOLATED_ALPHA,
            AtcRgb = QFormat.Q_FORMAT_ATC_RGB,
            Etc1 = QFormat.Q_FORMAT_ETC1_RGB8,
            Etc2Rgba = QFormat.Q_FORMAT_ETC2_RGBA8,
            Astc8 = QFormat.Q_FORMAT_ASTC_8,
            Astc16 = QFormat.Q_FORMAT_ASTC_16,
            RGBA8888 = QFormat.Q_FORMAT_RGBA_8UI,
            DXT1 = QFormat.Q_FORMAT_S3TC_DXT1_RGB,
            DXT3 = QFormat.Q_FORMAT_S3TC_DXT3_RGBA,
            DXT5 = QFormat.Q_FORMAT_S3TC_DXT5_RGBA,
        }

        /// <summary>
        /// data must be RGBA8888 byte array
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="targetFormat">The target format.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// </exception>
        /// <exception cref="System.DllNotFoundException"></exception>
        public static byte[] Compress(byte[] data, int width, int height, CompressionFormat targetFormat)
        {
            try
            {
                var format = (QFormat)targetFormat;
                var uwidth = (uint)width;
                var uheight = (uint)height;
                var src = NativeMethods.CreateEmptyQonvertImage();
                var dst = NativeMethods.CreateEmptyQonvertImage();
                var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                try
                {
                    src.nWidth = uwidth;
                    src.nHeight = uheight;
                    src.nFormat = (uint)QFormat.Q_FORMAT_RGBA_8UI;
                    src.pData = handle.AddrOfPinnedObject();
                    dst.nWidth = uwidth;
                    dst.nHeight = uheight;
                    dst.nFormat = (uint)format;
                    dst.pData = IntPtr.Zero;
                    if (NativeMethods.Qonvert(ref src, ref dst, IntPtr.Zero) != (uint)TReturnCode.Q_SUCCESS)
                        throw new Exception($"Error getting compressed data size for {format} Compression");
                    var compressed = new byte[dst.nDataSize];
                    var compressedHandle = GCHandle.Alloc(compressed, GCHandleType.Pinned);
                    try
                    {
                        dst.pData = compressedHandle.AddrOfPinnedObject();
                        if (NativeMethods.Qonvert(ref src, ref dst, IntPtr.Zero) != (uint)TReturnCode.Q_SUCCESS)
                            throw new Exception($"Error getting compressed data size for {format} Compression");
                        return compressed;
                    }
                    finally { compressedHandle.Free(); }
                }
                finally { handle.Free(); }
            }
            catch (DllNotFoundException ex) { throw new DllNotFoundException($"{ex.Message} See Documentation for more details."); }
        }
    }
}
