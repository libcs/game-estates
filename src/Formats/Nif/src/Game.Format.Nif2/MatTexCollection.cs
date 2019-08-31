/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

using System;
using System.Collections.Generic;

namespace Niflib
{

    /*!
     * A helper class used to gather and create material and texturing information
     * in a NIF version independant way.
     */
    public class MatTexCollection
    {
        public const uint NO_MATERIAL = 0xFFFFFFFF;
        public const uint NO_TEXTURE = 0xFFFFFFFF;

        /*! The vector of materials that this collection holds. */
        List<MaterialWrapper> materials = new List<MaterialWrapper>();
        /*! The vector of textures that this collection holds. */
        List<TextureWrapper> textures = new List<TextureWrapper>();

        /*
         * Constructor which optionally allows you to specify the root of a scene
         * to gather material information from.  This is equivalent to creating the
         * object and then calling the GatherMaterials function as a separate step.
         * \param[in] scene_root The root node of the scene to gather material
         * information from.  If set to null (the default) no information will be
         * gathered.
         */
        public MatTexCollection(NiAVObject scene_root)
        {
            if (scene_root != null)
                GatherMaterials(scene_root);
        }

        /*
         * This function gathers material data from the objects in the scene graph
         * rooted by the given AV object.  This will create associations between
         * the material and texturing properties and the objects that use them,
         * which then can be browsed or searched using the collection's other functions.
         * \param[in] scene_root The root node of the scene to gather material
         * information from.
         */
        public void GatherMaterials(NiAVObject scene_root)
        {
            if (scene_root == null)
                throw new Exception("MatTexCollection::GatherMaterials was called on a null scene root.");
            //Check and see if this object is a geometry object
            if (scene_root.IsDerivedType(NiGeometry.TYPE))
            {
                //Check and see if this geometry's unique combination of material and texture properties has already been found
                if (GetMaterialIndex(scene_root) == NO_MATERIAL)
                {
                    //Material was not already found.  Look for material/texture related properties.
                    var mat = scene_root.GetPropertyByType(NiMaterialProperty.TYPE);
                    var texing = scene_root.GetPropertyByType(NiTexturingProperty.TYPE);
                    var tex = scene_root.GetPropertyByType(NiTextureProperty.TYPE);
                    var multi = scene_root.GetPropertyByType(NiMultiTextureProperty.TYPE);
                    var spec = scene_root.GetPropertyByType(NiSpecularProperty.TYPE);
                    var alpha = scene_root.GetPropertyByType(NiAlphaProperty.TYPE);
                    var stencil = scene_root.GetPropertyByType(NiStencilProperty.TYPE);
                    //Make sure at least one isn't null
                    if (mat != null || texing != null || tex != null || multi != null || stencil != null)
                    {
                        //One isn't null, so create a new Material
                        var matC = mat as NiMaterialProperty;
                        var texingC = texing as NiTexturingProperty;
                        var texC = tex as NiTextureProperty;
                        var multiC = multi as NiMultiTextureProperty;
                        var specC = spec as NiSpecularProperty;
                        var alphaC = alpha as NiAlphaProperty;
                        var stencilC = stencil as NiStencilProperty;
                        //First, check if the material's textures have been found yet
                        if (texingC != null)
                        {
                            for (var i = 0; i < 8; ++i)
                                if (texingC.HasTexture(i))
                                {
                                    var td = texingC.GetTexture(i);
                                    if (td.source != null)
                                    {
                                        var index = GetTextureIndex(td.source);
                                        if (index == NO_TEXTURE)
                                            //Texture has not yet been found.  Create a new one.
                                            textures.Add(new TextureWrapper(td.source));
                                    }
                                }
                        }
                        else if (texC != null)
                        {
                            var image = texC.GetImage();
                            if (image != null)
                            {
                                var index = GetTextureIndex(image);
                                if (index == NO_TEXTURE)
                                    //Texture has not yet been found.  Create a new one.
                                    textures.Add(new TextureWrapper(image));
                            }
                        }
                        //TODO: Implement this for NiMultiTextureProperty as well
                        materials.push_back(new MaterialWrapper(matC, texingC, texC, multiC, specC, alphaC, stencilC, this));
                    }
                }
                //Done with this branch, so return.
                return;
            }

            //If this object is a NiNode, then call this function on its children
            var node = scene_root as NiNode;
            if (node != null)
                var children = node.GetChildren();
            for (var i = 0; i < children.Length; ++i)
                GatherMaterials(children[i]);
        }

