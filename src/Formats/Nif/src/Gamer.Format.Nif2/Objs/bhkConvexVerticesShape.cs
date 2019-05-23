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
	 * A convex shape built from vertices. Note that if the shape is used in
	 *         a non-static object (such as clutter), then they will simply fall
	 *         through ground when they are under a bhkListShape.
	 */
	public class bhkConvexVerticesShape : bhkConvexShape
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("bhkConvexVerticesShape", bhkConvexShape.TYPE);

		/*! verticesProperty */
		internal hkWorldObjCinfoProperty verticesProperty;
		/*! normalsProperty */
		internal hkWorldObjCinfoProperty normalsProperty;
		/*! Number of vertices. */
		internal uint numVertices;
		/*! Vertices. Fourth component is 0. Lexicographically sorted. */
		internal IList<Vector4> vertices;
		/*! The number of half spaces. */
		internal uint numNormals;
		/*!
		 * Half spaces as determined by the set of vertices above. First three components define the normal pointing to the exterior, fourth component is the signed
		 * distance of the separating plane to the origin: it is minus the dot product of v and n, where v is any vertex on the separating plane, and n is the normal.
		 * Lexicographically sorted.
		 */
		internal IList<Vector4> normals;
		public bhkConvexVerticesShape()
		{
			numVertices = (uint)0;
			numNormals = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new bhkConvexVerticesShape();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out verticesProperty.data, s, info);
			Nif.NifStream(out verticesProperty.size, s, info);
			Nif.NifStream(out verticesProperty.capacityAndFlags, s, info);
			Nif.NifStream(out normalsProperty.data, s, info);
			Nif.NifStream(out normalsProperty.size, s, info);
			Nif.NifStream(out normalsProperty.capacityAndFlags, s, info);
			Nif.NifStream(out numVertices, s, info);
			vertices = new Vector4[numVertices];
			for (var i3 = 0; i3 < vertices.Count; i3++)
			{
				Nif.NifStream(out vertices[i3], s, info);
			}
			Nif.NifStream(out numNormals, s, info);
			normals = new Vector4[numNormals];
			for (var i3 = 0; i3 < normals.Count; i3++)
			{
				Nif.NifStream(out normals[i3], s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numNormals = (uint)normals.Count;
			numVertices = (uint)vertices.Count;
			Nif.NifStream(verticesProperty.data, s, info);
			Nif.NifStream(verticesProperty.size, s, info);
			Nif.NifStream(verticesProperty.capacityAndFlags, s, info);
			Nif.NifStream(normalsProperty.data, s, info);
			Nif.NifStream(normalsProperty.size, s, info);
			Nif.NifStream(normalsProperty.capacityAndFlags, s, info);
			Nif.NifStream(numVertices, s, info);
			for (var i3 = 0; i3 < vertices.Count; i3++)
			{
				Nif.NifStream(vertices[i3], s, info);
			}
			Nif.NifStream(numNormals, s, info);
			for (var i3 = 0; i3 < normals.Count; i3++)
			{
				Nif.NifStream(normals[i3], s, info);
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
			numNormals = (uint)normals.Count;
			numVertices = (uint)vertices.Count;
			s.AppendLine($"      Data:  {verticesProperty.data}");
			s.AppendLine($"      Size:  {verticesProperty.size}");
			s.AppendLine($"      Capacity and Flags:  {verticesProperty.capacityAndFlags}");
			s.AppendLine($"      Data:  {normalsProperty.data}");
			s.AppendLine($"      Size:  {normalsProperty.size}");
			s.AppendLine($"      Capacity and Flags:  {normalsProperty.capacityAndFlags}");
			s.AppendLine($"      Num Vertices:  {numVertices}");
			array_output_count = 0;
			for (var i3 = 0; i3 < vertices.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Vertices[{i3}]:  {vertices[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Num Normals:  {numNormals}");
			array_output_count = 0;
			for (var i3 = 0; i3 < normals.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Normals[{i3}]:  {normals[i3]}");
				array_output_count++;
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
