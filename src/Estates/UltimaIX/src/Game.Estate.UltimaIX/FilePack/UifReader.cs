using Game.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Game.Core.Debug;

namespace Game.Estate.UltimaIX.FilePack
{
    // ---- SAPPEAR --- //

    public class SiFile
    {
        public SiFile(string name) => Name = name;

        public string Name;
        public SiHeader Header;
        public SiMesh[] Meshes;

        public void Deserialize(GenericReader r)
        {
            Header = new SiHeader(r);
            Meshes = new SiMesh[Header.MeshMap.Length];
            for (var i = 0; i < Header.MeshMap.Length; i++)
            {
                r.Position = Header.MeshMap[i];
                Meshes[i] = new SiMesh(r);
            }
            //Footer = new NiFooter();
            //Footer.Deserialize(r);
        }

        //public IEnumerable<string> GetTexturePaths()
        //{
        //    foreach (var niObject in Blocks)
        //        if (niObject is NiSourceTexture niSourceTexture)
        //            if (!string.IsNullOrEmpty(niSourceTexture.FileName))
        //                yield return niSourceTexture.FileName;
        //}
    }

    public class SiHeader
    {
        public uint MeshCount;
        public uint LodCount;
        public Vector3 CylinderBaseCentre;
        public float CylinderBaseHeight;
        public float CylinderBaseRadius;
        public Vector3 SphereCenter;
        public float SphereRadius;
        public float Unk1;
        public Vector3 MinBounds;
        public Vector3 MaxBounds;
        public uint LodThreshold0;
        public uint LodThreshold1;
        public uint LodThreshold2;
        public uint LodThreshold3;
        public Vector3 CenterOfMass;
        public float MassOrVolume;
        public Matrix4x4 InertiaMatrix;
        public float InertiaRelated;
        public uint[] MeshMap;

        public SiHeader(GenericReader r)
        {
            MeshCount = r.ReadUInt32();
            LodCount = r.ReadUInt32();
            CylinderBaseCentre = r.ReadVector3();
            CylinderBaseHeight = r.ReadSingle();
            CylinderBaseRadius = r.ReadSingle();
            SphereCenter = r.ReadVector3();
            SphereRadius = r.ReadSingle();
            Unk1 = r.ReadSingle();
            MinBounds = r.ReadVector3();
            MaxBounds = r.ReadVector3();
            LodThreshold0 = r.ReadUInt32();
            LodThreshold1 = r.ReadUInt32();
            LodThreshold2 = r.ReadUInt32();
            LodThreshold3 = r.ReadUInt32();
            CenterOfMass = r.ReadVector3();
            MassOrVolume = r.ReadSingle();
            InertiaMatrix = r.ReadRowMajorMatrix3x3();
            InertiaRelated = r.ReadSingle();
            //
            MeshMap = new uint[MeshCount * LodCount];
            for (var i = 0; i < MeshMap.Length; i++)
                MeshMap[i] = r.ReadUInt32();
        }
    }

    public class SiMesh
    {
        public uint LimbId; // The ID of this submesh
        public uint ParentId; // The ID of the parent mesh
        public uint ScaleX; // Scale of the submesh in the X direction
        public uint ScaleY; // Scale of the submesh in the Y direction
        public uint ScaleZ; // Scale of the submesh in the Z direction
        public Vector3 Position; // Position/Offset coordinates to parent mesh
        public float OrientationW; // Rotation Scalar
        public float OrientationX; // Rotation X
        public float OrientationY; // Rotation Y
        public float OrientationZ; // Rotation Z
        public SiMeshLod MeshLod;

        public System.IO.Stream _bs; public long _bsp; public long Pos => _bs.Position - _bsp;

        public SiMesh(GenericReader r)
        {
            _bs = r.BaseStream; _bsp = _bs.Position;
            LimbId = r.ReadUInt32();
            ParentId = r.ReadUInt32();
            ScaleX = r.ReadUInt32();
            ScaleY = r.ReadUInt32();
            ScaleZ = r.ReadUInt32();
            Position = r.ReadVector3();
            OrientationW = r.ReadSingle();
            OrientationX = r.ReadSingle();
            OrientationY = r.ReadSingle();
            OrientationZ = r.ReadSingle();
            MeshLod = new SiMeshLod(r);
        }
    }

