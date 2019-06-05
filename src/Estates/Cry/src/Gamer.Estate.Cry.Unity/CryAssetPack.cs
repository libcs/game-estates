using Gamer.Estate.Cry.FilePack;
using Gamer.Core;
using System;
using UnityEngine;
using Gamer.Proxy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gamer.Estate.Cry
{
    public class CryAssetPack : ResFile, IAssetUnityPack
    {
        TextureManager _textureManager;
        MaterialManager _materialManager;
        object _nifManager;

        public CryAssetPack(ProxySink client, PakFile file) : base(client, file)
        {
            _textureManager = new TextureManager(this);
            _materialManager = new MaterialManager(_textureManager);
            _nifManager = null;
        }

        public MaterialManager MaterialManager => _materialManager;
        public Texture2D LoadTexture(string texturePath, int method = 0) => _textureManager.LoadTexture(texturePath, method);
        public void PreloadTextureTask(string texturePath) => _textureManager.PreloadTextureTask(texturePath);
        public GameObject CreateObject(string filePath) => throw new NotImplementedException(); //_nifManager.InstantiateNif(filePath);
        public void PreloadObjectTask(string filePath) => throw new NotImplementedException(); //_nifManager.PreloadNifFileAsync(filePath);
        public Task<HashSet<string>> GetContainsSetAsync() => throw new NotImplementedException();
    }
}
