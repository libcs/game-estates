/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */


using System;
using System.Collections.Generic;

namespace Niflib
{
    /*!
     * Used by the ComplexShape::WeightedVertex strut to store a single
     * skin-weight/bone influence combination for a vertex.
     */
    public class SkinInfluence
    {
        /*! 
         * Index into the ComplexShape::skinInfluences array of the bone
         * influence for this skin weight.
         */
        public uint influenceIndex = ComplexShape.CS_NO_INDEX;
        /*! 
         * The amount of influence the indexed bone has on this vertex, between
         * 0.0 and 1.0
         */
        public float weight;
    }

    /*!
     * Used by the ComplexShape class to store a single vertex and any
     * Associated skin weights
     */
    public class WeightedVertex
    {
        /*! The 3D position of this vertex. */
        public Vector3 position;
        /*! A list of weight/influence index pairs for this vertex. */
        public List<SkinInfluence> weights;
    }

    /*!
     * Used by the ComplexShape::ComplexPoint struct to store a single texture
     * cooridinate set/texture coordinate pair of indices.
     */
    public class TexCoordIndex
    {
        /*!
         * Index into the ComplexShape::texCoordSets array of texture
         * coordinate sets.
         */
        public uint texCoordSetIndex = ComplexShape.CS_NO_INDEX;

        /*!
         * Index into the ComplexShape::TexCoordSet::texCoords array of the
         * texture coordinate set referenced by texCoordSetIndex.
         */
        public uint texCoordIndex = ComplexShape.CS_NO_INDEX;
    }

    /*!
     * Used by ComplexShape::ComplexFace class to describe a single point in
     * the 3D model.  Points share their data in case of duplication, so all
     * information, such as position, normal vector, texture coordinates, etc.,
     * are stored as indices into the asociated data arrays.
     */
    public class ComplexPoint
    {
        /*! 
         * Index into the ComplexShape::vertices array which stores the
         * position and any associated skin weights for this point.
         */
        public uint vertexIndex = ComplexShape.CS_NO_INDEX;
        /*! 
         * Index into the ComplexShape::normals array which stores the normal
         * vector for this point.
         */
        public uint normalIndex = ComplexShape.CS_NO_INDEX;
        /*!
         * Index into the ComplexShape::colors array which stores the vertex
         * color for this point
         */
        public uint colorIndex = ComplexShape.CS_NO_INDEX;
        /*!
         * An array of texture coordinate set/texture coordinate index pairs
         * describing all the UV coordinates for this point.
         */
        public List<TexCoordIndex> texCoordIndices;
    }

    /*! 
     * Used by ComplexShape to describe a single polygon.  Complex shape
     * polygons can have more than three points, unlike the triangels required
     * within the NIF format.  Each face may also be associated with a
     * different set of NiProperty classes, enabling each face to have unique
     * material settings.
     */
    public class ComplexFace
    {
        /*! A list of points which make up this polygon */
        public ComplexPoint[] points;
        /*!
         * Index into the ComplexShape::propGroups array which specifies which
         * set of NiProperty classes to apply to this face.
         */
        public uint propGroupIndex = ComplexShape.CS_NO_INDEX;
    }

    /*!
     * Used by ComplexShape to store texture coordinate data and the
     * associated type of texture, such as base, detail, or dark map.
     */
    public class TexCoordSet
    {
        /*!
         * The type of the texture such as base, detail, bump, etc.
         */
        public TexType texType;
        /*!
         * A list of all the texture cooridnates for this texture set.
         */
        public List<TexCoord> texCoords;
    }

    /*!
     * This class is a helper object to ease the process of converting between the
     * 3D model format of a NIF file, which is optimized for real time display via
     * OpenGL or DirectX, and the more compact, complex format usually prefered by
     * 3D modeling software.
     * 
     * It is capable of mering multiple NiTriShape objects into one multi-material
     * object with indexed data, or taking such an object and splitting it up into
     * multiple NiTriShape objects.
     */
    public class ComplexShape
    {
        /*! Marks empty data indices */
        public const uint CS_NO_INDEX = 0xFFFFFFFF;

