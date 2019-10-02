using Game.Core;
using Game.Estate.UltimaIX.FilePack;
using Game.Core.Netstream;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Estate.UltimaIX
{
    public class UltimaIXAssetPack : ResFile, IAssetUnityPack
    {
        TextureManager _textureManager;
        MaterialManager _materialManager;
        //UltimaIXManager _ultimaIXManager;

        public UltimaIXAssetPack(StreamSink client, IFlxFile flxFile, IIdxFile idxFile) : base(client, flxFile, idxFile)
        {
            _textureManager = new TextureManager(this);
            _materialManager = new MaterialManager(_textureManager);
            //_ultimaIXManager = new UltimaIXManager(this, _materialManager, 0);
        }

        public MaterialManager MaterialManager => _materialManager;
        public Texture2D LoadTexture(string texturePath, int method = 0) => _textureManager.LoadTexture(texturePath, method);
        public void PreloadTextureTask(string texturePath) => _textureManager.PreloadTextureTask(texturePath);
        public GameObject CreateObject(string filePath) => null; //_ultimaIXManager.InstantiateObj(filePath);
        public void PreloadObjectTask(string filePath) { } // => _ultimaIXManager.PreloadObjectTask(filePath);
    }
}