    public class SiMeshLod
    {
        public uint MeshSize; // The size of the submesh in bytes, excluding this value, or 0 if there is no such submesh at this LOD level.
        public uint Flags; // Appears to be a bitmask, with 4 and 8 being most common.
        public uint Unused1;
        public Vector3 SphereCenter; // LOD level's sphere center
        public float SphereRadius; // LOD level's sphere radius
        public Vector3 MinBounds; // Minimum bounding box.
        public Vector3 MaxBounds; // Maximum bounding box.
        public uint Ignorable1;
        public uint Ignorable2;
        public uint FaceCount; // Number of faces in the submesh.
        public uint MountFaceCount;
        public uint VertexCount; // Number of vertices in the submesh.
        public uint MountVertexCount;
        public uint MaxFaceCount;
        public uint MaterialCount; // Number of materials.
        public uint FaceOffset; // Offset of the faces relative to the start of the detail level plus 4.
        public uint MountFaceOffset;
        public uint VertexOffset; // Offset of the vertices relative to the start of the detail level plus 4.
        public uint MountVertexCount2;
        public uint MaterialOffset; // Offset of the materials relative to the start of the detail level plus 4.
        public uint[] SortedFacesOffset = new uint[3];
        public uint ProbablyUnused1;
        public uint ProbablyUnused2;
        public SiFace[] Faces;
        public Vector3[] Vertices;
        public SiMaterial[] Materials;

        public System.IO.Stream _bs; public long _bsp; public long Pos => _bs.Position - _bsp;

        public SiMeshLod(GenericReader r)
        {
            _bs = r.BaseStream; _bsp = _bs.Position;
            MeshSize = r.ReadUInt32();
            Flags = r.ReadUInt32();
            Unused1 = r.ReadUInt32();
            SphereCenter = r.ReadVector3();
            SphereRadius = r.ReadSingle();
            MinBounds = r.ReadVector3();
            MaxBounds = r.ReadVector3();
            Ignorable1 = r.ReadUInt32();
            Ignorable2 = r.ReadUInt32();
            FaceCount = r.ReadUInt32();
            MountFaceCount = r.ReadUInt32();
            VertexCount = r.ReadUInt32();
            MountVertexCount = r.ReadUInt32();
            MaxFaceCount = r.ReadUInt32();
            MaterialCount = r.ReadUInt32();
            FaceOffset = r.ReadUInt32();
            MountFaceOffset = r.ReadUInt32();
            VertexOffset = r.ReadUInt32();
            MountVertexCount2 = r.ReadUInt32();
            MaterialOffset = r.ReadUInt32();
            SortedFacesOffset[0] = r.ReadUInt32();
            SortedFacesOffset[1] = r.ReadUInt32();
            SortedFacesOffset[2] = r.ReadUInt32();
            SortedFacesOffset[3] = r.ReadUInt32();
            ProbablyUnused1 = r.ReadUInt32();
            ProbablyUnused2 = r.ReadUInt32();
            //
            r.Position = FaceOffset + 4;
            Faces = new SiFace[FaceCount];
            for (var i = 0; i < Faces.Length; i++)
                Faces[i] = new SiFace(r);
            r.Position = VertexOffset + 4;
            Vertices = new Vector3[VertexCount];
            for (var i = 0; i < Vertices.Length; i++)
                Vertices[i] = r.ReadVector3();
            r.Position = MaterialOffset + 4;
            Materials = new SiMaterial[MaterialCount];
            for (var i = 0; i < Materials.Length; i++)
                Materials[i] = new SiMaterial(r);
            //r.Position = SortedFacesOffset + 4;
        }
    }

    public class SiFace
    {
        public SiPoint Points; // Points in the face, described below.
        public uint Flags; // only first 12 bits appear to be used
        public uint Flags2; // unused?
        public Vector3 NormalVector;
        public float VectorW;
        public uint Material; // Sometimes a zero-based index into the bitmap16.flx/bitmapC.flx/bitmapsh.flx file (whichever is the active option) for the texture to use. In other cases this has a pattern but no strict correlation to the material. Use the material list instead to select textures
        public uint Color; // Color of the face in RGBA order, each element being between 0 (black/transparent) and 255 (bright/opaque).
        public uint[] CollisionRelated = new uint[2]; // for collision system (index list [so only values from 0, 1, or 2] that contains the index of the vertex that is closest to each of the faces [order is: left,right,front,back,bottom,top]

        public System.IO.Stream _bs; public long _bsp; public long Pos => _bs.Position - _bsp;

        public SiFace(GenericReader r)
        {
            _bs = r.BaseStream; _bsp = _bs.Position;
            Points = new SiPoint(r);
            Flags = r.ReadUInt32();
            Flags2 = r.ReadUInt32();
            NormalVector = r.ReadVector3();
            VectorW = r.ReadSingle();
            Material = r.ReadUInt32();
            Color = r.ReadUInt32();
            CollisionRelated[0] = r.ReadUInt32();
            CollisionRelated[1] = r.ReadUInt32();
        }
    }

    public class SiPoint
    {
        public uint Point; // Point index.
        public uint PointOffset; // Offset to the point in bytes
        public Vector3 Normal; // Not always a unit vector.
        public Vector2 UVCoordinates; // UV coordinates.

        public System.IO.Stream _bs; public long _bsp; public long Pos => _bs.Position - _bsp;

