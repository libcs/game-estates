using Game.Core;
using Game.Estate.UltimaIX.Format;
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
            var filePath = FindTexture(texturePath);
            return filePath != null
                ? Task.Run(async () =>
                {
                    var fileData = await LoadFileDataAsync(filePath);
                    if (fileData == null) throw new NotSupportedException($"File not found: {filePath}");
                    else if (texturePath.StartsWith("bitmap/")) return FlxFile.LoadRawBitmap(new MemoryStream(fileData)).Frames[0];
                    else if (texturePath.StartsWith("texture/")) return FlxFile.LoadRawTexture(new MemoryStream(fileData));
                    else throw new NotSupportedException($"Unsupported texture type: {filePath}");
                })
                : Task.FromResult<Texture2DInfo>(null);
        }

        public Task<object> LoadObjectInfoAsync(string filePath) => Task.Run(async () =>
        {
            var fileData = await LoadFileDataAsync(filePath);
            var file = new SiFile(filePath, new BinaryFileReader(new MemoryStream(fileData)));
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