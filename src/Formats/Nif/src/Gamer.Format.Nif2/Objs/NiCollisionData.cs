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
	/*! Collision box. */
	public class NiCollisionData : NiCollisionObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiCollisionData", NiCollisionObject.TYPE);

		/*! propagationMode */
		internal PropagationMode propagationMode;
		/*! collisionMode */
		internal CollisionMode collisionMode;
		/*! Use Alternate Bounding Volume. */
		internal byte useAbv;
		/*! boundingVolume */
		internal BoundingVolume boundingVolume;
		public NiCollisionData()
		{
			propagationMode = (PropagationMode)0;
			collisionMode = (CollisionMode)0;
			useAbv = (byte)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiCollisionData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out propagationMode, s, info);
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(out collisionMode, s, info);
			}
			Nif.NifStream(out useAbv, s, info);
			if ((useAbv == 1))
			{
				Nif.NifStream(out boundingVolume.collisionType, s, info);
				if ((boundingVolume.collisionType == 0))
				{
					Nif.NifStream(out boundingVolume.sphere.center, s, info);
					Nif.NifStream(out boundingVolume.sphere.radius, s, info);
				}
				if ((boundingVolume.collisionType == 1))
				{
					Nif.NifStream(out boundingVolume.box.center, s, info);
					for (var i5 = 0; i5 < 3; i5++)
					{
						Nif.NifStream(out boundingVolume.box.axis[i5], s, info);
					}
					Nif.NifStream(out boundingVolume.box.extent, s, info);
				}
				if ((boundingVolume.collisionType == 2))
				{
					Nif.NifStream(out boundingVolume.capsule.center, s, info);
					Nif.NifStream(out boundingVolume.capsule.origin, s, info);
					Nif.NifStream(out boundingVolume.capsule.extent, s, info);
					Nif.NifStream(out boundingVolume.capsule.radius, s, info);
				}
				if ((boundingVolume.collisionType == 5))
				{
					Nif.NifStream(out boundingVolume.halfSpace.plane.normal, s, info);
					Nif.NifStream(out boundingVolume.halfSpace.plane.constant, s, info);
					Nif.NifStream(out boundingVolume.halfSpace.center, s, info);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(propagationMode, s, info);
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(collisionMode, s, info);
			}
			Nif.NifStream(useAbv, s, info);
			if ((useAbv == 1))
			{
				Nif.NifStream(boundingVolume.collisionType, s, info);
				if ((boundingVolume.collisionType == 0))
				{
					Nif.NifStream(boundingVolume.sphere.center, s, info);
					Nif.NifStream(boundingVolume.sphere.radius, s, info);
				}
				if ((boundingVolume.collisionType == 1))
				{
					Nif.NifStream(boundingVolume.box.center, s, info);
					for (var i5 = 0; i5 < 3; i5++)
					{
						Nif.NifStream(boundingVolume.box.axis[i5], s, info);
					}
					Nif.NifStream(boundingVolume.box.extent, s, info);
				}
				if ((boundingVolume.collisionType == 2))
				{
					Nif.NifStream(boundingVolume.capsule.center, s, info);
					Nif.NifStream(boundingVolume.capsule.origin, s, info);
					Nif.NifStream(boundingVolume.capsule.extent, s, info);
					Nif.NifStream(boundingVolume.capsule.radius, s, info);
				}
				if ((boundingVolume.collisionType == 5))
				{
					Nif.NifStream(boundingVolume.halfSpace.plane.normal, s, info);
					Nif.NifStream(boundingVolume.halfSpace.plane.constant, s, info);
					Nif.NifStream(boundingVolume.halfSpace.center, s, info);
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
			s.AppendLine($"      Propagation Mode:  {propagationMode}");
			s.AppendLine($"      Collision Mode:  {collisionMode}");
			s.AppendLine($"      Use ABV:  {useAbv}");
			if ((useAbv == 1))
			{
				s.AppendLine($"        Collision Type:  {boundingVolume.collisionType}");
				if ((boundingVolume.collisionType == 0))
				{
					s.AppendLine($"          Center:  {boundingVolume.sphere.center}");
					s.AppendLine($"          Radius:  {boundingVolume.sphere.radius}");
				}
				if ((boundingVolume.collisionType == 1))
				{
					s.AppendLine($"          Center:  {boundingVolume.box.center}");
					array_output_count = 0;
					for (var i5 = 0; i5 < 3; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						{
							s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
							break;
						}
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
							break;
						s.AppendLine($"            Axis[{i5}]:  {boundingVolume.box.axis[i5]}");
						array_output_count++;
					}
					s.AppendLine($"          Extent:  {boundingVolume.box.extent}");
				}
				if ((boundingVolume.collisionType == 2))
				{
					s.AppendLine($"          Center:  {boundingVolume.capsule.center}");
					s.AppendLine($"          Origin:  {boundingVolume.capsule.origin}");
					s.AppendLine($"          Extent:  {boundingVolume.capsule.extent}");
					s.AppendLine($"          Radius:  {boundingVolume.capsule.radius}");
				}
				if ((boundingVolume.collisionType == 5))
				{
					s.AppendLine($"          Normal:  {boundingVolume.halfSpace.plane.normal}");
					s.AppendLine($"          Constant:  {boundingVolume.halfSpace.plane.constant}");
					s.AppendLine($"          Center:  {boundingVolume.halfSpace.center}");
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
