using Game.Core;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using static Game.Core.Debug;

namespace Game.Estate.UltimaIX.FilePack
{
    partial class ResFile
    {
        public void TestLoadFileData(int take)
        {
            foreach (var file in _flxFile.GetContainsSet().Take(take))
            {
                Log(file);
                LoadFileDataAsync(file).Wait();
            }
        }

        public Task<Texture2DInfo> LoadTextureInfoAsync(string texturePath)
        {
            var flxPath = ((FlxFile)_flxFile).FilePath;
            var bits = flxPath.Contains("Texture8") ? 8 : flxPath.Contains("Texture16") ? 16 : 0;
            if (bits == 0)
                throw new InvalidOperationException("Texture8 or Texture16");
            var filePath = FindTexture(texturePath);
            return filePath != null
                ? Task.Run(async () =>
                {
                    var fileData = await LoadFileDataAsync(filePath);
                    if (fileData != null) return LoadRawTexture(new MemoryStream(fileData), bits);
                    else throw new NotSupportedException($"Unsupported texture type: {filePath}");
                })
                : Task.FromResult<Texture2DInfo>(null);
        }

        public Task<object> LoadObjectInfoAsync(string filePath) => Task.Run(async () =>
        {
            var fileData = await LoadFileDataAsync(filePath);
            var file = new SiFile(filePath);
            file.Deserialize(new BinaryFileReader(new MemoryStream(fileData)));
            return (object)file;
        });

        /// <summary>
        /// Finds the actual path of a texture.
        /// </summary>
        string FindTexture(string texturePath)
        {
            if (ContainsFile(texturePath))
                return texturePath;
            Log($"Could not find file \"{texturePath}\" in a FLX file.");
            return null;
        }

        static Texture2DInfo LoadRawTexture(Stream inputStream, int bits)
        {
            if (bits == 16)
                using (var r = new BinaryFileReader(inputStream))
                {
                    var width = r.ReadInt32();
                    var height = r.ReadInt32();
                    r.Skip(8);
                    var rawData = r.ReadBytes(width * height * 2);
                    return new Texture2DInfo(width, height, TextureFormat.RGB565, false, rawData);
                }
            else if (bits == 8)
                using (var r = new BinaryFileReader(inputStream))
                {
                    var pal = GetPallet8();
                    var width = r.ReadInt32();
                    var height = r.ReadInt32();
                    r.Skip(8);
                    var data = r.ReadBytes(width * height);
                    var b = new MemoryStream();
                    for (var i = 0; i < height; i++)
                        for (var j = 0; j < width; j++)
                            b.Write(pal[data[j * width + i]], 0, 4);
                    var rawData = b.ToArray();
                    return new Texture2DInfo(width, height, TextureFormat.ARGB32, false, rawData);
                }
            else throw new ArgumentOutOfRangeException(nameof(bits), $"{bits}");
        }

        static byte[][] _pallet8;
        static byte[][] GetPallet8()
        {
            if (_pallet8 != null) return _pallet8;
            var assembly = typeof(ResFile).Assembly;
            lock (assembly)
            {
                if (_pallet8 != null) return _pallet8;
                var pallet8 = new byte[256][];
                using (var bs = assembly.GetManifestResourceStream("Game.Estate.UltimaIX.FilePack.ankh.pal"))
                using (var br = new BinaryReader(bs))
                    for (var i = 0; i < pallet8.Length; i++)
                    {
                        var color = BitConverter.GetBytes(br.ReadUInt32());
                        //var r = color[2]; var g = color[1]; var b = color[0];
                        //color[0] = 0; color[1] = r; color[2] = g; color[3] = b;
                        pallet8[i] = color;
                    }
                return _pallet8 = pallet8;
            }
        }
    }
}