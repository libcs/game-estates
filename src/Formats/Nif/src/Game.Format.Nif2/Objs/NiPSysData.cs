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
	/*! Particle system data. */
	public class NiPSysData : NiParticlesData
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPSysData", NiParticlesData.TYPE);

		/*! particleDescriptions */
		internal IList<ParticleDesc> particleDescriptions;
		/*! hasRotationSpeeds */
		internal bool hasRotationSpeeds;
		/*! rotationSpeeds */
		internal IList<float> rotationSpeeds;
		/*! numAddedParticles */
		internal ushort numAddedParticles;
		/*! addedParticlesBase */
		internal ushort addedParticlesBase;
		public NiPSysData()
		{
			hasRotationSpeeds = false;
			numAddedParticles = (ushort)0;
			addedParticlesBase = (ushort)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPSysData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			if ((!((info.version == 0x14020007) && (info.userVersion2 > 0))))
			{
				particleDescriptions = new ParticleDesc[numVertices];
				for (var i4 = 0; i4 < particleDescriptions.Count; i4++)
				{
					Nif.NifStream(out particleDescriptions[i4].translation, s, info);
					if (info.version <= 0x0A040001)
					{
						for (var i6 = 0; i6 < 3; i6++)
						{
							Nif.NifStream(out particleDescriptions[i4].unknownFloats1[i6], s, info);
						}
					}
					Nif.NifStream(out particleDescriptions[i4].unknownFloat1, s, info);
					Nif.NifStream(out particleDescriptions[i4].unknownFloat2, s, info);
					Nif.NifStream(out particleDescriptions[i4].unknownFloat3, s, info);
					Nif.NifStream(out particleDescriptions[i4].unknownInt1, s, info);
				}
			}
			if (info.version >= 0x14000002)
			{
				Nif.NifStream(out hasRotationSpeeds, s, info);
			}
			if (info.version >= 0x14000002 && ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))))
			{
				if (hasRotationSpeeds)
				{
					rotationSpeeds = new float[numVertices];
					for (var i5 = 0; i5 < rotationSpeeds.Count; i5++)
					{
						Nif.NifStream(out rotationSpeeds[i5], s, info);
					}
				}
			}
			if ((!((info.version == 0x14020007) && (info.userVersion2 > 0))))
			{
				Nif.NifStream(out numAddedParticles, s, info);
				Nif.NifStream(out addedParticlesBase, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			if ((!((info.version == 0x14020007) && (info.userVersion2 > 0))))
			{
				for (var i4 = 0; i4 < particleDescriptions.Count; i4++)
				{
					Nif.NifStream(particleDescriptions[i4].translation, s, info);
					if (info.version <= 0x0A040001)
					{
						for (var i6 = 0; i6 < 3; i6++)
						{
							Nif.NifStream(particleDescriptions[i4].unknownFloats1[i6], s, info);
						}
					}
					Nif.NifStream(particleDescriptions[i4].unknownFloat1, s, info);
					Nif.NifStream(particleDescriptions[i4].unknownFloat2, s, info);
					Nif.NifStream(particleDescriptions[i4].unknownFloat3, s, info);
					Nif.NifStream(particleDescriptions[i4].unknownInt1, s, info);
				}
			}
			if (info.version >= 0x14000002)
			{
				Nif.NifStream(hasRotationSpeeds, s, info);
			}
			if (info.version >= 0x14000002 && ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))))
			{
				if (hasRotationSpeeds)
				{
					for (var i5 = 0; i5 < rotationSpeeds.Count; i5++)
					{
						Nif.NifStream(rotationSpeeds[i5], s, info);
					}
				}
			}
			if ((!((info.version == 0x14020007) && (info.userVersion2 > 0))))
			{
				Nif.NifStream(numAddedParticles, s, info);
				Nif.NifStream(addedParticlesBase, s, info);
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
			array_output_count = 0;
			for (var i3 = 0; i3 < particleDescriptions.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Translation:  {particleDescriptions[i3].translation}");
				array_output_count = 0;
				for (var i4 = 0; i4 < 3; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Unknown Floats 1[{i4}]:  {particleDescriptions[i3].unknownFloats1[i4]}");
					array_output_count++;
				}
				s.AppendLine($"        Unknown Float 1:  {particleDescriptions[i3].unknownFloat1}");
				s.AppendLine($"        Unknown Float 2:  {particleDescriptions[i3].unknownFloat2}");
				s.AppendLine($"        Unknown Float 3:  {particleDescriptions[i3].unknownFloat3}");
				s.AppendLine($"        Unknown Int 1:  {particleDescriptions[i3].unknownInt1}");
			}
			s.AppendLine($"      Has Rotation Speeds:  {hasRotationSpeeds}");
			if (hasRotationSpeeds)
			{
				array_output_count = 0;
				for (var i4 = 0; i4 < rotationSpeeds.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Rotation Speeds[{i4}]:  {rotationSpeeds[i4]}");
					array_output_count++;
				}
			}
			s.AppendLine($"      Num Added Particles:  {numAddedParticles}");
			s.AppendLine($"      Added Particles Base:  {addedParticlesBase}");
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
