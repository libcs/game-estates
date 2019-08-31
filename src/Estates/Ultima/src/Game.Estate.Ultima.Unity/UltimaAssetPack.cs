using Game.Core;
using Game.Estate.Ultima.FilePack;
using Game.Estate.Ultima.Format;
using Game.Core.Netstream;
using UnityEngine;

namespace Game.Estate.Ultima
{
    public class UltimaAssetPack : ResFile, IAssetUnityPack
    {
        TextureManager _textureManager;
        MaterialManager _materialManager;
        SifManager _staManager;

        public UltimaAssetPack(StreamSink client) : base(client)
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
