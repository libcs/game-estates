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

/*! Guild 2-Specific node */
public class NiPSysTrailEmitter : NiPSysEmitter {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPSysTrailEmitter", NiPSysEmitter.TYPE);
	/*! Unknown */
	internal int unknownInt1;
	/*! Unknown */
	internal float unknownFloat1;
	/*! Unknown */
	internal float unknownFloat2;
	/*! Unknown */
	internal float unknownFloat3;
	/*! Unknown */
	internal int unknownInt2;
	/*! Unknown */
	internal float unknownFloat4;
	/*! Unknown */
	internal int unknownInt3;
	/*! Unknown */
	internal float unknownFloat5;
	/*! Unknown */
	internal int unknownInt4;
	/*! Unknown */
	internal float unknownFloat6;
	/*! Unknown */
	internal float unknownFloat7;

	public NiPSysTrailEmitter() {
	unknownInt1 = (int)0;
	unknownFloat1 = 0.0f;
	unknownFloat2 = 0.0f;
	unknownFloat3 = 0.0f;
	unknownInt2 = (int)0;
	unknownFloat4 = 0.0f;
	unknownInt3 = (int)0;
	unknownFloat5 = 0.0f;
	unknownInt4 = (int)0;
	unknownFloat6 = 0.0f;
	unknownFloat7 = 0.0f;
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
public static NiObject Create() => new NiPSysTrailEmitter();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out unknownInt1, s, info);
	Nif.NifStream(out unknownFloat1, s, info);
	Nif.NifStream(out unknownFloat2, s, info);
	Nif.NifStream(out unknownFloat3, s, info);
	Nif.NifStream(out unknownInt2, s, info);
	Nif.NifStream(out unknownFloat4, s, info);
	Nif.NifStream(out unknownInt3, s, info);
	Nif.NifStream(out unknownFloat5, s, info);
	Nif.NifStream(out unknownInt4, s, info);
	Nif.NifStream(out unknownFloat6, s, info);
	Nif.NifStream(out unknownFloat7, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(unknownInt1, s, info);
	Nif.NifStream(unknownFloat1, s, info);
	Nif.NifStream(unknownFloat2, s, info);
	Nif.NifStream(unknownFloat3, s, info);
	Nif.NifStream(unknownInt2, s, info);
	Nif.NifStream(unknownFloat4, s, info);
	Nif.NifStream(unknownInt3, s, info);
	Nif.NifStream(unknownFloat5, s, info);
	Nif.NifStream(unknownInt4, s, info);
	Nif.NifStream(unknownFloat6, s, info);
	Nif.NifStream(unknownFloat7, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Unknown Int 1:  {unknownInt1}");
	s.AppendLine($"  Unknown Float 1:  {unknownFloat1}");
	s.AppendLine($"  Unknown Float 2:  {unknownFloat2}");
	s.AppendLine($"  Unknown Float 3:  {unknownFloat3}");
	s.AppendLine($"  Unknown Int 2:  {unknownInt2}");
	s.AppendLine($"  Unknown Float 4:  {unknownFloat4}");
	s.AppendLine($"  Unknown Int 3:  {unknownInt3}");
	s.AppendLine($"  Unknown Float 5:  {unknownFloat5}");
	s.AppendLine($"  Unknown Int 4:  {unknownInt4}");
	s.AppendLine($"  Unknown Float 6:  {unknownFloat6}");
	s.AppendLine($"  Unknown Float 7:  {unknownFloat7}");
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