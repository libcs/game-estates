/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.
//-----------------------------------NOTICE----------------------------------//
// Only add custom code in the designated areas to preserve between builds   //
//-----------------------------------NOTICE----------------------------------//

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Mesh data: vertices, vertex normals, etc. */
	public class NiGeometryData : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiGeometryData", NiObject.TYPE);

		/*! Always zero. */
		internal int groupId;
		/*! Number of vertices. */
		internal ushort numVertices;
		/*! Bethesda uses this for max number of particles in NiPSysData. */
		internal ushort bsMaxVertices;
		/*! Used with NiCollision objects when OBB or TRI is set. */
		internal byte keepFlags;
		/*! Unknown. */
		internal byte compressFlags;
		/*! Is the vertex array present? (Always non-zero.) */
		internal bool hasVertices;
		/*! The mesh vertices. */
		internal IList<Vector3> vertices;
		/*! vectorFlags */
		internal VectorFlags vectorFlags;
		/*! bsVectorFlags */
		internal BSVectorFlags bsVectorFlags;
		/*! materialCrc */
		internal uint materialCrc;
		/*! Do we have lighting normals? These are essential for proper lighting: if not present, the model will only be influenced by ambient light. */
		internal bool hasNormals;
		/*! The lighting normals. */
		internal IList<Vector3> normals;
		/*! Tangent vectors. */
		internal IList<Vector3> tangents;
		/*! Bitangent vectors. */
		internal IList<Vector3> bitangents;
		/*! Center of the bounding box (smallest box that contains all vertices) of the mesh. */
		internal Vector3 center;
		/*! Radius of the mesh: maximal Euclidean distance between the center and all vertices. */
		internal float radius;
		/*! Unknown, always 0? */
		internal Array13<short> unknown13Shorts;
		/*!
		 * Do we have vertex colors? These are usually used to fine-tune the lighting of the model.
		 * 
		 *             Note: how vertex colors influence the model can be controlled by having a NiVertexColorProperty object as a property child of the root node. If this
		 * property object is not present, the vertex colors fine-tune lighting.
		 * 
		 *             Note 2: set to either 0 or 0xFFFFFFFF for NifTexture compatibility.
		 */
		internal bool hasVertexColors;
		/*! The vertex colors. */
		internal IList<Color4> vertexColors;
		/*!
		 * The lower 6 (or less?) bits of this field represent the number of UV texture sets. The other bits are probably flag bits. For versions 10.1.0.0 and up, if bit
		 * 12 is set then extra vectors are present after the normals.
		 */
		internal ushort numUvSets;
		/*!
		 * Do we have UV coordinates?
		 * 
		 *             Note: for compatibility with NifTexture, set this value to either 0x00000000 or 0xFFFFFFFF.
		 */
		internal bool hasUv;
		/*! The UV texture coordinates. They follow the OpenGL standard: some programs may require you to flip the second coordinate. */
		internal IList<TexCoord[]> uvSets;
		/*! Consistency Flags */
		internal ConsistencyType consistencyFlags;
		/*! Unknown. */
		internal AbstractAdditionalGeometryData additionalData;
		public NiGeometryData()
		{
			groupId = (int)0;
			numVertices = (ushort)0;
			bsMaxVertices = (ushort)0;
			keepFlags = (byte)0;
			compressFlags = (byte)0;
			hasVertices = 1;
			vectorFlags = (VectorFlags)0;
			bsVectorFlags = (BSVectorFlags)0;
			materialCrc = (uint)0;
			hasNormals = false;
			radius = 0.0f;
			hasVertexColors = false;
			numUvSets = (ushort)0;
			hasUv = false;
			consistencyFlags = ConsistencyType.CT_MUTABLE;
			additionalData = null;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiGeometryData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			if (info.version >= 0x0A010072)
			{
				Nif.NifStream(out groupId, s, info);
			}
			if ((!IsDerivedType(NiPSysData.TYPE)))
			{
				Nif.NifStream(out numVertices, s, info);
			}
			if ((info.userVersion2 < 34))
			{
				if (IsDerivedType(NiPSysData.TYPE))
				{
					Nif.NifStream(out (ushort)numVertices, s, info);
				}
			}
			if ((info.userVersion2 >= 34))
			{
				if (IsDerivedType(NiPSysData.TYPE))
				{
					Nif.NifStream(out bsMaxVertices, s, info);
				}
			}
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(out keepFlags, s, info);
				Nif.NifStream(out compressFlags, s, info);
			}
			Nif.NifStream(out hasVertices, s, info);
			if (hasVertices)
			{
				vertices = new Vector3[numVertices];
				for (var i4 = 0; i4 < vertices.Count; i4++)
				{
					Nif.NifStream(out vertices[i4], s, info);
				}
			}
			if (info.version >= 0x0A000100 && ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))))
			{
				Nif.NifStream(out vectorFlags, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 0)))
			{
				Nif.NifStream(out bsVectorFlags, s, info);
			}
			if (info.version >= 0x14020007 && info.version <= 0x14020007 && info.userVersion == 12)
			{
				Nif.NifStream(out materialCrc, s, info);
			}
			Nif.NifStream(out hasNormals, s, info);
			if (hasNormals)
			{
				normals = new Vector3[numVertices];
				for (var i4 = 0; i4 < normals.Count; i4++)
				{
					Nif.NifStream(out normals[i4], s, info);
				}
			}
			if (info.version >= 0x0A010000)
			{
				if ((hasNormals && ((vectorFlags | bsVectorFlags) & 4096)))
				{
					tangents = new Vector3[numVertices];
					for (var i5 = 0; i5 < tangents.Count; i5++)
					{
						Nif.NifStream(out tangents[i5], s, info);
					}
					bitangents = new Vector3[numVertices];
					for (var i5 = 0; i5 < bitangents.Count; i5++)
					{
						Nif.NifStream(out bitangents[i5], s, info);
					}
				}
			}
			Nif.NifStream(out center, s, info);
			Nif.NifStream(out radius, s, info);
			if (info.version >= 0x14030009 && info.version <= 0x14030009 && info.userVersion == 131072)
			{
				for (var i4 = 0; i4 < 13; i4++)
				{
					Nif.NifStream(out unknown13Shorts[i4], s, info);
				}
			}
			Nif.NifStream(out hasVertexColors, s, info);
			if (hasVertexColors)
			{
				vertexColors = new Color4[numVertices];
				for (var i4 = 0; i4 < vertexColors.Count; i4++)
				{
					Nif.NifStream(out vertexColors[i4], s, info);
				}
			}
			if (info.version <= 0x04020200)
			{
				Nif.NifStream(out numUvSets, s, info);
			}
			if (info.version <= 0x04000002)
			{
				Nif.NifStream(out hasUv, s, info);
			}
			uvSets = new TexCoord[((numUvSets & 63) | ((vectorFlags & 63) | (bsVectorFlags & 1)))];
			for (var i3 = 0; i3 < uvSets.Count; i3++)
			{
				uvSets[i3].Resize(numVertices);
				for (var i4 = 0; i4 < uvSets[i3].Count; i4++)
				{
					Nif.NifStream(out uvSets[i3][i4], s, info);
				}
			}
			if (info.version >= 0x0A000100)
			{
				Nif.NifStream(out consistencyFlags, s, info);
			}
			if (info.version >= 0x14000004)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numVertices = (ushort)vertices.Count;
			if (info.version >= 0x0A010072)
			{
				Nif.NifStream(groupId, s, info);
			}
			if ((!IsDerivedType(NiPSysData.TYPE)))
			{
				Nif.NifStream(numVertices, s, info);
			}
			if ((info.userVersion2 < 34))
			{
				if (IsDerivedType(NiPSysData.TYPE))
				{
					Nif.NifStream((ushort)numVertices, s, info);
				}
			}
			if ((info.userVersion2 >= 34))
			{
				if (IsDerivedType(NiPSysData.TYPE))
				{
					Nif.NifStream(bsMaxVertices, s, info);
				}
			}
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(keepFlags, s, info);
				Nif.NifStream(compressFlags, s, info);
			}
			Nif.NifStream(hasVertices, s, info);
			if (hasVertices)
			{
				for (var i4 = 0; i4 < vertices.Count; i4++)
				{
					Nif.NifStream(vertices[i4], s, info);
				}
			}
			if (info.version >= 0x0A000100 && ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))))
			{
				Nif.NifStream(vectorFlags, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 0)))
			{
				Nif.NifStream(bsVectorFlags, s, info);
			}
			if (info.version >= 0x14020007 && info.version <= 0x14020007 && info.userVersion == 12)
			{
				Nif.NifStream(materialCrc, s, info);
			}
			Nif.NifStream(hasNormals, s, info);
			if (hasNormals)
			{
				for (var i4 = 0; i4 < normals.Count; i4++)
				{
					Nif.NifStream(normals[i4], s, info);
				}
			}
			if (info.version >= 0x0A010000)
			{
				if ((hasNormals && ((vectorFlags | bsVectorFlags) & 4096)))
				{
					for (var i5 = 0; i5 < tangents.Count; i5++)
					{
						Nif.NifStream(tangents[i5], s, info);
					}
					for (var i5 = 0; i5 < bitangents.Count; i5++)
					{
						Nif.NifStream(bitangents[i5], s, info);
					}
				}
			}
			Nif.NifStream(center, s, info);
			Nif.NifStream(radius, s, info);
			if (info.version >= 0x14030009 && info.version <= 0x14030009 && info.userVersion == 131072)
			{
				for (var i4 = 0; i4 < 13; i4++)
				{
					Nif.NifStream(unknown13Shorts[i4], s, info);
				}
			}
			Nif.NifStream(hasVertexColors, s, info);
			if (hasVertexColors)
			{
				for (var i4 = 0; i4 < vertexColors.Count; i4++)
				{
					Nif.NifStream(vertexColors[i4], s, info);
				}
			}
			if (info.version <= 0x04020200)
			{
				Nif.NifStream(numUvSets, s, info);
			}
			if (info.version <= 0x04000002)
			{
				Nif.NifStream(hasUv, s, info);
			}
			for (var i3 = 0; i3 < uvSets.Count; i3++)
			{
				for (var i4 = 0; i4 < uvSets[i3].Count; i4++)
				{
					Nif.NifStream(uvSets[i3][i4], s, info);
				}
			}
			if (info.version >= 0x0A000100)
			{
				Nif.NifStream(consistencyFlags, s, info);
			}
			if (info.version >= 0x14000004)
			{
				WriteRef((NiObject)additionalData, s, info, link_map, missing_link_stack);
			}
		}

		/*!
		 * Summarizes the information contained in this object in English.
		 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
		 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
		 */
		public override string AsString(bool verbose = false)
		{
			var s = new System.Text.StringBuilder();
			var array_output_count = 0U;
			s.Append(base.AsString());
			numVertices = (ushort)vertices.Count;
			s.AppendLine($"      Group ID:  {groupId}");
			if ((!IsDerivedType(NiPSysData.TYPE)))
			{
				s.AppendLine($"        Num Vertices:  {numVertices}");
			}
			if (IsDerivedType(NiPSysData.TYPE))
			{
				s.AppendLine($"        BS Max Vertices:  {bsMaxVertices}");
			}
			s.AppendLine($"      Keep Flags:  {keepFlags}");
			s.AppendLine($"      Compress Flags:  {compressFlags}");
			s.AppendLine($"      Has Vertices:  {hasVertices}");
			if (hasVertices)
			{
				array_output_count = 0;
				for (var i4 = 0; i4 < vertices.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Vertices[{i4}]:  {vertices[i4]}");
					array_output_count++;
				}
			}
			s.AppendLine($"      Vector Flags:  {vectorFlags}");
			s.AppendLine($"      BS Vector Flags:  {bsVectorFlags}");
			s.AppendLine($"      Material CRC:  {materialCrc}");
			s.AppendLine($"      Has Normals:  {hasNormals}");
			if (hasNormals)
			{
				array_output_count = 0;
				for (var i4 = 0; i4 < normals.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Normals[{i4}]:  {normals[i4]}");
					array_output_count++;
				}
			}
			if ((hasNormals && ((vectorFlags | bsVectorFlags) & 4096)))
			{
				array_output_count = 0;
				for (var i4 = 0; i4 < tangents.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Tangents[{i4}]:  {tangents[i4]}");
					array_output_count++;
				}
				array_output_count = 0;
				for (var i4 = 0; i4 < bitangents.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Bitangents[{i4}]:  {bitangents[i4]}");
					array_output_count++;
				}
			}
			s.AppendLine($"      Center:  {center}");
			s.AppendLine($"      Radius:  {radius}");
			array_output_count = 0;
			for (var i3 = 0; i3 < 13; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unknown 13 shorts[{i3}]:  {unknown13Shorts[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Has Vertex Colors:  {hasVertexColors}");
			if (hasVertexColors)
			{
				array_output_count = 0;
				for (var i4 = 0; i4 < vertexColors.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Vertex Colors[{i4}]:  {vertexColors[i4]}");
					array_output_count++;
				}
			}
			s.AppendLine($"      Num UV Sets:  {numUvSets}");
			s.AppendLine($"      Has UV:  {hasUv}");
			array_output_count = 0;
			for (var i3 = 0; i3 < uvSets.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				for (var i4 = 0; i4 < uvSets[i3].Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          UV Sets[{i4}]:  {uvSets[i3][i4]}");
					array_output_count++;
				}
			}
			s.AppendLine($"      Consistency Flags:  {consistencyFlags}");
			s.AppendLine($"      Additional Data:  {additionalData}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			if (info.version >= 0x14000004)
			{
				additionalData = FixLink<AbstractAdditionalGeometryData>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (additionalData != null)
				refs.Add((NiObject)additionalData);
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			return ptrs;
		}
	}
}