        /*!
         * Clears all materials and textures stored in this collection.
         */
        public void Clear()
        {
            materials.clear();
            textures.clear();
        }

        /*
         * Reports the number of materials indexed by this collection.
         * \return The number of materials for which data have been gathered in this
         * collection.
         */
        public uint GetNumMaterials()
        {
            return materials.size();
        }

        /*
         * Retrieves a specific material wrapper by index.  This is a class which
         * allows the attributes of the material and texturing properties to be
         * manipulated.
         * \param[in] index The index of the material wrapper to retrieve.  Must
         * be less than the number reported by GetNumMaterials.
         * \return The material wrapper stored at the specified index.
         */
        public MaterialWrapper GetMaterial(uint index)
        {
            if (index >= materials.Length)
                throw new Exception("The index passed to MatTexCollection::GetMaterial was out of range.");
            return materials[index];
        }

        /*
         * Reports the number of textures indexed by this collection.
         * \return The number of textures for which data have been gathered in this
         * collection.
         */
        public uint GetNumTextures()
        {
            return textures.size();
        }

        /*
         * Retrieves a specific texture wrapper by index.  This is a class which
         * allows the attributes of the texture to be manipulated.
         * \param[in] index The index of the texture wrapper to retrieve.  Must
         * be less than the number reported by GetNumTextures.
         * \return The texture wrapper stored at the specified index.
         */
        public TextureWrapper GetTexture(uint index)
        {
            if (index >= textures.Length)
                throw new Exception("The index passed to MatTexCollection::GetTexture was out of range.");
            return textures[index];
        }

        /*
         * Retrieves the texture index of the texture wrapper that encloses the
         * specified NiSouceTexture, if any.
         * \param[in] src_tex The NiSourceTexture property to match in the search.
         * \return The index of the matching texture, or NO_TEXTURE if a match is
         * not found.
         */
        public uint GetTextureIndex(NiSourceTexture src_tex)
        {
            for (var i = 0; i < textures.Length; ++i)
                if (textures[i].src_tex == src_tex)
                    //Match found, return its index
                    return i;
            //No match was found, return NO_MATERIAL
            return NO_TEXTURE;
        }

        /*
         * Retrieves the texture index of the texture wrapper that encloses the
         * specified NiImage, if any.
         * \param[in] image The NiImage property to match in the search.
         * \return The index of the matching texture, or NO_TEXTURE if a match is
         * not found.
         */
        public uint GetTextureIndex(NiImage image)
        {
            for (var i = 0; i < textures.Length; ++i)
                if (textures[i].image == image)
                    //Match found, return its index
                    return i;
            //No match was found, return NO_MATERIAL
            return NO_TEXTURE;
        }

        /*!
         * Creates a new material and adds it to the end of the array of materials
         * contained in this collection with an internal format matching the version
         * number specified.
         * \param[in] version The NIF version to target when creating the underlying NIF
         * objects that store the texture data.
         * \return The index of the newly created texture.
         */
        public uint CreateTexture(uint version)
        {
            if (version < Nif.VER_3_3_0_13)
            {
                //Old image object style
                var image = new NiImage();
                //Create texture wrapper and add it to the array
                textures.Add(new TextureWrapper(image));
            }
            else
            {
                //New iamge object style
                var src_tex = new NiSourceTexture();
                //Create texture wrapper and add it to the array
                textures.Add(new TextureWrapper(src_tex));
            }
            //Return the index of the newly created texture
            return textures.Length - 1;
        }

