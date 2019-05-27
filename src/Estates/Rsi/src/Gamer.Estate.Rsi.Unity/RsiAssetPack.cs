using Gamer.Core;
using Gamer.Estate.Rsi.FilePack;
using Gamer.Format.Cry;
using Gamer.Proxy;
using UnityEngine;

namespace Gamer.Estate.Rsi
{
    public class RsiAssetPack : ResFile, IAssetUnityPack
    {
        TextureManager _textureManager;
        MaterialManager _materialManager;
        CryManager _cryManager;

        public RsiAssetPack(ProxySink client, PakFile file) : base(client, file)
        {
            _textureManager = new TextureManager(this);
            _materialManager = new MaterialManager(_textureManager);
            _cryManager = null;
        }

        public Texture2D LoadTexture(string texturePath, int method = 0) => _textureManager.LoadTexture(texturePath, method);

        public void PreloadTextureTask(string texturePath) => _textureManager.PreloadTextureTask(texturePath);

        public GameObject CreateObject(string filePath) => _cryManager.InstantiateObj(filePath);

        public void PreloadObjectTask(string filePath) => _cryManager.PreloadObjectTask(filePath);
    }
}
