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
	/*! Fallout 4 Item Slot Parent */
	public class BSConnectPoint__Parents : NiExtraData
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSConnectPoint::Parents", NiExtraData.TYPE);

		/*! numConnectPoints */
		internal uint numConnectPoints;
		/*! connectPoints */
		internal IList<BSConnectPoint> connectPoints;
		public BSConnectPoint__Parents()
		{
			numConnectPoints = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSConnectPoint__Parents();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out numConnectPoints, s, info);
			connectPoints = new BSConnectPoint[numConnectPoints];
			for (var i3 = 0; i3 < connectPoints.Count; i3++)
			{
				Nif.NifStream(out connectPoints[i3].parent, s, info);
				Nif.NifStream(out connectPoints[i3].name, s, info);
				Nif.NifStream(out connectPoints[i3].rotation, s, info);
				Nif.NifStream(out connectPoints[i3].translation, s, info);
				Nif.NifStream(out connectPoints[i3].scale, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numConnectPoints = (uint)connectPoints.Count;
			Nif.NifStream(numConnectPoints, s, info);
			for (var i3 = 0; i3 < connectPoints.Count; i3++)
			{
				Nif.NifStream(connectPoints[i3].parent, s, info);
				Nif.NifStream(connectPoints[i3].name, s, info);
				Nif.NifStream(connectPoints[i3].rotation, s, info);
				Nif.NifStream(connectPoints[i3].translation, s, info);
				Nif.NifStream(connectPoints[i3].scale, s, info);
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
			numConnectPoints = (uint)connectPoints.Count;
			s.AppendLine($"      Num Connect Points:  {numConnectPoints}");
			array_output_count = 0;
			for (var i3 = 0; i3 < connectPoints.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Parent:  {connectPoints[i3].parent}");
				s.AppendLine($"        Name:  {connectPoints[i3].name}");
				s.AppendLine($"        Rotation:  {connectPoints[i3].rotation}");
				s.AppendLine($"        Translation:  {connectPoints[i3].translation}");
				s.AppendLine($"        Scale:  {connectPoints[i3].scale}");
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
