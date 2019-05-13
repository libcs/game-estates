using System;
using System.Collections.Generic;
using System.IO;
using static Gamer.Core.Debug;

namespace Gamer.Format.Cry
{
    /// <summary>
    /// String32 Name, int Start, int End - complete
    /// </summary>
    public struct RangeEntity
    {
        public string Name; // String32! 32 byte char array.
        public int Start;
        public int End;
    }

    public struct Matrix3x3    // a 3x3 transformation matrix
    {
        public float m00;
        public float m01;
        public float m02;
        public float m10;
        public float m11;
        public float m12;
        public float m20;
        public float m21;
        public float m22;

        /// <summary>
        /// Determines whether this instance is identity.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is identity; otherwise, <c>false</c>.
        /// </returns>
        public bool IsIdentity() => Math.Abs(m00 - 1.0) > 0.00001 ||
            Math.Abs(m01) > 0.00001 ||
            Math.Abs(m02) > 0.00001 ||
            Math.Abs(m10) > 0.00001 ||
            Math.Abs(m11 - 1.0) > 0.00001 ||
            Math.Abs(m12) > 0.00001 ||
            Math.Abs(m20) > 0.00001 ||
            Math.Abs(m21) > 0.00001 ||
            Math.Abs(m22 - 1.0) > 0.00001
                ? false
                : true;

        /// <summary>
        /// Gets the copy.
        /// </summary>
        /// <returns>copy of the matrix33</returns>
        public Matrix3x3 GetCopy() => new Matrix3x3
        {
            m00 = m00,
            m01 = m01,
            m02 = m02,
            m10 = m10,
            m11 = m11,
            m12 = m12,
            m20 = m20,
            m21 = m21,
            m22 = m22
        };

        public float GetDeterminant() =>
            m00 * m11 * m22
            + m01 * m12 * m20
            + m02 * m10 * m21
            - m20 * m11 * m02
            - m10 * m01 * m22
            - m00 * m21 * m12;

        /// <summary>
        /// Gets the transpose.
        /// </summary>
        /// <returns>copy of the matrix33</returns>
        public Matrix3x3 GetTranspose() => new Matrix3x3
        {
            m00 = m00,
            m01 = m10,
            m02 = m20,
            m10 = m01,
            m11 = m11,
            m12 = m21,
            m20 = m02,
            m21 = m12,
            m22 = m22
        };

        public Matrix3x3 Mult(Matrix3x3 mat) => new Matrix3x3
        {
            m00 = (m00 * mat.m00) + (m01 * mat.m10) + (m02 * mat.m20),
            m01 = (m00 * mat.m01) + (m01 * mat.m11) + (m02 * mat.m21),
            m02 = (m00 * mat.m02) + (m01 * mat.m12) + (m02 * mat.m22),
            m10 = (m10 * mat.m00) + (m11 * mat.m10) + (m12 * mat.m20),
            m11 = (m10 * mat.m01) + (m11 * mat.m11) + (m12 * mat.m21),
            m12 = (m10 * mat.m02) + (m11 * mat.m12) + (m12 * mat.m22),
            m20 = (m20 * mat.m00) + (m21 * mat.m10) + (m22 * mat.m20),
            m21 = (m20 * mat.m01) + (m21 * mat.m11) + (m22 * mat.m21),
            m22 = (m20 * mat.m02) + (m21 * mat.m12) + (m22 * mat.m22)
        };

        public static Matrix3x3 operator *(Matrix3x3 lhs, Matrix3x3 rhs) => lhs.Mult(rhs);

        /// <summary>
        /// Multiply the 3x3 matrix by a Vector 3 to get the rotation
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        public Vector3 Mult3x1(Vector3 vector) => new Vector3
        {
            x = (vector.x * m00) + (vector.y * m10) + (vector.z * m20),
            y = (vector.x * m01) + (vector.y * m11) + (vector.z * m21),
            z = (vector.x * m02) + (vector.y * m12) + (vector.z * m22)
        };

        public static Vector3 operator *(Matrix3x3 rhs, Vector3 lhs) => rhs.Mult3x1(lhs);