        /*
         * Retrieves the material index of the material that affects a specified
         * NiAVObject, if any.
         * \param[in] obj The AV object to find the material index for.
         * \return The index of the material that affects the specified object or
         * NO_MATERIAL if no match is found.
         */
        public uint GetMaterialIndex(NiAVObject obj)
        {
            //Search for a material that matches the properties that the NiAVObject has.
            var properties = obj.GetProperties();
            return GetMaterialIndex(properties);
        }

        /*
         * Retrieves the material index of the material that matches the given list
         * of properties, if any.
         * \param[in] mat The NiMaterialProperty to match.
         * \param[in] texing The NiTexturingProperty to match.
         * \param[in] tex The NiTextureProperty to match.
         * \param[in] multi The NiMultiTextureProperty to match.
         * \return The index of the material that matches the specified properties,
         * or NO_MATERIAL if no match is found.
         */
        public uint GetMaterialIndex(NiMaterialProperty mat, NiTexturingProperty texing, NiTextureProperty tex, NiMultiTextureProperty multi, NiSpecularProperty spec, NiAlphaProperty alpha, NiStencilProperty stencil)
        {
            for (var i = 0; i < materials.Length; ++i)
                if (materials[i].mat_prop == mat &&
                     materials[i].texing_prop == texing &&
                     materials[i].tex_prop == tex &&
                     materials[i].multi_prop == multi &&
                     materials[i].spec_prop == spec &&
                     materials[i].alpha_prop == alpha &&
                     materials[i].stencil_prop == stencil)
                    //Match found, return its index
                    return i;
            //No match was found, return NO_MATERIAL
            return NO_MATERIAL;
        }

        /*
         * Retrieves the material index of the material that matches the given list
         * of properties, if any.
         * \param[in] properties An unsorted list of properties that is thought to contain some related to materials.
         * \return The index of the material that matches the given properties,
         * or NO_MATERIAL if no match is found.
         */
        public uint GetMaterialIndex(NiProperty[] properties)
        {
            //Get Material and Texturing properties, if any
            NiMaterialProperty mat = null;
            NiTexturingProperty texing = null;
            NiTextureProperty tex = null;
            NiMultiTextureProperty multi = null;
            NiSpecularProperty spec = null;
            NiAlphaProperty alpha = null;
            NiStencilProperty stencil = null;
            for (var i = 0; i < properties.Length; ++i)
            {
                if (properties[i] == null)
                    continue;
                if (properties[i].IsDerivedType(NiMaterialProperty.TYPE)) mat = (NiMaterialProperty)properties[i];
                else if (properties[i].IsDerivedType(NiTexturingProperty.TYPE)) texing = (NiTexturingProperty)properties[i];
                else if (properties[i].IsDerivedType(NiTextureProperty.TYPE)) tex = (NiTextureProperty)properties[i];
                else if (properties[i].IsDerivedType(NiMultiTextureProperty.TYPE)) multi = (NiMultiTextureProperty)properties[i];
                else if (properties[i].IsDerivedType(NiSpecularProperty.TYPE)) spec = (NiSpecularProperty)properties[i];
                else if (properties[i].IsDerivedType(NiAlphaProperty.TYPE)) alpha = (NiAlphaProperty)properties[i];
                else if (properties[i].IsDerivedType(NiStencilProperty.TYPE)) stencil = (NiStencilProperty)properties[i];
            }
            //Do the search
            return GetMaterialIndex(mat, texing, tex, multi, spec, alpha, stencil);
        }

