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
	/*! Abstract base class for all particle emitters. */
	public class NiPSEmitter : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPSEmitter", NiObject.TYPE);

		/*! name */
		internal IndexString name;
		/*! speed */
		internal float speed;
		/*! speedVar */
		internal float speedVar;
		/*! speedFlipRatio */
		internal float speedFlipRatio;
		/*! declination */
		internal float declination;
		/*! declinationVar */
		internal float declinationVar;
		/*! planarAngle */
		internal float planarAngle;
		/*! planarAngleVar */
		internal float planarAngleVar;
		/*! color */
		internal ByteColor4 color;
		/*! size */
		internal float size;
		/*! sizeVar */
		internal float sizeVar;
		/*! lifespan */
		internal float lifespan;
		/*! lifespanVar */
		internal float lifespanVar;
		/*! rotationAngle */
		internal float rotationAngle;
		/*! rotationAngleVar */
		internal float rotationAngleVar;
		/*! rotationSpeed */
		internal float rotationSpeed;
		/*! rotationSpeedVar */
		internal float rotationSpeedVar;
		/*! rotationAxis */
		internal Vector3 rotationAxis;
		/*! randomRotSpeedSign */
		internal bool randomRotSpeedSign;
		/*! randomRotAxis */
		internal bool randomRotAxis;
		/*! Unknown. */
		internal bool unknown;
		public NiPSEmitter()
		{
			speed = 0.0f;
			speedVar = 0.0f;
			speedFlipRatio = 0.0f;
			declination = 0.0f;
			declinationVar = 0.0f;
			planarAngle = 0.0f;
			planarAngleVar = 0.0f;
			size = 0.0f;
			sizeVar = 0.0f;
			lifespan = 0.0f;
			lifespanVar = 0.0f;
			rotationAngle = 0.0f;
			rotationAngleVar = 0.0f;
			rotationSpeed = 0.0f;
			rotationSpeedVar = 0.0f;
			randomRotSpeedSign = false;
			randomRotAxis = false;
			unknown = false;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPSEmitter();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out name, s, info);
			Nif.NifStream(out speed, s, info);
			Nif.NifStream(out speedVar, s, info);
			if (info.version >= 0x14060100)
			{
				Nif.NifStream(out speedFlipRatio, s, info);
			}
			Nif.NifStream(out declination, s, info);
			Nif.NifStream(out declinationVar, s, info);
			Nif.NifStream(out planarAngle, s, info);
			Nif.NifStream(out planarAngleVar, s, info);
			if (info.version <= 0x14060000)
			{
				Nif.NifStream(out color.r, s, info);
				Nif.NifStream(out color.g, s, info);
				Nif.NifStream(out color.b, s, info);
				Nif.NifStream(out color.a, s, info);
			}
			Nif.NifStream(out size, s, info);
			Nif.NifStream(out sizeVar, s, info);
			Nif.NifStream(out lifespan, s, info);
			Nif.NifStream(out lifespanVar, s, info);
			Nif.NifStream(out rotationAngle, s, info);
			Nif.NifStream(out rotationAngleVar, s, info);
			Nif.NifStream(out rotationSpeed, s, info);
			Nif.NifStream(out rotationSpeedVar, s, info);
			Nif.NifStream(out rotationAxis, s, info);
			Nif.NifStream(out randomRotSpeedSign, s, info);
			Nif.NifStream(out randomRotAxis, s, info);
			if (info.version >= 0x1E000000 && info.version <= 0x1E000001)
			{
				Nif.NifStream(out unknown, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(name, s, info);
			Nif.NifStream(speed, s, info);
			Nif.NifStream(speedVar, s, info);
			if (info.version >= 0x14060100)
			{
				Nif.NifStream(speedFlipRatio, s, info);
			}
			Nif.NifStream(declination, s, info);
			Nif.NifStream(declinationVar, s, info);
			Nif.NifStream(planarAngle, s, info);
			Nif.NifStream(planarAngleVar, s, info);
			if (info.version <= 0x14060000)
			{
				Nif.NifStream(color.r, s, info);
				Nif.NifStream(color.g, s, info);
				Nif.NifStream(color.b, s, info);
				Nif.NifStream(color.a, s, info);
			}
			Nif.NifStream(size, s, info);
			Nif.NifStream(sizeVar, s, info);
			Nif.NifStream(lifespan, s, info);
			Nif.NifStream(lifespanVar, s, info);
			Nif.NifStream(rotationAngle, s, info);
			Nif.NifStream(rotationAngleVar, s, info);
			Nif.NifStream(rotationSpeed, s, info);
			Nif.NifStream(rotationSpeedVar, s, info);
			Nif.NifStream(rotationAxis, s, info);
			Nif.NifStream(randomRotSpeedSign, s, info);
			Nif.NifStream(randomRotAxis, s, info);
			if (info.version >= 0x1E000000 && info.version <= 0x1E000001)
			{
				Nif.NifStream(unknown, s, info);
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
			s.AppendLine($"      Name:  {name}");
			s.AppendLine($"      Speed:  {speed}");
			s.AppendLine($"      Speed Var:  {speedVar}");
			s.AppendLine($"      Speed Flip Ratio:  {speedFlipRatio}");
			s.AppendLine($"      Declination:  {declination}");
			s.AppendLine($"      Declination Var:  {declinationVar}");
			s.AppendLine($"      Planar Angle:  {planarAngle}");
			s.AppendLine($"      Planar Angle Var:  {planarAngleVar}");
			s.AppendLine($"      r:  {color.r}");
			s.AppendLine($"      g:  {color.g}");
			s.AppendLine($"      b:  {color.b}");
			s.AppendLine($"      a:  {color.a}");
			s.AppendLine($"      Size:  {size}");
			s.AppendLine($"      Size Var:  {sizeVar}");
			s.AppendLine($"      Lifespan:  {lifespan}");
			s.AppendLine($"      Lifespan Var:  {lifespanVar}");
			s.AppendLine($"      Rotation Angle:  {rotationAngle}");
			s.AppendLine($"      Rotation Angle Var:  {rotationAngleVar}");
			s.AppendLine($"      Rotation Speed:  {rotationSpeed}");
			s.AppendLine($"      Rotation Speed Var:  {rotationSpeedVar}");
			s.AppendLine($"      Rotation Axis:  {rotationAxis}");
			s.AppendLine($"      Random Rot Speed Sign:  {randomRotSpeedSign}");
			s.AppendLine($"      Random Rot Axis:  {randomRotAxis}");
			s.AppendLine($"      Unknown:  {unknown}");
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
