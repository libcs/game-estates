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
	/*! Generic rotating particles data object. */
	public class NiParticlesData : NiGeometryData
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiParticlesData", NiGeometryData.TYPE);

		/*! The maximum number of particles (matches the number of vertices). */
		internal ushort numParticles;
		/*! The particles' size. */
		internal float particleRadius;
		/*! Is the particle size array present? */
		internal bool hasRadii;
		/*! The individual particle sizes. */
		internal IList<float> radii;
		/*! The number of active particles at the time the system was saved. This is also the number of valid entries in the following arrays. */
		internal ushort numActive;
		/*! Is the particle size array present? */
		internal bool hasSizes;
		/*! The individual particle sizes. */
		internal IList<float> sizes;
		/*! Is the particle rotation array present? */
		internal bool hasRotations;
		/*! The individual particle rotations. */
		internal IList<Quaternion> rotations;
		/*! Are the angles of rotation present? */
		internal bool hasRotationAngles;
		/*! Angles of rotation */
		internal IList<float> rotationAngles;
		/*! Are axes of rotation present? */
		internal bool hasRotationAxes;
		/*! Axes of rotation. */
		internal IList<Vector3> rotationAxes;
		/*! hasTextureIndices */
		internal bool hasTextureIndices;
		/*! How many quads to use in BSPSysSubTexModifier for texture atlasing */
		internal uint numSubtextureOffsets;
		/*! Defines UV offsets */
		internal IList<Vector4> subtextureOffsets;
		/*! Sets aspect ratio for Subtexture Offset UV quads */
		internal float aspectRatio;
		/*! aspectFlags */
		internal ushort aspectFlags;
		/*! speedToAspectAspect2 */
		internal float speedToAspectAspect2;
		/*! speedToAspectSpeed1 */
		internal float speedToAspectSpeed1;
		/*! speedToAspectSpeed2 */
		internal float speedToAspectSpeed2;
		public NiParticlesData()
		{
			numParticles = (ushort)0;
			particleRadius = 0.0f;
			hasRadii = false;
			numActive = (ushort)0;
			hasSizes = false;
			hasRotations = false;
			hasRotationAngles = false;
			hasRotationAxes = false;
			hasTextureIndices = false;
			numSubtextureOffsets = (uint)0;
			aspectRatio = 0.0f;
			aspectFlags = (ushort)0;
			speedToAspectAspect2 = 0.0f;
			speedToAspectSpeed1 = 0.0f;
			speedToAspectSpeed2 = 0.0f;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiParticlesData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			if (info.version <= 0x04000002)
			{
				Nif.NifStream(out numParticles, s, info);
			}
			if (info.version <= 0x0A000100)
			{
				Nif.NifStream(out particleRadius, s, info);
			}
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(out hasRadii, s, info);
			}
			if (info.version >= 0x0A010000 && ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))))
			{
				if (hasRadii)
				{
					radii = new float[numVertices];
					for (var i5 = 0; i5 < radii.Count; i5++)
					{
						Nif.NifStream(out radii[i5], s, info);
					}
				}
			}
			Nif.NifStream(out numActive, s, info);
			Nif.NifStream(out hasSizes, s, info);
			if ((!((info.version == 0x14020007) && (info.userVersion2 > 0))))
			{
				if (hasSizes)
				{
					sizes = new float[numVertices];
					for (var i5 = 0; i5 < sizes.Count; i5++)
					{
						Nif.NifStream(out sizes[i5], s, info);
					}
				}
			}
			if (info.version >= 0x0A000100)
			{
				Nif.NifStream(out hasRotations, s, info);
			}
			if (info.version >= 0x0A000100 && ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))))
			{
				if (hasRotations)
				{
					rotations = new Quaternion[numVertices];
					for (var i5 = 0; i5 < rotations.Count; i5++)
					{
						Nif.NifStream(out rotations[i5], s, info);
					}
				}
			}
			if (info.version >= 0x14000004)
			{
				Nif.NifStream(out hasRotationAngles, s, info);
			}
			if ((!((info.version == 0x14020007) && (info.userVersion2 > 0))))
			{
				if (hasRotationAngles)
				{
					rotationAngles = new float[numVertices];
					for (var i5 = 0; i5 < rotationAngles.Count; i5++)
					{
						Nif.NifStream(out rotationAngles[i5], s, info);
					}
				}
			}
			if (info.version >= 0x14000004)
			{
				Nif.NifStream(out hasRotationAxes, s, info);
			}
			if (info.version >= 0x14000004 && ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))))
			{
				if (hasRotationAxes)
				{
					rotationAxes = new Vector3[numVertices];
					for (var i5 = 0; i5 < rotationAxes.Count; i5++)
					{
						Nif.NifStream(out rotationAxes[i5], s, info);
					}
				}
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 0)))
			{
				Nif.NifStream(out hasTextureIndices, s, info);
			}
			if ((info.userVersion2 > 34))
			{
				Nif.NifStream(out numSubtextureOffsets, s, info);
			}
			if (((info.version == 0x14020007) && ((info.userVersion2 <= 34) && (info.userVersion2 > 0))))
			{
				Nif.NifStream(out (byte)numSubtextureOffsets, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 0)))
			{
				subtextureOffsets = new Vector4[numSubtextureOffsets];
				for (var i4 = 0; i4 < subtextureOffsets.Count; i4++)
				{
					Nif.NifStream(out subtextureOffsets[i4], s, info);
				}
			}
			if ((info.userVersion2 > 34))
			{
				Nif.NifStream(out aspectRatio, s, info);
				Nif.NifStream(out aspectFlags, s, info);
				Nif.NifStream(out speedToAspectAspect2, s, info);
				Nif.NifStream(out speedToAspectSpeed1, s, info);
				Nif.NifStream(out speedToAspectSpeed2, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numSubtextureOffsets = (uint)subtextureOffsets.Count;
			if (info.version <= 0x04000002)
			{
				Nif.NifStream(numParticles, s, info);
			}
			if (info.version <= 0x0A000100)
			{
				Nif.NifStream(particleRadius, s, info);
			}
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(hasRadii, s, info);
			}
			if (info.version >= 0x0A010000 && ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))))
			{
				if (hasRadii)
				{
					for (var i5 = 0; i5 < radii.Count; i5++)
					{
						Nif.NifStream(radii[i5], s, info);
					}
				}
			}
			Nif.NifStream(numActive, s, info);
			Nif.NifStream(hasSizes, s, info);
			if ((!((info.version == 0x14020007) && (info.userVersion2 > 0))))
			{
				if (hasSizes)
				{
					for (var i5 = 0; i5 < sizes.Count; i5++)
					{
						Nif.NifStream(sizes[i5], s, info);
					}
				}
			}
			if (info.version >= 0x0A000100)
			{
				Nif.NifStream(hasRotations, s, info);
			}
			if (info.version >= 0x0A000100 && ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))))
			{
				if (hasRotations)
				{
					for (var i5 = 0; i5 < rotations.Count; i5++)
					{
						Nif.NifStream(rotations[i5], s, info);
					}
				}
			}
			if (info.version >= 0x14000004)
			{
				Nif.NifStream(hasRotationAngles, s, info);
			}
			if ((!((info.version == 0x14020007) && (info.userVersion2 > 0))))
			{
				if (hasRotationAngles)
				{
					for (var i5 = 0; i5 < rotationAngles.Count; i5++)
					{
						Nif.NifStream(rotationAngles[i5], s, info);
					}
				}
			}
			if (info.version >= 0x14000004)
			{
				Nif.NifStream(hasRotationAxes, s, info);
			}
			if (info.version >= 0x14000004 && ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))))
			{
				if (hasRotationAxes)
				{
					for (var i5 = 0; i5 < rotationAxes.Count; i5++)
					{
						Nif.NifStream(rotationAxes[i5], s, info);
					}
				}
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 0)))
			{
				Nif.NifStream(hasTextureIndices, s, info);
			}
			if ((info.userVersion2 > 34))
			{
				Nif.NifStream(numSubtextureOffsets, s, info);
			}
			if (((info.version == 0x14020007) && ((info.userVersion2 <= 34) && (info.userVersion2 > 0))))
			{
				Nif.NifStream((byte)numSubtextureOffsets, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 0)))
			{
				for (var i4 = 0; i4 < subtextureOffsets.Count; i4++)
				{
					Nif.NifStream(subtextureOffsets[i4], s, info);
				}
			}
			if ((info.userVersion2 > 34))
			{
				Nif.NifStream(aspectRatio, s, info);
				Nif.NifStream(aspectFlags, s, info);
				Nif.NifStream(speedToAspectAspect2, s, info);
				Nif.NifStream(speedToAspectSpeed1, s, info);
				Nif.NifStream(speedToAspectSpeed2, s, info);
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
			numSubtextureOffsets = (uint)subtextureOffsets.Count;
			s.AppendLine($"      Num Particles:  {numParticles}");
			s.AppendLine($"      Particle Radius:  {particleRadius}");
			s.AppendLine($"      Has Radii:  {hasRadii}");
			if (hasRadii)
			{
				array_output_count = 0;
				for (var i4 = 0; i4 < radii.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Radii[{i4}]:  {radii[i4]}");
					array_output_count++;
				}
			}
			s.AppendLine($"      Num Active:  {numActive}");
			s.AppendLine($"      Has Sizes:  {hasSizes}");
			if (hasSizes)
			{
				array_output_count = 0;
				for (var i4 = 0; i4 < sizes.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Sizes[{i4}]:  {sizes[i4]}");
					array_output_count++;
				}
			}
			s.AppendLine($"      Has Rotations:  {hasRotations}");
			if (hasRotations)
			{
				array_output_count = 0;
				for (var i4 = 0; i4 < rotations.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Rotations[{i4}]:  {rotations[i4]}");
					array_output_count++;
				}
			}
			s.AppendLine($"      Has Rotation Angles:  {hasRotationAngles}");
			if (hasRotationAngles)
			{
				array_output_count = 0;
				for (var i4 = 0; i4 < rotationAngles.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Rotation Angles[{i4}]:  {rotationAngles[i4]}");
					array_output_count++;
				}
			}
			s.AppendLine($"      Has Rotation Axes:  {hasRotationAxes}");
			if (hasRotationAxes)
			{
				array_output_count = 0;
				for (var i4 = 0; i4 < rotationAxes.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Rotation Axes[{i4}]:  {rotationAxes[i4]}");
					array_output_count++;
				}
			}
			s.AppendLine($"      Has Texture Indices:  {hasTextureIndices}");
			s.AppendLine($"      Num Subtexture Offsets:  {numSubtextureOffsets}");
			array_output_count = 0;
			for (var i3 = 0; i3 < subtextureOffsets.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Subtexture Offsets[{i3}]:  {subtextureOffsets[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Aspect Ratio:  {aspectRatio}");
			s.AppendLine($"      Aspect Flags:  {aspectFlags}");
			s.AppendLine($"      Speed to Aspect Aspect 2:  {speedToAspectAspect2}");
			s.AppendLine($"      Speed to Aspect Speed 1:  {speedToAspectSpeed1}");
			s.AppendLine($"      Speed to Aspect Speed 2:  {speedToAspectSpeed2}");
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
