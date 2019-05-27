using Gamer.Core;
using Gamer.Format.Nif;
using Gamer.Estate.Tes.FilePack;
using UnityEngine;
using Gamer.Proxy;

namespace Gamer.Estate.Tes
{
    public class TesAssetPack : BsaMultiFile, IAssetUnityPack
    {
        TextureManager _textureManager;
        MaterialManager _materialManager;
        NifManager _nifManager;

        public TesAssetPack(ProxySink proxySink, string[] filePaths) : base(proxySink, filePaths)
        {
            _textureManager = new TextureManager(this);
            _materialManager = new MaterialManager(_textureManager);
            _nifManager = new NifManager(this, _materialManager, 0);
        }

        public Texture2D LoadTexture(string texturePath, int method = 0) => _textureManager.LoadTexture(texturePath, method);
        public void PreloadTextureTask(string texturePath) => _textureManager.PreloadTextureTask(texturePath);
        public GameObject CreateObject(string filePath) => _nifManager.CreateObject(filePath);
        public void PreloadObjectTask(string filePath) => _nifManager.PreloadObjectTask(filePath);
    }
}
