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
	/*! A hinge constraint. */
	public class bhkHingeConstraint : bhkConstraint
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("bhkHingeConstraint", bhkConstraint.TYPE);

		/*! Hinge constraing. */
		internal HingeDescriptor hinge;
		public bhkHingeConstraint()
		{
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new bhkHingeConstraint();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			if (info.version <= 0x14000005)
			{
				Nif.NifStream(out hinge.pivotA, s, info);
				Nif.NifStream(out hinge.perpAxisInA1, s, info);
				Nif.NifStream(out hinge.perpAxisInA2, s, info);
				Nif.NifStream(out hinge.pivotB, s, info);
				Nif.NifStream(out hinge.axisB, s, info);
			}
			if (info.version >= 0x14020007)
			{
				Nif.NifStream(out hinge.axisA, s, info);
				Nif.NifStream(out (Vector4)hinge.perpAxisInA1, s, info);
				Nif.NifStream(out (Vector4)hinge.perpAxisInA2, s, info);
				Nif.NifStream(out (Vector4)hinge.pivotA, s, info);
				Nif.NifStream(out (Vector4)hinge.axisB, s, info);
				Nif.NifStream(out hinge.perpAxisInB1, s, info);
				Nif.NifStream(out hinge.perpAxisInB2, s, info);
				Nif.NifStream(out (Vector4)hinge.pivotB, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			if (info.version <= 0x14000005)
			{
				Nif.NifStream(hinge.pivotA, s, info);
				Nif.NifStream(hinge.perpAxisInA1, s, info);
				Nif.NifStream(hinge.perpAxisInA2, s, info);
				Nif.NifStream(hinge.pivotB, s, info);
				Nif.NifStream(hinge.axisB, s, info);
			}
			if (info.version >= 0x14020007)
			{
				Nif.NifStream(hinge.axisA, s, info);
				Nif.NifStream((Vector4)hinge.perpAxisInA1, s, info);
				Nif.NifStream((Vector4)hinge.perpAxisInA2, s, info);
				Nif.NifStream((Vector4)hinge.pivotA, s, info);
				Nif.NifStream((Vector4)hinge.axisB, s, info);
				Nif.NifStream(hinge.perpAxisInB1, s, info);
				Nif.NifStream(hinge.perpAxisInB2, s, info);
				Nif.NifStream((Vector4)hinge.pivotB, s, info);
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
			s.Append(base.AsString());
			s.AppendLine($"      Pivot A:  {hinge.pivotA}");
			s.AppendLine($"      Perp Axis In A1:  {hinge.perpAxisInA1}");
			s.AppendLine($"      Perp Axis In A2:  {hinge.perpAxisInA2}");
			s.AppendLine($"      Pivot B:  {hinge.pivotB}");
			s.AppendLine($"      Axis B:  {hinge.axisB}");
			s.AppendLine($"      Axis A:  {hinge.axisA}");
			s.AppendLine($"      Perp Axis In B1:  {hinge.perpAxisInB1}");
			s.AppendLine($"      Perp Axis In B2:  {hinge.perpAxisInB2}");
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