        /*!
         * This function splits the contents of the ComplexShape into multiple
         * NiTriBasedGeom objects.
         * \param parent The parent NiNode that the resulting NiTriBasedGeom
         * objects will be attached to.
         * \param transform The transform for the resulting object or group of
         * objects
         * \param max_bones_per_partition The maximum number of bones to allow in
         * each skin partition.  Set to zero to skip creation of partition.
         * \param stripify Whether or not to generate efficient triangle strips.
         * \param tangent_space Whether or not to generate Oblivion tangent space
         * information.
         * \param min_vertex_weight Remove vertex weights bellow a given value
         * \param use_dismember_partitions Uses BSDismemberSkinInstance with custom partitions for dismember
         * \return A reference to the root NiAVObject that was created.
         */
        public NiAVObject Split(NiNode parent, Matrix44 transform, int max_bones_per_partition = 4, bool stripify = false, bool tangent_space = false, float min_vertex_weight = 0.001f, byte tspace_flags = 0)
        {
            //Make sure parent is not NULL
            if (parent == null)
                throw new Exception("A parent is necessary to split a complex shape.");

            var use_dismember_partitions = false;
            if (DismemberPartitionsFaces.Count > 0)
            {
                if (DismemberPartitionsFaces.Count != Faces.Count)
                    throw new Exception("The number of faces mapped to skin partitions is different from the actual face count.");
                if (DismemberPartitionsBodyParts.Count == 0)
                    throw new Exception("The number of dismember partition body parts can't be 0.");
                use_dismember_partitions = true;
            }

            //There will be one NiTriShape per property group
            //with a minimum of 1
            var num_shapes = PropGroups.Length;
            if (num_shapes == 0)
                num_shapes = 1;

            var shapes = new NiTriBasedGeom[num_shapes];
            //Loop through each shape slot and create a NiTriShape
            for (var shape_num = 0; shape_num < shapes.Length; ++shape_num)
                shapes[shape_num] = stripify ? (NiTriBasedGeom)new NiTriStrips() : new NiTriShape();

            NiAVObject root;
            //If there is just one shape, return it.  Otherwise
            //create a node, parent all shapes to it, and return
            // that
            if (shapes.Length == 1)
            {
                //One shape
                shapes[0].name = Name;
                root = shapes[0];
            }
            else
            {
                //Multiple shapes
                var niNode = new NiNode();
                niNode.name = Name;
                for (var i = 0; i < shapes.Length; ++i)
                {
                    niNode.AddChild(shapes[i]);
                    //Set Shape Name
                    shapes[i].name = $"{Name} {i}";
                }
                root = niNode;
            }
            parent.AddChild(root);

            //Set transform of root
            root.SetLocalTransform(transform);

            //Create NiTriShapeData and fill it out with all data that is relevant
            //to this shape based on the material.
            for (var shape_num = 0; shape_num < shapes.Length; ++shape_num)
            {
                var niData = stripify ? (NiTriBasedGeomData)new NiTriStripsData() : new NiTriShapeData();
                shapes[shape_num].Data = (NiGeometryData)niData;

                //Create a list of CompoundVertex to make it easier to
                //test for the need to clone a vertex
                var compVerts = new List<CompoundVertex>();

                //List of triangles for the final shape to use
                var shapeTriangles = new List<Triangle>();

                //a vector that holds in what dismember groups or skin partition does each face belong
                var current_dismember_partitions = DismemberPartitionsBodyParts;

                //create a map betweem the faces and the dismember groups
                var current_dismember_partitions_faces = new List<uint>();

                //since we might have dismember partitions the face index is also required
                var current_face_index = 0;

                //Loop through all faces, and all points on each face
                //to set the vertices in the CompoundVertex list
                foreach (var face in Faces)
                {
                    //Ignore faces with less than 3 vertices
                    if (face.points.Count < 3)
                        continue;

                    //Skip this face if the material does not relate to this shape
                    if (face.propGroupIndex != shape_num)
                        continue;

                    var shapeFacePoints = new List<ushort>();
                    foreach (var point in face.points)
                    {
                        //--Set up Compound vertex--//
                        var cv = new CompoundVertex();
                        if (Vertices.Count > 0)
                        {
                            var wv = Vertices[(int)point.vertexIndex];
                            cv.position = wv.position;
                            if (SkinInfluences.Count > 0)
                                for (var i = 0; i < wv.weights.Count; ++i)
                                {
                                    var inf = wv.weights[i];
                                    cv.weights[SkinInfluences[(int)inf.influenceIndex]] = inf.weight;
                                }
                        }
                        if (Normals.Count > 0)
                            cv.normal = Normals[(int)point.normalIndex];
                        if (Colors.Count > 0)
                            cv.color = Colors[(int)point.colorIndex];
                        if (TexCoordSets.Count > 0)
                            for (var i = 0; i < point.texCoordIndices.Count; ++i)
                            {
                                var set = TexCoordSets[(int)point.texCoordIndices[i].texCoordSetIndex];
                                cv.texCoords[set.texType] = set.texCoords[(int)point.texCoordIndices[i].texCoordIndex];
                            }

                        var found_match = false;
                        //Search for an identical vertex in the list
                        for (var cv_index = 0; cv_index < compVerts.Count; ++cv_index)
                            if (compVerts[cv_index] == cv)
                            {
                                //We found a match, push its index into the face list
                                found_match = true;
                                shapeFacePoints.Add((ushort)cv_index);
                                break;
                            }

                        //If no match was found, append this vertex to the list
                        if (!found_match)
                        {
                            compVerts.Add(cv);
                            //put the new vertex into the face point list
                            shapeFacePoints.Add((ushort)(compVerts.Count - 1));
                        }
                        //Next Point
                    }

                    if (!use_dismember_partitions)
                    {
                        //Starting from vertex 0, create a fan of triangles to fill
                        //in non-triangle polygons
                        var new_face = new Triangle();
                        for (var i = 0; i < shapeFacePoints.Count - 2; ++i)
                        {
                            new_face[0] = shapeFacePoints[0];
                            new_face[1] = shapeFacePoints[i + 1];
                            new_face[2] = shapeFacePoints[i + 2];
                            //Push the face into the face list
                            shapeTriangles.Add(new_face);
                        }
                        //Next Face
                    }
                    else
                    {
                        //Starting from vertex 0, create a fan of triangles to fill
                        //in non-triangle polygons
                        var new_face = new Triangle();
                        for (var i = 0; i < shapeFacePoints.Count - 2; ++i)
                        {
                            new_face[0] = shapeFacePoints[0];
                            new_face[1] = shapeFacePoints[i + 1];
                            new_face[2] = shapeFacePoints[i + 2];

                            //Push the face into the face list
                            shapeTriangles.Add(new_face);

                            //all the resulting triangles belong in the the same dismember partition or better said skin partition
                            current_dismember_partitions_faces.Add(DismemberPartitionsFaces[current_face_index]);
                        }
                    }
                    current_face_index++;
                }

                //Clean up the dismember skin partitions
                //if no face points to a certain dismember partition then that dismember partition must be removed
                if (use_dismember_partitions)
                {
                    var used_dismember_groups = new List<bool>(current_dismember_partitions.Count); //: (, false);
                    for (var x = 0; x < current_dismember_partitions_faces.Count; x++)
                        if (!used_dismember_groups[(int)current_dismember_partitions_faces[x]])
                            used_dismember_groups[(int)current_dismember_partitions_faces[x]] = true;
                    var cleaned_up_dismember_partitions = new List<BodyPartList>();
                    for (var x = 0; x < current_dismember_partitions.Count; x++)
                        if (!used_dismember_groups[x])
                        {
                            for (var y = 0; y < current_dismember_partitions_faces.Count; y++)
                                if (current_dismember_partitions_faces[y] > x)
                                    current_dismember_partitions_faces[y]--;
                        }
                        else
                            cleaned_up_dismember_partitions.Add(current_dismember_partitions[x]);
                    current_dismember_partitions = cleaned_up_dismember_partitions;
                }

                //Attatch properties if any
                //Check if the properties are skyrim specific in which case attach them in the 2 special slots called bs_properties
                if (PropGroups.Length > 0)
                {
                    BSLightingShaderProperty shader_property = null;
                    foreach (var prop in PropGroups[shape_num])
                    {
                        var current_property = prop;
                        if (current_property.GetType().IsSameType(BSLightingShaderProperty.TYPE))
                        {
                            shader_property = (BSLightingShaderProperty)current_property;
                            break;
                        }
                    }

                    if (shader_property == null)
                        foreach (var prop in PropGroups)
                            shapes[shape_num].AddProperty(prop);
                    else
                    {
                        NiAlphaProperty alpha_property = null;
                        foreach (var prop in PropGroups[shape_num])
                            if (prop.GetType().IsSameType(NiAlphaProperty.TYPE))
                                alpha_property = (NiAlphaProperty)prop;
                        var bs_properties = new NiProperty[2];
                        bs_properties[0] = shader_property;
                        bs_properties[1] = alpha_property;
                        shapes[shape_num].BSProperties = bs_properties;
                    }
                }

                //--Set Shape Data--//

                //lists to hold data
                var shapeVerts = new Vector3[compVerts.Count];
                var shapeNorms = new Vector3[compVerts.Count];
                var shapeColors = new Color4[compVerts.Count];
                var shapeTCs = new TexCoord[][] { };
                var shapeTexCoordSets = new List<int>();
                Dictionary<NiNode, List<BoneVertData>> shapeWeights;

                //Search for a NiTexturingProperty to build list of
                //texture coordinates sets to create
                var niProp = shapes[shape_num].GetPropertyByType(NiTexturingProperty.TYPE);
                NiTexturingProperty niTexProp = null;
                if (niProp != null)
                    niTexProp = (NiTexturingProperty)niProp;
                if (niTexProp != null)
                {
                    for (var tex_num = 0; tex_num < 8; ++tex_num)
                        if (niTexProp.HasTexture(tex_num))
                        {
                            shapeTexCoordSets.Add(tex_num);
                            var td = niTexProp.GetTexture(tex_num);
                            td.uvSet = (int)shapeTexCoordSets.Count - 1;
                            niTexProp.SetTexture(tex_num, td);
                        }
                }
                //Always include the base map if it's there, whether there's a texture or not
                else shapeTexCoordSets.Add(BASE_MAP);
                Array.Resize(ref shapeTCs, shapeTexCoordSets.Count);
                for (var set = 0; set < shapeTCs.Length; set++)
                    Array.Resize(ref shapeTCs[set], compVerts.Count);

                //Loop through all compound vertices, adding the data
                //to the correct arrays.
                uint vert_index = 0;
                foreach (var cv in compVerts)
                {
                    shapeVerts[vert_index] = cv.position;
                    shapeColors[vert_index] = cv.color;
                    shapeNorms[vert_index] = cv.normal;
                    shapeNorms[vert_index] = cv.normal;
                    uint tex_index = 0;
                    foreach (var tex in shapeTexCoordSets)
                    {
                        if (cv.texCoords.find(TexType(tex)) != cv.texCoords.end())
                            shapeTCs[tex_index][vert_index] = cv->texCoords[TexType(tex)];
                        tex_index++;
                    }
                    BoneVertData sk;
                    foreach (var wt in weights)
                    {
                        //Only record influences that make a noticable contribution
                        if (wt.second > min_vertex_weight)
                        {
                            sk.index = vert_index;
                            sk.weight = wt->second;
                            if (shapeWeights.find(wt.first) == shapeWeights.end())
                                shapeWeights[wt->first] = new List<BoneVertData>();
                            shapeWeights[wt->first].Add(sk);
                        }
                    }
                    ++vert_index;
                }

                //Finally, set the data into the NiTriShapeData
                if (Vertices.Count > 0)
                {
                    niData.SetVertices(shapeVerts);
                    niData.SetTriangles(shapeTriangles);
                }
                if (Normals.Count > 0)
                    niData.SetNormals(shapeNorms);
                if (Colors.Count > 0)
                    niData.SetVertexColors(shapeColors);
                if (TexCoordSets.Count > 0)
                {
                    niData.SetUVSetCount((int)shapeTCs.Length);
                    for (var tex_index = 0; tex_index < shapeTCs.Length; ++tex_index)
                        niData.SetUVSet(tex_index, shapeTCs[tex_index]);
                }

                //If there are any skin influences, bind the skin
                if (shapeWeights.Count > 0)
                {
                    var shapeInfluences = new List<NiNode>();
                    foreach (var inf in shapeWeights)
                        shapeInfluences.Add(inf.Key);

                    if (!use_dismember_partitions)
                        shapes[shape_num].BindSkin(shapeInfluences);
                    else
                    {
                        shapes[shape_num].BindSkinWith(shapeInfluences, BSDismemberSkinInstance.Create);
                        var dismember_skin = (BSDismemberSkinInstance)shapes[shape_num].GetSkinInstance();
                        dismember_skin.SetPartitions(current_dismember_partitions);
                    }

                    for (var inf = 0; inf < shapeInfluences.Count; ++inf)
                        shapes[shape_num].SetBoneWeights(inf, shapeWeights[shapeInfluences[inf]]);

                    shapes[shape_num].NormalizeSkinWeights();

                    if (use_dismember_partitions)
                    {
                        var face_map = new int[current_dismember_partitions_faces.Count];
                        for (var x = 0; x < current_dismember_partitions_faces.Count; x++)
                            face_map[x] = current_dismember_partitions_faces[x];
                        shapes[shape_num].GenHardwareSkinInfo(max_bones_per_partition, 4, stripify, face_map);

                        var dismember_skin = (BSDismemberSkinInstance)shapes[shape_num].GetSkinInstance();
                        dismember_skin.SetPartitions(current_dismember_partitions);
                    }
                    else if (max_bones_per_partition > 0)
                        shapes[shape_num].GenHardwareSkinInfo(max_bones_per_partition, 4, stripify);

                    //var skinInst = shapes[shape_num].GetSkinInstance();
                    //if (skinInst != null) {
                    //	var skinData = skinInst.GetSkinData();
                    //	if (skinData != null)
                    //		for (var inf = 0; inf < shapeInfluences.Count; ++inf)
                    //			skinData.SetBoneWeights(inf, shapeWeights[shapeInfluences[inf]]);
                    //}
                }

                //If tangent space was requested, generate it
                if (tangent_space)
                {
                    if (tspace_flags == 0)
                        shapes[shape_num].UpdateTangentSpace();
                    else if (shapes[shape_num].GetData() != null)
                    {
                        shapes[shape_num].GetData().SetUVSetCount(1);
                        shapes[shape_num].GetData().SetTspaceFlag(tspace_flags);
                        shapes[shape_num].UpdateTangentSpace(1);
                    }
                }
                //Next Shape
            }
            return root;
        }

