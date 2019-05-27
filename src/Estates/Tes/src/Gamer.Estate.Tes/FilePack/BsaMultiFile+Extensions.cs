using Gamer.Core;
using Gamer.Core.Format;
using Gamer.Format.Nif;
using System;
using System.IO;
using System.Threading.Tasks;
using static Gamer.Core.Debug;

namespace Gamer.Estate.Tes.FilePack
{
    partial class BsaMultiFile
    {
        public async Task<Texture2DInfo> LoadTextureInfoAsync(string texturePath)
        {
            var filePath = FindTexture(texturePath);
            if (filePath != null)
            {
                var fileData = await LoadFileDataAsync(filePath);
                return await Task.Run(() =>
                {
                    var fileExtension = Path.GetExtension(filePath);
                    if (fileExtension.ToLowerInvariant() == ".dds") return DdsReader.LoadDDSTexture(new MemoryStream(fileData));
                    else throw new NotSupportedException($"Unsupported texture type: {fileExtension}");
                });
            }
            Log($"Could not find file \"{texturePath}\" in a BSA file.");
            return null;
        }

        public async Task<object> LoadObjectInfoAsync(string filePath)
        {
            var fileData = await LoadFileDataAsync(filePath);
            return await Task.Run(() =>
            {
                var file = new NiFile(Path.GetFileNameWithoutExtension(filePath));
                file.Deserialize(new BinaryFileReader(new MemoryStream(fileData)));
                return (object)file;
            });
        }

        /// <summary>
        /// Finds the actual path of a texture.
        /// </summary>
        string FindTexture(string texturePath)
        {
            var textureName = Path.GetFileNameWithoutExtension(texturePath);
            var textureNameInTexturesDir = "textures/" + textureName;
            var filePath = textureNameInTexturesDir + ".dds";
            if (ContainsFile(filePath))
                return filePath;
            //filePath = textureNameInTexturesDir + ".tga";
            //if (ContainsFile(filePath))
            //    return filePath;
            var texturePathWithoutExtension = Path.GetDirectoryName(texturePath) + '/' + textureName;
            filePath = texturePathWithoutExtension + ".dds";
            if (ContainsFile(filePath))
                return filePath;
            //filePath = texturePathWithoutExtension + ".tga";
            //if (ContainsFile(filePath))
            //    return filePath;
            return null;
        }
    }
}