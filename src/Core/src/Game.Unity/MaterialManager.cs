using Game.Core.Materials;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Core
{
    /// <summary>
    /// MatTestMode
    /// </summary>
    public enum MatTestMode { Always, Less, LEqual, Equal, GEqual, Greater, NotEqual, Never }

    /// <summary>
    /// MaterialType
    /// </summary>
    public enum MaterialType { None, Default, Standard, BumpedDiffuse, Unlit }

    public struct MaterialTextures
    {
        public string MainFilePath;
        public string DarkFilePath;
        public string DetailFilePath;
        public string GlossFilePath;
        public string GlowFilePath;
        public string BumpFilePath;
    }

    public struct MaterialProps
    {
        public MaterialTextures Textures;
        public bool AlphaBlended;
        public BlendMode SrcBlendMode;
        public BlendMode DstBlendMode;
        public bool AlphaTest;
        public float AlphaCutoff;
        public bool ZWrite;
    }

    /// <summary>
    /// Manages loading and instantiation of materials.
    /// </summary>
    public class MaterialManager
    {
        BaseMaterial _material;

        public TextureManager TextureManager { get; }

        public MaterialManager(TextureManager textureManager)
        {
            TextureManager = textureManager;
            _material = new BumpedDiffuseMaterial(textureManager);
            //switch (MaterialType.Default)
            //{
            //    case MaterialType.None: _material = null; break;
            //    case MaterialType.Default: _material = new DefaultMaterial(textureManager); break;
            //    case MaterialType.Standard: _material = new StandardMaterial(textureManager); break;
            //    case MaterialType.Unlit: _material = new UnliteMaterial(textureManager); break;
            //    default: _material = new BumpedDiffuseMaterial(textureManager); break;
            //}
        }

        public Material BuildMaterialTerrain() => _material.BuildMaterialTerrain();
        public Material BuildMaterialFromProperties(MaterialProps mp) => _material.BuildMaterialFromProperties(mp);
        Material BuildMaterial() => _material.BuildMaterial();
        Material BuildMaterialBlended(BlendMode sourceBlendMode, BlendMode destinationBlendMode) => _material.BuildMaterialBlended(sourceBlendMode, destinationBlendMode);
        Material BuildMaterialTested(float cutoff = 0.5f) => _material.BuildMaterialTested(cutoff);
    }
}