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
	/*! Abstract audio-visual base class from which all of Gamebryo's scene graph objects inherit. */
	public class NiAVObject : NiObjectNET
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiAVObject", NiObjectNET.TYPE);

		/*! Basic flags for AV objects. For Bethesda streams above 26 only. */
		internal uint flags;
		/*! The translation vector. */
		internal Vector3 translation;
		/*! The rotation part of the transformation matrix. */
		internal Matrix33 rotation;
		/*! Scaling part (only uniform scaling is supported). */
		internal float scale;
		/*! Unknown function. Always seems to be (0, 0, 0) */
		internal Vector3 velocity;
		/*! numProperties */
		internal uint numProperties;
		/*! All rendering properties attached to this object. */
		internal IList<NiProperty> properties;
		/*! Always 2,0,2,0. */
		internal Array4<uint> unknown1;
		/*! 0 or 1. */
		internal byte unknown2;
		/*! hasBoundingVolume */
		internal bool hasBoundingVolume;
		/*! boundingVolume */
		internal BoundingVolume boundingVolume;
		/*! collisionObject */
		internal NiCollisionObject collisionObject;
		public NiAVObject()
		{
			flags = (uint)14;
			scale = 1.0f;
			numProperties = (uint)0;
			unknown2 = (byte)0;
			hasBoundingVolume = false;
			collisionObject = null;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiAVObject();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			if ((info.userVersion2 > 26))
			{
				Nif.NifStream(out flags, s, info);
			}
			if (info.version >= 0x03000000 && ((info.userVersion2 <= 26)))
			{
				Nif.NifStream(out (ushort)flags, s, info);
			}
			Nif.NifStream(out translation, s, info);
			Nif.NifStream(out rotation, s, info);
			Nif.NifStream(out scale, s, info);
			if (info.version <= 0x04020200)
			{
				Nif.NifStream(out velocity, s, info);
			}
			if ((info.userVersion2 <= 34))
			{
				Nif.NifStream(out numProperties, s, info);
				properties = new Ref[numProperties];
				for (var i4 = 0; i4 < properties.Count; i4++)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
			}
			if (info.version <= 0x02030000)
			{
				for (var i4 = 0; i4 < 4; i4++)
				{
					Nif.NifStream(out unknown1[i4], s, info);
				}
				Nif.NifStream(out unknown2, s, info);
			}
			if (info.version >= 0x03000000 && info.version <= 0x04020200)
			{
				Nif.NifStream(out hasBoundingVolume, s, info);
				if (hasBoundingVolume)
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
						for (var i6 = 0; i6 < 3; i6++)
						{
							Nif.NifStream(out boundingVolume.box.axis[i6], s, info);
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
			if (info.version >= 0x0A000100)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numProperties = (uint)properties.Count;
			if ((info.userVersion2 > 26))
			{
				Nif.NifStream(flags, s, info);
			}
			if (info.version >= 0x03000000 && ((info.userVersion2 <= 26)))
			{
				Nif.NifStream((ushort)flags, s, info);
			}
			Nif.NifStream(translation, s, info);
			Nif.NifStream(rotation, s, info);
			Nif.NifStream(scale, s, info);
			if (info.version <= 0x04020200)
			{
				Nif.NifStream(velocity, s, info);
			}
			if ((info.userVersion2 <= 34))
			{
				Nif.NifStream(numProperties, s, info);
				for (var i4 = 0; i4 < properties.Count; i4++)
				{
					WriteRef((NiObject)properties[i4], s, info, link_map, missing_link_stack);
				}
			}
			if (info.version <= 0x02030000)
			{
				for (var i4 = 0; i4 < 4; i4++)
				{
					Nif.NifStream(unknown1[i4], s, info);
				}
				Nif.NifStream(unknown2, s, info);
			}
			if (info.version >= 0x03000000 && info.version <= 0x04020200)
			{
				Nif.NifStream(hasBoundingVolume, s, info);
				if (hasBoundingVolume)
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
						for (var i6 = 0; i6 < 3; i6++)
						{
							Nif.NifStream(boundingVolume.box.axis[i6], s, info);
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
			if (info.version >= 0x0A000100)
			{
				WriteRef((NiObject)collisionObject, s, info, link_map, missing_link_stack);
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
			numProperties = (uint)properties.Count;
			s.AppendLine($"      Flags:  {flags}");
			s.AppendLine($"      Translation:  {translation}");
			s.AppendLine($"      Rotation:  {rotation}");
			s.AppendLine($"      Scale:  {scale}");
			s.AppendLine($"      Velocity:  {velocity}");
			s.AppendLine($"      Num Properties:  {numProperties}");
			array_output_count = 0;
			for (var i3 = 0; i3 < properties.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Properties[{i3}]:  {properties[i3]}");
				array_output_count++;
			}
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
				s.AppendLine($"        Unknown 1[{i3}]:  {unknown1[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Unknown 2:  {unknown2}");
			s.AppendLine($"      Has Bounding Volume:  {hasBoundingVolume}");
			if (hasBoundingVolume)
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
			s.AppendLine($"      Collision Object:  {collisionObject}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			if ((info.userVersion2 <= 34))
			{
				for (var i4 = 0; i4 < properties.Count; i4++)
				{
					properties[i4] = FixLink<NiProperty>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (info.version >= 0x0A000100)
			{
				collisionObject = FixLink<NiCollisionObject>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < properties.Count; i3++)
			{
				if (properties[i3] != null)
					refs.Add((NiObject)properties[i3]);
			}
			if (collisionObject != null)
				refs.Add((NiObject)collisionObject);
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < properties.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
