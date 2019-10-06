﻿using Game.Core;
using System;
using System.IO;
using System.Threading.Tasks;
using static Game.Core.CoreDebug;

namespace Game.Estate.UltimaIX.FilePack
{
    partial class ResMultiFile
    {
        public Task<Texture2DInfo> LoadTextureInfoAsync(string texturePath)
        {
            //var flxPath = FilePath;
            //var bitsPerPixel = flxPath.Contains("Texture8") ? 1 : flxPath.Contains("Texture16") ? 2 : 0;
            //if (bitsPerPixel == 0)
            //    throw new InvalidOperationException("Texture8 or Texture16");
            var bitsPerPixel = 1;
            var filePath = FindTexture(texturePath);
            return filePath != null
                ? Task.Run(async () =>
                {
                    var fileData = await LoadFileDataAsync(filePath);
                    if (fileData == null) throw new NotSupportedException($"File not found: {filePath}");
                    else if (texturePath.StartsWith("bitmap/")) return FlxFile.LoadRawBitmap(new MemoryStream(fileData)).Frames[0];
                    else if (texturePath.StartsWith("texture/")) return FlxFile.LoadRawMinimapTile(new MemoryStream(fileData), bitsPerPixel);
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
    }
}