        /*
         * Creates a new material and adds it to the end of the array of materials
         * contained in this collection.  The type of material data that will
         * appear in the new material must be specified, and a version number can be
         * used to determine how the data will be stored in the eventual NIF file.
         * Note that the multi_tex option is only a suggestion, as later NIF versions
         * combine the texture and multi-texture data into one NIF object.
         * \param[in] color Whether or not to include color data in the new
         * material.
         * \param[in] texture Whether or not to include base texture data in the
         * new material.
         * \param[in] multi_tex Whether or not to include multi-texture data in the
         * new material.
         * \param[in] multi_tex Whether or not to include multi-texture data in the
         * new material.  This is only a suggestion as some NIF versions cannot
         * separate this from base texture information.
         * \param[in] specular Whether or not to include specular lighting data in
         * the new material.
         * \param[in] translucenty Whether or not to include alpha translucenty
         * data in the new material.
         * \param[in] version The NIF version to target when creating the underlying NIF
         * objects that store the requested types of data.
         * \return The index of the newly created material.
         */
        public uint CreateMaterial(bool color, bool texture, bool multi_tex, bool specular, bool translucency, uint version)
        {
            //Make sure at least one option is set to true
            if (!color && !texture && !multi_tex && !specular && !translucency)
                throw new Exception("At least one of the types of texture/material info needs to be stored in a new material.  All the argumetns to MatTexCollection::CreateMaterial cannot be false.");

            NiMaterialProperty mat = null;
            NiTexturingProperty texing = null;
            NiTextureProperty tex = null;
            NiMultiTextureProperty multi = null;
            NiSpecularProperty spec = null;
            NiAlphaProperty alpha = null;
            NiStencilProperty stencil = null;
            if (color) mat = new NiMaterialProperty();
            if (specular) spec = new NiSpecularProperty();
            if (translucency) alpha = new NiAlphaProperty();
            if (version < Nif.VER_3_3_0_13)
            {
                //Old texturing property style
                if (texture) tex = new NiTextureProperty();
                if (multi_tex) multi = new NiMultiTextureProperty();
            }
            //New texturing property style
            else if (texture) texing = new NiTexturingProperty();

            //Create Material and add it to the array
            materials.Add(new MaterialWrapper(mat, texing, tex, multi, spec, alpha, stencil, this));

            //Return the index of the newly created material
            return materials.Length - 1;
        }
    }

    //MaterialWrapper//////////////////////////////////////////////////////////////
    /*
     * A class that wraps up all the various NIF property objects that affect the
     * color and texture of an object.  It's especially useful to be able to use it
     * to hide the differences between old and new texture properties.
     */
    public class MaterialWrapper
    {
        /*! The NiMaterialProperty that this object wraps, if any. */
        NiMaterialProperty mat_prop;
        /*! The NiTexturingProperty that this object wraps, if any. */
        NiTexturingProperty texing_prop;
        /*! The NiTextureProperty that this object wraps, if any. */
        NiTextureProperty tex_prop;
        /*! The NiMultiTextureProperty that this object wraps, if any. */
        NiMultiTextureProperty multi_prop;
        /*! The NiSpecularProperty that this object wraps, if any. */
        NiSpecularProperty spec_prop;
        /*! The NiAlphaProperty that this object wraps, if any. */
        NiAlphaProperty alpha_prop;
        /*! The NiStencilProperty that this object wraps if any */
        NiStencilProperty stencil_prop;
        /*! A pointer back to the MatTexCollection that created this wrapper */
        MatTexCollection _creator;

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal MaterialWrapper(NiMaterialProperty mat, NiTexturingProperty texing, NiTextureProperty tex, NiMultiTextureProperty multi, NiSpecularProperty spec, NiAlphaProperty alpha, NiStencilProperty stencil, MatTexCollection creator)
        {
            mat_prop = mat;
            texing_prop = texing;
            tex_prop = tex;
            multi_prop = multi;
            spec_prop = spec;
            alpha_prop = alpha;
            stencil_prop = stencil;
            _creator = creator;
        }

