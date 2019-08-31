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
	 * A list of shapes.
	 * 
	 *         Do not put a bhkPackedNiTriStripsShape in the Sub Shapes. Use a
	 *         separate collision nodes without a list shape for those.
	 * 
	 *         Also, shapes collected in a bhkListShape may not have the correct
	 *         walking noise, so only use it for non-walkable objects.
	 */
	public class bhkListShape : bhkShapeCollection
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("bhkListShape", bhkShapeCollection.TYPE);

		/*! The number of sub shapes referenced. */
		internal uint numSubShapes;
		/*! List of shapes. */
		internal IList<bhkShape> subShapes;
		/*! The material of the shape. */
		internal HavokMaterial material;
		/*! childShapeProperty */
		internal hkWorldObjCinfoProperty childShapeProperty;
		/*! childFilterProperty */
		internal hkWorldObjCinfoProperty childFilterProperty;
		/*! Count. */
		internal uint numUnknownInts;
		/*! Unknown. */
		internal IList<uint> unknownInts;
		public bhkListShape()
		{
			numSubShapes = (uint)0;
			numUnknownInts = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new bhkListShape();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out numSubShapes, s, info);
			subShapes = new Ref[numSubShapes];
			for (var i3 = 0; i3 < subShapes.Count; i3++)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
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
			Nif.NifStream(out childShapeProperty.data, s, info);
			Nif.NifStream(out childShapeProperty.size, s, info);
			Nif.NifStream(out childShapeProperty.capacityAndFlags, s, info);
			Nif.NifStream(out childFilterProperty.data, s, info);
			Nif.NifStream(out childFilterProperty.size, s, info);
			Nif.NifStream(out childFilterProperty.capacityAndFlags, s, info);
			Nif.NifStream(out numUnknownInts, s, info);
			unknownInts = new uint[numUnknownInts];
			for (var i3 = 0; i3 < unknownInts.Count; i3++)
			{
				Nif.NifStream(out unknownInts[i3], s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numUnknownInts = (uint)unknownInts.Count;
			numSubShapes = (uint)subShapes.Count;
			Nif.NifStream(numSubShapes, s, info);
			for (var i3 = 0; i3 < subShapes.Count; i3++)
			{
				WriteRef((NiObject)subShapes[i3], s, info, link_map, missing_link_stack);
			}
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
			Nif.NifStream(childShapeProperty.data, s, info);
			Nif.NifStream(childShapeProperty.size, s, info);
			Nif.NifStream(childShapeProperty.capacityAndFlags, s, info);
			Nif.NifStream(childFilterProperty.data, s, info);
			Nif.NifStream(childFilterProperty.size, s, info);
			Nif.NifStream(childFilterProperty.capacityAndFlags, s, info);
			Nif.NifStream(numUnknownInts, s, info);
			for (var i3 = 0; i3 < unknownInts.Count; i3++)
			{
				Nif.NifStream(unknownInts[i3], s, info);
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
			numUnknownInts = (uint)unknownInts.Count;
			numSubShapes = (uint)subShapes.Count;
			s.AppendLine($"      Num Sub Shapes:  {numSubShapes}");
			array_output_count = 0;
			for (var i3 = 0; i3 < subShapes.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Sub Shapes[{i3}]:  {subShapes[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Unknown Int:  {material.unknownInt}");
			s.AppendLine($"      Material:  {material.material_ob}");
			s.AppendLine($"      Material:  {material.material_fo}");
			s.AppendLine($"      Material:  {material.material_sk}");
			s.AppendLine($"      Data:  {childShapeProperty.data}");
			s.AppendLine($"      Size:  {childShapeProperty.size}");
			s.AppendLine($"      Capacity and Flags:  {childShapeProperty.capacityAndFlags}");
			s.AppendLine($"      Data:  {childFilterProperty.data}");
			s.AppendLine($"      Size:  {childFilterProperty.size}");
			s.AppendLine($"      Capacity and Flags:  {childFilterProperty.capacityAndFlags}");
			s.AppendLine($"      Num Unknown Ints:  {numUnknownInts}");
			array_output_count = 0;
			for (var i3 = 0; i3 < unknownInts.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unknown Ints[{i3}]:  {unknownInts[i3]}");
				array_output_count++;
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			for (var i3 = 0; i3 < subShapes.Count; i3++)
			{
				subShapes[i3] = FixLink<bhkShape>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < subShapes.Count; i3++)
			{
				if (subShapes[i3] != null)
					refs.Add((NiObject)subShapes[i3]);
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < subShapes.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
