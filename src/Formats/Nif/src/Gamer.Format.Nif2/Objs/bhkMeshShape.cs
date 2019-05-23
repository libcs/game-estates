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
	/*! bhkMeshShape */
	public class bhkMeshShape : bhkShape
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("bhkMeshShape", bhkShape.TYPE);

		/*! Unknown. */
		internal Array2<uint> unknowns;
		/*! radius */
		internal float radius;
		/*! unused2 */
		internal Array8<byte> unused2;
		/*! scale */
		internal Vector4 scale;
		/*! numShapeProperties */
		internal uint numShapeProperties;
		/*! shapeProperties */
		internal IList<hkWorldObjCinfoProperty> shapeProperties;
		/*! Unknown. */
		internal Array3<int> unknown2;
		/*! The number of strips data objects referenced. */
		internal uint numStripsData;
		/*! Refers to a bunch of NiTriStripsData objects that make up this shape. */
		internal IList<NiTriStripsData> stripsData;
		public bhkMeshShape()
		{
			radius = 0.0f;
			numShapeProperties = (uint)0;
			numStripsData = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new bhkMeshShape();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			for (var i3 = 0; i3 < 2; i3++)
			{
				Nif.NifStream(out unknowns[i3], s, info);
			}
			Nif.NifStream(out radius, s, info);
			for (var i3 = 0; i3 < 8; i3++)
			{
				Nif.NifStream(out unused2[i3], s, info);
			}
			Nif.NifStream(out scale, s, info);
			Nif.NifStream(out numShapeProperties, s, info);
			shapeProperties = new hkWorldObjCinfoProperty[numShapeProperties];
			for (var i3 = 0; i3 < shapeProperties.Count; i3++)
			{
				Nif.NifStream(out shapeProperties[i3].data, s, info);
				Nif.NifStream(out shapeProperties[i3].size, s, info);
				Nif.NifStream(out shapeProperties[i3].capacityAndFlags, s, info);
			}
			for (var i3 = 0; i3 < 3; i3++)
			{
				Nif.NifStream(out unknown2[i3], s, info);
			}
			if (info.version <= 0x0A000100)
			{
				Nif.NifStream(out numStripsData, s, info);
				stripsData = new Ref[numStripsData];
				for (var i4 = 0; i4 < stripsData.Count; i4++)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numStripsData = (uint)stripsData.Count;
			numShapeProperties = (uint)shapeProperties.Count;
			for (var i3 = 0; i3 < 2; i3++)
			{
				Nif.NifStream(unknowns[i3], s, info);
			}
			Nif.NifStream(radius, s, info);
			for (var i3 = 0; i3 < 8; i3++)
			{
				Nif.NifStream(unused2[i3], s, info);
			}
			Nif.NifStream(scale, s, info);
			Nif.NifStream(numShapeProperties, s, info);
			for (var i3 = 0; i3 < shapeProperties.Count; i3++)
			{
				Nif.NifStream(shapeProperties[i3].data, s, info);
				Nif.NifStream(shapeProperties[i3].size, s, info);
				Nif.NifStream(shapeProperties[i3].capacityAndFlags, s, info);
			}
			for (var i3 = 0; i3 < 3; i3++)
			{
				Nif.NifStream(unknown2[i3], s, info);
			}
			if (info.version <= 0x0A000100)
			{
				Nif.NifStream(numStripsData, s, info);
				for (var i4 = 0; i4 < stripsData.Count; i4++)
				{
					WriteRef((NiObject)stripsData[i4], s, info, link_map, missing_link_stack);
				}
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
			numStripsData = (uint)stripsData.Count;
			numShapeProperties = (uint)shapeProperties.Count;
			array_output_count = 0;
			for (var i3 = 0; i3 < 2; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unknowns[{i3}]:  {unknowns[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Radius:  {radius}");
			array_output_count = 0;
			for (var i3 = 0; i3 < 8; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unused 2[{i3}]:  {unused2[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Scale:  {scale}");
			s.AppendLine($"      Num Shape Properties:  {numShapeProperties}");
			array_output_count = 0;
			for (var i3 = 0; i3 < shapeProperties.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Data:  {shapeProperties[i3].data}");
				s.AppendLine($"        Size:  {shapeProperties[i3].size}");
				s.AppendLine($"        Capacity and Flags:  {shapeProperties[i3].capacityAndFlags}");
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < 3; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unknown 2[{i3}]:  {unknown2[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Num Strips Data:  {numStripsData}");
			array_output_count = 0;
			for (var i3 = 0; i3 < stripsData.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Strips Data[{i3}]:  {stripsData[i3]}");
				array_output_count++;
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			if (info.version <= 0x0A000100)
			{
				for (var i4 = 0; i4 < stripsData.Count; i4++)
				{
					stripsData[i4] = FixLink<NiTriStripsData>(objects, link_stack, missing_link_stack, info);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < stripsData.Count; i3++)
			{
				if (stripsData[i3] != null)
					refs.Add((NiObject)stripsData[i3]);
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < stripsData.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
