using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gamer.Core
{
    public interface IAssetPack : IDisposable
    {
        HashSet<string> GetContainsSet();
        bool ContainsFile(string filePath);
        Task<byte[]> LoadFileDataAsync(string filePath);
    }
}