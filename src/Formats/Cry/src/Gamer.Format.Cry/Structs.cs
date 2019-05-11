using MathNet.Numerics.LinearAlgebra;
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

    /// <summary>
    /// Vector in 3D space {x,y,z}
    /// </summary>
    public struct Vector3
    {
        public double x;
        public double y;
        public double z;
        public double w; // Currently Unused
        object p1;
        object p2;
        object p3;

        public Vector3(object p1, object p2, object p3) : this() { this.p1 = p1; this.p2 = p2; this.p3 = p3; }
        public void ReadVector3(BinaryReader b) { x = b.ReadSingle(); y = b.ReadSingle(); z = b.ReadSingle(); return; }
        public Vector3 Add(Vector3 vector) => new Vector3 { x = vector.x + x, y = vector.y + y, z = vector.z + z };
        public static Vector3 operator +(Vector3 lhs, Vector3 rhs) => new Vector3() { x = lhs.x + rhs.x, y = lhs.y + rhs.y, z = lhs.z + rhs.z };
        public static Vector3 operator -(Vector3 lhs, Vector3 rhs) => new Vector3() { x = lhs.x - rhs.x, y = lhs.y - rhs.y, z = lhs.z - rhs.z };
        public Vector4 ToVector4() => new Vector4 { x = x, y = y, z = z, w = 1 };
        internal Vector<double> ToMathVector3() { var r = Vector<double>.Build.Dense(3); r[0] = x; r[1] = y; r[2] = z; return r; }
        public Vector3 GetVector3(Vector<double> vector) => new Vector3 { x = vector[0], y = vector[1], z = vector[2] };
        public void WriteVector3(string label = null)
        {
            Log($"*** WriteVector3 *** - {label}");
            Log($"{x:F7}  {y:F7}  {z:F7}");
            Log();
        }
    }

    public struct Vector4
    {
        public double x;
        public double y;
        public double z;
        public double w;

        public Vector4(double x, double y, double z, double w) { this.x = x; this.y = y; this.z = z; this.w = w; }
        public Vector3 ToVector3() { var r = new Vector3(); if (w == 0) { r.x = x; r.y = y; r.z = z; } else { r.x = x / w; r.y = y / w; r.z = z / w; } return r; }
        public void WriteVector4()
        {
            Log("=============================================");
            Log($"x:{x:F7}  y:{y:F7}  z:{z:F7} w:{w:F7}");
        }
    }

    public struct Matrix33    // a 3x3 transformation matrix
    {
        public double m11;
        public double m12;
        public double m13;
        public double m21;
        public double m22;
        public double m23;
        public double m31;
        public double m32;
        public double m33;

        public void ReadMatrix33(BinaryReader b)
        {
            // Reads a Matrix33 structure
            m11 = b.ReadSingle();
            m12 = b.ReadSingle();
            m13 = b.ReadSingle();
            m21 = b.ReadSingle();
            m22 = b.ReadSingle();
            m23 = b.ReadSingle();
            m31 = b.ReadSingle();
            m32 = b.ReadSingle();
            m33 = b.ReadSingle();
        }

        /// <summary>
        /// Determines whether this instance is identity.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is identity; otherwise, <c>false</c>.
        /// </returns>
        public bool IsIdentity() => Math.Abs(m11 - 1.0) > 0.00001 ||
            Math.Abs(m12) > 0.00001 ||
            Math.Abs(m13) > 0.00001 ||
            Math.Abs(m21) > 0.00001 ||
            Math.Abs(m22 - 1.0) > 0.00001 ||
            Math.Abs(m23) > 0.00001 ||
            Math.Abs(m31) > 0.00001 ||
            Math.Abs(m32) > 0.00001 ||
            Math.Abs(m33 - 1.0) > 0.00001
                ? false
                : true;

        /// <summary>
        /// Gets the copy.
        /// </summary>
        /// <returns>copy of the matrix33</returns>
        public Matrix33 GetCopy() => new Matrix33
        {
            m11 = m11,
            m12 = m12,
            m13 = m13,
            m21 = m21,
            m22 = m22,
            m23 = m23,
            m31 = m31,
            m32 = m32,
            m33 = m33
        };

        public double GetDeterminant() =>
            m11 * m22 * m33
            + m12 * m23 * m31
            + m13 * m21 * m32
            - m31 * m22 * m13
            - m21 * m12 * m33
            - m11 * m32 * m23;

        /// <summary>
        /// Gets the transpose.
        /// </summary>
        /// <returns>copy of the matrix33</returns>
        public Matrix33 GetTranspose() => new Matrix33
        {
            m11 = m11,
            m12 = m21,
            m13 = m31,
            m21 = m12,
            m22 = m22,
            m23 = m32,
            m31 = m13,
            m32 = m23,
            m33 = m33
        };

        public Matrix33 Mult(Matrix33 mat) => new Matrix33
        {
            m11 = (m11 * mat.m11) + (m12 * mat.m21) + (m13 * mat.m31),
            m12 = (m11 * mat.m12) + (m12 * mat.m22) + (m13 * mat.m32),
            m13 = (m11 * mat.m13) + (m12 * mat.m23) + (m13 * mat.m33),
            m21 = (m21 * mat.m11) + (m22 * mat.m21) + (m23 * mat.m31),
            m22 = (m21 * mat.m12) + (m22 * mat.m22) + (m23 * mat.m32),
            m23 = (m21 * mat.m13) + (m22 * mat.m23) + (m23 * mat.m33),
            m31 = (m31 * mat.m11) + (m32 * mat.m21) + (m33 * mat.m31),
            m32 = (m31 * mat.m12) + (m32 * mat.m22) + (m33 * mat.m32),
            m33 = (m31 * mat.m13) + (m32 * mat.m23) + (m33 * mat.m33)
        };

        public static Matrix33 operator *(Matrix33 lhs, Matrix33 rhs) => lhs.Mult(rhs);

        /// <summary>
        /// Multiply the 3x3 matrix by a Vector 3 to get the rotation
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        public Vector3 Mult3x1(Vector3 vector) => new Vector3
        {
            x = (vector.x * m11) + (vector.y * m21) + (vector.z * m31),
            y = (vector.x * m12) + (vector.y * m22) + (vector.z * m32),
            z = (vector.x * m13) + (vector.y * m23) + (vector.z * m33)
        };

        public static Vector3 operator *(Matrix33 rhs, Vector3 lhs) => rhs.Mult3x1(lhs);

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
            if (Math.Abs(mat.m12) + Math.Abs(mat.m13)
                + Math.Abs(mat.m21) + Math.Abs(mat.m23)
                + Math.Abs(mat.m31) + Math.Abs(mat.m32) > 0.01)
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
                x = Math.Pow(mat.m11, 0.5),
                y = Math.Pow(mat.m22, 0.5),
                z = Math.Pow(mat.m33, 0.5)
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

        /// <summary>
        /// Returns a Math.Net matrix from a Cryengine Matrix33 object)
        /// </summary>
        /// <returns>New Math.Net matrix.</returns>
        public Matrix<double> ToMathMatrix()
        {
            var r = Matrix<double>.Build.Dense(3, 3);
            r[0, 0] = m11;
            r[0, 1] = m12;
            r[0, 2] = m13;
            r[1, 0] = m21;
            r[1, 1] = m22;
            r[1, 2] = m23;
            r[2, 0] = m31;
            r[2, 1] = m32;
            r[2, 2] = m33;
            return r;
        }

        public double Determinant() => ToMathMatrix().Determinant();
        public Matrix33 Inverse() => GetMatrix33(ToMathMatrix().Inverse());
        public Matrix33 Conjugate() => GetMatrix33(ToMathMatrix().Conjugate());
        public Matrix33 ConjugateTranspose() => GetMatrix33(ToMathMatrix().ConjugateTranspose());
        public Matrix33 ConjugateTransposeThisAndMultiply(Matrix33 inputMatrix) => GetMatrix33(ToMathMatrix().ConjugateTransposeThisAndMultiply(inputMatrix.ToMathMatrix()));
        public Vector3 Diagonal() => new Vector3().GetVector3(ToMathMatrix().Diagonal());

        public Matrix33 GetMatrix33(Matrix<double> matrix) => new Matrix33
        {
            m11 = matrix[0, 0],
            m12 = matrix[0, 1],
            m13 = matrix[0, 2],
            m21 = matrix[1, 0],
            m22 = matrix[1, 1],
            m23 = matrix[1, 2],
            m31 = matrix[2, 0],
            m32 = matrix[2, 1],
            m33 = matrix[2, 2]
        };

        public void WriteMatrix33(string label = null)
        {
            Log($"====== {label} ===========");
            Log($"{m11:F7}  {m12:F7}  {m13:F7}");
            Log($"{m21:F7}  {m22:F7}  {m23:F7}");
            Log($"{m31:F7}  {m32:F7}  {m33:F7}");
        }
    }

    /// <summary>
    /// A 4x4 Transformation matrix.  These are row major matrices (m13 is first row, 3rd column). [first value is row, second is column.]
    /// </summary>
    public struct Matrix44
    {
        public double m11;
        public double m12;
        public double m13;
        public double m14;
        public double m21;
        public double m22;
        public double m23;
        public double m24;
        public double m31;
        public double m32;
        public double m33;
        public double m34;
        public double m41;
        public double m42;
        public double m43;
        public double m44;

        /// <summary>
        /// Pass the matrix a Vector4 (4x1) vector to get the transform of the vector
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        public Vector4 Mult4x1(Vector4 vector) => new Vector4
        {
            x = (m11 * vector.x) + (m21 * vector.y) + (m31 * vector.z) + m41 / 100,
            y = (m12 * vector.x) + (m22 * vector.y) + (m32 * vector.z) + m42 / 100,
            z = (m13 * vector.x) + (m23 * vector.y) + (m33 * vector.z) + m43 / 100,
            w = (m14 * vector.x) + (m24 * vector.y) + (m34 * vector.z) + m44 / 100
        };

        public static Vector4 operator *(Matrix44 lhs, Vector4 vector) => new Vector4
        {
            x = (lhs.m11 * vector.x) + (lhs.m21 * vector.y) + (lhs.m31 * vector.z) + lhs.m41 / 100,
            y = (lhs.m12 * vector.x) + (lhs.m22 * vector.y) + (lhs.m32 * vector.z) + lhs.m42 / 100,
            z = (lhs.m13 * vector.x) + (lhs.m23 * vector.y) + (lhs.m33 * vector.z) + lhs.m43 / 100,
            w = (lhs.m14 * vector.x) + (lhs.m24 * vector.y) + (lhs.m34 * vector.z) + lhs.m44 / 100
        };

        public static Matrix44 operator *(Matrix44 lhs, Matrix44 rhs) => new Matrix44
        {
            // First row
            m11 = (lhs.m11 * rhs.m11) + (lhs.m12 * rhs.m21) + (lhs.m13 * rhs.m31) + (lhs.m14 * rhs.m41),
            m12 = (lhs.m11 * rhs.m12) + (lhs.m12 * rhs.m22) + (lhs.m13 * rhs.m32) + (lhs.m14 * rhs.m42),
            m13 = (lhs.m11 * rhs.m13) + (lhs.m12 * rhs.m23) + (lhs.m13 * rhs.m33) + (lhs.m14 * rhs.m43),
            m14 = (lhs.m11 * rhs.m14) + (lhs.m12 * rhs.m24) + (lhs.m13 * rhs.m34) + (lhs.m14 * rhs.m44),
            // second row
            m21 = (lhs.m21 * rhs.m11) + (lhs.m22 * rhs.m21) + (lhs.m23 * rhs.m31) + (lhs.m24 * rhs.m41),
            m22 = (lhs.m21 * rhs.m12) + (lhs.m22 * rhs.m22) + (lhs.m23 * rhs.m32) + (lhs.m24 * rhs.m42),
            m23 = (lhs.m21 * rhs.m13) + (lhs.m22 * rhs.m23) + (lhs.m23 * rhs.m33) + (lhs.m24 * rhs.m43),
            m24 = (lhs.m21 * rhs.m14) + (lhs.m22 * rhs.m24) + (lhs.m23 * rhs.m34) + (lhs.m24 * rhs.m44),
            // third row
            m31 = (lhs.m31 * rhs.m11) + (lhs.m32 * rhs.m21) + (lhs.m33 * rhs.m31) + (lhs.m34 * rhs.m41),
            m32 = (lhs.m31 * rhs.m12) + (lhs.m32 * rhs.m22) + (lhs.m33 * rhs.m32) + (lhs.m34 * rhs.m42),
            m33 = (lhs.m31 * rhs.m13) + (lhs.m32 * rhs.m23) + (lhs.m33 * rhs.m33) + (lhs.m34 * rhs.m43),
            m34 = (lhs.m31 * rhs.m14) + (lhs.m32 * rhs.m24) + (lhs.m33 * rhs.m34) + (lhs.m34 * rhs.m44),
            // fourth row
            m41 = (lhs.m41 * rhs.m11) + (lhs.m42 * rhs.m21) + (lhs.m43 * rhs.m31) + (lhs.m44 * rhs.m41),
            m42 = (lhs.m41 * rhs.m12) + (lhs.m42 * rhs.m22) + (lhs.m43 * rhs.m32) + (lhs.m44 * rhs.m42),
            m43 = (lhs.m41 * rhs.m13) + (lhs.m42 * rhs.m23) + (lhs.m43 * rhs.m33) + (lhs.m44 * rhs.m43),
            m44 = (lhs.m41 * rhs.m14) + (lhs.m42 * rhs.m24) + (lhs.m43 * rhs.m34) + (lhs.m44 * rhs.m44)
        };

        public Vector3 GetTranslation() => new Vector3
        {
            x = m14,
            y = m24,
            z = m34
        };

        /// <summary>
        /// Gets the Rotation portion of a Transform Matrix44 (upper left).
        /// </summary>
        /// <returns>New Matrix33 with the rotation component.</returns>
        public Matrix33 GetRotation() => new Matrix33()
        {
            m11 = m11,
            m12 = m12,
            m13 = m13,
            m21 = m21,
            m22 = m22,
            m23 = m23,
            m31 = m31,
            m32 = m32,
            m33 = m33,
        };

        public Vector3 GetScale() => new Vector3
        {
            x = m41 / 100,
            y = m42 / 100,
            z = m43 / 100
        };

        public Vector3 GetBoneTranslation() => new Vector3
        {
            x = m14,
            y = m24,
            z = m34
        };

        public double[,] ConvertTo4x4Array()
        {
            var r = new double[4, 4];
            r[0, 0] = m11;
            r[0, 1] = m12;
            r[0, 2] = m13;
            r[0, 3] = m14;
            r[1, 0] = m21;
            r[1, 1] = m22;
            r[1, 2] = m23;
            r[1, 3] = m24;
            r[2, 0] = m31;
            r[2, 1] = m32;
            r[2, 2] = m33;
            r[2, 3] = m34;
            r[3, 0] = m41;
            r[3, 1] = m42;
            r[3, 2] = m43;
            r[3, 3] = m44;
            return r;
        }

        public Matrix44 Inverse() => GetMatrix44(ToMathMatrix().Inverse());

        public Matrix44 GetTransformFromParts(Vector3 localTranslation, Matrix33 localRotation, Vector3 localScale) => new Matrix44
        {
            // For Node Chunks, the translation appears to be along the bottom of the matrix, and scale on right side.
            // Translation part
            m41 = localTranslation.x,
            m42 = localTranslation.y,
            m43 = localTranslation.z,
            // Rotation part
            m11 = localRotation.m11,
            m12 = localRotation.m12,
            m13 = localRotation.m13,
            m21 = localRotation.m21,
            m22 = localRotation.m22,
            m23 = localRotation.m23,
            m31 = localRotation.m31,
            m32 = localRotation.m32,
            m33 = localRotation.m33,
            // Scale part
            m14 = localScale.x,
            m24 = localScale.y,
            m34 = localScale.z,
            // Set final row
            m44 = 1
        };

        public static Matrix44 Identity() => new Matrix44()
        {
            m11 = 1,
            m12 = 0,
            m13 = 0,
            m14 = 0,
            m21 = 0,
            m22 = 1,
            m23 = 0,
            m24 = 0,
            m31 = 0,
            m32 = 0,
            m33 = 1,
            m34 = 0,
            m41 = 0,
            m42 = 0,
            m43 = 0,
            m44 = 1
        };

        public Matrix<double> ToMathMatrix()
        {
            var r = Matrix<double>.Build.Dense(4, 4);
            r[0, 0] = m11;
            r[0, 1] = m12;
            r[0, 2] = m13;
            r[0, 3] = m14;
            r[1, 0] = m21;
            r[1, 1] = m22;
            r[1, 2] = m23;
            r[1, 3] = m24;
            r[2, 0] = m31;
            r[2, 1] = m32;
            r[2, 2] = m33;
            r[2, 3] = m34;
            r[3, 0] = m41;
            r[3, 1] = m42;
            r[3, 2] = m43;
            r[3, 3] = m44;
            return r;
        }

        public Matrix44 GetMatrix44(Matrix<double> matrix) => new Matrix44
        {
            m11 = matrix[0, 0],
            m12 = matrix[0, 1],
            m13 = matrix[0, 2],
            m14 = matrix[0, 3],
            m21 = matrix[1, 0],
            m22 = matrix[1, 1],
            m23 = matrix[1, 2],
            m24 = matrix[1, 3],
            m31 = matrix[2, 0],
            m32 = matrix[2, 1],
            m33 = matrix[2, 2],
            m34 = matrix[2, 3],
            m41 = matrix[3, 0],
            m42 = matrix[3, 1],
            m43 = matrix[3, 2],
            m44 = matrix[3, 3],
        };

        public void WriteMatrix44()
        {
            Log($"=============================================");
            Log($"{m11:F7}  {m12:F7}  {m13:F7}  {m14:F7}");
            Log($"{m21:F7}  {m22:F7}  {m23:F7}  {m24:F7}");
            Log($"{m31:F7}  {m32:F7}  {m33:F7}  {m34:F7}");
            Log($"{m41:F7}  {m42:F7}  {m43:F7}  {m44:F7}");
            Log();
        }
    }

    /// <summary>
    /// A quaternion (x,y,z,w)
    /// </summary>
    public struct Quat
    {
        public double x;
        public double y;
        public double z;
        public double w;
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
        public double Radius;
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
        public double[] Unknown2; // If ARG==9?  array length = 2
    }

    public struct UV
    {
        public double U;
        public double V;
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
        public double r; // double Red
        public double g; // double green
        public double b; // double blue
    }

    public struct AABB
    {
        Vector3 min;
        Vector3 max;
    }

    public struct Tangent
    {
        // Tangents.  Divide each component by 32767 to get the actual value
        public double x;
        public double y;
        public double z;
        public double w;  // Handness?  Either 32767 (+1.0) or -32767 (-1.0)
    }

    public struct SkinVertex
    {
        public int Volumetric;
        public int[] Index;     // Array of 4 ints
        public float[] w;       // Array of 4 floats
        public Matrix33 M;
    }

    /// <summary>
    /// WORLDTOBONE is also the Bind Pose Matrix (BPM)
    /// </summary>
    public struct WORLDTOBONE
    {
        public double[,] worldToBone;   //  4x3 structure

        public void GetWorldToBone(BinaryReader b)
        {
            worldToBone = new double[3, 4];
            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 4; j++)
                    worldToBone[i, j] = b.ReadSingle();
            //Log($"worldToBone: {worldToBone[i, j]:F7}");
            return;
        }

        public Matrix44 GetMatrix44() => new Matrix44
        {
            m11 = worldToBone[0, 0],
            m12 = worldToBone[0, 1],
            m13 = worldToBone[0, 2],
            m14 = worldToBone[0, 3],
            m21 = worldToBone[1, 0],
            m22 = worldToBone[1, 1],
            m23 = worldToBone[1, 2],
            m24 = worldToBone[1, 3],
            m31 = worldToBone[2, 0],
            m32 = worldToBone[2, 1],
            m33 = worldToBone[2, 2],
            m34 = worldToBone[2, 3],
            m41 = 0,
            m42 = 0,
            m43 = 0,
            m44 = 1
        };

        public void WriteWorldToBone()
        {
            //Log("     *** World to Bone ***");
            Log($"     {worldToBone[0, 0]:F7}  {worldToBone[0, 1]:F7}  {worldToBone[0, 2]:F7}  {worldToBone[0, 3]:F7}");
            Log($"     {worldToBone[1, 0]:F7}  {worldToBone[1, 1]:F7}  {worldToBone[1, 2]:F7}  {worldToBone[1, 3]:F7}");
            Log($"     {worldToBone[2, 0]:F7}  {worldToBone[2, 1]:F7}  {worldToBone[2, 2]:F7}  {worldToBone[2, 3]:F7}");
        }

        internal Matrix33 GetWorldToBoneRotationMatrix() => new Matrix33
        {
            m11 = worldToBone[0, 0],
            m12 = worldToBone[0, 1],
            m13 = worldToBone[0, 2],
            m21 = worldToBone[1, 0],
            m22 = worldToBone[1, 1],
            m23 = worldToBone[1, 2],
            m31 = worldToBone[2, 0],
            m32 = worldToBone[2, 1],
            m33 = worldToBone[2, 2]
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
        public double[,] boneToWorld;   //  4x3 structure

        public void ReadBoneToWorld(BinaryReader b)
        {
            boneToWorld = new double[3, 4];
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
        public Matrix33 GetBoneToWorldRotationMatrix() => new Matrix33
        {
            m11 = boneToWorld[0, 0],
            m12 = boneToWorld[0, 1],
            m13 = boneToWorld[0, 2],
            m21 = boneToWorld[1, 0],
            m22 = boneToWorld[1, 1],
            m23 = boneToWorld[1, 2],
            m31 = boneToWorld[2, 0],
            m32 = boneToWorld[2, 1],
            m33 = boneToWorld[2, 2]
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
        public Matrix33 framemtx;

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
            framemtx.ReadMatrix33(b);
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
        public double mass;                         // 0xD8 ?
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
        public Matrix44 LocalTransform = new Matrix44();            // Because Cryengine tends to store transform relative to world, we have to add all the transforms from the node to the root.  Calculated, row major.
        public Vector3 LocalTranslation = new Vector3();            // To hold the local rotation vector
        public Matrix33 LocalRotation = new Matrix33();             // to hold the local rotation matrix

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

        public Matrix44 ToMatrix44(double[,] boneToWorld) => new Matrix44
        {
            m11 = boneToWorld[0, 0],
            m12 = boneToWorld[0, 1],
            m13 = boneToWorld[0, 2],
            m14 = boneToWorld[0, 3],
            m21 = boneToWorld[1, 0],
            m22 = boneToWorld[1, 1],
            m23 = boneToWorld[1, 2],
            m24 = boneToWorld[1, 3],
            m31 = boneToWorld[2, 0],
            m32 = boneToWorld[2, 1],
            m33 = boneToWorld[2, 2],
            m34 = boneToWorld[2, 3],
            m41 = 0,
            m42 = 0,
            m43 = 0,
            m44 = 1
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
        Matrix33 Rot;
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
        Matrix33 Frame_Matrix;        //" type="Matrix33" />
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
        public Matrix33 Unknown61; // Rotation matrix?
        public int[] Unknown70; //Array length 3
        public float Unknown80;
    }

    public struct PhysicsDataType2
    {
        public Matrix33 Unknown1;
        public int Unknown;
        public float[] Unknown3; // array length 6
        public int Unknown4;
    }

    public struct PhysicsStruct1
    {
        public Matrix33 Unknown1;
        public int Unknown2;
        public float[] Unknown3; // array length 6
    }

    public struct PhysicsStruct2
    {
        public Matrix33 Unknown1;
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