        /// <summary>
        /// Determines whether the matrix decomposes nicely into scale * rotation.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is scale rotation]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsScaleRotation()
        {
            var transpose = GetTranspose();
            var mat = Mult(transpose);
            if (Math.Abs(mat.m01) + Math.Abs(mat.m02)
                + Math.Abs(mat.m10) + Math.Abs(mat.m12)
                + Math.Abs(mat.m20) + Math.Abs(mat.m21) > 0.01)
            {
                Log(" is a Scale_Rot matrix");
                return false;
            }
            Log(" is not a Scale_Rot matrix");
            return true;
        }

        /// <summary>
        /// Get the scale, assuming IsScaleRotation is true
        /// </summary>
        /// <returns></returns>
        public Vector3 GetScale()
        {
            var mat = Mult(GetTranspose());
            var scale = new Vector3
            {
                x = (float)Math.Pow(mat.m00, 0.5f),
                y = (float)Math.Pow(mat.m11, 0.5f),
                z = (float)Math.Pow(mat.m22, 0.5f)
            };
            if (GetDeterminant() < 0)
            {
                scale.x = 0 - scale.x;
                scale.y = 0 - scale.y;
                scale.z = 0 - scale.z;
                return scale;
            }
            return scale;
        }

        /// <summary>
        /// Gets the scale, should also return the rotation matrix, but..eh...
        /// </summary>
        /// <returns></returns>
        public Vector3 GetScaleRotation() => GetScale();

        public bool IsRotation()
        {
            // NOTE: 0.01 instead of CgfFormat.EPSILON to work around bad files
            if (!IsScaleRotation()) return false;
            var scale = GetScale();
            return Math.Abs(scale.x - 1.0) > 0.01 || Math.Abs(scale.y - 1.0) > 0.01 || Math.Abs(scale.z - 1.0) > 0.1 ? false : true;
        }

        public float Determinant() => this.ToMathMatrix().Determinant();
        public Matrix3x3 Inverse() => StructsExtensions.GetMatrix3x3(this.ToMathMatrix().Inverse());
        public Matrix3x3 Conjugate() => StructsExtensions.GetMatrix3x3(this.ToMathMatrix().Conjugate());
        public Matrix3x3 ConjugateTranspose() => StructsExtensions.GetMatrix3x3(this.ToMathMatrix().ConjugateTranspose());
        public Matrix3x3 ConjugateTransposeThisAndMultiply(Matrix3x3 inputMatrix) => StructsExtensions.GetMatrix3x3(this.ToMathMatrix().ConjugateTransposeThisAndMultiply(inputMatrix.ToMathMatrix()));
        public Vector3 Diagonal() => new Vector3().GetVector3(this.ToMathMatrix().Diagonal());
    }

    /// <summary>
    /// Vertex with position p(Vector3) and normal n(Vector3)
    /// </summary>
    public struct Vertex
    {
        public Vector3 p;  // position
        public Vector3 n;  // normal
    }

    /// <summary>
    /// mesh face (3 vertex, Material index, smoothing group.  All ints)
    /// </summary>
    public struct Face
    {
        public int v0; // first vertex
        public int v1; // second vertex
        public int v2; // third vertex
        public int Material; // Material Index
        public int SmGroup; //smoothing group
    }

    /// <summary>
    /// Contains data about the parts of a mesh, such as vertices, radius and center.
    /// </summary>
    public struct MeshSubset
    {
        public uint FirstIndex;
        public uint NumIndices;
        public uint FirstVertex;
        public uint NumVertices;
        public uint MatID;
        public float Radius;
        public Vector3 Center;

        public void WriteMeshSubset()
        {
            Log($"*** Mesh Subset ***");
            Log($"    First Index:  {FirstIndex}");
            Log($"    Num Indices:  {NumIndices}");
            Log($"    First Vertex: {FirstVertex}");
            Log($"    Num Vertices: {NumVertices}");
            Log($"    Mat ID:       {MatID}");
            Log($"    Radius:       {Radius:F7}");
            Log($"    Center:");
            Center.WriteVector3();
        }
    }

    public struct Key
    {
        public int Time; // Time in ticks
        public Vector3 AbsPos; // absolute position
        public Vector3 RelPos; // relative position
        public Quat RelQuat; //Relative Quaternion if ARG==1?
        public Vector3 Unknown1; // If ARG==6 or 10?
        public float[] Unknown2; // If ARG==9?  array length = 2
    }

    public struct UV
    {
        public float U;
        public float V;
    }

    public struct UVFace
    {
        public int t0; // first vertex index
        public int t1; // second vertex index
        public int t2; // third vertex index
    }

