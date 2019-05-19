using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Gamer.Core
{
    public interface IAssetPack : IDisposable
    {
        bool ContainsFile(string filePath);
        Task<byte[]> LoadFileDataAsync(string filePath);
    }
}