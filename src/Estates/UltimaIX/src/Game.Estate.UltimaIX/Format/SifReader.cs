using Game.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Estate.UltimaIX.Format
{
    // ---- SAPPEAR --- //

    public class SiFile
    {
        public readonly string Name;
        public readonly uint MeshCount;
        public readonly uint LodCount;
        public readonly Vector3 CylinderBaseCentre;
        public readonly float CylinderBaseHeight;
        public readonly float CylinderBaseRadius;
        public readonly Vector3 SphereCenter;
        public readonly float SphereRadius;
        public readonly float Unk1;
        public readonly Vector3 MinBounds;
        public readonly Vector3 MaxBounds;
        public readonly uint LodThreshold0;
        public readonly uint LodThreshold1;
        public readonly uint LodThreshold2;
        public readonly uint LodThreshold3;
        public readonly Vector3 CenterOfMass;
        public readonly float MassOrVolume;
        public readonly Matrix4x4 InertiaMatrix;
        public readonly float InertiaRelated;
        public readonly SiMesh[] Meshes;

        public SiFile(string name, GenericReader r)
        {
            Name = name;
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
            var meshCount = (int)(MeshCount * LodCount);
            var meshOffset = r.ReadTArray<uint>(meshCount * 4, meshCount);
            Meshes = new SiMesh[meshCount];
            for (var i = 0; i < meshOffset.Length; i++)
            {
                r.Position = meshOffset[i];
                Meshes[i] = new SiMesh(r);
            }
        }

        public IEnumerable<string> GetTexturePaths()
        {
            foreach (var mesh in Meshes)
                foreach (var materials in mesh.Lod.Materials)
                    if (materials.TextureId != 0)
                        yield return materials.Texture;
        }
    }

    public class SiMesh
    {
        public readonly uint LimbId; // The ID of this submesh
        public readonly uint ParentId; // The ID of the parent mesh
        public readonly uint ScaleX; // Scale of the submesh in the X direction
        public readonly uint ScaleY; // Scale of the submesh in the Y direction
        public readonly uint ScaleZ; // Scale of the submesh in the Z direction
        public readonly Vector3 Position; // Position/Offset coordinates to parent mesh
        public readonly float OrientationW; // Rotation Scalar
        public readonly float OrientationX; // Rotation X
        public readonly float OrientationY; // Rotation Y
        public readonly float OrientationZ; // Rotation Z
        public readonly SiMeshLod Lod;

        public SiMesh(GenericReader r)
        {
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
            Lod = new SiMeshLod(r);
        }
    }

    public class SiMeshLod
    {
        public readonly uint MeshSize; // The size of the submesh in bytes, excluding this value, or 0 if there is no such submesh at this LOD level.
        public readonly uint Flags; // Appears to be a bitmask, with 4 and 8 being most common.
        public readonly uint Unused1;
        public readonly Vector3 SphereCenter; // LOD level's sphere center
        public readonly float SphereRadius; // LOD level's sphere radius
        public readonly Vector3 MinBounds; // Minimum bounding box.
        public readonly Vector3 MaxBounds; // Maximum bounding box.
        public readonly uint Ignorable1;
        public readonly uint Ignorable2;
        public readonly uint FaceCount; // Number of faces in the submesh.
        public readonly uint MountFaceCount;
        public readonly uint VertexCount; // Number of vertices in the submesh.
        public readonly uint MountVertexCount;
        public readonly uint MaxFaceCount;
        public readonly uint MaterialCount; // Number of materials.
        public readonly uint FaceOffset; // Offset of the faces relative to the start of the detail level plus 4.
        public readonly uint MountFaceOffset;
        public readonly uint VertexOffset; // Offset of the vertices relative to the start of the detail level plus 4.
        public readonly uint MountVertexCount2;
        public readonly uint MaterialOffset; // Offset of the materials relative to the start of the detail level plus 4.
        public readonly uint[] SortedFacesOffset = new uint[4];
        public readonly uint ProbablyUnused1;
        public readonly uint ProbablyUnused2;
        public readonly SiFace[] Faces;
        public readonly Vector3[] Vertices;
        public readonly SiMaterial[] Materials;

        public SiMeshLod(GenericReader r)
        {
            var bsp = r.Position;
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
            r.Position = bsp + FaceOffset + 4;
            Faces = new SiFace[FaceCount];
            for (var i = 0; i < Faces.Length; i++)
                Faces[i] = new SiFace(r);
            r.Position = bsp + VertexOffset + 4;
            Vertices = new Vector3[VertexCount];
            for (var i = 0; i < Vertices.Length; i++)
                Vertices[i] = r.ReadVector3();
            r.Position = bsp + MaterialOffset + 4;
            Materials = new SiMaterial[MaterialCount];
            for (var i = 0; i < Materials.Length; i++)
                Materials[i] = new SiMaterial(r);
        }
    }

    public class SiFace
    {
        public readonly SiPoint[] Points; // Points in the face, described below.
        public readonly uint Flags; // only first 12 bits appear to be used
        public readonly uint Flags2; // unused?
        public readonly Vector3 NormalVector;
        public readonly float VectorW;
        public readonly uint Material; // Sometimes a zero-based index into the bitmap16.flx/bitmapC.flx/bitmapsh.flx file (whichever is the active option) for the texture to use. In other cases this has a pattern but no strict correlation to the material. Use the material list instead to select textures
        public readonly uint Color; // Color of the face in RGBA order, each element being between 0 (black/transparent) and 255 (bright/opaque).
        public readonly uint[] CollisionRelated = new uint[2]; // for collision system (index list [so only values from 0, 1, or 2] that contains the index of the vertex that is closest to each of the faces [order is: left,right,front,back,bottom,top]

        public SiFace(GenericReader r)
        {
            Points = new[] { new SiPoint(r), new SiPoint(r), new SiPoint(r) };
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
        public readonly uint Point; // Point index.
        public readonly uint PointOffset; // Offset to the point in bytes
        public readonly Vector3 Normal; // Not always a unit vector.
        public readonly Vector2 UVCoordinates; // UV coordinates.

        public SiPoint(GenericReader r)
        {
            Point = r.ReadUInt32();
            PointOffset = r.ReadUInt32();
            Normal = r.ReadVector3();
            UVCoordinates = r.ReadVector2();
        }
    }

    public class SiMaterial
    {
        public string Texture => $"texture/{TextureId}";
        public readonly ushort TextureId; // Zero-based index of the texture to use from the bitmap16.flx/bitmapC.flx/bitmapsh.flx file (whichever is the active option).
        public readonly ushort Flags;
        public readonly ushort SubtextureCount;
        public readonly ushort Flags2;
        public readonly ushort FirstFaceId; // Zero-based index of the first face with this material.
        public readonly ushort FaceCount; // The number of faces with this material.
        public readonly byte DefaultAlpha;
        public readonly byte ModifiedAlpha;
        public readonly byte AnimationStart; // Starting Frame for animation
        public readonly byte AnimationEnd; // Ending Frame for animation
        public readonly byte CurFrame;
        public readonly byte AnimationSpeed; // Speed of animation in frames per second
        public readonly byte AnimationType;
        public readonly byte PlaybackDirection; // 0 - forward, 1 - backward
        public readonly uint AnimationTimerRelated; // Animation timer value

        public SiMaterial(GenericReader r)
        {
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
        public readonly uint Index; // Same as the record index.
        public readonly uint Unknown1;
        public readonly uint TotalFrameCount; // The maximum number of frames.
        public readonly uint Unknown2; // Always [1Eh, 21H].
        public readonly string Filename; // The source filename.
        public readonly uint HeaderLength; // The number of elements in the header.
        public readonly uint Unknown3; // The start appears to be repeats of part ids.
        public readonly uint PartCount; // The number of parts in the animation.
        public readonly SiAnimationPart[] Parts; // The list of parts.
        public readonly uint SuffixCount; // The number of suffixes.
        public readonly Vector3Int[] Suffixes; // The list of suffixes, each of which is a uint32 triple. These may be segments of the animation.

        public System.IO.Stream _bs; public long _bsp; public long Pos => _bs.Position - _bsp;

        public SiAnimation(GenericReader r)
        {
            _bs = r.BaseStream; _bsp = _bs.Position;
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
        public readonly uint Id; // A one-based identifier of the part. Values above 255 appear to be unrelated parts from the LightWave file (such as the camera and light rigging).
        public readonly string Name; // Name of the part.
        public readonly uint FrameCount; // Number of frames.
        public readonly SiAnimationFrame[] Frames; // List of frames.

        public System.IO.Stream _bs; public long _bsp; public long Pos => _bs.Position - _bsp;

        public SiAnimationPart(GenericReader r)
        {
            _bs = r.BaseStream; _bsp = _bs.Position;
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
        public readonly uint Ms; // The number of milliseconds from the start of the animation to display this frame for this part.
        public readonly Vector3 Position; // The relative transform for positioning the part.
        public readonly Vector4 Unknown1; // Possibly rotation-related, but it's not clear how. This doesn't appear to be a quaternion.
        public readonly Vector3 Scale; // The scaling factor.

        public System.IO.Stream _bs; public long _bsp; public long Pos => _bs.Position - _bsp;

        public SiAnimationFrame(GenericReader r)
        {
            _bs = r.BaseStream; _bsp = _bs.Position;
            Ms = r.ReadUInt32();
            Position = r.ReadVector3();
            Unknown1 = r.ReadVector4();
            Scale = r.ReadVector3();
        }
    }
}