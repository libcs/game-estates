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
 * Describes a visible scene element with vertices like a mesh, a particle system,
 * lines, etc.
 */
public class NiGeometry : NiAVObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiGeometry", NiAVObject.TYPE);
	/*!  */
	internal NiBound bound;
	/*!  */
	internal NiObject skin;
	/*! Data index (NiTriShapeData/NiTriStripData). */
	internal NiGeometryData data;
	/*!  */
	internal NiSkinInstance skinInstance;
	/*!  */
	internal MaterialData materialData;
	/*!  */
	internal BSShaderProperty shaderProperty;
	/*!  */
	internal NiAlphaProperty alphaProperty;

	public NiGeometry() {
	skin = null;
	data = null;
	skinInstance = null;
	shaderProperty = null;
	alphaProperty = null;
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
public static NiObject Create() => new NiGeometry();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	if ((info.userVersion2 >= 100)) {
		if (IsDerivedType(NiParticleSystem.TYPE)) {
			Nif.NifStream(out bound.center, s, info);
			Nif.NifStream(out bound.radius, s, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}
	if ((info.userVersion2 < 100)) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if ((info.userVersion2 >= 100)) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}
	if ((info.version >= 0x0303000D) && ((info.userVersion2 < 100))) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if ((info.userVersion2 >= 100)) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}
	if ((info.version >= 0x0A000100) && ((info.userVersion2 < 100))) {
		if ((info.version >= 0x0A000100) && (info.version <= 0x14010003)) {
			Nif.NifStream(out materialData.hasShader, s, info);
			if (materialData.hasShader) {
				Nif.NifStream(out materialData.shaderName, s, info);
				Nif.NifStream(out materialData.shaderExtraData, s, info);
			}
		}
		if (info.version >= 0x14020005) {
			Nif.NifStream(out materialData.numMaterials, s, info);
			materialData.materialName = new IndexString[materialData.numMaterials];
			for (var i3 = 0; i3 < materialData.materialName.Count; i3++) {
				Nif.NifStream(out materialData.materialName[i3], s, info);
			}
			materialData.materialExtraData = new int[materialData.numMaterials];
			for (var i3 = 0; i3 < materialData.materialExtraData.Count; i3++) {
				Nif.NifStream(out materialData.materialExtraData[i3], s, info);
			}
			Nif.NifStream(out materialData.activeMaterial, s, info);
		}
		if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
			Nif.NifStream(out materialData.unknownByte, s, info);
		}
		if ((info.version >= 0x0A040001) && (info.version <= 0x0A040001)) {
			Nif.NifStream(out materialData.unknownInteger2, s, info);
		}
		if (info.version >= 0x14020007) {
			Nif.NifStream(out materialData.materialNeedsUpdate, s, info);
		}
	}
	if ((info.version >= 0x0A000100) && ((info.userVersion2 >= 100))) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			if ((info.version >= 0x0A000100) && (info.version <= 0x14010003)) {
				Nif.NifStream(out materialData.hasShader, s, info);
				if (materialData.hasShader) {
					Nif.NifStream(out materialData.shaderName, s, info);
					Nif.NifStream(out materialData.shaderExtraData, s, info);
				}
			}
			if (info.version >= 0x14020005) {
				Nif.NifStream(out materialData.numMaterials, s, info);
				materialData.materialName = new IndexString[materialData.numMaterials];
				for (var i4 = 0; i4 < materialData.materialName.Count; i4++) {
					Nif.NifStream(out materialData.materialName[i4], s, info);
				}
				materialData.materialExtraData = new int[materialData.numMaterials];
				for (var i4 = 0; i4 < materialData.materialExtraData.Count; i4++) {
					Nif.NifStream(out materialData.materialExtraData[i4], s, info);
				}
				Nif.NifStream(out materialData.activeMaterial, s, info);
			}
			if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
				Nif.NifStream(out materialData.unknownByte, s, info);
			}
			if ((info.version >= 0x0A040001) && (info.version <= 0x0A040001)) {
				Nif.NifStream(out materialData.unknownInteger2, s, info);
			}
			if (info.version >= 0x14020007) {
				Nif.NifStream(out materialData.materialNeedsUpdate, s, info);
			}
		}
	}
	if ((info.version >= 0x14020007) && (info.userVersion == 12)) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	if ((info.userVersion2 >= 100)) {
		if (IsDerivedType(NiParticleSystem.TYPE)) {
			Nif.NifStream(bound.center, s, info);
			Nif.NifStream(bound.radius, s, info);
			WriteRef((NiObject)skin, s, info, link_map, missing_link_stack);
		}
	}
	if ((info.userVersion2 < 100)) {
		WriteRef((NiObject)data, s, info, link_map, missing_link_stack);
	}
	if ((info.userVersion2 >= 100)) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			WriteRef((NiObject)data, s, info, link_map, missing_link_stack);
		}
	}
	if ((info.version >= 0x0303000D) && ((info.userVersion2 < 100))) {
		WriteRef((NiObject)skinInstance, s, info, link_map, missing_link_stack);
	}
	if ((info.userVersion2 >= 100)) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			WriteRef((NiObject)skinInstance, s, info, link_map, missing_link_stack);
		}
	}
	if ((info.version >= 0x0A000100) && ((info.userVersion2 < 100))) {
		materialData.numMaterials = (uint)materialData.materialName.Count;
		if ((info.version >= 0x0A000100) && (info.version <= 0x14010003)) {
			Nif.NifStream(materialData.hasShader, s, info);
			if (materialData.hasShader) {
				Nif.NifStream(materialData.shaderName, s, info);
				Nif.NifStream(materialData.shaderExtraData, s, info);
			}
		}
		if (info.version >= 0x14020005) {
			Nif.NifStream(materialData.numMaterials, s, info);
			for (var i3 = 0; i3 < materialData.materialName.Count; i3++) {
				Nif.NifStream(materialData.materialName[i3], s, info);
			}
			for (var i3 = 0; i3 < materialData.materialExtraData.Count; i3++) {
				Nif.NifStream(materialData.materialExtraData[i3], s, info);
			}
			Nif.NifStream(materialData.activeMaterial, s, info);
		}
		if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
			Nif.NifStream(materialData.unknownByte, s, info);
		}
		if ((info.version >= 0x0A040001) && (info.version <= 0x0A040001)) {
			Nif.NifStream(materialData.unknownInteger2, s, info);
		}
		if (info.version >= 0x14020007) {
			Nif.NifStream(materialData.materialNeedsUpdate, s, info);
		}
	}
	if ((info.version >= 0x0A000100) && ((info.userVersion2 >= 100))) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			materialData.numMaterials = (uint)materialData.materialName.Count;
			if ((info.version >= 0x0A000100) && (info.version <= 0x14010003)) {
				Nif.NifStream(materialData.hasShader, s, info);
				if (materialData.hasShader) {
					Nif.NifStream(materialData.shaderName, s, info);
					Nif.NifStream(materialData.shaderExtraData, s, info);
				}
			}
			if (info.version >= 0x14020005) {
				Nif.NifStream(materialData.numMaterials, s, info);
				for (var i4 = 0; i4 < materialData.materialName.Count; i4++) {
					Nif.NifStream(materialData.materialName[i4], s, info);
				}
				for (var i4 = 0; i4 < materialData.materialExtraData.Count; i4++) {
					Nif.NifStream(materialData.materialExtraData[i4], s, info);
				}
				Nif.NifStream(materialData.activeMaterial, s, info);
			}
			if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
				Nif.NifStream(materialData.unknownByte, s, info);
			}
			if ((info.version >= 0x0A040001) && (info.version <= 0x0A040001)) {
				Nif.NifStream(materialData.unknownInteger2, s, info);
			}
			if (info.version >= 0x14020007) {
				Nif.NifStream(materialData.materialNeedsUpdate, s, info);
			}
		}
	}
	if ((info.version >= 0x14020007) && (info.userVersion == 12)) {
		WriteRef((NiObject)shaderProperty, s, info, link_map, missing_link_stack);
		WriteRef((NiObject)alphaProperty, s, info, link_map, missing_link_stack);
	}

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	uint array_output_count = 0;
	s.Append(base.AsString());
	if (IsDerivedType(NiParticleSystem.TYPE)) {
		s.AppendLine($"    Center:  {bound.center}");
		s.AppendLine($"    Radius:  {bound.radius}");
		s.AppendLine($"    Skin:  {skin}");
	}
	s.AppendLine($"  Data:  {data}");
	s.AppendLine($"  Skin Instance:  {skinInstance}");
	materialData.numMaterials = (uint)materialData.materialName.Count;
	s.AppendLine($"  Has Shader:  {materialData.hasShader}");
	if (materialData.hasShader) {
		s.AppendLine($"    Shader Name:  {materialData.shaderName}");
		s.AppendLine($"    Shader Extra Data:  {materialData.shaderExtraData}");
	}
	s.AppendLine($"  Num Materials:  {materialData.numMaterials}");
	array_output_count = 0;
	for (var i1 = 0; i1 < materialData.materialName.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Material Name[{i1}]:  {materialData.materialName[i1]}");
		array_output_count++;
	}
	array_output_count = 0;
	for (var i1 = 0; i1 < materialData.materialExtraData.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Material Extra Data[{i1}]:  {materialData.materialExtraData[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Active Material:  {materialData.activeMaterial}");
	s.AppendLine($"  Unknown Byte:  {materialData.unknownByte}");
	s.AppendLine($"  Unknown Integer 2:  {materialData.unknownInteger2}");
	s.AppendLine($"  Material Needs Update:  {materialData.materialNeedsUpdate}");
	s.AppendLine($"  Shader Property:  {shaderProperty}");
	s.AppendLine($"  Alpha Property:  {alphaProperty}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if ((info.userVersion2 >= 100)) {
		if (IsDerivedType(NiParticleSystem.TYPE)) {
			skin = FixLink<NiObject>(objects, link_stack, missing_link_stack, info);
		}
	}
	if ((info.userVersion2 < 100)) {
		data = FixLink<NiGeometryData>(objects, link_stack, missing_link_stack, info);
	}
	if ((info.userVersion2 >= 100)) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			data = FixLink<NiGeometryData>(objects, link_stack, missing_link_stack, info);
		}
	}
	if ((info.version >= 0x0303000D) && ((info.userVersion2 < 100))) {
		skinInstance = FixLink<NiSkinInstance>(objects, link_stack, missing_link_stack, info);
	}
	if ((info.userVersion2 >= 100)) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			skinInstance = FixLink<NiSkinInstance>(objects, link_stack, missing_link_stack, info);
		}
	}
	if ((info.version >= 0x14020007) && (info.userVersion == 12)) {
		shaderProperty = FixLink<BSShaderProperty>(objects, link_stack, missing_link_stack, info);
		alphaProperty = FixLink<NiAlphaProperty>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (skin != null)
		refs.Add((NiObject)skin);
	if (data != null)
		refs.Add((NiObject)data);
	if (skinInstance != null)
		refs.Add((NiObject)skinInstance);
	if (shaderProperty != null)
		refs.Add((NiObject)shaderProperty);
	if (alphaProperty != null)
		refs.Add((NiObject)alphaProperty);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}


}

}