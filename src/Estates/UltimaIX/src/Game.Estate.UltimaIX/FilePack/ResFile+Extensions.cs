using static Game.Core.CoreDebug;

namespace Game.Estate.UltimaIX.FilePack
{
    partial class ResFile
    {
        public void TestLoadFileData(int take)
        {
            foreach (var file in new[] { "0" })
            {
                Log(file);
                LoadFileDataAsync(file).Wait();
            }
        }
    }
}