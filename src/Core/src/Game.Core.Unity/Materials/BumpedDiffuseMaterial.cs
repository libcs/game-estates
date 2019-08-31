using UnityEngine;
using ur = UnityEngine.Rendering;

namespace Game.Core.Materials
{
    /// <summary>
    /// A material that uses the legacy Bumped Diffuse Shader.
    /// </summary>
    public class BumpedDiffuseMaterial : BaseMaterial
    {
        public BumpedDiffuseMaterial(TextureManager textureManager) : base(textureManager) { }

        public override Material BuildMaterialFromProperties(MaterialProps mp)
        {
            // check if the material is already cached
            if (!_existingMaterials.TryGetValue(mp, out var material))
            {
                // otherwise create a new material and cache it
                if (mp.AlphaBlended) material = BuildMaterialBlended(mp.SrcBlendMode, mp.DstBlendMode);
                else if (mp.AlphaTest) material = BuildMaterialTested(mp.AlphaCutoff);
                else material = BuildMaterial();
                if (mp.Textures.MainFilePath != null)
                {
                    material.mainTexture = _textureManager.LoadTexture(mp.Textures.MainFilePath);
                    if (NormalGeneratorIntensity != null) material.SetTexture("_BumpMap", GenerateNormalMap((Texture2D)material.mainTexture, NormalGeneratorIntensity.Value));
                }
                if (mp.Textures.BumpFilePath != null) material.SetTexture("_BumpMap", _textureManager.LoadTexture(mp.Textures.BumpFilePath));
                _existingMaterials[mp] = material;
            }
            return material;
        }

        public override Material BuildMaterial() => new Material(Shader.Find("Legacy Shaders/Bumped Diffuse"));

        public override Material BuildMaterialTerrain() => new Material(Shader.Find("Nature/Terrain/Diffuse"));

        public override Material BuildMaterialBlended(ur.BlendMode sourceBlendMode, ur.BlendMode destinationBlendMode)
        {
            var material = new Material(Shader.Find("Legacy Shaders/Transparent/Cutout/Bumped Diffuse"));
            material.SetInt("_SrcBlend", (int)sourceBlendMode);
            material.SetInt("_DstBlend", (int)destinationBlendMode);
            return material;
        }

        public override Material BuildMaterialTested(float cutoff = 0.5f)
        {
            var material = new Material(Shader.Find("Legacy Shaders/Transparent/Cutout/Bumped Diffuse"));
            material.SetFloat("_AlphaCutoff", cutoff);
            return material;
        }
    }
}