/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//-----------------------------------NOTICE----------------------------------//
// Some of this file is automatically filled in by a Python script.  Only    //
// add custom code in the designated areas or it will be overwritten during  //
// the next update.                                                          //
//-----------------------------------NOTICE----------------------------------//

using System;
using System.IO;
using System.Collections.Generic;


namespace Niflib {

/*! Generic particle system node. */
public class NiParticles : NiGeometry {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiParticles", NiGeometry.TYPE);
	/*!  */
	internal BSVertexDesc vertexDesc;

	public NiParticles() {
	}

	/*!
	 * Used to determine the type of a particular instance of this object.
	 * \return The type constant for the actual type of the object.
	 */
	public override Type_ GetType() => TYPE;

	/*!
	 * A factory function used during file reading to create an instance of this type of object.
	 * \return A pointer to a newly allocated instance of this type of object.
	 */
	public static NiObject Create() => new NiParticles();

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

		base.Read(s, link_stack, info);
		if ((info.userVersion2 >= 100)) {
			Nif.NifStream(out vertexDesc.vf1, s, info);
			Nif.NifStream(out vertexDesc.vf2, s, info);
			Nif.NifStream(out vertexDesc.vf3, s, info);
			Nif.NifStream(out vertexDesc.vf4, s, info);
			Nif.NifStream(out vertexDesc.vf5, s, info);
			Nif.NifStream(out vertexDesc.vertexAttributes, s, info);
			Nif.NifStream(out vertexDesc.vf8, s, info);
		}

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

		base.Write(s, link_map, missing_link_stack, info);
		if ((info.userVersion2 >= 100)) {
			Nif.NifStream(vertexDesc.vf1, s, info);
			Nif.NifStream(vertexDesc.vf2, s, info);
			Nif.NifStream(vertexDesc.vf3, s, info);
			Nif.NifStream(vertexDesc.vf4, s, info);
			Nif.NifStream(vertexDesc.vf5, s, info);
			Nif.NifStream(vertexDesc.vertexAttributes, s, info);
			Nif.NifStream(vertexDesc.vf8, s, info);
		}

	}

	/*!
	 * Summarizes the information contained in this object in English.
	 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
	 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
	 */
	public override string AsString(bool verbose = false) {

		var s = new System.Text.StringBuilder();
		s.Append(base.AsString());
		s.AppendLine($"    VF1:  {vertexDesc.vf1}");
		s.AppendLine($"    VF2:  {vertexDesc.vf2}");
		s.AppendLine($"    VF3:  {vertexDesc.vf3}");
		s.AppendLine($"    VF4:  {vertexDesc.vf4}");
		s.AppendLine($"    VF5:  {vertexDesc.vf5}");
		s.AppendLine($"    Vertex Attributes:  {vertexDesc.vertexAttributes}");
		s.AppendLine($"    VF8:  {vertexDesc.vf8}");
		return s.ToString();

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

		base.FixLinks(objects, link_stack, missing_link_stack, info);

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override List<NiObject> GetRefs() {
		var refs = base.GetRefs();
		return refs;
	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override List<NiObject> GetPtrs() {
		var ptrs = base.GetPtrs();
		return ptrs;
	}


}

}