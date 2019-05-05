using Gamer.Estate.Rsi.FilePack;
using Gamer.Core;
using System;
using UnityEngine;

namespace Gamer.Estate.Rsi
{
    public class RsiAssetPack : ResFile, IAssetPack
    {
        TextureManager _textureManager;
        MaterialManager _materialManager;
        object _nifManager;

        public RsiAssetPack(PakFile file) : base(file)
        {
            _textureManager = new TextureManager(this);
            _materialManager = new MaterialManager(_textureManager);
            _nifManager = null;
        }

        public Texture2D LoadTexture(string texturePath, int method = 0) => _textureManager.LoadTexture(texturePath, method);

        public void PreloadTextureAsync(string texturePath) => _textureManager.PreloadTextureFileAsync(texturePath);

        public GameObject CreateObject(string filePath) => throw new NotImplementedException(); //_nifManager.InstantiateNif(filePath);

        public void PreloadObjectAsync(string filePath) => throw new NotImplementedException(); //_nifManager.PreloadNifFileAsync(filePath);
    }
}
