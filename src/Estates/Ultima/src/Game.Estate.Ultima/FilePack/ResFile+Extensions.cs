﻿using Game.Core;
using Game.Estate.Ultima.Format;
using System;
using System.Threading.Tasks;

namespace Game.Estate.Ultima.FilePack
{
    partial class ResFile
    {
        public Task<Texture2DInfo> LoadTextureInfoAsync(string texturePath)
        {
            Texture2DInfo texture = null;
            switch (texturePath.Substring(0, 3))
            {
                case "lnd": texture = _artmulResource.GetLandTexture(int.Parse(texturePath.Substring(3))); break;
                case "sta": texture = _artmulResource.GetStaticTexture(int.Parse(texturePath.Substring(3))); break;
                case "gmp": texture = _gumpMulResource.GetGumpTexture(int.Parse(texturePath.Substring(3))); break;
                case "tex": texture = _texmapResource.GetTexmapTexture(int.Parse(texturePath.Substring(3))); break;
                //case "ani": texture = _animationResource.GetAnimation(int.Parse(texturePath.Substring(3))); break;
                //case "fnt": texture = _fontsResource.GetAsciiFont(int.Parse(texturePath.Substring(3))); break;
                default: throw new ArgumentOutOfRangeException(nameof(texturePath), texturePath);
            }
            return Task.FromResult(texture);
        }

        public Task<object> LoadObjectInfoAsync(string filePath) => Task.Run(() =>
        {
            var file = new SiFile(this, filePath);
            return (object)file;
        });

        internal void GetStaticDimensions(short index, out int width, out int height)
        {
            var texture = _artmulResource.GetStaticTexture(index);
            if (texture == null)
                throw new IndexOutOfRangeException("texture not found");
            width = texture.Width;
            height = texture.Height;
            //_artmulResource.GetStaticDimensions(index, out width, out height);
        }

        internal void GetGumpDimensions(short index, out int width, out int height)
        {
            var texture = _gumpMulResource.GetGumpTexture(index);
            if (texture == null)
                throw new IndexOutOfRangeException("texture not found");
            width = texture.Width;
            height = texture.Height;
        }
    }
}