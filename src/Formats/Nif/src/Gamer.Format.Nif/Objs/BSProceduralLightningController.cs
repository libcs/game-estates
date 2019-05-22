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

/*!
 * Skyrim, Paired with dummy TriShapes, this controller generates lightning shapes
 * for special effects.
 *     First interpolator controls Generation.
 */
public class BSProceduralLightningController : NiTimeController {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSProceduralLightningController", NiTimeController.TYPE);
	/*! References generation interpolator. */
	internal NiInterpolator interpolator1_Generation;
	/*! References interpolator for Mutation of strips */
	internal NiInterpolator interpolator2_Mutation;
	/*! References subdivision interpolator. */
	internal NiInterpolator interpolator3_Subdivision;
	/*! References branches interpolator. */
	internal NiInterpolator interpolator4_NumBranches;
	/*! References branches variation interpolator. */
	internal NiInterpolator interpolator5_NumBranchesVar;
	/*! References length interpolator. */
	internal NiInterpolator interpolator6_Length;
	/*! References length variation interpolator. */
	internal NiInterpolator interpolator7_LengthVar;
	/*! References width interpolator. */
	internal NiInterpolator interpolator8_Width;
	/*! References interpolator for amplitude control. 0=straight, 50=wide */
	internal NiInterpolator interpolator9_ArcOffset;
	/*!  */
	internal ushort subdivisions;
	/*!  */
	internal ushort numBranches;
	/*!  */
	internal ushort numBranchesVariation;
	/*! How far lightning will stretch to. */
	internal float length;
	/*! How far lightning variation will stretch to. */
	internal float lengthVariation;
	/*! How wide the bolt will be. */
	internal float width;
	/*! Influences forking behavior with a multiplier. */
	internal float childWidthMult;
	/*!  */
	internal float arcOffset;
	/*!  */
	internal bool fadeMainBolt;
	/*!  */
	internal bool fadeChildBolts;
	/*!  */
	internal bool animateArcOffset;
	/*! Reference to a shader property. */
	internal BSShaderProperty shaderProperty;

	public BSProceduralLightningController() {
	interpolator1_Generation = null;
	interpolator2_Mutation = null;
	interpolator3_Subdivision = null;
	interpolator4_NumBranches = null;
	interpolator5_NumBranchesVar = null;
	interpolator6_Length = null;
	interpolator7_LengthVar = null;
	interpolator8_Width = null;
	interpolator9_ArcOffset = null;
	subdivisions = (ushort)0;
	numBranches = (ushort)0;
	numBranchesVariation = (ushort)0;
	length = 0.0f;
	lengthVariation = 0.0f;
	width = 0.0f;
	childWidthMult = 0.0f;
	arcOffset = 0.0f;
	fadeMainBolt = false;
	fadeChildBolts = false;
	animateArcOffset = false;
	shaderProperty = null;
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
public static NiObject Create() => new BSProceduralLightningController();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out subdivisions, s, info);
	Nif.NifStream(out numBranches, s, info);
	Nif.NifStream(out numBranchesVariation, s, info);
	Nif.NifStream(out length, s, info);
	Nif.NifStream(out lengthVariation, s, info);
	Nif.NifStream(out width, s, info);
	Nif.NifStream(out childWidthMult, s, info);
	Nif.NifStream(out arcOffset, s, info);
	Nif.NifStream(out fadeMainBolt, s, info);
	Nif.NifStream(out fadeChildBolts, s, info);
	Nif.NifStream(out animateArcOffset, s, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	WriteRef((NiObject)interpolator1_Generation, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)interpolator2_Mutation, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)interpolator3_Subdivision, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)interpolator4_NumBranches, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)interpolator5_NumBranchesVar, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)interpolator6_Length, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)interpolator7_LengthVar, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)interpolator8_Width, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)interpolator9_ArcOffset, s, info, link_map, missing_link_stack);
	Nif.NifStream(subdivisions, s, info);
	Nif.NifStream(numBranches, s, info);
	Nif.NifStream(numBranchesVariation, s, info);
	Nif.NifStream(length, s, info);
	Nif.NifStream(lengthVariation, s, info);
	Nif.NifStream(width, s, info);
	Nif.NifStream(childWidthMult, s, info);
	Nif.NifStream(arcOffset, s, info);
	Nif.NifStream(fadeMainBolt, s, info);
	Nif.NifStream(fadeChildBolts, s, info);
	Nif.NifStream(animateArcOffset, s, info);
	WriteRef((NiObject)shaderProperty, s, info, link_map, missing_link_stack);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Interpolator 1: Generation:  {interpolator1_Generation}");
	s.AppendLine($"  Interpolator 2: Mutation:  {interpolator2_Mutation}");
	s.AppendLine($"  Interpolator 3: Subdivision:  {interpolator3_Subdivision}");
	s.AppendLine($"  Interpolator 4: Num Branches:  {interpolator4_NumBranches}");
	s.AppendLine($"  Interpolator 5: Num Branches Var:  {interpolator5_NumBranchesVar}");
	s.AppendLine($"  Interpolator 6: Length:  {interpolator6_Length}");
	s.AppendLine($"  Interpolator 7: Length Var:  {interpolator7_LengthVar}");
	s.AppendLine($"  Interpolator 8: Width:  {interpolator8_Width}");
	s.AppendLine($"  Interpolator 9: Arc Offset:  {interpolator9_ArcOffset}");
	s.AppendLine($"  Subdivisions:  {subdivisions}");
	s.AppendLine($"  Num Branches:  {numBranches}");
	s.AppendLine($"  Num Branches Variation:  {numBranchesVariation}");
	s.AppendLine($"  Length:  {length}");
	s.AppendLine($"  Length Variation:  {lengthVariation}");
	s.AppendLine($"  Width:  {width}");
	s.AppendLine($"  Child Width Mult:  {childWidthMult}");
	s.AppendLine($"  Arc Offset:  {arcOffset}");
	s.AppendLine($"  Fade Main Bolt:  {fadeMainBolt}");
	s.AppendLine($"  Fade Child Bolts:  {fadeChildBolts}");
	s.AppendLine($"  Animate Arc Offset:  {animateArcOffset}");
	s.AppendLine($"  Shader Property:  {shaderProperty}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	interpolator1_Generation = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
	interpolator2_Mutation = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
	interpolator3_Subdivision = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
	interpolator4_NumBranches = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
	interpolator5_NumBranchesVar = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
	interpolator6_Length = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
	interpolator7_LengthVar = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
	interpolator8_Width = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
	interpolator9_ArcOffset = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
	shaderProperty = FixLink<BSShaderProperty>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (interpolator1_Generation != null)
		refs.Add((NiObject)interpolator1_Generation);
	if (interpolator2_Mutation != null)
		refs.Add((NiObject)interpolator2_Mutation);
	if (interpolator3_Subdivision != null)
		refs.Add((NiObject)interpolator3_Subdivision);
	if (interpolator4_NumBranches != null)
		refs.Add((NiObject)interpolator4_NumBranches);
	if (interpolator5_NumBranchesVar != null)
		refs.Add((NiObject)interpolator5_NumBranchesVar);
	if (interpolator6_Length != null)
		refs.Add((NiObject)interpolator6_Length);
	if (interpolator7_LengthVar != null)
		refs.Add((NiObject)interpolator7_LengthVar);
	if (interpolator8_Width != null)
		refs.Add((NiObject)interpolator8_Width);
	if (interpolator9_ArcOffset != null)
		refs.Add((NiObject)interpolator9_ArcOffset);
	if (shaderProperty != null)
		refs.Add((NiObject)shaderProperty);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}


}

}