        /*!
         *	Applies the material and texture properties controlled by this wrapper
         * to the specified AV object.  Note that properties affect any child
         * objects in the scene graph as well.
         * \param[in] target The AV object to apply the material and texture
         * properties to.
         */
        public void ApplyToObject(NiAVObject target)
        {
            //Add any non-null properties to the target object
            if (mat_prop != null) target.AddProperty(mat_prop);
            if (texing_prop != null) target.AddProperty(texing_prop);
            if (tex_prop != null) target.AddProperty(tex_prop);
            if (multi_prop != null) target.AddProperty(multi_prop);
            if (spec_prop != null) target.AddProperty(spec_prop);
            if (alpha_prop != null) target.AddProperty(alpha_prop);
            if (stencil_prop != null) target.AddProperty(stencil_prop);
        }

        /*!
         * Returns a list of all the properties stored in this material wrapper.
         * \return All the properties controlled by this wrapper.
         */
        public List<NiProperty> GetProperties()
        {
            var properties = new List<NiProperty>();
            NiProperty prop;
            if (mat_prop != null)
            {
                prop = mat_prop;
                properties.Add(prop);
            }
            if (texing_prop != null)
            {
                prop = texing_prop;
                properties.Add(prop);
            }
            if (tex_prop != null)
            {
                prop = tex_prop;
                properties.Add(prop);
            }
            if (multi_prop != null)
            {
                prop = multi_prop;
                properties.Add(prop);
            }
            if (spec_prop != null)
            {
                prop = spec_prop;
                properties.Add(prop);
            }
            if (alpha_prop != null)
            {
                prop = alpha_prop;
                properties.Add(prop);
            }
            if (stencil_prop != null)
            {
                prop = stencil_prop;
                properties.Add(prop);
            }
            return properties;
        }

        //--Color Functions--//

        /*!
         * This function simply returns a reference to the NiMaterialProperty that
         * this wrapper uses to store color information, if any.  Since all
         * supported NIF versions currently use the same material property object,
         * these funtions are not wrapped.
         * \return The material property that this wrapper uses to store color
         * information, or NULL if there is no color information stored.
         */
        public NiMaterialProperty GetColorInfo() => mat_prop;

        /*!
         * This function simply returns a reference to the NiSpecularProperty that
         * this wrapper uses to store specular information, if any.  Since all
         * supported NIF versions currently use the same specular property object,
         * these funtions are not wrapped.
         * \return The specular property that this wrapper uses to specular
         * information, or NULL if there is no specular information stored.
         */
        public NiSpecularProperty GetSpecularInfo() => spec_prop;

        /*!
         * This function simply returns a reference to the NiAlphaProperty that
         * this wrapper uses to store translucency information, if any.  Since all
         * supported NIF versions currently use the same alpha property object,
         * these funtions are not wrapped.
         * \return The alpha property that this wrapper uses to store translucency
         * information, or NULL if there is no translucency information stored.
         */
        public NiAlphaProperty GetTranslucencyInfo() => alpha_prop;

        //--Texturing Functions--//

        public bool HasTexture(TexType slot)
        {
            if (texing_prop != null)
                return texing_prop.HasTexture((int)slot);
            if (tex_prop != null && slot == TexType.BASE_MAP)
                if (tex_prop.GetImage() != null)
                    return true;
            //TODO: Figure out which slots are what in NiMultiTextureProperty so this can be implemented for that too
            //Texture not found
            return false;
        }

        /*
         * Retrieves the texture index of the texture that is used by the specified
         * texture slot.
         * \param[in] slot The type of texture slot to get the texture index of.
         * \return The index of the texture used by the specified material at the
         * given slot, or NO_TEXTURE if no match is found.
         */
        public uint GetTextureIndex(TexType slot)
        {
            if (texing_prop != null)
            {
                if (!texing_prop.HasTexture((int)slot))
                    return MatTexCollection.NO_TEXTURE;
                var td = texing_prop.GetTexture((int)slot);
                if (td.source == null)
                    return MatTexCollection.NO_TEXTURE;
                return _creator.GetTextureIndex(td.source);
            }
            if (tex_prop != null && slot == TexType.BASE_MAP)
            {
                var img = tex_prop.GetImage();
                if (img == null)
                    return MatTexCollection.NO_TEXTURE;
                return _creator.GetTextureIndex(img);
            }
            //TODO: Figure out which slots are what in NiMultiTextureProperty so this can be implemented for that too
            //Texture not found
            return MatTexCollection.NO_TEXTURE;
        }

