using Gamer.Core;
using Gamer.Estate.Cry.FilePack;
using Gamer.Format.Cry;
using Gamer.Proxy;
using UnityEngine;

namespace Gamer.Estate.Cry
{
    public class CryAssetPack : ResFile, IAssetUnityPack
    {
        TextureManager _textureManager;
        MaterialManager _materialManager;
        CryManager _cryManager;

        public CryAssetPack(ProxySink client, PakFile file) : base(client, file)
        {
            _textureManager = new TextureManager(this);
            _materialManager = new MaterialManager(_textureManager);
            _cryManager = new CryManager(this, _materialManager, 0);
        }

        public MaterialManager MaterialManager => _materialManager;
        public Texture2D LoadTexture(string texturePath, int method = 0) => _textureManager.LoadTexture(texturePath, method);
        public void PreloadTextureTask(string texturePath) => _textureManager.PreloadTextureTask(texturePath);
        public GameObject CreateObject(string filePath) => _cryManager.InstantiateObj(filePath);
        public void PreloadObjectTask(string filePath) => _cryManager.PreloadObjectTask(filePath);
    }
}
