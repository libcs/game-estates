﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Core.Materials
{
    /// <summary>
    /// An abstract class to describe a material.
    /// </summary>
    public abstract class BaseMaterial
    {
        public float? NormalGeneratorIntensity = 0.75f;

        protected Dictionary<MaterialProps, Material> _existingMaterials;
        protected TextureManager _textureManager;

        public BaseMaterial(TextureManager textureManager)
        {
            _textureManager = textureManager;
            _existingMaterials = new Dictionary<MaterialProps, Material>();
        }

        public abstract Material BuildMaterialFromProperties(MaterialProps mp);
        public abstract Material BuildMaterial();
        public abstract Material BuildMaterialTerrain();
        public abstract Material BuildMaterialBlended(BlendMode sourceBlendMode, BlendMode destinationBlendMode);
        public abstract Material BuildMaterialTested(float cutoff = 0.5f);

        protected static Texture2D GenerateNormalMap(Texture2D source, float strength)
        {
            strength = Mathf.Clamp(strength, 0.0F, 1.0F);
            float xLeft, xRight, yUp, yDown, yDelta, xDelta;
            var normalTexture = new Texture2D(source.width, source.height, TextureFormat.ARGB32, true);
            for (var y = 0; y < normalTexture.height; y++)
                for (var x = 0; x < normalTexture.width; x++)
                {
                    xLeft = source.GetPixel(x - 1, y).grayscale * strength;
                    xRight = source.GetPixel(x + 1, y).grayscale * strength;
                    yUp = source.GetPixel(x, y - 1).grayscale * strength;
                    yDown = source.GetPixel(x, y + 1).grayscale * strength;
                    xDelta = (xLeft - xRight + 1) * 0.5f;
                    yDelta = (yUp - yDown + 1) * 0.5f;
                    normalTexture.SetPixel(x, y, new Color(xDelta, yDelta, 1.0f, yDelta));
                }
            normalTexture.Apply();
            return normalTexture;
        }
    }
}