        /*
         * Sets a new texture to the specified texture slot.  Overwrites any
         * texture that's already there.
         * \param[in] slot The type of texture slot to set a new texture for.
         * \param[in] tex_index The index of the texture to set the specified slot
         * to.  Must be an index from the same MatTexCollection that holds this
         * material wrapper.
         */
        public void SetTextureIndex(TexType slot, uint tex_index)
        {
            //Get the texture from the creator.  This will cause an exception if it fails.
            var tw = _creator.GetTexture(tex_index);
            if (texing_prop != null)
            {
                var td = new TexDesc();
                td.source = tw.src_tex;
                texing_prop.SetTexture((int)slot, td);
            }
            if (tex_prop != null && slot == TexType.BASE_MAP)
                tex_prop.SetImage(tw.image);
            //TODO:  Figure out which slots are what in NiMultiTextureProperty so this can be implemented for that too
        }

        /*
         * Retrieves the UV Set Index of the texture in the specified slot.  This
         * is the index into the array of UV sets in the NiTriBasedGeom object that
         * this material will be applied to.
         * \param[in] slot The type of texture slot to get the UV set index for.
         * \return The UV set index used by the specified texture slot.
         */
        public uint GetTexUVSetIndex(TexType slot)
        {
            if (texing_prop != null)
            {
                if (!texing_prop.HasTexture((int)slot))
                    throw new Exception("The texture at the specified index does not exist.");
                var td = texing_prop.GetTexture((int)slot);
                return td.uvSet;
            }
            //Just return default value for now.  Not sure where this data may or may not be stored in the old style texture properties.
            return 0;
        }

        /*
         * Sets the UV Set Index of the texture in the specified slot.  This
         * is the index into the array of UV sets in the NiTriBasedGeom object that
         * this material will be applied to.
         * \param[in] slot The type of texture slot to set the UV set index for.
         * \param[in] uv_set The new UV set index that will be used by the
         * specified texture slot.
         */
        public void SetTexUVSetIndex(TexType slot, uint uv_set)
        {
            if (texing_prop != null)
            {
                if (!texing_prop.HasTexture((int)slot))
                    throw new Exception("The texture at the specified index does not exist.");
                var td = texing_prop.GetTexture((int)slot);
                td.uvSet = uv_set;
                texing_prop.SetTexture((int)slot, td);
            }
            //Just silently fail for now.  Not sure where this data may or may not be stored in the old style texture properties.
        }

        /*
         * Retrieves the texture clamp mode of the specified texture slot.  This
         * specifies the way that textures will be displayed for UV coordinates
         * that fall outside the 0-1 range.
         * \param[in] slot The type of texture slot to get the clamp mode for.
         * \return The clamp mode of the specified texture slot.
         */
        public TexClampMode GetTexClampMode(TexType slot)
        {
            if (texing_prop != null)
            {
                if (!texing_prop.HasTexture((int)slot))
                    throw new Exception("The texture at the specified index does not exist.");
                var td = texing_prop.GetTexture((int)slot);
                return td.clampMode;
            }
            //Just return default value for now.  Not sure where this data may or may not be stored in the old style texture properties.
            return TexClampMode.WRAP_S_WRAP_T;
        }

