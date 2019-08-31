using Game.Core;
using Game.Estate.Cry.FilePack;
using Game.Format.Cry;
using Game.Core.Netstream;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Estate.Cry
{
    public class CryAssetPack : ResFile, IAssetUnityPack
    {
        TextureManager _textureManager;
        MaterialManager _materialManager;
        CryManager _cryManager;

        public CryAssetPack(StreamSink client, IPakFile file) : base(client, file)
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

        // custom
        public async Task<CryAssetPack> GetSocAssetPackAsync(string filePath) => new CryAssetPack(StreamSink.DefaultStreamSink, await LoadSocPackAsync(filePath));
    }
}
