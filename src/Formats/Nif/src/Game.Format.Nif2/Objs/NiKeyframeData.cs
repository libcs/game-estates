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
	 * DEPRECATED (10.2), RENAMED (10.2) to NiTransformData.
	 *         Wrapper for transformation animation keys.
	 */
	public class NiKeyframeData : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiKeyframeData", NiObject.TYPE);

		/*!
		 * The number of quaternion rotation keys. If the rotation type is XYZ (type 4) then this *must* be set to 1, and in this case the actual number of keys is stored
		 * in the XYZ Rotations field.
		 */
		internal uint numRotationKeys;
		/*! The type of interpolation to use for rotation.  Can also be 4 to indicate that separate X, Y, and Z values are used for the rotation instead of Quaternions. */
		internal KeyType rotationType;
		/*! The rotation keys if Quaternion rotation is used. */
		internal IList<Key<Quaternion>> quaternionKeys;
		/*! order */
		internal float order;
		/*! Individual arrays of keys for rotating X, Y, and Z individually. */
		internal Array3<KeyGroup<float>> xyzRotations;
		/*! Translation keys. */
		internal KeyGroup<Vector3> translations;
		/*! Scale keys. */
		internal KeyGroup<float> scales;
		public NiKeyframeData()
		{
			numRotationKeys = (uint)0;
			rotationType = (KeyType)0;
			order = 0.0f;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiKeyframeData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out numRotationKeys, s, info);
			if ((numRotationKeys != 0))
			{
				Nif.NifStream(out rotationType, s, info);
			}
			if ((rotationType != 4))
			{
				quaternionKeys = new Key[numRotationKeys];
				for (var i4 = 0; i4 < quaternionKeys.Count; i4++)
				{
					Nif.NifStream(out quaternionKeys[i4], s, info, rotationType);
				}
			}
			if (info.version <= 0x0A010000)
			{
				if ((rotationType == 4))
				{
					Nif.NifStream(out order, s, info);
				}
			}
			if ((rotationType == 4))
			{
				for (var i4 = 0; i4 < 3; i4++)
				{
					Nif.NifStream(out xyzRotations[i4].numKeys, s, info);
					if ((xyzRotations[i4].numKeys != 0))
					{
						Nif.NifStream(out xyzRotations[i4].interpolation, s, info);
					}
					xyzRotations[i4].keys = new Key[xyzRotations[i4].numKeys];
					for (var i5 = 0; i5 < xyzRotations[i4].keys.Count; i5++)
					{
						Nif.NifStream(out xyzRotations[i4].keys[i5], s, info, xyzRotations[i4].interpolation);
					}
				}
			}
			Nif.NifStream(out translations.numKeys, s, info);
			if ((translations.numKeys != 0))
			{
				Nif.NifStream(out translations.interpolation, s, info);
			}
			translations.keys = new Key[translations.numKeys];
			for (var i3 = 0; i3 < translations.keys.Count; i3++)
			{
				Nif.NifStream(out translations.keys[i3], s, info, translations.interpolation);
			}
			Nif.NifStream(out scales.numKeys, s, info);
			if ((scales.numKeys != 0))
			{
				Nif.NifStream(out scales.interpolation, s, info);
			}
			scales.keys = new Key[scales.numKeys];
			for (var i3 = 0; i3 < scales.keys.Count; i3++)
			{
				Nif.NifStream(out scales.keys[i3], s, info, scales.interpolation);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(numRotationKeys, s, info);
			if ((numRotationKeys != 0))
			{
				Nif.NifStream(rotationType, s, info);
			}
			if ((rotationType != 4))
			{
				for (var i4 = 0; i4 < quaternionKeys.Count; i4++)
				{
					Nif.NifStream(quaternionKeys[i4], s, info, rotationType);
				}
			}
			if (info.version <= 0x0A010000)
			{
				if ((rotationType == 4))
				{
					Nif.NifStream(order, s, info);
				}
			}
			if ((rotationType == 4))
			{
				for (var i4 = 0; i4 < 3; i4++)
				{
					xyzRotations[i4].numKeys = (uint)xyzRotations[i4].keys.Count;
					Nif.NifStream(xyzRotations[i4].numKeys, s, info);
					if ((xyzRotations[i4].numKeys != 0))
					{
						Nif.NifStream(xyzRotations[i4].interpolation, s, info);
					}
					for (var i5 = 0; i5 < xyzRotations[i4].keys.Count; i5++)
					{
						Nif.NifStream(xyzRotations[i4].keys[i5], s, info, xyzRotations[i4].interpolation);
					}
				}
			}
			translations.numKeys = (uint)translations.keys.Count;
			Nif.NifStream(translations.numKeys, s, info);
			if ((translations.numKeys != 0))
			{
				Nif.NifStream(translations.interpolation, s, info);
			}
			for (var i3 = 0; i3 < translations.keys.Count; i3++)
			{
				Nif.NifStream(translations.keys[i3], s, info, translations.interpolation);
			}
			scales.numKeys = (uint)scales.keys.Count;
			Nif.NifStream(scales.numKeys, s, info);
			if ((scales.numKeys != 0))
			{
				Nif.NifStream(scales.interpolation, s, info);
			}
			for (var i3 = 0; i3 < scales.keys.Count; i3++)
			{
				Nif.NifStream(scales.keys[i3], s, info, scales.interpolation);
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
			s.AppendLine($"      Num Rotation Keys:  {numRotationKeys}");
			if ((numRotationKeys != 0))
			{
				s.AppendLine($"        Rotation Type:  {rotationType}");
			}
			if ((rotationType != 4))
			{
				array_output_count = 0;
				for (var i4 = 0; i4 < quaternionKeys.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Quaternion Keys[{i4}]:  {quaternionKeys[i4]}");
					array_output_count++;
				}
			}
			if ((rotationType == 4))
			{
				s.AppendLine($"        Order:  {order}");
				array_output_count = 0;
				for (var i4 = 0; i4 < 3; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					xyzRotations[i4].numKeys = (uint)xyzRotations[i4].keys.Count;
					s.AppendLine($"          Num Keys:  {xyzRotations[i4].numKeys}");
					if ((xyzRotations[i4].numKeys != 0))
					{
						s.AppendLine($"            Interpolation:  {xyzRotations[i4].interpolation}");
					}
					array_output_count = 0;
					for (var i5 = 0; i5 < xyzRotations[i4].keys.Count; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						{
							s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
							break;
						}
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
							break;
						s.AppendLine($"            Keys[{i5}]:  {xyzRotations[i4].keys[i5]}");
						array_output_count++;
					}
				}
			}
			translations.numKeys = (uint)translations.keys.Count;
			s.AppendLine($"      Num Keys:  {translations.numKeys}");
			if ((translations.numKeys != 0))
			{
				s.AppendLine($"        Interpolation:  {translations.interpolation}");
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < translations.keys.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Keys[{i3}]:  {translations.keys[i3]}");
				array_output_count++;
			}
			scales.numKeys = (uint)scales.keys.Count;
			s.AppendLine($"      Num Keys:  {scales.numKeys}");
			if ((scales.numKeys != 0))
			{
				s.AppendLine($"        Interpolation:  {scales.interpolation}");
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < scales.keys.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Keys[{i3}]:  {scales.keys[i3]}");
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