        /* 
         * Merges together multiple NiTriBasedGeom objects and stores their data
         * in this ComplexShape object.
         * \param root The root NiAVObject to which all of the NiTriBasedGeom
         * objects to be merged are attached.  It could be a single NiTribasedGeom
         * or a NiNode that is a split mesh proxy.
         * \sa NiNode::IsSplitMeshProxy
         */
        public void Merge(NiAVObject root)
        {
            if (root == null)
                throw new ArgumentNullException("Called ComplexShape::Merge with a null root reference.");

            var shapes = new List<NiTriBasedGeom>();
            //Determine root type
            if (root.IsDerivedType(NiTriBasedGeom.TYPE))
                //The function was called on a single shape. Add it to the list
                shapes.Add((NiTriBasedGeom)root);
            else if (root.IsDerivedType(NiNode.TYPE))
            {
                //The function was called on a NiNode.  Search for shape children
                var nodeRoot = (NiNode)root;
                var children = nodeRoot.GetChildren();
                for (var child = 0; child < children.Count; ++child)
                    if (children[child]->IsDerivedType(NiTriBasedGeom.TYPE))
                        shapes.Add((NiTriBasedGeom)children[child]);
                if (shapes.Count == 0)
                    throw new InvalidOperationException("The NiNode passed to ComplexShape::Merge has no shape children.");
            }
            else throw new InvalidOperationException("The ComplexShape::Merge function requies either a NiNode or a NiTriBasedGeom AVObject.");

            //The vector of VertNorm struts allows us to to refuse
            //to merge vertices that have different normals.
            var vns = new List<VertNorm>();

            //Clear all existing data
            Clear();

            //Merge in data from each shape
            var has_any_verts = false;
            var has_any_norms = false;
            Array.Resize(ref PropGroups, shapes.Count);
            uint prop_group_index = 0;
            foreach (var geom in shapes)
            {
                var current_property_group = geom.GetProperties();

                //Special code to handle the Bethesda Skyrim properties
                var bs_properties = geom.GetBSProperties();
                if (bs_properties[0] != null)
                    current_property_group.Add(bs_properties[0]);
                if (bs_properties[1] != null)
                    current_property_group.Add(bs_properties[1]);
                //Get properties of this shape
                propGroups[prop_group_index] = current_property_group;

                var geomData = (NiTriBasedGeomData)geom.GetData();
                if (geomData == null)
                    throw new InvalidOperationException("One of the NiTriBasedGeom found by ComplexShape::Merge with a NiTriBasedGeom has no NiTriBasedGeomData attached.");

                //Get Data
                List<Vector3> shapeVerts;
                List<Vector3> shapeNorms;
                //If this is a skin influenced mesh, get vertices from niGeom
                if (geom.GetSkinInstance() != null)
                {
                    geom.GetSkinDeformation(shapeVerts, shapeNorms);
                    if (geom.GetSkinInstance().GetType().IsSameType(BSDismemberSkinInstance.TYPE))
                    {
                        var dismember_skin = (BSDismemberSkinInstance)geom.GetSkinInstance();
                        NiSkinPartitionRef skin_partition = dismember_skin.GetSkinPartition();
                    }
                }
                else
                {
                    shapeVerts = geomData.GetVertices();
                    shapeNorms = geomData.GetNormals();
                }

                var shapeColors = geomData.GetColors();
                var shapeUVs = new List<List<TexCoord>>(geomData.GetUVSetCount());
                for (var i = 0; i < shapeUVs.Count; ++i)
                    shapeUVs[i] = geomData.GetUVSet(i);
                var shapeTris = geomData.GetTriangles();

                //Lookup table
                var lookUp = new List<MergeLookUp>(geomData.GetVertexCount());

                //Vertices and normals
                if (shapeVerts.Count != 0)
                    has_any_verts = true;

                var shape_has_norms = (shapeNorms.Count == shapeVerts.Count);
                if (shape_has_norms)
                    has_any_norms = true;
                for (var v = 0; v < shapeVerts.Count; ++v)
                {
                    var newVert = new VertNorm();
                    newVert.position = shapeVerts[v];
                    if (shape_has_norms)
                        newVert.normal = shapeNorms[v];

                    //Search for matching vert/norm
                    var match_found = false;
                    for (var vn_index = 0; vn_index < vns.Count; ++vn_index)
                        if (vns[vn_index] == newVert)
                        {
                            //Match found, use existing index
                            lookUp[v].vertIndex = (uint)vn_index;
                            if (shapeNorms.Count != 0)
                                lookUp[v].normIndex = (uint)vn_index;
                            match_found = true;
                            //Stop searching
                            break;
                        }
                    if (!match_found)
                    {
                        //No match found, add this vert/norm to the list
                        vns.Add(newVert);
                        //Record new index
                        lookUp[v].vertIndex = (uint)vns.Count - 1;
                        if (shapeNorms.Count != 0)
                            lookUp[v].normIndex = (uint)vns.Count - 1;
                    }
                }

                //Colors
                for (var c = 0; c < shapeColors.Count; ++c)
                {
                    Color4 newColor;
                    newColor = shapeColors[c];
                    //Search for matching color
                    var match_found = false;
                    for (var c_index = 0; c_index < Colors.Count; ++c_index)
                        if (Colors[c_index].r == newColor.r && Colors[c_index].g == newColor.g && Colors[c_index].b == newColor.b && Colors[c_index].a == newColor.a)
                        {
                            //Match found, use existing index
                            lookUp[c].colorIndex = (uint)c_index;
                            match_found = true;
                            //Stop searching
                            break;
                        }
                    if (!match_found)
                    {
                        //No match found, add this color to the list
                        Colors.Add(newColor);
                        //Record new index
                        lookUp[c].colorIndex = (uint)Colors.Count - 1;
                    }
                }

                //Texture Coordinates

                //Create UV set list
                List<TexType> uvSetList = new List<TexType>(shapeUVs.Count);
                //Initialize to base
                for (var tex = 0; tex < uvSetList.Count; ++tex)
                    uvSetList[tex] = BASE_MAP;
                var niProp = geom.GetPropertyByType(NiTexturingProperty.TYPE);
                NiTexturingProperty niTexingProp = null;
                if (niProp != null)
                    niTexingProp = (NiTexturingProperty)niProp;
                niProp = geom.GetPropertyByType(NiTextureProperty.TYPE);
                NiTextureProperty niTexProp = null;
                if (niProp != null)
                    niTexProp = (NiTextureProperty)niProp;
                BSShaderTextureSet bsTexProp = null;
                niProp = geom.GetPropertyByType(BSShaderTextureSet.TYPE);
                if (niProp != null)
                    bsTexProp = (BSShaderTextureSet)niProp;
                niProp = geom.GetBSProperties()[0];
                if (niProp != null && niProp.GetType().IsSameType(BSLightingShaderProperty.TYPE))
                {
                    var bs_shader = (BSLightingShaderProperty)niProp;
                    if (bs_shader.GetTextureSet() != null)
                        bsTexProp = bs_shader.GetTextureSet();
                }
                niProp = geom.GetBSProperties()[1];
                if (niProp != null && niProp.GetType().IsSameType(BSLightingShaderProperty.TYPE))
                {
                    var bs_shader = (BSLightingShaderProperty)niProp;
                    if (bs_shader.GetTextureSet() != null)
                        bsTexProp = bs_shader.GetTextureSet();
                }

                //Create a list of UV sets to check
                //pair.first = Texture Type
                //pair.second = UV Set ID
                var uvSets = new List<Tuple<TexType, uint>>();
                if (shapeUVs.Count != 0 && (niTexingProp != null || niTexProp != null || bsTexProp != null))
                {
                    if (niTexingProp != null)
                    {
                        //Add the UV set to the list for every type of texture slot that uses it
                        for (int tex = 0; tex < 8; ++tex)
                            if (niTexingProp.HasTexture(tex))
                            {
                                var td = niTexingProp.GetTexture(tex);
                                uvSets.Add(new Tuple<TexType, uint>((TexType)tex, td.uvSet));
                            }
                    }
                    else if (niTexProp != null || bsTexProp != null)
                        //Add the base UV set to the list and just use zero.
                        uvSets.Add(new Tuple<TexType, uint>(BASE_MAP, 0));

                    //Add the UV set to the list for every type of texture slot that uses it
                    for (var i = 0; i < uvSets.Count; ++i)
                    {
                        var newType = uvSets[i].Item1;
                        var set = uvSets[i].Item2;
                        //Search for matching UV set
                        var match_found = false;
                        uint uvSetIndex;
                        for (var set_index = 0; set_index < TexCoordSets.Count; ++set_index)
                            if (TexCoordSets[set_index].texType == newType)
                            {
                                //Match found, use existing index
                                uvSetIndex = (uint)set_index;
                                match_found = true;
                                //Stop searching
                                break;
                            }

                        if (!match_found)
                        {
                            //No match found, add this UV set to the list
                            var newTCS = new TexCoordSet();
                            newTCS.texType = newType;
                            TexCoordSets.Add(newTCS);
                            //Record new index
                            uvSetIndex = (uint)TexCoordSets.Count - 1;
                        }

                        //Loop through texture coordinates in this set
                        if (set >= shapeUVs.Count || set < 0)
                            throw new InvalidOperationException("One of the UV sets specified in the NiTexturingProperty did not exist in the NiTriBasedGeomData.");
                        for (var v = 0; v < shapeUVs[(int)set].Count; ++v)
                        {
                            var newCoord = shapeUVs[set][v];
                            //Search for matching texture coordinate
                            var match_found = false;
                            for (var tc_index = 0; tc_index < TexCoordSets[(int)uvSetIndex].texCoords.Count; ++tc_index)
                                if (TexCoordSets[(int)uvSetIndex].texCoords[tc_index] == newCoord)
                                {
                                    //Match found, use existing index
                                    lookUp[v].uvIndices[uvSetIndex] = tc_index;
                                    match_found = true;
                                    //Stop searching
                                    break;
                                }

                            //Done with loop, check if match was found
                            if (!match_found)
                            {
                                //No match found, add this texture coordinate to the list
                                TexCoordSets[uvSetIndex].texCoords.Add(newCoord);
                                //Record new index
                                lookUp[v].uvIndices[uvSetIndex] = (uint)TexCoordSets[uvSetIndex].texCoords.Count - 1;
                            }
                        }
                    }
                }

                //Use look up table to build list of faces
                for (var t = 0; t < shapeTris.Count; ++t)
                {
                    var newFace = new ComplexFace();
                    newFace.propGroupIndex = prop_group_index;
                    Array.Resize(ref newFace.points, 3);
                    var tri = shapeTris[t];
                    for (uint p = 0; p < 3; ++p)
                    {
                        if (shapeVerts.Count != 0)
                            newFace.points[p].vertexIndex = lookUp[tri[p]].vertIndex;
                        if (shapeNorms.Count != 0)
                            newFace.points[p].normalIndex = lookUp[tri[p]].normIndex;
                        if (shapeColors.Count != 0)
                            newFace.points[p].colorIndex = lookUp[tri[p]].colorIndex;
                        foreach (var set in lookUp[tri[p]].uvIndices)
                        {
                            TexCoordIndex tci;
                            tci.texCoordSetIndex = set.Key;
                            tci.texCoordIndex = set.Value;
                            newFace.points[p].texCoordIndices.Add(tci);
                        }
                    }
                    faces.Add(newFace);
                }

                //Use look up table to set vertex weights, if any
                NiSkinInstanceRef skinInst = geom.GetSkinInstance();
                if (skinInst != null)
                {
                    NiSkinDataRef skinData = skinInst.GetSkinData();
                    if (skinData != null)
                    {
                        //Get influence list
                        List<NiNodeRef> shapeBones = skinInst.GetBones();
                        //Get weights
                        List<BoneVertData> shapeWeights;
                        for (uint b = 0; b < shapeBones.Count; ++b)
                        {
                            shapeWeights = skinData.GetBoneWeights(b);
                            for (uint w = 0; w < shapeWeights.Count; ++w)
                            {
                                uint vn_index = lookUp[shapeWeights[w].index].vertIndex;
                                NiNodeRef boneRef = shapeBones[b];
                                float weight = shapeWeights[w].weight;
                                vns[vn_index].weights[boneRef] = weight;
                            }
                        }
                    }

                    //Check to see if the skin is actually a dismember skin instance in which case import the partitions too
                    if (skinInst.GetType().IsSameType(BSDismemberSkinInstance.TYPE))
                    {
                        BSDismemberSkinInstanceRef dismember_skin = (BSDismemberSkinInstance)geom.GetSkinInstance();
                        NiSkinPartitionRef skin_partition = dismember_skin.GetSkinPartition();

                        //These are the partition data of the current shapes
                        List<BodyPartList> current_body_parts;
                        List<int> current_body_parts_faces;

                        for (uint y = 0; y < dismember_skin.GetPartitions().Count; y++)
                            current_body_parts.Add(dismember_skin.GetPartitions().at(y));
                        for (uint y = 0; y < shapeTris.Count; y++)
                            current_body_parts_faces.Add(0);

                        for (int y = 0; y < skin_partition.GetNumPartitions(); y++)
                        {
                            List<Triangle> partition_triangles = skin_partition.GetTriangles(y);
                            List<ushort> partition_vertex_map = skin_partition.GetVertexMap(y);
                            bool has_vertex_map = false;
                            if (partition_vertex_map.Count > 0)
                                has_vertex_map = true;
                            for (uint z = 0; z < partition_triangles.Count; z++)
                            {
                                uint w = faces.Count - shapeTris.Count;
                                int merged_x;
                                int merged_y;
                                int merged_z;
                                if (has_vertex_map)
                                {
                                    merged_x = lookUp[partition_vertex_map[partition_triangles[z].v1]].vertIndex;
                                    merged_y = lookUp[partition_vertex_map[partition_triangles[z].v2]].vertIndex;
                                    merged_z = lookUp[partition_vertex_map[partition_triangles[z].v3]].vertIndex;
                                }
                                else
                                {
                                    merged_x = lookUp[partition_triangles[z].v1].vertIndex;
                                    merged_y = lookUp[partition_triangles[z].v2].vertIndex;
                                    merged_z = lookUp[partition_triangles[z].v3].vertIndex;
                                }
                                for (; w < faces.size(); w++)
                                {
                                    ComplexFace current_face = faces[w];

                                    //keep this commented code is case my theory that all triangles must have vertices arranged in a certain way and that you can't rearrange vertices in a triangle
                                    /*if(current_face.points[0].vertexIndex == merged_x) {
                                        if(current_face.points[1].vertexIndex == merged_y && current_face.points[2].vertexIndex == merged_z) {
                                            is_same_face = true;
                                            break;
                                        } else if(current_face.points[2].vertexIndex == merged_y && current_face.points[1].vertexIndex == merged_z) {
                                            is_same_face = true;
                                            break;
                                        }
                                    } else if(current_face.points[1].vertexIndex == merged_x) {
                                        if(current_face.points[0].vertexIndex == merged_y && current_face.points[2].vertexIndex == merged_z) {
                                            is_same_face = true;
                                            break;
                                        } else if(current_face.points[2].vertexIndex == merged_y && current_face.points[0].vertexIndex == merged_z) {
                                            is_same_face = true;
                                            break;
                                        }
                                    } else if(current_face.points[2].vertexIndex == merged_x) {
                                        if(current_face.points[0].vertexIndex == merged_y && current_face.points[1].vertexIndex == merged_z) {
                                            is_same_face = true;
                                            break;
                                        } else if(current_face.points[1].vertexIndex == merged_y && current_face.points[0].vertexIndex == merged_z) {
                                            is_same_face = true;
                                            break;
                                        }
                                    } */

                                    if (current_face.points[0].vertexIndex == merged_x && current_face.points[1].vertexIndex == merged_y && current_face.points[2].vertexIndex == merged_z)
                                        break;
                                }

                                if (w - (faces.Count - shapeTris.Count) < shapeTris.Count)
                                    current_body_parts_faces[w - (faces.Count - shapeTris.Count)] = y;
                            }
                        }

                        for (uint y = 0; y < current_body_parts.Count; y++)
                        {
                            int match_index = -1;
                            for (uint z = 0; z < dismemberPartitionsBodyParts.Count; z++)
                                if (dismemberPartitionsBodyParts[z].bodyPart == current_body_parts[y].bodyPart
                                    && dismemberPartitionsBodyParts[z].partFlag == current_body_parts[y].partFlag)
                                {
                                    match_index = z;
                                    break;
                                }
                            if (match_index < 0)
                            {
                                dismemberPartitionsBodyParts.Add(current_body_parts[y]);
                                match_index = dismemberPartitionsBodyParts.Count - 1;
                            }
                            for (uint z = 0; z < current_body_parts_faces.Count; z++)
                                if (current_body_parts_faces[z] == y)
                                    current_body_parts_faces[z] = match_index;
                        }
                        for (uint x = 0; x < current_body_parts_faces.Count; x++)
                            dismemberPartitionsFaces.Add(current_body_parts_faces[x]);
                    }
                }
                //Next Shape
                ++prop_group_index;
            }

            //Finished with all shapes.  Build up a list of influences
            var boneLookUp = new Dictionary<NiNode, uint>();
            for (var v = 0; v < vns.Count; ++v)
                foreach (var w in vns[v].weights)
                    boneLookUp[w.Key] = 0; //will change later

            skinInfluences.resize(boneLookUp.Count);
            uint si_index = 0;
            foreach (var si in boneLookUp)
            {
                si.Value = si_index;
                skinInfluences[si_index] = si.Key;
                ++si_index;
            }

            //Copy vns data to vertices and normals
            if (has_any_verts)
                Vertices.resize(vns.Count);
            if (has_any_norms)
                Normals.resize(vns.Count);
            for (var v = 0; v < vns.Count; ++v)
            {
                if (has_any_verts)
                {
                    Vertices[v].position = vns[v].position;
                    Vertices[v].weights.resize(vns[v].weights.size());
                    uint weight_index = 0;
                    foreach (var w in vns[v].weights)
                    {
                        Vertices[v].weights[weight_index].influenceIndex = boneLookUp[w.Key];
                        Vertices[v].weights[weight_index].weight = w.Value;
                        ++weight_index;
                    }
                }
                if (has_any_norms)
                    Normals[v] = vns[v].normal;
            }
            //Done Merging
        }

