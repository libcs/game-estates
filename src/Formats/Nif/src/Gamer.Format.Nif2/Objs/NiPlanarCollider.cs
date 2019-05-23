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
	/*! LEGACY (pre-10.1) particle modifier. */
	public class NiPlanarCollider : NiParticleModifier
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPlanarCollider", NiParticleModifier.TYPE);

		/*! Usually 0? */
		internal ushort unknownShort;
		/*! Unknown. */
		internal float unknownFloat1;
		/*! Unknown. */
		internal float unknownFloat2;
		/*! Unknown. */
		internal ushort unknownShort2;
		/*! Unknown. */
		internal float unknownFloat3;
		/*! Unknown. */
		internal float unknownFloat4;
		/*! Unknown. */
		internal float unknownFloat5;
		/*! Unknown. */
		internal float unknownFloat6;
		/*! Unknown. */
		internal float unknownFloat7;
		/*! Unknown. */
		internal float unknownFloat8;
		/*! Unknown. */
		internal float unknownFloat9;
		/*! Unknown. */
		internal float unknownFloat10;
		/*! Unknown. */
		internal float unknownFloat11;
		/*! Unknown. */
		internal float unknownFloat12;
		/*! Unknown. */
		internal float unknownFloat13;
		/*! Unknown. */
		internal float unknownFloat14;
		/*! Unknown. */
		internal float unknownFloat15;
		/*! Unknown. */
		internal float unknownFloat16;
		public NiPlanarCollider()
		{
			unknownShort = (ushort)0;
			unknownFloat1 = 0.0f;
			unknownFloat2 = 0.0f;
			unknownShort2 = (ushort)0;
			unknownFloat3 = 0.0f;
			unknownFloat4 = 0.0f;
			unknownFloat5 = 0.0f;
			unknownFloat6 = 0.0f;
			unknownFloat7 = 0.0f;
			unknownFloat8 = 0.0f;
			unknownFloat9 = 0.0f;
			unknownFloat10 = 0.0f;
			unknownFloat11 = 0.0f;
			unknownFloat12 = 0.0f;
			unknownFloat13 = 0.0f;
			unknownFloat14 = 0.0f;
			unknownFloat15 = 0.0f;
			unknownFloat16 = 0.0f;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPlanarCollider();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			if (info.version >= 0x0A000100)
			{
				Nif.NifStream(out unknownShort, s, info);
			}
			Nif.NifStream(out unknownFloat1, s, info);
			Nif.NifStream(out unknownFloat2, s, info);
			if (info.version >= 0x04020200 && info.version <= 0x04020200)
			{
				Nif.NifStream(out unknownShort2, s, info);
			}
			Nif.NifStream(out unknownFloat3, s, info);
			Nif.NifStream(out unknownFloat4, s, info);
			Nif.NifStream(out unknownFloat5, s, info);
			Nif.NifStream(out unknownFloat6, s, info);
			Nif.NifStream(out unknownFloat7, s, info);
			Nif.NifStream(out unknownFloat8, s, info);
			Nif.NifStream(out unknownFloat9, s, info);
			Nif.NifStream(out unknownFloat10, s, info);
			Nif.NifStream(out unknownFloat11, s, info);
			Nif.NifStream(out unknownFloat12, s, info);
			Nif.NifStream(out unknownFloat13, s, info);
			Nif.NifStream(out unknownFloat14, s, info);
			Nif.NifStream(out unknownFloat15, s, info);
			Nif.NifStream(out unknownFloat16, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			if (info.version >= 0x0A000100)
			{
				Nif.NifStream(unknownShort, s, info);
			}
			Nif.NifStream(unknownFloat1, s, info);
			Nif.NifStream(unknownFloat2, s, info);
			if (info.version >= 0x04020200 && info.version <= 0x04020200)
			{
				Nif.NifStream(unknownShort2, s, info);
			}
			Nif.NifStream(unknownFloat3, s, info);
			Nif.NifStream(unknownFloat4, s, info);
			Nif.NifStream(unknownFloat5, s, info);
			Nif.NifStream(unknownFloat6, s, info);
			Nif.NifStream(unknownFloat7, s, info);
			Nif.NifStream(unknownFloat8, s, info);
			Nif.NifStream(unknownFloat9, s, info);
			Nif.NifStream(unknownFloat10, s, info);
			Nif.NifStream(unknownFloat11, s, info);
			Nif.NifStream(unknownFloat12, s, info);
			Nif.NifStream(unknownFloat13, s, info);
			Nif.NifStream(unknownFloat14, s, info);
			Nif.NifStream(unknownFloat15, s, info);
			Nif.NifStream(unknownFloat16, s, info);
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
			s.AppendLine($"      Unknown Short:  {unknownShort}");
			s.AppendLine($"      Unknown Float 1:  {unknownFloat1}");
			s.AppendLine($"      Unknown Float 2:  {unknownFloat2}");
			s.AppendLine($"      Unknown Short 2:  {unknownShort2}");
			s.AppendLine($"      Unknown Float 3:  {unknownFloat3}");
			s.AppendLine($"      Unknown Float 4:  {unknownFloat4}");
			s.AppendLine($"      Unknown Float 5:  {unknownFloat5}");
			s.AppendLine($"      Unknown Float 6:  {unknownFloat6}");
			s.AppendLine($"      Unknown Float 7:  {unknownFloat7}");
			s.AppendLine($"      Unknown Float 8:  {unknownFloat8}");
			s.AppendLine($"      Unknown Float 9:  {unknownFloat9}");
			s.AppendLine($"      Unknown Float 10:  {unknownFloat10}");
			s.AppendLine($"      Unknown Float 11:  {unknownFloat11}");
			s.AppendLine($"      Unknown Float 12:  {unknownFloat12}");
			s.AppendLine($"      Unknown Float 13:  {unknownFloat13}");
			s.AppendLine($"      Unknown Float 14:  {unknownFloat14}");
			s.AppendLine($"      Unknown Float 15:  {unknownFloat15}");
			s.AppendLine($"      Unknown Float 16:  {unknownFloat16}");
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
