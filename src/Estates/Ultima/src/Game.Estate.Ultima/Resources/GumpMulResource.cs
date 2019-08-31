using Game.Core;
using Game.Estate.Ultima.Resources.IO;
using UnityEngine;
using static Game.Core.Debug;

namespace Game.Estate.Ultima.Resources
{
    public class GumpMulResource
    {
        object _graphicsDevice;
        readonly PixelPicking _picking = new PixelPicking();
        Texture2DInfo[] _textureCache = new Texture2DInfo[0x10000];

        public AFileIndex FileIndex { get; } = ClientVersion.InstallationIsUopFormat ?
            UltimaFileManager.CreateFileIndex("gumpartLegacyMUL.uop", 0xFFFF, true, ".tga") :
            UltimaFileManager.CreateFileIndex("Gumpidx.mul", "Gumpart.mul", 0x10000, 12);

        public GumpMulResource(object graphics) => _graphicsDevice = graphics;

        public unsafe Texture2DInfo GetGumpTexture(int textureId, bool replaceMask080808 = false)
        {
            if (textureId < 0)
                return null;
            if (_textureCache[textureId] == null)
            {
                var r = FileIndex.Seek(textureId, out int length, out int extra, out bool patched);
                if (r == null)
                    return null;
                var width = (extra >> 16) & 0xFFFF;
                var height = extra & 0xFFFF;
                if (width == 0 || height == 0)
                    return null;
                var shortsToRead = length - (height * 2);
                if (r.BaseStream.Length - r.Position < (shortsToRead * 2))
                {
                    Log($"Could not read gump {textureId:X4}: not enough data. Gump texture file truncated?");
                    return null;
                }
                var lookups = r.ReadManyInt32(height);
                var metrics_dataread_start = (int)r.Position;
                var fileData = r.ReadManyUInt16(shortsToRead);
                var pixels = new byte[width * height * 4];
                fixed (byte* line = &pixels[0])
                fixed (ushort* data = &fileData[0])
                    for (var y = 0; y < height; ++y)
                    {
                        ushort* dataRef = data + (lookups[y] - height) * 2;
                        uint* cur = (uint*)line + (y * width);
                        uint* end = cur + width;
                        while (cur < end)
                        {
                            var color = (*dataRef++).FromBGR555(false);
                            uint* next = cur + *dataRef++;
                            if (color == 0)
                                cur = next;
                            else
                            {
                                color |= 0xFF000000;
                                while (cur < next)
                                    *cur++ = color;
                            }
                        }
                    }
                Metrics.ReportDataRead(length);
                //if (replaceMask080808)
                //    for (var i = 0; i < pixels.Length; i++)
                //        if (pixels[i] == 0x8421)
                //            pixels[i] = 0xFC1F;
                var texture = new Texture2DInfo(width, height, TextureFormat.BGRA32, false, pixels); //: SurfaceFormat.Bgra5551
                _textureCache[textureId] = texture;
                _picking.Set(textureId, width, height, pixels);
            }
            return _textureCache[textureId];
        }

        public bool IsPointInGumpTexture(int textureId, int x, int y) => _picking.Get(textureId, x, y);
    }
}