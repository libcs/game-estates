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
	/*!
	 * Fallout 4 Packed Combined Shared Geometry Data.
	 *         Geometry is NOT baked into the file. It is instead a reference to the shape via a Shape ID (currently undecoded)
	 *         which loads the geometry via the STAT form for the NIF.
	 */
	public class BSPackedCombinedSharedGeomDataExtra : NiExtraData
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSPackedCombinedSharedGeomDataExtra", NiExtraData.TYPE);

		/*! vertexDesc */
		internal BSVertexDesc vertexDesc;
		/*! numVertices */
		internal uint numVertices;
		/*! numTriangles */
		internal uint numTriangles;
		/*! Unknown. */
		internal uint unknownFlags1;
		/*! Unknown. */
		internal uint unknownFlags2;
		/*! numData */
		internal uint numData;
		/*! object */
		internal IList<BSPackedGeomObject> object;
		/*! objectData */
		internal IList<BSPackedSharedGeomData> objectData;
		public BSPackedCombinedSharedGeomDataExtra()
		{
			numVertices = (uint)0;
			numTriangles = (uint)0;
			unknownFlags1 = (uint)0;
			unknownFlags2 = (uint)0;
			numData = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSPackedCombinedSharedGeomDataExtra();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out vertexDesc.vf1, s, info);
			Nif.NifStream(out vertexDesc.vf2, s, info);
			Nif.NifStream(out vertexDesc.vf3, s, info);
			Nif.NifStream(out vertexDesc.vf4, s, info);
			Nif.NifStream(out vertexDesc.vf5, s, info);
			Nif.NifStream(out vertexDesc.vertexAttributes, s, info);
			Nif.NifStream(out vertexDesc.vf8, s, info);
			Nif.NifStream(out numVertices, s, info);
			Nif.NifStream(out numTriangles, s, info);
			Nif.NifStream(out unknownFlags1, s, info);
			Nif.NifStream(out unknownFlags2, s, info);
			Nif.NifStream(out numData, s, info);
			object = new BSPackedGeomObject[numData];
			for (var i3 = 0; i3 < object.Count; i3++)
			{
				Nif.NifStream(out object[i3].shapeId1, s, info);
				Nif.NifStream(out object[i3].shapeId2, s, info);
			}
			objectData = new BSPackedSharedGeomData[numData];
			for (var i3 = 0; i3 < objectData.Count; i3++)
			{
				Nif.NifStream(out objectData[i3].numVerts, s, info);
				Nif.NifStream(out objectData[i3].lodLevels, s, info);
				Nif.NifStream(out objectData[i3].triCountLod0, s, info);
				Nif.NifStream(out objectData[i3].triOffsetLod0, s, info);
				Nif.NifStream(out objectData[i3].triCountLod1, s, info);
				Nif.NifStream(out objectData[i3].triOffsetLod1, s, info);
				Nif.NifStream(out objectData[i3].triCountLod2, s, info);
				Nif.NifStream(out objectData[i3].triOffsetLod2, s, info);
				Nif.NifStream(out objectData[i3].numCombined, s, info);
				objectData[i3].combined = new BSPackedGeomDataCombined[objectData[i3].numCombined];
				for (var i4 = 0; i4 < objectData[i3].combined.Count; i4++)
				{
					Nif.NifStream(out objectData[i3].combined[i4].grayscaleToPaletteScale, s, info);
					Nif.NifStream(out objectData[i3].combined[i4].transform.rotation, s, info);
					Nif.NifStream(out objectData[i3].combined[i4].transform.translation, s, info);
					Nif.NifStream(out objectData[i3].combined[i4].transform.scale, s, info);
					Nif.NifStream(out objectData[i3].combined[i4].boundingSphere.center, s, info);
					Nif.NifStream(out objectData[i3].combined[i4].boundingSphere.radius, s, info);
				}
				Nif.NifStream(out objectData[i3].vertexDesc.vf1, s, info);
				Nif.NifStream(out objectData[i3].vertexDesc.vf2, s, info);
				Nif.NifStream(out objectData[i3].vertexDesc.vf3, s, info);
				Nif.NifStream(out objectData[i3].vertexDesc.vf4, s, info);
				Nif.NifStream(out objectData[i3].vertexDesc.vf5, s, info);
				Nif.NifStream(out objectData[i3].vertexDesc.vertexAttributes, s, info);
				Nif.NifStream(out objectData[i3].vertexDesc.vf8, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numData = (uint)object.Count;
			Nif.NifStream(vertexDesc.vf1, s, info);
			Nif.NifStream(vertexDesc.vf2, s, info);
			Nif.NifStream(vertexDesc.vf3, s, info);
			Nif.NifStream(vertexDesc.vf4, s, info);
			Nif.NifStream(vertexDesc.vf5, s, info);
			Nif.NifStream(vertexDesc.vertexAttributes, s, info);
			Nif.NifStream(vertexDesc.vf8, s, info);
			Nif.NifStream(numVertices, s, info);
			Nif.NifStream(numTriangles, s, info);
			Nif.NifStream(unknownFlags1, s, info);
			Nif.NifStream(unknownFlags2, s, info);
			Nif.NifStream(numData, s, info);
			for (var i3 = 0; i3 < object.Count; i3++)
			{
				Nif.NifStream(object[i3].shapeId1, s, info);
				Nif.NifStream(object[i3].shapeId2, s, info);
			}
			for (var i3 = 0; i3 < objectData.Count; i3++)
			{
				objectData[i3].numCombined = (uint)objectData[i3].combined.Count;
				Nif.NifStream(objectData[i3].numVerts, s, info);
				Nif.NifStream(objectData[i3].lodLevels, s, info);
				Nif.NifStream(objectData[i3].triCountLod0, s, info);
				Nif.NifStream(objectData[i3].triOffsetLod0, s, info);
				Nif.NifStream(objectData[i3].triCountLod1, s, info);
				Nif.NifStream(objectData[i3].triOffsetLod1, s, info);
				Nif.NifStream(objectData[i3].triCountLod2, s, info);
				Nif.NifStream(objectData[i3].triOffsetLod2, s, info);
				Nif.NifStream(objectData[i3].numCombined, s, info);
				for (var i4 = 0; i4 < objectData[i3].combined.Count; i4++)
				{
					Nif.NifStream(objectData[i3].combined[i4].grayscaleToPaletteScale, s, info);
					Nif.NifStream(objectData[i3].combined[i4].transform.rotation, s, info);
					Nif.NifStream(objectData[i3].combined[i4].transform.translation, s, info);
					Nif.NifStream(objectData[i3].combined[i4].transform.scale, s, info);
					Nif.NifStream(objectData[i3].combined[i4].boundingSphere.center, s, info);
					Nif.NifStream(objectData[i3].combined[i4].boundingSphere.radius, s, info);
				}
				Nif.NifStream(objectData[i3].vertexDesc.vf1, s, info);
				Nif.NifStream(objectData[i3].vertexDesc.vf2, s, info);
				Nif.NifStream(objectData[i3].vertexDesc.vf3, s, info);
				Nif.NifStream(objectData[i3].vertexDesc.vf4, s, info);
				Nif.NifStream(objectData[i3].vertexDesc.vf5, s, info);
				Nif.NifStream(objectData[i3].vertexDesc.vertexAttributes, s, info);
				Nif.NifStream(objectData[i3].vertexDesc.vf8, s, info);
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
			numData = (uint)object.Count;
			s.AppendLine($"      VF1:  {vertexDesc.vf1}");
			s.AppendLine($"      VF2:  {vertexDesc.vf2}");
			s.AppendLine($"      VF3:  {vertexDesc.vf3}");
			s.AppendLine($"      VF4:  {vertexDesc.vf4}");
			s.AppendLine($"      VF5:  {vertexDesc.vf5}");
			s.AppendLine($"      Vertex Attributes:  {vertexDesc.vertexAttributes}");
			s.AppendLine($"      VF8:  {vertexDesc.vf8}");
			s.AppendLine($"      Num Vertices:  {numVertices}");
			s.AppendLine($"      Num Triangles:  {numTriangles}");
			s.AppendLine($"      Unknown Flags 1:  {unknownFlags1}");
			s.AppendLine($"      Unknown Flags 2:  {unknownFlags2}");
			s.AppendLine($"      Num Data:  {numData}");
			array_output_count = 0;
			for (var i3 = 0; i3 < object.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Shape ID 1:  {object[i3].shapeId1}");
				s.AppendLine($"        Shape ID 2:  {object[i3].shapeId2}");
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < objectData.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				objectData[i3].numCombined = (uint)objectData[i3].combined.Count;
				s.AppendLine($"        Num Verts:  {objectData[i3].numVerts}");
				s.AppendLine($"        LOD Levels:  {objectData[i3].lodLevels}");
				s.AppendLine($"        Tri Count LOD0:  {objectData[i3].triCountLod0}");
				s.AppendLine($"        Tri Offset LOD0:  {objectData[i3].triOffsetLod0}");
				s.AppendLine($"        Tri Count LOD1:  {objectData[i3].triCountLod1}");
				s.AppendLine($"        Tri Offset LOD1:  {objectData[i3].triOffsetLod1}");
				s.AppendLine($"        Tri Count LOD2:  {objectData[i3].triCountLod2}");
				s.AppendLine($"        Tri Offset LOD2:  {objectData[i3].triOffsetLod2}");
				s.AppendLine($"        Num Combined:  {objectData[i3].numCombined}");
				array_output_count = 0;
				for (var i4 = 0; i4 < objectData[i3].combined.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					s.AppendLine($"          Grayscale to Palette Scale:  {objectData[i3].combined[i4].grayscaleToPaletteScale}");
					s.AppendLine($"          Rotation:  {objectData[i3].combined[i4].transform.rotation}");
					s.AppendLine($"          Translation:  {objectData[i3].combined[i4].transform.translation}");
					s.AppendLine($"          Scale:  {objectData[i3].combined[i4].transform.scale}");
					s.AppendLine($"          Center:  {objectData[i3].combined[i4].boundingSphere.center}");
					s.AppendLine($"          Radius:  {objectData[i3].combined[i4].boundingSphere.radius}");
				}
				s.AppendLine($"        VF1:  {objectData[i3].vertexDesc.vf1}");
				s.AppendLine($"        VF2:  {objectData[i3].vertexDesc.vf2}");
				s.AppendLine($"        VF3:  {objectData[i3].vertexDesc.vf3}");
				s.AppendLine($"        VF4:  {objectData[i3].vertexDesc.vf4}");
				s.AppendLine($"        VF5:  {objectData[i3].vertexDesc.vf5}");
				s.AppendLine($"        Vertex Attributes:  {objectData[i3].vertexDesc.vertexAttributes}");
				s.AppendLine($"        VF8:  {objectData[i3].vertexDesc.vf8}");
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
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
