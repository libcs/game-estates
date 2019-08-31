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
	/*! Emits particles from one or more NiMesh objects. A random mesh emitter is selected for each particle emission. */
	public class NiPSMeshEmitter : NiPSEmitter
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPSMeshEmitter", NiPSEmitter.TYPE);

		/*! numMeshEmitters */
		internal uint numMeshEmitters;
		/*! meshEmitters */
		internal IList<NiMesh> meshEmitters;
		/*! emitAxis */
		internal Vector3 emitAxis;
		/*! emitterObject */
		internal NiAVObject emitterObject;
		/*! meshEmissionType */
		internal EmitFrom meshEmissionType;
		/*! initialVelocityType */
		internal VelocityType initialVelocityType;
		public NiPSMeshEmitter()
		{
			numMeshEmitters = (uint)0;
			emitterObject = null;
			meshEmissionType = (EmitFrom)0;
			initialVelocityType = (VelocityType)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPSMeshEmitter();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out numMeshEmitters, s, info);
			meshEmitters = new *[numMeshEmitters];
			for (var i3 = 0; i3 < meshEmitters.Count; i3++)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			if (info.version <= 0x14060000)
			{
				Nif.NifStream(out emitAxis, s, info);
			}
			if (info.version >= 0x14060100)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			Nif.NifStream(out meshEmissionType, s, info);
			Nif.NifStream(out initialVelocityType, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numMeshEmitters = (uint)meshEmitters.Count;
			Nif.NifStream(numMeshEmitters, s, info);
			for (var i3 = 0; i3 < meshEmitters.Count; i3++)
			{
				WriteRef((NiObject)meshEmitters[i3], s, info, link_map, missing_link_stack);
			}
			if (info.version <= 0x14060000)
			{
				Nif.NifStream(emitAxis, s, info);
			}
			if (info.version >= 0x14060100)
			{
				WriteRef((NiObject)emitterObject, s, info, link_map, missing_link_stack);
			}
			Nif.NifStream(meshEmissionType, s, info);
			Nif.NifStream(initialVelocityType, s, info);
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
			numMeshEmitters = (uint)meshEmitters.Count;
			s.AppendLine($"      Num Mesh Emitters:  {numMeshEmitters}");
			array_output_count = 0;
			for (var i3 = 0; i3 < meshEmitters.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Mesh Emitters[{i3}]:  {meshEmitters[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Emit Axis:  {emitAxis}");
			s.AppendLine($"      Emitter Object:  {emitterObject}");
			s.AppendLine($"      Mesh Emission Type:  {meshEmissionType}");
			s.AppendLine($"      Initial Velocity Type:  {initialVelocityType}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			for (var i3 = 0; i3 < meshEmitters.Count; i3++)
			{
				meshEmitters[i3] = FixLink<NiMesh>(objects, link_stack, missing_link_stack, info);
			}
			if (info.version >= 0x14060100)
			{
				emitterObject = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < meshEmitters.Count; i3++)
			{
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < meshEmitters.Count; i3++)
			{
				if (meshEmitters[i3] != null)
					ptrs.Add((NiObject)meshEmitters[i3]);
			}
			if (emitterObject != null)
				ptrs.Add((NiObject)emitterObject);
			return ptrs;
		}
	}
}
