using Gamer.Asset.Nif.Format;
using Gamer.Asset.Tes.FilePack;
using Shared;
using System;
using UnityEngine;

namespace Gamer.Asset.Tes
{
    public class TesAssetPack : BsaMultiFile, IAssetPack
    {
        TextureManager _textureManager;
        MaterialManager _materialManager;
        NifManager _nifManager;

        public TesAssetPack(string filePath) : this(new[] { filePath }) { }
        public TesAssetPack(string[] filePaths) : base(filePaths)
        {
            _textureManager = new TextureManager(this);
            _materialManager = new MaterialManager(_textureManager);
            _nifManager = new NifManager(this, _materialManager, 0);
        }

        public Texture2D LoadTexture(string texturePath, int method = 0) => _textureManager.LoadTexture(texturePath, method);

        public void PreloadTextureAsync(string texturePath) => _textureManager.PreloadTextureFileAsync(texturePath);

        public GameObject CreateObject(string filePath) => _nifManager.InstantiateNif(filePath);

        public void PreloadObjectAsync(string filePath) => _nifManager.PreloadNifFileAsync(filePath);
    }
}
