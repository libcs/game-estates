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
	 * DEPRECATED (20.5), functionality included in NiMeshScreenElements.
	 *         Two dimensional screen elements.
	 */
	public class NiScreenElementsData : NiTriShapeData
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiScreenElementsData", NiTriShapeData.TYPE);

		/*! maxPolygons */
		internal ushort maxPolygons;
		/*! polygons */
		internal IList<Polygon> polygons;
		/*! polygonIndices */
		internal IList<ushort> polygonIndices;
		/*! polygonGrowBy */
		internal ushort polygonGrowBy;
		/*! numPolygons */
		internal ushort numPolygons;
		/*! maxVertices */
		internal ushort maxVertices;
		/*! verticesGrowBy */
		internal ushort verticesGrowBy;
		/*! maxIndices */
		internal ushort maxIndices;
		/*! indicesGrowBy */
		internal ushort indicesGrowBy;
		public NiScreenElementsData()
		{
			maxPolygons = (ushort)0;
			polygonGrowBy = (ushort)1;
			numPolygons = (ushort)0;
			maxVertices = (ushort)0;
			verticesGrowBy = (ushort)1;
			maxIndices = (ushort)0;
			indicesGrowBy = (ushort)1;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiScreenElementsData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out maxPolygons, s, info);
			polygons = new Polygon[maxPolygons];
			for (var i3 = 0; i3 < polygons.Count; i3++)
			{
				Nif.NifStream(out polygons[i3].numVertices, s, info);
				Nif.NifStream(out polygons[i3].vertexOffset, s, info);
				Nif.NifStream(out polygons[i3].numTriangles, s, info);
				Nif.NifStream(out polygons[i3].triangleOffset, s, info);
			}
			polygonIndices = new ushort[maxPolygons];
			for (var i3 = 0; i3 < polygonIndices.Count; i3++)
			{
				Nif.NifStream(out polygonIndices[i3], s, info);
			}
			Nif.NifStream(out polygonGrowBy, s, info);
			Nif.NifStream(out numPolygons, s, info);
			Nif.NifStream(out maxVertices, s, info);
			Nif.NifStream(out verticesGrowBy, s, info);
			Nif.NifStream(out maxIndices, s, info);
			Nif.NifStream(out indicesGrowBy, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			maxPolygons = (ushort)polygons.Count;
			Nif.NifStream(maxPolygons, s, info);
			for (var i3 = 0; i3 < polygons.Count; i3++)
			{
				Nif.NifStream(polygons[i3].numVertices, s, info);
				Nif.NifStream(polygons[i3].vertexOffset, s, info);
				Nif.NifStream(polygons[i3].numTriangles, s, info);
				Nif.NifStream(polygons[i3].triangleOffset, s, info);
			}
			for (var i3 = 0; i3 < polygonIndices.Count; i3++)
			{
				Nif.NifStream(polygonIndices[i3], s, info);
			}
			Nif.NifStream(polygonGrowBy, s, info);
			Nif.NifStream(numPolygons, s, info);
			Nif.NifStream(maxVertices, s, info);
			Nif.NifStream(verticesGrowBy, s, info);
			Nif.NifStream(maxIndices, s, info);
			Nif.NifStream(indicesGrowBy, s, info);
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
			maxPolygons = (ushort)polygons.Count;
			s.AppendLine($"      Max Polygons:  {maxPolygons}");
			array_output_count = 0;
			for (var i3 = 0; i3 < polygons.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Num Vertices:  {polygons[i3].numVertices}");
				s.AppendLine($"        Vertex Offset:  {polygons[i3].vertexOffset}");
				s.AppendLine($"        Num Triangles:  {polygons[i3].numTriangles}");
				s.AppendLine($"        Triangle Offset:  {polygons[i3].triangleOffset}");
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < polygonIndices.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Polygon Indices[{i3}]:  {polygonIndices[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Polygon Grow By:  {polygonGrowBy}");
			s.AppendLine($"      Num Polygons:  {numPolygons}");
			s.AppendLine($"      Max Vertices:  {maxVertices}");
			s.AppendLine($"      Vertices Grow By:  {verticesGrowBy}");
			s.AppendLine($"      Max Indices:  {maxIndices}");
			s.AppendLine($"      Indices Grow By:  {indicesGrowBy}");
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
