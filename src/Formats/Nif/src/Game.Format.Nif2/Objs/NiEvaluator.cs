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
	/*! NiEvaluator */
	public class NiEvaluator : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiEvaluator", NiObject.TYPE);

		/*! The name of the animated NiAVObject. */
		internal IndexString nodeName;
		/*! The RTTI type of the NiProperty the controller is attached to, if applicable. */
		internal IndexString propertyType;
		/*! The RTTI type of the NiTimeController. */
		internal IndexString controllerType;
		/*! An ID that can uniquely identify the controller among others of the same type on the same NiObjectNET. */
		internal IndexString controllerId;
		/*! An ID that can uniquely identify the interpolator among others of the same type on the same NiObjectNET. */
		internal IndexString interpolatorId;
		/*!
		 * Channel Indices are BASE/POS = 0, ROT = 1, SCALE = 2, FLAG = 3
		 *             Channel Types are:
		 *              INVALID = 0, COLOR, BOOL, FLOAT, POINT3, ROT = 5
		 *             Any channel may be | 0x40 which means POSED
		 *             The FLAG (3) channel flags affects the whole evaluator:
		 *              REFERENCED = 0x1, TRANSFORM = 0x2, ALWAYSUPDATE = 0x4, SHUTDOWN = 0x8
		 */
		internal Array4<byte> channelTypes;
		public NiEvaluator()
		{
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiEvaluator();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out nodeName, s, info);
			Nif.NifStream(out propertyType, s, info);
			Nif.NifStream(out controllerType, s, info);
			Nif.NifStream(out controllerId, s, info);
			Nif.NifStream(out interpolatorId, s, info);
			for (var i3 = 0; i3 < 4; i3++)
			{
				Nif.NifStream(out channelTypes[i3], s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(nodeName, s, info);
			Nif.NifStream(propertyType, s, info);
			Nif.NifStream(controllerType, s, info);
			Nif.NifStream(controllerId, s, info);
			Nif.NifStream(interpolatorId, s, info);
			for (var i3 = 0; i3 < 4; i3++)
			{
				Nif.NifStream(channelTypes[i3], s, info);
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
			s.AppendLine($"      Node Name:  {nodeName}");
			s.AppendLine($"      Property Type:  {propertyType}");
			s.AppendLine($"      Controller Type:  {controllerType}");
			s.AppendLine($"      Controller ID:  {controllerId}");
			s.AppendLine($"      Interpolator ID:  {interpolatorId}");
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
				s.AppendLine($"        Channel Types[{i3}]:  {channelTypes[i3]}");
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
