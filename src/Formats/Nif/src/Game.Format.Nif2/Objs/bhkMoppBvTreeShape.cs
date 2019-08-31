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
	/*! Memory optimized partial polytope bounding volume tree shape (not an entity). */
	public class bhkMoppBvTreeShape : bhkBvTreeShape
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("bhkMoppBvTreeShape", bhkBvTreeShape.TYPE);

		/*! The shape. */
		internal bhkShape shape;
		/*! Garbage data from memory. Referred to as User Data, Shape Collection, and Code. */
		internal Array3<uint> unused;
		/*! Scale. */
		internal float shapeScale;
		/*! Number of bytes for MOPP data. */
		internal uint moppDataSize;
		/*! Origin of the object in mopp coordinates. This is the minimum of all vertices in the packed shape along each axis, minus 0.1. */
		internal Vector3 origin;
		/*!
		 * The scaling factor to quantize the MOPP: the quantization factor is equal to 256*256 divided by this number. In Oblivion files, scale is taken equal to
		 * 256*256*254 / (size + 0.2) where size is the largest dimension of the bounding box of the packed shape.
		 */
		internal float scale;
		/*! Tells if MOPP Data was organized into smaller chunks (PS3) or not (PC) */
		internal MoppDataBuildType buildType;
		/*! The tree of bounding volume data. */
		internal IList<byte> moppData;
		public bhkMoppBvTreeShape()
		{
			shape = null;
			shapeScale = 1.0f;
			moppDataSize = (uint)0;
			scale = 0.0f;
			buildType = (MoppDataBuildType)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new bhkMoppBvTreeShape();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			for (var i3 = 0; i3 < 3; i3++)
			{
				Nif.NifStream(out unused[i3], s, info);
			}
			Nif.NifStream(out shapeScale, s, info);
			Nif.NifStream(out moppDataSize, s, info);
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(out origin, s, info);
				Nif.NifStream(out scale, s, info);
			}
			if ((info.userVersion2 > 34))
			{
				Nif.NifStream(out buildType, s, info);
			}
			moppData = new byte[moppDataSize];
			for (var i3 = 0; i3 < moppData.Count; i3++)
			{
				Nif.NifStream(out moppData[i3], s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			moppDataSize = moppDataSizeCalc(info);
			WriteRef((NiObject)shape, s, info, link_map, missing_link_stack);
			for (var i3 = 0; i3 < 3; i3++)
			{
				Nif.NifStream(unused[i3], s, info);
			}
			Nif.NifStream(shapeScale, s, info);
			Nif.NifStream(moppDataSize, s, info);
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(origin, s, info);
				Nif.NifStream(scale, s, info);
			}
			if ((info.userVersion2 > 34))
			{
				Nif.NifStream(buildType, s, info);
			}
			for (var i3 = 0; i3 < moppData.Count; i3++)
			{
				Nif.NifStream(moppData[i3], s, info);
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
			s.AppendLine($"      Shape:  {shape}");
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
				s.AppendLine($"        Unused[{i3}]:  {unused[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Shape Scale:  {shapeScale}");
			s.AppendLine($"      MOPP Data Size:  {moppDataSize}");
			s.AppendLine($"      Origin:  {origin}");
			s.AppendLine($"      Scale:  {scale}");
			s.AppendLine($"      Build Type:  {buildType}");
			array_output_count = 0;
			for (var i3 = 0; i3 < moppData.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        MOPP Data[{i3}]:  {moppData[i3]}");
				array_output_count++;
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			shape = FixLink<bhkShape>(objects, link_stack, missing_link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (shape != null)
				refs.Add((NiObject)shape);
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
