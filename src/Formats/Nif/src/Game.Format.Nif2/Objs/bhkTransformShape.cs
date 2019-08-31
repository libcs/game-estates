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
	/*! Transforms a shape. */
	public class bhkTransformShape : bhkShape
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("bhkTransformShape", bhkShape.TYPE);

		/*! The shape that this object transforms. */
		internal bhkShape shape;
		/*! The material of the shape. */
		internal HavokMaterial material;
		/*! radius */
		internal float radius;
		/*! Garbage data from memory. */
		internal Array8<byte> unused;
		/*! A transform matrix. */
		internal Matrix44 transform;
		public bhkTransformShape()
		{
			shape = null;
			radius = 0.0f;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new bhkTransformShape();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			if (info.version <= 0x0A000102)
			{
				Nif.NifStream(out material.unknownInt, s, info);
			}
			if (info.version <= 0x14000005 && ((info.userVersion2 < 16)))
			{
				Nif.NifStream(out material.material_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34)))
			{
				Nif.NifStream(out material.material_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34)))
			{
				Nif.NifStream(out material.material_sk, s, info);
			}
			Nif.NifStream(out radius, s, info);
			for (var i3 = 0; i3 < 8; i3++)
			{
				Nif.NifStream(out unused[i3], s, info);
			}
			Nif.NifStream(out transform, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			WriteRef((NiObject)shape, s, info, link_map, missing_link_stack);
			if (info.version <= 0x0A000102)
			{
				Nif.NifStream(material.unknownInt, s, info);
			}
			if (info.version <= 0x14000005 && ((info.userVersion2 < 16)))
			{
				Nif.NifStream(material.material_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34)))
			{
				Nif.NifStream(material.material_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34)))
			{
				Nif.NifStream(material.material_sk, s, info);
			}
			Nif.NifStream(radius, s, info);
			for (var i3 = 0; i3 < 8; i3++)
			{
				Nif.NifStream(unused[i3], s, info);
			}
			Nif.NifStream(transform, s, info);
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
			s.AppendLine($"      Unknown Int:  {material.unknownInt}");
			s.AppendLine($"      Material:  {material.material_ob}");
			s.AppendLine($"      Material:  {material.material_fo}");
			s.AppendLine($"      Material:  {material.material_sk}");
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
				s.AppendLine($"        Unused[{i3}]:  {unused[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Transform:  {transform}");
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
