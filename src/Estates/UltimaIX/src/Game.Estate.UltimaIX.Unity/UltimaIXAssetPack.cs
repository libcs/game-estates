using Game.Core;
using Game.Estate.UltimaIX.FilePack;
using Game.Core.Netstream;
using UnityEngine;
using Game.Estate.UltimaIX.Format;

namespace Game.Estate.UltimaIX
{
    public class UltimaIXAssetPack : ResMultiFile, IAssetUnityPack
    {
        TextureManager _textureManager;
        MaterialManager _materialManager;
        SifManager _sifManager;

        public UltimaIXAssetPack(StreamSink streamSink, string[] filePaths) : base(streamSink, filePaths)
        {
            _textureManager = new TextureManager(this);
            _materialManager = new MaterialManager(_textureManager);
            _sifManager = new SifManager(this, _materialManager, 0);
        }

        public MaterialManager MaterialManager => _materialManager;
        public Texture2D LoadTexture(string texturePath, int method = 0) => _textureManager.LoadTexture(texturePath, method);
        public void PreloadTextureTask(string texturePath) => _textureManager.PreloadTextureTask(texturePath);
        public GameObject CreateObject(string filePath) => _sifManager.CreateObject(filePath);
        public void PreloadObjectTask(string filePath) => _sifManager.PreloadObjectTask(filePath);
    }
}
