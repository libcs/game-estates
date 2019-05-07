using Gamer.Core;
using System;
using System.Threading.Tasks;
using static Gamer.Core.Debug;

namespace Gamer.Estate.Rsi.FilePack
{
    partial class ResFile
    {
        public void TestLoadFileData()
        {
            foreach (var file in _pakFile._files)
            {
                Log(file.Path);
                LoadFileData(file);
            }
        }

        public Task<Texture2DInfo> LoadTextureInfoAsync(string texturePath)
        {
            throw new NotImplementedException();
        }

        public Task<object> LoadObjectInfoAsync(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}