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
	/*! Havok objects that have a position in the world? */
	public class bhkWorldObject : bhkSerializable
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("bhkWorldObject", bhkSerializable.TYPE);

		/*! Link to the body for this collision object. */
		internal bhkShape shape;
		/*! Unknown. */
		internal uint unknownInt;
		/*! havokFilter */
		internal HavokFilter havokFilter;
		/*! Garbage data from memory. */
		internal Array4<byte> unused;
		/*! broadPhaseType */
		internal BroadPhaseType broadPhaseType;
		/*! unusedBytes */
		internal Array3<byte> unusedBytes;
		/*! cinfoProperty */
		internal hkWorldObjCinfoProperty cinfoProperty;
		public bhkWorldObject()
		{
			shape = null;
			unknownInt = (uint)0;
			broadPhaseType = (BroadPhaseType)1;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new bhkWorldObject();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			if (info.version <= 0x0A000102)
			{
				Nif.NifStream(out unknownInt, s, info);
			}
			if (info.version <= 0x14000005 && ((info.userVersion2 < 16)))
			{
				Nif.NifStream(out havokFilter.layer_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34)))
			{
				Nif.NifStream(out havokFilter.layer_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34)))
			{
				Nif.NifStream(out havokFilter.layer_sk, s, info);
			}
			Nif.NifStream(out havokFilter.flagsAndPartNumber, s, info);
			Nif.NifStream(out havokFilter.group, s, info);
			for (var i3 = 0; i3 < 4; i3++)
			{
				Nif.NifStream(out unused[i3], s, info);
			}
			Nif.NifStream(out broadPhaseType, s, info);
			for (var i3 = 0; i3 < 3; i3++)
			{
				Nif.NifStream(out unusedBytes[i3], s, info);
			}
			Nif.NifStream(out cinfoProperty.data, s, info);
			Nif.NifStream(out cinfoProperty.size, s, info);
			Nif.NifStream(out cinfoProperty.capacityAndFlags, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			WriteRef((NiObject)shape, s, info, link_map, missing_link_stack);
			if (info.version <= 0x0A000102)
			{
				Nif.NifStream(unknownInt, s, info);
			}
			if (info.version <= 0x14000005 && ((info.userVersion2 < 16)))
			{
				Nif.NifStream(havokFilter.layer_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34)))
			{
				Nif.NifStream(havokFilter.layer_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34)))
			{
				Nif.NifStream(havokFilter.layer_sk, s, info);
			}
			Nif.NifStream(havokFilter.flagsAndPartNumber, s, info);
			Nif.NifStream(havokFilter.group, s, info);
			for (var i3 = 0; i3 < 4; i3++)
			{
				Nif.NifStream(unused[i3], s, info);
			}
			Nif.NifStream(broadPhaseType, s, info);
			for (var i3 = 0; i3 < 3; i3++)
			{
				Nif.NifStream(unusedBytes[i3], s, info);
			}
			Nif.NifStream(cinfoProperty.data, s, info);
			Nif.NifStream(cinfoProperty.size, s, info);
			Nif.NifStream(cinfoProperty.capacityAndFlags, s, info);
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
			s.AppendLine($"      Unknown Int:  {unknownInt}");
			s.AppendLine($"      Layer:  {havokFilter.layer_ob}");
			s.AppendLine($"      Layer:  {havokFilter.layer_fo}");
			s.AppendLine($"      Layer:  {havokFilter.layer_sk}");
			s.AppendLine($"      Flags and Part Number:  {havokFilter.flagsAndPartNumber}");
			s.AppendLine($"      Group:  {havokFilter.group}");
			array_output_count = 0;
			for (var i3 = 0; i3 < 4; i3++)
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
			s.AppendLine($"      Broad Phase Type:  {broadPhaseType}");
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
				s.AppendLine($"        Unused Bytes[{i3}]:  {unusedBytes[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Data:  {cinfoProperty.data}");
			s.AppendLine($"      Size:  {cinfoProperty.size}");
			s.AppendLine($"      Capacity and Flags:  {cinfoProperty.capacityAndFlags}");
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