        /* 
         * Clears out all the data stored in this ComplexShape
         */
        public void Clear()
        {
            Vertices.Clear();
            Colors.Clear();
            Normals.Clear();
            TexCoordSets.Clear();
            Faces.Clear();
            PropGroups.Clear();
            SkinInfluences.Clear();
            Name = null;
            DismemberPartitionsBodyParts.Clear();
            DismemberPartitionsFaces.Clear();
        }

        /*
         * Gets or Sets the name of ComplexShape which will be used when splitting it into NiTriBasedGeom objects.
         */
        public string Name { get; set; }

        /*
         * Gets or Sets the vertex data that will be used by the ComplexShape, which includes position and skin weighting information.  The data is referenced by vector index, so repetition of values is not necessary.
         */
        public List<WeightedVertex> Vertices { get; set; }

        /*
         * Gets or Sets the color data that will be used by the ComplexShape.  The data is referenced by vector index, so repetition of values is not necessary.
         */
        public List<Color4> Colors { get; set; }

        /*
         * Gets or Sets the normal data that will be used by the ComplexShape.  The data is referenced by vector index, so repetition of values is not necessary.
         */
        public List<Vector3> Normals { get; set; }

        /*
         * Gets or Sets the texture coordinate data used by the ComplexShape.  This includes a list of UV sets, each with a texture type and texture coordiantes.  The coordinate data is referenced by index, so repetition of values within a particular set is not necessary.
         */
        public List<TexCoordSet> TexCoordSets { get; set; }

