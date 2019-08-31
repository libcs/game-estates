using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static Game.Core.Debug;

namespace Game.Core
{
    public class TextureManager
    {
        readonly IAssetUnityPack _asset;
        readonly Dictionary<string, Task<Texture2DInfo>> _textureFilePreloadTasks = new Dictionary<string, Task<Texture2DInfo>>();
        readonly Dictionary<string, Texture2D> _cachedTextures = new Dictionary<string, Texture2D>();

        public TextureManager(IAssetUnityPack asset) => _asset = asset;

        public Texture2D LoadTexture(string texturePath, int method = 0)
        {
            if (!_cachedTextures.TryGetValue(texturePath, out var texture))
            {
                // Load & cache the texture.
                var textureInfo = LoadTextureInfo(texturePath);
                texture = textureInfo != null ? textureInfo.ToTexture2D() : new Texture2D(1, 1);
                //if (method == 1) TextureUtils.FlipTexture2DVertically(texture);
                //if (method == 2) TextureUtils.RotateTexture2D(texture);
                _cachedTextures[texturePath] = texture;
            }
            return texture;
        }

        public void PreloadTextureTask(string texturePath)
        {
            // If the texture has already been created we don't have to load the file again.
            if (_cachedTextures.ContainsKey(texturePath))
                return;
            // Start loading the texture file asynchronously if we haven't already started.
            if (!_textureFilePreloadTasks.TryGetValue(texturePath, out var textureFileLoadingTask))
                textureFileLoadingTask = _textureFilePreloadTasks[texturePath] = _asset.LoadTextureInfoAsync(texturePath);
        }

        Texture2DInfo LoadTextureInfo(string texturePath)
        {
            Assert(!_cachedTextures.ContainsKey(texturePath));
            PreloadTextureTask(texturePath);
            var textureInfo = _textureFilePreloadTasks[texturePath].Result();
            _textureFilePreloadTasks.Remove(texturePath);
            return textureInfo;
        }
    }
}