        /*
         * Sets the texture clamp mode of the specified texture slot.  This
         * specifies the way that textures will be displayed for UV coordinates
         * that fall outside the 0-1 range.
         * \param[in] slot The type of texture slot to get the clamp mode for.
         * \param[in] mode The new clamp mode for the specified texture slot.
         */
        public void SetTexClampMode(TexType slot, TexClampMode mode)
        {
            if (texing_prop != null)
            {
                if (!texing_prop.HasTexture((int)slot))
                    throw new Exception("The texture at the specified index does not exist.");
                var td = texing_prop.GetTexture((int)slot);
                td.clampMode = mode;
                texing_prop.SetTexture((int)slot, td);
            }
            //Just silently fail for now.  Not sure where this data may or may not be stored in the old style texture properties.
        }

        /*
         * Retrieves the texture filter mode of the specified texture slot.  This
         * specifies the way the texure will look when it's minified or magnified.
         * \param[in] slot The type of texture slot to set the filter mode for.
         * \return The texture filter mode of the specified texture slot.
         */
        public TexFilterMode GetTexFilterMode(TexType slot)
        {
            if (texing_prop != null)
            {
                if (!texing_prop.HasTexture((int)slot))
                    throw new Exception("The texture at the specified index does not exist.");
                var td = texing_prop.GetTexture((int)slot);
                return td.filterMode;
            }
            //Just return default value for now.  Not sure where this data may or may not be stored in the old style texture properties.
            return TexFilterMode.FILTER_BILERP;
        }

        /*
         * Sets the texture filter mode of the specified texture slot.  This
         * specifies the way the texure will look when it's minified or magnified.
         * \param[in] slot The type of texture slot to set the filter mode for.
         * \param[in] mode The new texture filter mode for the specified texture
         * slot.
         */
        public void SetTexFilterMode(TexType slot, TexFilterMode mode)
        {
            if (texing_prop != null)
            {
                if (!texing_prop.HasTexture((int)slot))
                    throw new Exception("The texture at the specified index does not exist.");
                var td = texing_prop.GetTexture((int)slot);
                td.filterMode = mode;
                texing_prop.SetTexture((int)slot, td);
            }
            //Just silently fail for now.  Not sure where this data may or may not be stored in the old style texture properties.
        }
    }