        /*
         * Gets or Sets the faces used by the ComplexShape.  Each face references 3 or more vertex positions (triangles are not required), each of which can reference color, normal, and texture, information.  Each face also references a property group which defines the material and any other per-face attributes.
         * \param[in] n The new face data.  Will replace any existing data.
         */
        public List<ComplexFace> Faces { get; set; }

        /*
         * Gets or Sets the property groups used by the Complex Shape.  Each group of NiProperty values can be assigned by vector index to a face in the ComplexShape by index, allowing for material and other properties to be specified on a per-face basis.  If the ComplexShape is split, each property group that is used by faces in the mesh will result in a separate NiTriBasedGeom with the specified propreties attached.
         */
        public List<NiProperty>[] PropGroups { get; set; }

        /*
         * Gets or Sets the skin influences used by the Complex Shape.  These are the NiNode objects which cause deformations in skin meshes.  They are referenced in the vertex data by vector index.
         * \param[in] n The new skin influences.  Will replace any existing data.
         */
        public List<NiNode> SkinInfluences { get; set; }

        /*
         * Gets or Sets the association between the faces in the complex shape and the corresponding body parts
         */
        public List<uint> DismemberPartitionsFaces { get; set; }

        /*
         * Gets or Sets a list of the dismember groups
         */
        public List<BodyPartList> DismemberPartitionsBodyParts { get; set; }
    }

