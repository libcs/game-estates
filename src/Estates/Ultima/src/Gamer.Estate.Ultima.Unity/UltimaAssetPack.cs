using Gamer.Base;
using Gamer.Estate.Ultima.FilePack;
using Gamer.Estate.Ultima.Format;
using UnityEngine;

namespace Gamer.Estate.Ultima
{
    public class UltimaAssetPack : ResFile, IAssetPack
    {
        TextureManager _textureManager;
        MaterialManager _materialManager;
        SifManager _staManager;

        public UltimaAssetPack()
        {
            _textureManager = new TextureManager(this);
            _materialManager = new MaterialManager(_textureManager);
            _staManager = new SifManager(this, _materialManager);
        }

        public Texture2D LoadTexture(string texturePath, int method = 0) => _textureManager.LoadTexture(texturePath, method);

        public void PreloadTextureAsync(string texturePath) => _textureManager.PreloadTextureFileAsync(texturePath);

        public GameObject CreateObject(string filePath) => _staManager.InstantiateSta(filePath);

        public void PreloadObjectAsync(string filePath) => _staManager.PreloadStaFileAsync(filePath);
    }
}
