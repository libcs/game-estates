using Shared.Core;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Shared
{
    public interface IAssetPack : IDisposable
    {
        Task<Texture2DInfo> LoadTextureInfoAsync(string texturePath);
        Texture2D LoadTexture(string texturePath, int method = 0);
        void PreloadTextureAsync(string texturePath);
        Task<object> LoadObjectInfoAsync(string filePath);
        GameObject CreateObject(string filePath);
        void PreloadObjectAsync(string filePath);
    }
}