    public struct ControllerInfo
    {
        public uint ControllerID;
        public uint PosKeyTimeTrack;
        public uint PosTrack;
        public uint RotKeyTimeTrack;
        public uint RotTrack;
    }

    // Fill this in later.  line 369 in cgf.xml.
    //public struct TextureMap { }

    public struct IRGB
    {
        public byte r; // red
        public byte g; // green
        public byte b; // blue

        public IRGB Read(BinaryReader b) => new IRGB
        {
            r = b.ReadByte(),
            g = b.ReadByte(),
            b = b.ReadByte()
        };
    }

    /// <summary>
    /// May also be known as ColorB.
    /// </summary>
    public struct IRGBA
    {
        public byte r; // red
        public byte g; // green
        public byte b; // blue
        public byte a; // alpha

        public IRGBA Read(BinaryReader b) => new IRGBA
        {
            r = b.ReadByte(),
            g = b.ReadByte(),
            b = b.ReadByte(),
            a = b.ReadByte()
        };
    }

    public struct FRGB
    {
        public float r; // float Red
        public float g; // float green
        public float b; // float blue
    }

    public struct AABB
    {
        Vector3 min;
        Vector3 max;
    }

    public struct Tangent
    {
        // Tangents.  Divide each component by 32767 to get the actual value
        public float x;
        public float y;
        public float z;
        public float w;  // Handness?  Either 32767 (+1.0) or -32767 (-1.0)
    }

    public struct SkinVertex
    {
        public int Volumetric;
        public int[] Index;     // Array of 4 ints
        public float[] w;       // Array of 4 floats
        public Matrix3x3 M;
    }

    /// <summary>
    /// WORLDTOBONE is also the Bind Pose Matrix (BPM)
    /// </summary>
    public struct WORLDTOBONE
    {
        public float[,] worldToBone;   //  4x3 structure

        public void GetWorldToBone(BinaryReader b)
        {
            worldToBone = new float[3, 4];
            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 4; j++)
                    worldToBone[i, j] = b.ReadSingle();
            //Log($"worldToBone: {worldToBone[i, j]:F7}");
            return;
        }

        public Matrix4x4 GetMatrix44() => new Matrix4x4
        {
            m00 = worldToBone[0, 0],
            m01 = worldToBone[0, 1],
            m02 = worldToBone[0, 2],
            m03 = worldToBone[0, 3],
            m10 = worldToBone[1, 0],
            m11 = worldToBone[1, 1],
            m12 = worldToBone[1, 2],
            m13 = worldToBone[1, 3],
            m20 = worldToBone[2, 0],
            m21 = worldToBone[2, 1],
            m22 = worldToBone[2, 2],
            m23 = worldToBone[2, 3],
            m30 = 0,
            m31 = 0,
            m32 = 0,
            m33 = 1
        };

        public void WriteWorldToBone()
        {
            //Log("     *** World to Bone ***");
            Log($"     {worldToBone[0, 0]:F7}  {worldToBone[0, 1]:F7}  {worldToBone[0, 2]:F7}  {worldToBone[0, 3]:F7}");
            Log($"     {worldToBone[1, 0]:F7}  {worldToBone[1, 1]:F7}  {worldToBone[1, 2]:F7}  {worldToBone[1, 3]:F7}");
            Log($"     {worldToBone[2, 0]:F7}  {worldToBone[2, 1]:F7}  {worldToBone[2, 2]:F7}  {worldToBone[2, 3]:F7}");
        }

        internal Matrix3x3 GetWorldToBoneRotationMatrix() => new Matrix3x3
        {
            m00 = worldToBone[0, 0],
            m01 = worldToBone[0, 1],
            m02 = worldToBone[0, 2],
            m10 = worldToBone[1, 0],
            m11 = worldToBone[1, 1],
            m12 = worldToBone[1, 2],
            m20 = worldToBone[2, 0],
            m21 = worldToBone[2, 1],
            m22 = worldToBone[2, 2]
        };

