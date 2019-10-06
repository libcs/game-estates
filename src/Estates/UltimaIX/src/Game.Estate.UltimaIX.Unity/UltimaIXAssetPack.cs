using Game.Core;
using Game.Estate.UltimaIX.FilePack;
using Game.Core.Netstream;
using UnityEngine;

namespace Game.Estate.UltimaIX
{
    public class UltimaIXAssetPack : ResMultiFile, IAssetUnityPack
    {
        TextureManager _textureManager;
        MaterialManager _materialManager;
        //UifManager _uifManager;

        public UltimaIXAssetPack(StreamSink client, string[] filePaths) : base(client, filePaths)
        {
            _textureManager = new TextureManager(this);
            _materialManager = new MaterialManager(_textureManager);
            //_uifManager = new UifManager(this, _materialManager, 0);
        }

        public MaterialManager MaterialManager => _materialManager;
        public Texture2D LoadTexture(string texturePath, int method = 0) => _textureManager.LoadTexture(texturePath, method);
        public void PreloadTextureTask(string texturePath) => _textureManager.PreloadTextureTask(texturePath);
        public GameObject CreateObject(string filePath) => null; //_uifManager.InstantiateObj(filePath);
        public void PreloadObjectTask(string filePath) { } // => _uifManager.PreloadObjectTask(filePath);
    }
}
