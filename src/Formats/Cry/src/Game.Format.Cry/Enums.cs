﻿namespace Game.Format.Cry
{
    public enum FileVersionEnum : uint
    {
        CryTek_3_4 = 0x744,
        CryTek_3_5 = 0x745,
        CryTek_3_6 = 0x746,
    }

    public enum FileTypeEnum : uint
    {
        GEOM = 0xFFFF0000,
        ANIM = 0xFFFF0001
    }

    public enum MtlNameTypeEnum : uint
    {
        // It looks like there is a 0x04 type now as well, for mech parts.  Not sure what that is.
        // Also a 0x0B type now as well.
        Library = 0x01,
        Single = 0x10,
        Child = 0x12,
        Unknown1 = 0x0B, // Collision materials?  In MWO, these are the torsos, arms, legs from body/<mech>.mtl
        Unknown2 = 0x04
    }

    public enum ChunkTypeEnum : uint
    {
        Any = 0x0,
        Mesh = 0xCCCC0000,
        Helper = 0xCCCC0001,
        VertAnim = 0xCCCC0002,
        BoneAnim = 0xCCCC0003,
        GeomNameList = 0xCCCC0004,
        BoneNameList = 0xCCCC0005,
        MtlList = 0xCCCC0006,
        MRM = 0xCCCC0007, //obsolete
        SceneProps = 0xCCCC0008,
        Light = 0xCCCC0009,
        PatchMesh = 0xCCCC000A,
        Node = 0xCCCC000B,
        Mtl = 0xCCCC000C,
        Controller = 0xCCCC000D,
        Timing = 0xCCCC000E,
        BoneMesh = 0xCCCC000F,
        BoneLightBinding = 0xCCCC0010,
        MeshMorphTarget = 0xCCCC0011,
        BoneInitialPos = 0xCCCC0012,
        SourceInfo = 0xCCCC0013, //Describes the source from which the cgf was exported: source max file, machine and user.
        MtlName = 0xCCCC0014, //provides material name as used in the material.xml file
        ExportFlags = 0xCCCC0015, //Describes export information.
        DataStream = 0xCCCC0016, //A data Stream
        MeshSubsets = 0xCCCC0017, //Describes an array of mesh subsets
        MeshPhysicsData = 0xCCCC0018, //Physicalized mesh data
        CompiledBones = 0xACDC0000,
        CompiledPhysicalBones = 0xACDC0001,
        CompiledMorphTargets = 0xACDC0002,
        CompiledPhysicalProxies = 0xACDC0003,
        CompiledIntFaces = 0xACDC0004,
        CompiledIntSkinVertices = 0xACDC0005,
        CompiledExt2IntMap = 0xACDC0006,
        BreakablePhysics = 0xACDC0007,
        FaceMap = 0xAAFC0000, //unknown chunk
        SpeedInfo = 0xAAFC0002, //Speed and distnace info
        FootPlantInfo = 0xAAFC0003, // Footplant info
        BonesBoxes = 0xAAFC0004, // unknown chunk
        FoliageInfo = 0xAAFC0005, //unknown chunk
        // Star Citizen versions
        CompiledBonesSC = 0xCCCC1000,
        CompiledPhysicalBonesSC = 0xCCCC1001,
        CompiledMorphTargetsSC = 0xCCCC1002,
        CompiledPhysicalProxiesSC = 0xCCCC1003,
        CompiledIntFacesSC = 0xCCCC1004,
        CompiledIntSkinVerticesSC = 0xCCCC1005,
        CompiledExt2IntMapSC = 0xCCCC1006,
        BoneBoxesSC = 0x08013004,
        // Star Citizen new
        NotSureUkn1SC = 0xCCCBF002,
        XmlFileSC = 0xCCCBF004,
    }

    public enum ChunkTypetmp
    {
        Any,
        Mesh,
        Helper,
        VertAnim,
        BoneAnim,
        GeomNameList,
        BoneNameList,
        MtlList,
        MRM,
        SceneProps,
        Light,
        PatchMesh,
        Node,
        Mtl,
        Controller,
        Timing,
        BoneMesh,
        BoneLightBinding,
        MeshMorphTarget,
        BoneInitialPos,
        SourceInfo,
        MtlName,
        ExportFlags,
        DataStream,
        MeshSubsets,
        MeshPhysicsData,
        CompiledBones,
        CompiledPhysicalBones,
        CompiledMorphtargets,
        CompiledPhysicalProxies,
        CompiledIntFaces,
        CompiledIntSkinVertices,
        CompiledExt2IntMap,
        BreakablePhysics,
        FaceMap,
        SpeedInfo,
        FootPlantInfo,
        BonesBoxes,
        UnknownAAFC0005
    }

    public enum ChunkVersion : uint
    {
        ChkVersion
    }

    public enum HelperTypeEnum : uint
    {
        POINT,
        DUMMY,
        XREF,
        CAMERA,
        GEOMETRY
    }

    public enum MtlType : uint
    {
        UNKNOWN,
        STANDARD,
        MULTI,
        TWOSIDED
    }

    public enum MtlNamePhysicsType : uint
    {
        NONE = 0xFFFFFFFF,
        DEFAULT = 0x00000000,
        NOCOLLIDE = 0x00000001,
        OBSTRUCT = 0x00000002,
        DEFAULTPROXY = 0x000000FF,  // this needs to be checked.  cgf.xml says 256; not sure if hex or dec
        UNKNOWN = 0x00001100,       // collision mesh?
    }

    public enum LightType : uint
    {
        OMNI,
        SPOT,
        DIRECT,
        AMBIENT
    }

    public enum CtrlType : uint
    {
        NONE,
        CRYBONE,
        LINEAR1,
        LINEAR3,
        LINEARQ,
        BEZIER1,
        BEZIER3,
        BEZIERQ,
        TBC1,
        TBC3,
        TBCQ,
        BSPLINE2O,
        BSPLINE1O,
        BSPLINE2C,
        BSPLINE1C,
        CONST          // this was given a value of 11, which is the same as BSPLINE2o.
    }

    public enum TextureMapping : uint
    {
        NORMAL,
        ENVIRONMENT,
        SCREENENVIRONMENT,
        CUBIC,
        AUTOCUBIC
    }

    public enum DataStreamTypeEnum : uint
    {
        VERTICES,
        NORMALS,
        UVS,
        COLORS,
        COLORS2,
        INDICES,
        TANGENTS,
        SHCOEFFS,
        SHAPEDEFORMATION,
        BONEMAP,
        FACEMAP,
        VERTMATS,
        UNKNOWN1,   // Prey Normals?
        UNKNOWN2,
        UNKNOWN3,
        VERTSUVS,
        UNKNOWN5,
        UNKNOWN6
    }

    public enum PhysicsPrimitiveType : uint
    {
        CUBE = 0X0,
        POLYHEDRON = 0X1,
        CYLINDER = 0X5,
        UNKNOWN6 = 0X6   // nothing between 2-4, no idea what unknown is.
    }

    public enum ECgfStreamType : uint
    {
        CGF_STREAM_POSITIONS,
        CGF_STREAM_NORMALS,
        CGF_STREAM_TEXCOORDS,
        CGF_STREAM_COLORS,
        CGF_STREAM_COLORS2,
        CGF_STREAM_INDICES,
        CGF_STREAM_TANGENTS,
        CGF_STREAM_DUMMY0_,  // used to be CGF_STREAM_SHCOEFFS, dummy is needed to keep existing assets loadable
        CGF_STREAM_DUMMY1_,  // used to be CGF_STREAM_SHAPEDEFORMATION, dummy is needed to keep existing assets loadable
        CGF_STREAM_BONEMAPPING,
        CGF_STREAM_FACEMAP,
        CGF_STREAM_VERT_MATS,
        CGF_STREAM_QTANGENTS,
        CGF_STREAM_SKINDATA,
        CGF_STREAM_DUMMY2_,  // used to be CGF_STREAM_PS3EDGEDATA, dummy is needed to keep existing assets loadable
        CGF_STREAM_P3S_C4B_T2S,
        CGF_STREAM_NUM_TYPES
    };

    public enum XmlFileType
    {
        MATERIAL,
        PREFAB,
        CHRPARAMS
    }
}