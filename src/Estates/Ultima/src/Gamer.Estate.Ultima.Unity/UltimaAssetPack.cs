using Gamer.Core;
using Gamer.Estate.Ultima.FilePack;
using Gamer.Estate.Ultima.Format;
using Gamer.Proxy;
using UnityEngine;

namespace Gamer.Estate.Ultima
{
    public class UltimaAssetPack : ResFile, IAssetUnityPack
    {
        TextureManager _textureManager;
        MaterialManager _materialManager;
        SifManager _staManager;

        public UltimaAssetPack(ProxySink client) : base(client)
        {
            _textureManager = new TextureManager(this);
            _materialManager = new MaterialManager(_textureManager);
            _staManager = new SifManager(this, _materialManager);
        }

        public MaterialManager MaterialManager => _materialManager;
        public Texture2D LoadTexture(string texturePath, int method = 0) => _textureManager.LoadTexture(texturePath, method);
        public void PreloadTextureTask(string texturePath) => _textureManager.PreloadTextureTask(texturePath);
        public GameObject CreateObject(string filePath) => _staManager.CreateObject(filePath);
        public void PreloadObjectTask(string filePath) => _staManager.PreloadObjectTask(filePath);
    }
}
