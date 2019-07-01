using Gamer.Core;
using Gamer.Core.Format;
using Gamer.Format.Cry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Gamer.Core.Debug;

namespace Gamer.Estate.Cry.FilePack
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
            var files = new List<(string, Stream)> { (filePath, new MemoryStream(await LoadFileDataAsync(filePath))) };
            var mFilePath = Path.ChangeExtension(filePath, $"{Path.GetExtension(filePath)}m");
            if (ContainsFile(mFilePath))
            {
                Log($"Found geometry file {Path.GetFileName(mFilePath)}");
                files.Add((mFilePath, new MemoryStream(await LoadFileDataAsync(mFilePath))));
            }
            var file = new CryFile(filePath);
            await file.LoadAsync(files, null, FindMaterial, async path => (path, new MemoryStream(await LoadFileDataAsync(path))));
            return (object)file;
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

        /// <summary>
        /// Finds the actual path of a material.
        /// </summary>
        string FindMaterial(string materialPath, string fileName, string cleanName, string dataDir)
        {
            // First try relative to file being processed
            if (Path.GetExtension(materialPath) != ".mtl") materialPath = Path.ChangeExtension(materialPath, "mtl");
            // Then try just the last part of the chunk, relative to the file being processed
            if (!ContainsFile(materialPath)) materialPath = Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileName(cleanName));
            if (Path.GetExtension(materialPath) != ".mtl") materialPath = Path.ChangeExtension(materialPath, "mtl");
            // Then try relative to the ObjectDir
            if (!ContainsFile(materialPath) && dataDir != null) materialPath = Path.Combine(dataDir, cleanName);
            if (Path.GetExtension(materialPath) != ".mtl") materialPath = Path.ChangeExtension(materialPath, "mtl");
            // Then try just the fileName.mtl
            if (!ContainsFile(materialPath)) materialPath = fileName;
            if (Path.GetExtension(materialPath) != ".mtl") materialPath = Path.ChangeExtension(materialPath, "mtl");
            return ContainsFile(materialPath) ? materialPath : null;
        }
    }
}