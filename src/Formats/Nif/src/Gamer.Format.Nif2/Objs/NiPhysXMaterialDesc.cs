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
	/*! For serializing NxMaterialDesc objects. */
	public class NiPhysXMaterialDesc : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPhysXMaterialDesc", NiObject.TYPE);

		/*! index */
		internal ushort index;
		/*! numStates */
		internal uint numStates;
		/*! materialDescs */
		internal IList<NxMaterialDesc> materialDescs;
		public NiPhysXMaterialDesc()
		{
			index = (ushort)0;
			numStates = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPhysXMaterialDesc();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out index, s, info);
			Nif.NifStream(out numStates, s, info);
			materialDescs = new NxMaterialDesc[numStates];
			for (var i3 = 0; i3 < materialDescs.Count; i3++)
			{
				Nif.NifStream(out materialDescs[i3].dynamicFriction, s, info);
				Nif.NifStream(out materialDescs[i3].staticFriction, s, info);
				Nif.NifStream(out materialDescs[i3].restitution, s, info);
				Nif.NifStream(out materialDescs[i3].dynamicFrictionV, s, info);
				Nif.NifStream(out materialDescs[i3].staticFrictionV, s, info);
				Nif.NifStream(out materialDescs[i3].directionOfAnisotropy, s, info);
				Nif.NifStream(out materialDescs[i3].flags, s, info);
				Nif.NifStream(out materialDescs[i3].frictionCombineMode, s, info);
				Nif.NifStream(out materialDescs[i3].restitutionCombineMode, s, info);
				if (info.version <= 0x14020300)
				{
					Nif.NifStream(out materialDescs[i3].hasSpring, s, info);
					if (materialDescs[i3].hasSpring)
					{
						Nif.NifStream(out materialDescs[i3].spring.spring, s, info);
						Nif.NifStream(out materialDescs[i3].spring.damper, s, info);
						Nif.NifStream(out materialDescs[i3].spring.targetValue, s, info);
					}
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numStates = (uint)materialDescs.Count;
			Nif.NifStream(index, s, info);
			Nif.NifStream(numStates, s, info);
			for (var i3 = 0; i3 < materialDescs.Count; i3++)
			{
				Nif.NifStream(materialDescs[i3].dynamicFriction, s, info);
				Nif.NifStream(materialDescs[i3].staticFriction, s, info);
				Nif.NifStream(materialDescs[i3].restitution, s, info);
				Nif.NifStream(materialDescs[i3].dynamicFrictionV, s, info);
				Nif.NifStream(materialDescs[i3].staticFrictionV, s, info);
				Nif.NifStream(materialDescs[i3].directionOfAnisotropy, s, info);
				Nif.NifStream(materialDescs[i3].flags, s, info);
				Nif.NifStream(materialDescs[i3].frictionCombineMode, s, info);
				Nif.NifStream(materialDescs[i3].restitutionCombineMode, s, info);
				if (info.version <= 0x14020300)
				{
					Nif.NifStream(materialDescs[i3].hasSpring, s, info);
					if (materialDescs[i3].hasSpring)
					{
						Nif.NifStream(materialDescs[i3].spring.spring, s, info);
						Nif.NifStream(materialDescs[i3].spring.damper, s, info);
						Nif.NifStream(materialDescs[i3].spring.targetValue, s, info);
					}
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
			numStates = (uint)materialDescs.Count;
			s.AppendLine($"      Index:  {index}");
			s.AppendLine($"      Num States:  {numStates}");
			array_output_count = 0;
			for (var i3 = 0; i3 < materialDescs.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Dynamic Friction:  {materialDescs[i3].dynamicFriction}");
				s.AppendLine($"        Static Friction:  {materialDescs[i3].staticFriction}");
				s.AppendLine($"        Restitution:  {materialDescs[i3].restitution}");
				s.AppendLine($"        Dynamic Friction V:  {materialDescs[i3].dynamicFrictionV}");
				s.AppendLine($"        Static Friction V:  {materialDescs[i3].staticFrictionV}");
				s.AppendLine($"        Direction of Anisotropy:  {materialDescs[i3].directionOfAnisotropy}");
				s.AppendLine($"        Flags:  {materialDescs[i3].flags}");
				s.AppendLine($"        Friction Combine Mode:  {materialDescs[i3].frictionCombineMode}");
				s.AppendLine($"        Restitution Combine Mode:  {materialDescs[i3].restitutionCombineMode}");
				s.AppendLine($"        Has Spring:  {materialDescs[i3].hasSpring}");
				if (materialDescs[i3].hasSpring)
				{
					s.AppendLine($"          Spring:  {materialDescs[i3].spring.spring}");
					s.AppendLine($"          Damper:  {materialDescs[i3].spring.damper}");
					s.AppendLine($"          Target Value:  {materialDescs[i3].spring.targetValue}");
				}
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
