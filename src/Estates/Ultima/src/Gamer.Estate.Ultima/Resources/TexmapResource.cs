using Gamer.Base.Core;
using Gamer.Estate.Ultima.Resources.IO;
using UnityEngine;
using static System.Diagnostics.Debug;

namespace Gamer.Estate.Ultima.Resources
{
    public class TexmapResource
    {
        Texture2DInfo[] _cache = new Texture2DInfo[0x4000];
        readonly AFileIndex _index = FileManager.CreateFileIndex("texidx.mul", "texmaps.mul", 0x4000, -1); // !!! must find patch file reference for texmap.

        const int DEFAULT_TEXTURE = 0x007F; // index 127 is the first 'unused' texture.

        public TexmapResource(object graphics) { }

        public Texture2DInfo GetTexmapTexture(int index)
        {
            index &= 0x3FFF;
            if (_cache[index] == null)
            {
                _cache[index] = ReadTexmapTexture(index);
                if (_cache[index] == null)
                    _cache[index] = GetTexmapTexture(127);
            }
            return _cache[index];
        }

         unsafe Texture2DInfo ReadTexmapTexture(int index)
        {
            var r = _index.Seek(index, out int length, out int extra, out bool isPatched);
            if (r == null)
                return null;
            if (r.BaseStream.Length == 0)
            {
                Print($"Requested texmap texture #{index} does not exist. Replacing with 'unused' graphic.");
                return GetTexmapTexture(DEFAULT_TEXTURE);
            }
            var metrics_dataread_start = (int)r.Position;
            var textureSize = extra == 0 ? 64 : 128;
            var fileSize = textureSize * textureSize;
            var pixels = new byte[fileSize * 4];
            var fileData = r.ReadManyUInt16(fileSize);
            fixed (byte* pData = pixels)
            {
                uint* pDataRef = (uint*)pData;
                int count = 0;
                int max = fileSize;
                while (count < max)
                    *pDataRef++ = fileData[count++].FromBGR555();
            }
            var texture = new Texture2DInfo(textureSize, textureSize, TextureFormat.BGRA32, false, pixels);
            Metrics.ReportDataRead((int)r.Position - metrics_dataread_start);
            return texture;
        }
    }
}