        public SiPoint(GenericReader r)
        {
            _bs = r.BaseStream; _bsp = _bs.Position;
            Point = r.ReadUInt32();
            PointOffset = r.ReadUInt32();
            Normal = r.ReadVector3();
            UVCoordinates = r.ReadVector2();
        }
    }

    public class SiMaterial
    {
        public ushort TextureId; // Zero-based index of the texture to use from the bitmap16.flx/bitmapC.flx/bitmapsh.flx file (whichever is the active option).
        public ushort Flags;
        public ushort SubtextureCount;
        public ushort Flags2;
        public ushort FirstFaceId; // Zero-based index of the first face with this material.
        public ushort FaceCount; // The number of faces with this material.
        public byte DefaultAlpha;
        public byte ModifiedAlpha;
        public byte AnimationStart; // Starting Frame for animation
        public byte AnimationEnd; // Ending Frame for animation
        public byte CurFrame;
        public byte AnimationSpeed; // Speed of animation in frames per second
        public byte AnimationType;
        public byte PlaybackDirection; // 0 - forward, 1 - backward
        public uint AnimationTimerRelated; // Animation timer value

        public System.IO.Stream _bs; public long _bsp; public long Pos => _bs.Position - _bsp;

        public SiMaterial(GenericReader r)
        {
            _bs = r.BaseStream; _bsp = _bs.Position;
            TextureId = r.ReadUInt16();
            Flags = r.ReadUInt16();
            SubtextureCount = r.ReadUInt16();
            Flags2 = r.ReadUInt16();
            FirstFaceId = r.ReadUInt16();
            FaceCount = r.ReadUInt16();
            DefaultAlpha = r.ReadByte();
            ModifiedAlpha = r.ReadByte();
            AnimationStart = r.ReadByte();
            AnimationEnd = r.ReadByte();
            CurFrame = r.ReadByte();
            AnimationSpeed = r.ReadByte();
            AnimationType = r.ReadByte();
            PlaybackDirection = r.ReadByte();
            AnimationTimerRelated = r.ReadUInt32();
        }
    }

    // ---- ANIMATION --- //

    public class SiAnimation
    {
        public uint Index; // Same as the record index.
        public uint Unknown1;
        public uint TotalFrameCount; // The maximum number of frames.
        public uint Unknown2; // Always [1Eh, 21H].
        public string Filename; // The source filename.
        public uint HeaderLength; // The number of elements in the header.
        public uint Unknown3; // The start appears to be repeats of part ids.
        public uint PartCount; // The number of parts in the animation.
        public SiAnimationPart[] Parts; // The list of parts.
        public uint SuffixCount; // The number of suffixes.
        public Vector3Int[] Suffixes; // The list of suffixes, each of which is a uint32 triple. These may be segments of the animation.

        public SiAnimation(GenericReader r)
        {
            Index = r.ReadUInt32();
            Unknown1 = r.ReadUInt32();
            TotalFrameCount = r.ReadUInt32();
            Unknown2 = r.ReadUInt32();
            Filename = null;
            HeaderLength = r.ReadUInt32();
            Unknown3 = r.ReadUInt32();
            PartCount = r.ReadUInt32();
            Parts = new SiAnimationPart[PartCount];
            for (var i = 0; i < Parts.Length; i++)
                Parts[i] = new SiAnimationPart(r);
            SuffixCount = r.ReadUInt32();
            Suffixes = new Vector3Int[SuffixCount];
            for (var i = 0; i < Suffixes.Length; i++)
                Suffixes[i] = new Vector3Int((int)r.ReadUInt32(), (int)r.ReadUInt32(), (int)r.ReadUInt32());
        }
    }

    public class SiAnimationPart
    {
        public uint Id; // A one-based identifier of the part. Values above 255 appear to be unrelated parts from the LightWave file (such as the camera and light rigging).
        public string Name; // Name of the part.
        public uint FrameCount; // Number of frames.
        public SiAnimationFrame[] Frames; // List of frames.

        public SiAnimationPart(GenericReader r)
        {
            Id = r.ReadUInt32();
            Name = "";
            FrameCount = r.ReadUInt32();
            Frames = new SiAnimationFrame[FrameCount];
            for (var i = 0; i < Frames.Length; i++)
                Frames[i] = new SiAnimationFrame(r);
        }
    }

    public class SiAnimationFrame
    {
        public uint Ms; // The number of milliseconds from the start of the animation to display this frame for this part.
        public Vector3 Position; // The relative transform for positioning the part.
        public Vector4 Unknown1; // Possibly rotation-related, but it's not clear how. This doesn't appear to be a quaternion.
        public Vector3 Scale; // The scaling factor.

        public SiAnimationFrame(GenericReader r)
        {
            Ms = r.ReadUInt32();
            Position = r.ReadVector3();
            Unknown1 = r.ReadVector4();
            Scale = r.ReadVector3();
        }
    }
}