        internal Vector3 GetWorldToBoneTranslationVector() => new Vector3
        {
            x = worldToBone[0, 3],
            y = worldToBone[1, 3],
            z = worldToBone[2, 3]
        };
    }

    /// <summary>
    /// BONETOWORLD contains the world space location/rotation of a bone.
    /// </summary>
    public struct BONETOWORLD
    {
        public float[,] boneToWorld;   //  4x3 structure

        public void ReadBoneToWorld(BinaryReader b)
        {
            boneToWorld = new float[3, 4];
            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 4; j++)
                    boneToWorld[i, j] = b.ReadSingle();
            //Log($"boneToWorld: {boneToWorld[i, j]:F7}");
            return;
        }

        /// <summary>
        /// Returns the world space rotational matrix in a Math.net 3x3 matrix.
        /// </summary>
        /// <returns>Matrix33</returns>
        public Matrix3x3 GetBoneToWorldRotationMatrix() => new Matrix3x3
        {
            m00 = boneToWorld[0, 0],
            m01 = boneToWorld[0, 1],
            m02 = boneToWorld[0, 2],
            m10 = boneToWorld[1, 0],
            m11 = boneToWorld[1, 1],
            m12 = boneToWorld[1, 2],
            m20 = boneToWorld[2, 0],
            m21 = boneToWorld[2, 1],
            m22 = boneToWorld[2, 2]
        };

        public Vector3 GetBoneToWorldTranslationVector() => new Vector3
        {
            x = boneToWorld[0, 3],
            y = boneToWorld[1, 3],
            z = boneToWorld[2, 3]
        };

        public void WriteBoneToWorld()
        {
            Log($"*** Bone to World ***");
            Log($"{boneToWorld[0, 0]:F6}  {boneToWorld[0, 1]:F6}  {boneToWorld[0, 2]:F6} {boneToWorld[0, 3]:F6}");
            Log($"{boneToWorld[1, 0]:F6}  {boneToWorld[1, 1]:F6}  {boneToWorld[1, 2]:F6} {boneToWorld[1, 3]:F6}");
            Log($"{boneToWorld[2, 0]:F6}  {boneToWorld[2, 1]:F6}  {boneToWorld[2, 2]:F6} {boneToWorld[2, 3]:F6}");
        }
    }

    public struct PhysicsGeometry
    {
        public uint physicsGeom;
        public uint flags;              // 0x0C ?
        public Vector3 min;
        public Vector3 max;
        public Vector3 spring_angle;
        public Vector3 spring_tension;
        public Vector3 damping;
        public Matrix3x3 framemtx;

        /// <summary>
        /// Read a PhysicsGeometry structure
        /// </summary>
        /// <param name="b">The b.</param>
        public void ReadPhysicsGeometry(BinaryReader b)
        {
            physicsGeom = b.ReadUInt32();
            flags = b.ReadUInt32();
            min.ReadVector3(b);
            // min.WriteVector3();
            max.ReadVector3(b);
            // max.WriteVector3();
            spring_angle.ReadVector3(b);
            spring_tension.ReadVector3(b);
            damping.ReadVector3(b);
            framemtx.ReadMatrix3x3(b);
            return;
        }

        public void WritePhysicsGeometry()
        {
            Log("WritePhysicsGeometry");
        }
    }

    /// <summary>
    /// This is the same as BoneDescData
    /// </summary>
    public class CompiledBone
    {
        public uint ControllerID { get; set; }
        public PhysicsGeometry[] physicsGeometry;   // 2 of these.  One for live objects, other for dead (ragdoll?)
        public float mass;                         // 0xD8 ?
        public WORLDTOBONE worldToBone;             // 4x3 matrix
        public BONETOWORLD boneToWorld;             // 4x3 matrix of world translations/rotations of the bones.
        public string boneName;                     // String256 in old terms; convert to a real null terminated string.
        public uint limbID;                         // ID of this limb... usually just 0xFFFFFFFF
        public int offsetParent;                    // offset to the parent in number of CompiledBone structs (584 bytes)
        public int offsetChild;                     // Offset to the first child to this bone in number of CompiledBone structs
        public uint numChildren;                    // Number of children to this bone

        public uint parentID;                       // Not part of the read structure, but the controllerID of the parent bone put into the Bone Dictionary (the key)
        public long offset;                        // Not part of the structure, but the position in the file where this bone started.
        public List<uint> childIDs;                 // Not part of read struct.  Contains the controllerIDs of the children to this bone.
        public Matrix4x4 LocalTransform = new Matrix4x4();            // Because Cryengine tends to store transform relative to world, we have to add all the transforms from the node to the root.  Calculated, row major.
        public Vector3 LocalTranslation = new Vector3();            // To hold the local rotation vector
        public Matrix3x3 LocalRotation = new Matrix3x3();             // to hold the local rotation matrix

        public CompiledBone ParentBone { get; set; }

        public void ReadCompiledBone(BinaryReader b)
        {
            // Reads just a single 584 byte entry of a bone. At the end the seek position will be advanced, so keep that in mind.
            ControllerID = b.ReadUInt32(); // unique id of bone (generated from bone name)
            physicsGeometry = new PhysicsGeometry[2];
            physicsGeometry[0].ReadPhysicsGeometry(b); // lod 0 is the physics of alive body, 
            physicsGeometry[1].ReadPhysicsGeometry(b); // lod 1 is the physics of a dead body
            mass = b.ReadSingle();
            worldToBone = new WORLDTOBONE();
            worldToBone.GetWorldToBone(b);
            boneToWorld = new BONETOWORLD();
            boneToWorld.ReadBoneToWorld(b);
            boneName = b.ReadFString(256);
            limbID = b.ReadUInt32();
            offsetParent = b.ReadInt32();
            numChildren = b.ReadUInt32();
            offsetChild = b.ReadInt32();
            childIDs = new List<uint>(); // Calculated
        }

        public Matrix4x4 ToMatrix44(float[,] boneToWorld) => new Matrix4x4
        {
            m00 = boneToWorld[0, 0],
            m01 = boneToWorld[0, 1],
            m02 = boneToWorld[0, 2],
            m03 = boneToWorld[0, 3],
            m10 = boneToWorld[1, 0],
            m11 = boneToWorld[1, 1],
            m12 = boneToWorld[1, 2],
            m13 = boneToWorld[1, 3],
            m20 = boneToWorld[2, 0],
            m21 = boneToWorld[2, 1],
            m22 = boneToWorld[2, 2],
            m23 = boneToWorld[2, 3],
            m30 = 0,
            m31 = 0,
            m32 = 0,
            m33 = 1
        };

        public void WriteCompiledBone()
        {
            // Output the bone to the console
            Log($"*** Compiled bone {boneName}");
            Log($"    Parent Name: {parentID}");
            Log($"    Offset in file: {offset:X}");
            Log($"    Controller ID: {ControllerID}");
            Log($"    World To Bone:");
            boneToWorld.WriteBoneToWorld();
            Log($"    Limb ID: {limbID}");
            Log($"    Parent Offset: {offsetParent}");
            Log($"    Child Offset:  {offsetChild}");
            Log($"    Number of Children:  {numChildren}");
            Log($"*** End Bone {boneName}");
        }
    }

    public class CompiledPhysicalBone
    {
        public uint BoneIndex;
        public uint ParentOffset;
        public uint NumChildren;
        public uint ControllerID;
        public char[] prop;
        public PhysicsGeometry PhysicsGeometry;
        // Calculated values
        public long offset;
        public uint parentID; // ControllerID of parent
        public List<uint> childIDs; // Not part of read struct.  Contains the controllerIDs of the children to this bone.

        public CompiledBone GetBonePartner() => null;

        public void ReadCompiledPhysicalBone(BinaryReader b)
        {
            // Reads just a single 584 byte entry of a bone. At the end the seek position will be advanced, so keep that in mind.
            BoneIndex = b.ReadUInt32(); // unique id of bone (generated from bone name)
            ParentOffset = b.ReadUInt32();
            NumChildren = b.ReadUInt32();
            ControllerID = b.ReadUInt32();
            prop = b.ReadChars(32); // Not sure what this is used for.
            PhysicsGeometry.ReadPhysicsGeometry(b);
            // Calculated values
            childIDs = new List<uint>();
        }

        public void WriteCompiledPhysicalBone()
        {
            // Output the bone to the console
            Log($"*** Compiled bone ID {BoneIndex}");
            Log($"    Parent Offset: {ParentOffset}");
            Log($"    Controller ID: {ControllerID}");
            Log($"*** End Bone {BoneIndex}");
        }
    }

    /// <summary>
    /// A bone initial position matrix.
    /// </summary>
    public struct InitialPosMatrix
    {
        Matrix3x3 Rot;
        Vector3 Pos;
    }

    public struct BoneLink
    {
        public int BoneID;
        public Vector3 offset;
        public float Blending;
    }

    public class DirectionalBlends
    {
        public string AnimToken;
        public uint AnimTokenCRC32;
        public string ParaJointName;
        public short ParaJointIndex;
        public short RotParaJointIndex;
        public string StartJointName;
        public short StartJointIndex;
        public short RotStartJointIndex;
        public string ReferenceJointName;
        public short ReferenceJointIndex;

        public DirectionalBlends()
        {
            AnimToken = string.Empty;
            AnimTokenCRC32 = 0;
            ParaJointName = string.Empty;
            ParaJointIndex = -1;
            RotParaJointIndex = -1;
            StartJointName = string.Empty;
            StartJointIndex = -1;
            RotStartJointIndex = -1;
            ReferenceJointName = string.Empty;
            ReferenceJointIndex = 1;  //by default we use the Pelvis
        }
    };

    #region Skinning Structures

    public struct BoneEntity
    {
        int Bone_Id;                 //" type="int">Bone identifier.</add>
        int Parent_Id;               //" type="int">Parent identifier.</add>
        int Num_Children;            //" type="uint" />
        uint Bone_Name_CRC32;         //" type="uint">CRC32 of bone name as listed in the BoneNameListChunk.  In Python this can be calculated using zlib.crc32(name)</add>
        string Properties;            //" type="String32" />
        BonePhysics Physics;            //" type="BonePhysics" />
    }

    public struct BonePhysics           // 26 total words = 104 total bytes
    {
        uint Geometry;                //" type="Ref" template="BoneMeshChunk">Geometry of a separate mesh for this bone.</add>
        //<!-- joint parameters -->
        uint Flags;                   //" type="uint" />
        Vector3 Min;                   //" type="Vector3" />
        Vector3 Max;                   //" type="Vector3" />
        Vector3 Spring_Angle;          //" type="Vector3" />
        Vector3 Spring_Tension;        //" type="Vector3" />
        Vector3 Damping;               //" type="Vector3" />
        Matrix3x3 Frame_Matrix;        //" type="Matrix33" />
    }

    /// <summary>
    /// 4 bones, 4 weights for each vertex mapping.
    /// </summary>
    public struct MeshBoneMapping
    {
        public int[] BoneIndex;
        public int[] Weight; // Byte / 256?
    }

    public struct MeshPhysicalProxyHeader
    {
        public uint ChunkID;
        public uint NumPoints;
        public uint NumIndices;
        public uint NumMaterials;
    }

    public struct MeshMorphTargetHeader
    {
        public uint MeshID;
        public uint NameLength;
        public uint NumIntVertices;
        public uint NumExtVertices;
    }

    public struct MeshMorphTargetVertex
    {
        public uint VertexID;
        public Vector3 Vertex;

        public static MeshMorphTargetVertex Read(BinaryReader b)
        {
            var vertex = new MeshMorphTargetVertex
            {
                VertexID = b.ReadUInt32()
            };
            vertex.Vertex.ReadVector3(b);
            return vertex;
        }
    }

    public struct MorphTargets
    {
        uint MeshID;
        string Name;
        List<MeshMorphTargetVertex> IntMorph;
        List<MeshMorphTargetVertex> ExtMorph;
    }

    public struct TFace
    {
        public ushort i0, i1, i2;

        //public static bool operator =(TFace face)
        //{
        //    if (face.i0 == i0 && face.i1 == i1 && face.i2 == i2) return true;
        //    else return false;
        //}
    }

    public class MeshCollisionInfo
    {
        // AABB AABB;       // Bounding box structures?
        // OBB OBB;         // Has an m33, h and c value.
        public Vector3 Position;
        public List<short> Indices;
        public int BoneID;
    }

    public struct IntSkinVertex
    {
        public Vector3 Obsolete0;
        public Vector3 Position;
        public Vector3 Obsolete2;
        public ushort[] BoneIDs;     // 4 bone IDs
        public float[] Weights;     // Should be 4 of these
        public IRGBA Color;
    }

    public struct SpeedChunk
    {
        public float Speed;
        public float Distance;
        public float Slope;
        public int AnimFlags;
        public float[] MoveDir;
        public Quat StartPosition;
    }

    public struct PhysicalProxy
    {
        public uint ID;             // Chunk ID (although not technically a chunk
        public uint FirstIndex;
        public uint NumIndices;
        public uint FirstVertex;
        public uint NumVertices;
        public uint Material;     // Size of the weird data at the end of the hitbox structure.
        public Vector3[] Vertices;    // Array of vertices (x,y,z) length NumVertices
        public ushort[] Indices;      // Array of indices

        public void WriteHitBox()
        {
            Log($"     ** Hitbox **");
            Log($"        ID: {ID:X}");
            Log($"        Num Vertices: {NumVertices:X}");
            Log($"        Num Indices:  {NumIndices:X}");
            Log($"        Material Index: {Material:X}");
        }
    }

    public struct PhysicalProxyStub
    {
        uint ChunkID;
        List<Vector3> Points;
        List<short> Indices;
        List<string> Materials;
    }

    #endregion

    public struct PhysicsData
    {
        // Collision or hitbox info.  Part of the MeshPhysicsData chunk
        public int Unknown4;
        public int Unknown5;
        public float[] Unknown6;  // array length 3, Inertia?
        public Quat Rot;  // Most definitely a quaternion. Probably describes rotation of the physics object.
        public Vector3 Center;  // Center, or position. Probably describes translation of the physics object. Often corresponds to the center of the mesh data as described in the submesh chunk.
        public float Unknown10; // Mass?
        public int Unknown11;
        public int Unknown12;
        public float Unknown13;
        public float Unknown14;
        public PhysicsPrimitiveType PrimitiveType;
        public PhysicsCube Cube;  // Primitive Type 0
        public PhysicsPolyhedron PolyHedron;  // Primitive Type 1
        public PhysicsCylinder Cylinder; // Primitive Type 5
        public PhysicsShape6 UnknownShape6;  // Primitive Type 6
    }

    public struct PhysicsCube
    {
        public PhysicsStruct1 Unknown14;
        public PhysicsStruct1 Unknown15;
        public int Unknown16;
    }

    public struct PhysicsPolyhedron
    {
        public uint NumVertices;
        public uint NumTriangles;
        public int Unknown17;
        public int Unknown18;
        public byte HasVertexMap;
        public ushort[] VertexMap; // Array length NumVertices.  If the (non-physics) mesh has say 200 vertices, then the first 200
                                   // entries of this map give a mapping identifying the unique vertices.
                                   // The meaning of the extra entries is unknown.
        public byte UseDatasStream;
        public Vector3[] Vertices; // Array Length NumVertices
        public ushort[] Triangles; // Array length NumTriangles
        public byte Unknown210;
        public byte[] TriangleFlags; // Array length NumTriangles
        public ushort[] TriangleMap; // Array length NumTriangles
        public byte[] Unknown45; // Array length 16
        public int Unknown461;  //0
        public int Unknown462;  //0
        public float Unknown463; // 0.02
        public float Unknown464;
        // There is more.  See cgf.xml for the rest, but probably not really needed
    }

    public struct PhysicsCylinder
    {
        public float[] Unknown1;  // array length 8
        public int Unknown2;
        public PhysicsDataType2 Unknown3;
    }

    public struct PhysicsShape6
    {
        public float[] Unknown1; // array length 8
        public int Unknown2;
        public PhysicsDataType2 Unknown3;
    }

    public struct PhysicsDataType0
    {
        public int NumData;
        public PhysicsStruct2[] Data; // Array length NumData
        public int[] Unknown33; // array length 3
        public float Unknown80;
    }

    public struct PhysicsDataType1
    {
        public uint NumData1;  // usually 4294967295
        public PhysicsStruct50[] Data1; // Array length NumData1
        public int NumData2;
        public PhysicsStruct50[] Data2; // Array length NumData2
        public float[] Unknown60; // array length 6
        public Matrix3x3 Unknown61; // Rotation matrix?
        public int[] Unknown70; //Array length 3
        public float Unknown80;
    }

    public struct PhysicsDataType2
    {
        public Matrix3x3 Unknown1;
        public int Unknown;
        public float[] Unknown3; // array length 6
        public int Unknown4;
    }

    public struct PhysicsStruct1
    {
        public Matrix3x3 Unknown1;
        public int Unknown2;
        public float[] Unknown3; // array length 6
    }

    public struct PhysicsStruct2
    {
        public Matrix3x3 Unknown1;
        public float[] Unknown2;  // array length 6
        public int[] Unknown3; // array length 3
    }

    public struct PhysicsStruct50
    {
        public short Unknown11;
        public short Unknown12;
        public short Unknown21;
        public short Unknown22;
        public short Unknown23;
        public short Unknown24;
    }
}