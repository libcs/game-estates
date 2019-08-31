using System;
using System.Linq;
using static Game.Core.Debug;

namespace Game.Estate.Tes.FilePack
{
    partial class BsaFile
    {
        public void TestContainsFile()
        {
            foreach (var file in _files)
            {
                Log($"{file.Path} {file.PathHash}");
                if (!ContainsFile(file.Path))
                    throw new FormatException("Contains Invalid");
                else if (!_filesByHash[HashFilePath(file.Path)].Any(x => x.Path == file.Path))
                    throw new FormatException("Hash Invalid");
            }
        }

        public void TestLoadFileData(int take)
        {
            foreach (var file in _files.Take(take))
            {
                Log(file.Path);
                LoadFileDataAsync(file).Wait();
            }
        }
    }
}