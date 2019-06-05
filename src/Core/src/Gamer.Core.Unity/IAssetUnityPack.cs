using System.Threading.Tasks;
using UnityEngine;

namespace Gamer.Core
{
    public interface IAssetUnityPack : IAssetPack
    {
        MaterialManager MaterialManager { get; }
        Task<Texture2DInfo> LoadTextureInfoAsync(string texturePath);
        Texture2D LoadTexture(string texturePath, int method = 0);
        void PreloadTextureTask(string texturePath);
        Task<object> LoadObjectInfoAsync(string filePath);
        GameObject CreateObject(string filePath);
        void PreloadObjectTask(string filePath);
    }
}