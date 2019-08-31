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
	/*! An interpolator for transform keyframes. */
	public class NiTransformInterpolator : NiKeyBasedInterpolator
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiTransformInterpolator", NiKeyBasedInterpolator.TYPE);

		/*! transform */
		internal NiQuatTransform transform;
		/*! data */
		internal NiTransformData data;
		public NiTransformInterpolator()
		{
			data = null;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiTransformInterpolator();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out transform.translation, s, info);
			Nif.NifStream(out transform.rotation, s, info);
			Nif.NifStream(out transform.scale, s, info);
			if (info.version <= 0x0A01006D)
			{
				for (var i4 = 0; i4 < 3; i4++)
				{
					{
						Nif.NifStream(out bool tmp, s, info); transform.trsValid[i4] = tmp;
					}
				}
			}
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(transform.translation, s, info);
			Nif.NifStream(transform.rotation, s, info);
			Nif.NifStream(transform.scale, s, info);
			if (info.version <= 0x0A01006D)
			{
				for (var i4 = 0; i4 < 3; i4++)
				{
					{
						bool tmp = transform.trsValid[i4]; Nif.NifStream(tmp, s, info);
					}
				}
			}
			WriteRef((NiObject)data, s, info, link_map, missing_link_stack);
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
			s.AppendLine($"      Translation:  {transform.translation}");
			s.AppendLine($"      Rotation:  {transform.rotation}");
			s.AppendLine($"      Scale:  {transform.scale}");
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
				s.AppendLine($"        TRS Valid[{i3}]:  {transform.trsValid[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Data:  {data}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			data = FixLink<NiTransformData>(objects, link_stack, missing_link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (data != null)
				refs.Add((NiObject)data);
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
