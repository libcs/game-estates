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
	/*! A particle system. */
	public class NiParticleSystem : NiParticles
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiParticleSystem", NiParticles.TYPE);

		/*! farBegin */
		internal ushort farBegin;
		/*! farEnd */
		internal ushort farEnd;
		/*! nearBegin */
		internal ushort nearBegin;
		/*! nearEnd */
		internal ushort nearEnd;
		/*! data */
		internal NiPSysData data;
		/*! If true, Particles are birthed into world space.  If false, Particles are birthed into object space. */
		internal bool worldSpace;
		/*! The number of modifier references. */
		internal uint numModifiers;
		/*! The list of particle modifiers. */
		internal IList<NiPSysModifier> modifiers;
		public NiParticleSystem()
		{
			farBegin = (ushort)0;
			farEnd = (ushort)0;
			nearBegin = (ushort)0;
			nearEnd = (ushort)0;
			data = null;
			worldSpace = 1;
			numModifiers = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiParticleSystem();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			if ((info.userVersion2 >= 83))
			{
				Nif.NifStream(out farBegin, s, info);
				Nif.NifStream(out farEnd, s, info);
				Nif.NifStream(out nearBegin, s, info);
				Nif.NifStream(out nearEnd, s, info);
			}
			if ((info.userVersion2 >= 100))
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(out worldSpace, s, info);
				Nif.NifStream(out numModifiers, s, info);
				modifiers = new Ref[numModifiers];
				for (var i4 = 0; i4 < modifiers.Count; i4++)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numModifiers = (uint)modifiers.Count;
			if ((info.userVersion2 >= 83))
			{
				Nif.NifStream(farBegin, s, info);
				Nif.NifStream(farEnd, s, info);
				Nif.NifStream(nearBegin, s, info);
				Nif.NifStream(nearEnd, s, info);
			}
			if ((info.userVersion2 >= 100))
			{
				WriteRef((NiObject)data, s, info, link_map, missing_link_stack);
			}
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(worldSpace, s, info);
				Nif.NifStream(numModifiers, s, info);
				for (var i4 = 0; i4 < modifiers.Count; i4++)
				{
					WriteRef((NiObject)modifiers[i4], s, info, link_map, missing_link_stack);
				}
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
			numModifiers = (uint)modifiers.Count;
			s.AppendLine($"      Far Begin:  {farBegin}");
			s.AppendLine($"      Far End:  {farEnd}");
			s.AppendLine($"      Near Begin:  {nearBegin}");
			s.AppendLine($"      Near End:  {nearEnd}");
			s.AppendLine($"      Data:  {data}");
			s.AppendLine($"      World Space:  {worldSpace}");
			s.AppendLine($"      Num Modifiers:  {numModifiers}");
			array_output_count = 0;
			for (var i3 = 0; i3 < modifiers.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Modifiers[{i3}]:  {modifiers[i3]}");
				array_output_count++;
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			if ((info.userVersion2 >= 100))
			{
				data = FixLink<NiPSysData>(objects, link_stack, missing_link_stack, info);
			}
			if (info.version >= 0x0A010000)
			{
				for (var i4 = 0; i4 < modifiers.Count; i4++)
				{
					modifiers[i4] = FixLink<NiPSysModifier>(objects, link_stack, missing_link_stack, info);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (data != null)
				refs.Add((NiObject)data);
			for (var i3 = 0; i3 < modifiers.Count; i3++)
			{
				if (modifiers[i3] != null)
					refs.Add((NiObject)modifiers[i3]);
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < modifiers.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