    public class VertNorm
    {
        public Vector3 position;
        public Vector3 normal;
        public Dictionary<NiNode, float> weights;

        public static bool operator ==(VertNorm t, VertNorm n)
        {
            if (Math.Abs(t.position.x - n.position.x) > 0.001 || Math.Abs(t.position.y - n.position.y) > 0.001 || Math.Abs(t.position.z - n.position.z) > 0.001) return false;
            if (Math.Abs(t.normal.x - n.normal.x) > 0.001 || Math.Abs(t.normal.y - n.normal.y) > 0.001 || Math.Abs(t.normal.z - n.normal.z) > 0.001) return false;
            return true;
        }
        public static bool operator !=(VertNorm t, VertNorm n) => !(t == n);

        //public override bool Equals(object obj)
        //{
        //    var n = (VertNorm)obj;
        //    if (Math.Abs(position.x - n.position.x) > 0.001 || Math.Abs(position.y - n.position.y) > 0.001 || Math.Abs(position.z - n.position.z) > 0.001) return false;
        //    if (Math.Abs(normal.x - n.normal.x) > 0.001 || Math.Abs(normal.y - n.normal.y) > 0.001 || Math.Abs(normal.z - n.normal.z) > 0.001) return false;
        //    return true;
        //}
    }

    public class CompoundVertex
    {
        public Vector3 position;
        public Vector3 normal;
        public Color4 color;
        public Dictionary<TexType, TexCoord> texCoords;
        public Dictionary<NiNode, float> weights;

        public static bool operator ==(CompoundVertex t, CompoundVertex n)
        {
            if (t.position != n.position) return false;
            if (t.normal != n.normal) return false;
            if (t.color != n.color) return false;
            if (t.texCoords != n.texCoords) return false;
            if (t.weights != n.weights) return false;
            return true;
        }
        public static bool operator !=(CompoundVertex t, CompoundVertex n) => !(t == n);
    }

    public class MergeLookUp
    {
        public uint vertIndex;
        public uint normIndex;
        public uint colorIndex;
        public Dictionary<uint, uint> uvIndices; //TexCoordSet Index, TexCoord Index
    }
}
