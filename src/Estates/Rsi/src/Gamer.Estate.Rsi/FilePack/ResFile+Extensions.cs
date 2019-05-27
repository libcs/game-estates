using Gamer.Core;
using Gamer.Core.Format;
using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using static Gamer.Core.Debug;

namespace Gamer.Estate.Rsi.FilePack
{
    partial class ResFile
    {
        public void TestLoadFileData(int take)
        {
            foreach (var file in _pakFile._files.Take(take))
            {
                Log(file.Path);
                LoadFileDataAsync(file).Wait();
            }
        }

        public Task<Texture2DInfo> LoadTextureInfoAsync(string texturePath)
        {
            var filePath = FindTexture(texturePath);
            return filePath != null ? Task.Run(async () =>
            {
                var fileData = await LoadFileDataAsync(filePath);
                var fileExtension = Path.GetExtension(filePath);
                if (fileExtension.ToLowerInvariant() == ".dds") return DdsReader.LoadDDSTexture(new MemoryStream(fileData));
                else throw new NotSupportedException($"Unsupported texture type: {fileExtension}");
            }) : null;
        }

        public Task<object> LoadObjectInfoAsync(string filePath) => Task.Run(async () =>
        {
            var fileData = await LoadFileDataAsync(filePath);
            return (object)null;
            //var file = new NiFile(Path.GetFileNameWithoutExtension(filePath));
            //file.Deserialize(new BinaryFileReader(new MemoryStream(fileData)));
            //return (object)file;
        });

        /// <summary>
        /// Finds the actual path of a texture.
        /// </summary>
        string FindTexture(string texturePath)
        {
            var textureName = Path.GetFileNameWithoutExtension(texturePath);
            var textureNameInTexturesDir = $"textures/{textureName}";
            var filePath = $"{textureNameInTexturesDir}.dds";
            if (ContainsFile(filePath))
                return filePath;
            Log($"Could not find file \"{texturePath}\" in a BSA file.");
            return null;
        }
    }
}