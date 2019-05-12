using Gamer.Core;
using Gamer.Estate.Rsi.FilePack;
using Gamer.Format.Cry;
using UnityEngine;

namespace Gamer.Estate.Rsi
{
    public class RsiAssetPack : ResFile, IAssetPack
    {
        TextureManager _textureManager;
        MaterialManager _materialManager;
        CryManager _cryManager;

        public RsiAssetPack(PakFile file) : base(file)
        {
            _textureManager = new TextureManager(this);
            _materialManager = new MaterialManager(_textureManager);
            _cryManager = null;
        }

        public Texture2D LoadTexture(string texturePath, int method = 0) => _textureManager.LoadTexture(texturePath, method);

        public void PreloadTextureAsync(string texturePath) => _textureManager.PreloadTextureFileAsync(texturePath);

        public GameObject CreateObject(string filePath) => _cryManager.InstantiateObj(filePath);

        public void PreloadObjectAsync(string filePath) => _cryManager.PreloadObjFileAsync(filePath);
    }
}