    ///Texture Wrapper/////////////////////////////////////////////////////////////
    /*
     * A class that wraps up the objects that diferent versions of NIF files use to
     * store textures and allows them to me manipulated through the same class.
     */
    public class TextureWrapper
    {
        /*! The NiSourceTexture that this object wraps, if any. */
        NiSourceTexture src_tex;
        /*! The NiImage that this object wraps, if any. */
        NiImage image;

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal TextureWrapper(NiSourceTexture src)
        {
            src_tex = src;
        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal TextureWrapper(NiImage img)
        {
            image = img;
        }

        /*
         * Used to determine whether the texture that this object stores is
         * stored in an external file, rather than being packed into the NIF
         * file itself.
         * \return True if the texture is external, false otherwise.
         */
        public bool IsTextureExternal()
        {
            if (src_tex != null) return src_tex.IsTextureExternal();
            else if (image != null) return image.IsTextureExternal();
            //Texture not found
            else throw new Exception("TextureWrapper holds no data.  This should not be able to happen.");
        }

        /*
         * Retrieves the file name of the external texture.  If the texture is not
         * external, an empty string or the name of the original file may be
         * returned.
         * \return The file name of the external texture, if any.
         */
        public string GetTextureFileName()
        {
            if (src_tex != null) return src_tex.GetTextureFileName();
            else if (image != null) return image.GetTextureFileName();
            //Texture not found
            else throw new Exception("TextureWrapper holds no data.  This should not be able to happen.");
        }

        /*
         * Changes the texture mode to external, if it isn't already, and sets the
         * file name of the new texture location.
         * \param[in] The new file name for the external texture.
         */
        public void SetExternalTexture(string file_name)
        {
            if (src_tex != null) { src_tex.SetExternalTexture(file_name); return; }
            else if (image != null) image.SetExternalTexture(file_name);
            //Texture not found
            else throw new Exception("TextureWrapper holds no data.  This should not be able to happen.");
        }

        /*
         * Gets the pixel layout of the texture.  This describes the image format
         * of the texture, such as palattized, 32-bit, etc.
         * \return The pixel layout of the texture.
         */
        public PixelLayout GetPixelLayout()
        {
            if (src_tex != null) return src_tex.GetPixelLayout();
            //Just return default value for now.  Not sure where this data may or may not be stored in the old style image object.
            return PixelLayout.PIX_LAY_DEFAULT;
        }

        /*
         * Sets the pixel layout of the texture.  This describes the image format
         * of the texture, such as palattized, 32-bit, etc.  Older texture objects
         * don't seem to have this option, so calling this function won't affect
         * them.
         * \param[in] layout The new pixel layout for the texture.
         */
        public void SetPixelLayout(PixelLayout layout)
        {
            if (src_tex != null) src_tex.SetPixelLayout(layout);
            //Just silently fail for now.  Not sure where this data may or may not be stored in the old style image object.
        }

        /*
         * Gets the mipmap format of the texture.  This indicates whether or not
         * the textures will use mipmaps which are smaller versions of the texture
         * that are cached to improve the look of the texture as it is scaled down.
         * \return The mipmap format of the texture.
         */
        public MipMapFormat GetMipMapFormat()
        {
            if (src_tex != null) return src_tex.GetMipMapFormat();
            //Just return default value for now.  Not sure where this data may or may not be stored in the old style image object.
            return MipMapFormat.MIP_FMT_DEFAULT;
        }

        /*
         * Sets the mipmap format of the texture.  This indicates whether or not
         * the textures will use mipmaps which are smaller versions of the texture
         * that are cached to improve the look of the texture as it is scaled down.
         * Older texture objects don't seem to have this option, so calling this
         * function won't affect them.
         * \param[in] format The new mipmap format for the texture.
         */
        public void SetMipMapFormat(MipMapFormat format)
        {
            if (src_tex != null) src_tex.SetMipMapFormat(format);
            //Just silently fail for now.  Not sure where this data may or may not be stored in the old style image objects.
        }

        /*
         * Gets the alpha format of the texture.  This indicates the type of alpha
         * information the texture will contain, such as none, binary, or smooth.
         * \return The alpha format of the texture.
         */
        public AlphaFormat GetAlphaFormat()
        {
            if (src_tex != null) return src_tex.GetAlphaFormat();
            //Just return default value for now.  Not sure where this data may or may not be stored in the old style image object.
            return AlphaFormat.ALPHA_DEFAULT;
        }

        /*
         * Sets the alpha format of the texture.  This indicates the type of alpha
         * information the texture will contain, such as none, binary, or smooth.
         * Older texture objects don't seem to have this option, so calling this
         * function won't affect them.
         * \param[in] format The new alpha format for the texture.
         */
        public void SetAlphaFormat(AlphaFormat format)
        {
            if (src_tex != null) src_tex.SetAlphaFormat(format);
            //Just silently fail for now.  Not sure where this data may or may not be stored in the old style image objects.
        }

        /*
         * Retrieves the generic name property of the texture object, if it has one.
         * \return The NiObjectNET name of the texture object, if any.
         */
        public string GetObjectName()
        {
            if (src_tex != null) return src_tex.GetName();
            //NiImage objects don't seem to have a name since they derive from NiObject.  Return an empty string.
            return string.Empty;
        }

        /*
         * Sets the generic name property of the texture object, if it has one.  If
         * the object doesn't have the name parameter, this function will do
         * nothing.
         * \param[in] The new NiObjectNET name for the texture object.
         */
        public void SetObjectName(string name)
        {
            if (src_tex != null) src_tex.SetName(name);
            //NiImage objects don't seem to have a name since they derive from NiObject.  Do nothing.
        }
    }
}
