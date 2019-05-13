using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Gamer.Core.Debug;

namespace Gamer.Format.Cry.Core
{
    public abstract class ChunkNode : Chunk // cccc000b:   Node
    {
        /// <summary>
        /// Chunk Name (String[64])
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// Mesh or Helper Object ID
        /// </summary>
        public int ObjectNodeID { get; internal set; }
        /// <summary>
        /// Node parent.  if 0xFFFFFFFF, it's the top node.  Maybe...
        /// </summary>
        public int ParentNodeID { get; internal set; }  // Parent nodeID
        public int __NumChildren;
        /// <summary>
        /// Material ID for this chunk
        /// </summary>
        public int MatID { get; internal set; }
        public bool IsGroupHead { get; internal set; }
        public bool IsGroupMember { get; internal set; }
        /// <summary>
        /// Transformation Matrix
        /// </summary>
        public Matrix4x4 Transform { get; internal set; }
        /// <summary>
        /// Position vector of Transform
        /// </summary>
        public Vector3 Pos { get; internal set; }
        /// <summary>
        /// Rotation component of Transform
        /// </summary>
        public Quat Rot { get; internal set; }
        /// <summary>
        /// Scalar component of Transform
        /// </summary>
        public Vector3 Scale { get; internal set; }
        /// <summary>
        /// Position Controller ID - Obsolete
        /// </summary>
        public int PosCtrlID { get; internal set; }
        /// <summary>
        /// Rotation Controller ID - Obsolete
        /// </summary>
        public int RotCtrlID { get; internal set; }
        /// <summary>
        /// Scalar Controller ID - Obsolete
        /// </summary>
        public int SclCtrlID { get; internal set; }
        /// <summary>
        /// Appears to be a Blob of properties, separated by new lines
        /// </summary>
        public string Properties { get; internal set; }

        // Calculated Properties

        public Matrix4x4 LocalTransform = new Matrix4x4();            // Because Cryengine tends to store transform relative to world, we have to add all the transforms from the node to the root.  Calculated, row major.
        public Vector3 LocalTranslation = new Vector3();            // To hold the local rotation vector
        public Matrix3x3 LocalRotation = new Matrix3x3();             // to hold the local rotation matrix
        public Vector3 LocalScale = new Vector3();                  // to hold the local scale matrix

        ChunkNode _parentNode;
        public ChunkNode ParentNode
        {
            get
            {
                // Turns out chunk IDs are ints, not uints.  ~0 is shorthand for -1, or 0xFFFFFFFF in the uint world.
                if (ParentNodeID == ~0)
                    return null;
                if (_parentNode == null)
                {
                    if (_model.ChunkMap.ContainsKey(ParentNodeID))
                        _parentNode = _model.ChunkMap[ParentNodeID] as ChunkNode;
                    else _parentNode = _model.RootNode;
                }
                return _parentNode;
            }
            set
            {
                ParentNodeID = value == null ? ~0 : value.ID;
                _parentNode = value;
            }
        }

        public List<ChunkNode> ChildNodes { get; set; }

        Chunk _objectChunk;
        public Chunk ObjectChunk
        {
            get
            {
                if (_objectChunk == null && _model.ChunkMap.ContainsKey(ObjectNodeID))
                    _objectChunk = _model.ChunkMap[ObjectNodeID];
                return _objectChunk;
            }
            set => _objectChunk = value;
        }

        public Vector3 TransformSoFar => ParentNode != null
            ? ParentNode.TransformSoFar.Add(Transform.GetTranslation())
            : Transform.GetTranslation();

        public Matrix3x3 RotSoFar => ParentNode != null
                    ? Transform.GetRotation().Mult(ParentNode.RotSoFar)
                    : _model.RootNode.Transform.GetRotation();

        public List<ChunkNode> AllChildNodes => __NumChildren == 0 ? (List<ChunkNode>)null : _model.NodeMap.Values.Where(a => a.ParentNodeID == ID).ToList();

        /// <summary>
        /// Gets the transform of the vertex.  This will be both the rotation and translation of this vertex, plus all the parents.
        /// The transform matrix is a 4x4 matrix.  Vector3 is a 3x1.  We need to convert vector3 to vector4, multiply the matrix, then convert back to vector3.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public Vector3 GetTransform(Vector3 transform)
        {
            var vec3 = transform;
            // Apply the local transforms (rotation and translation) to the vector
            // Do rotations.  Rotations must come first, then translate.
            vec3 = RotSoFar.Mult3x1(vec3);
            // Do translations.  I think this is right.  Objects in right place, not rotated right.
            vec3 = vec3.Add(TransformSoFar);
            return vec3;
        }

        public override void Read(BinaryReader r)
        {
            base.Read(r);
            // Read the Name string
            Name = r.ReadFString(64);
            ObjectNodeID = r.ReadInt32(); // Object reference ID
            ParentNodeID = r.ReadInt32();
            __NumChildren = r.ReadInt32();
            MatID = r.ReadInt32();  // Material ID?
            SkipBytes(r, 4);
            // Read the 4x4 transform matrix.  Should do a couple of for loops, but data structures...
            Transform = new Matrix4x4
            {
                m00 = r.ReadSingle(),
                m01 = r.ReadSingle(),
                m02 = r.ReadSingle(),
                m03 = r.ReadSingle(),
                m10 = r.ReadSingle(),
                m11 = r.ReadSingle(),
                m12 = r.ReadSingle(),
                m13 = r.ReadSingle(),
                m20 = r.ReadSingle(),
                m21 = r.ReadSingle(),
                m22 = r.ReadSingle(),
                m23 = r.ReadSingle(),
                m30 = r.ReadSingle(),
                m31 = r.ReadSingle(),
                m32 = r.ReadSingle(),
                m33 = r.ReadSingle(),
            };
            // Read the position Pos Vector3
            Pos = new Vector3
            {
                x = r.ReadSingle() / 100,
                y = r.ReadSingle() / 100,
                z = r.ReadSingle() / 100,
            };
            // Read the rotation Rot Quad
            Rot = new Quat
            {
                w = r.ReadSingle(),
                x = r.ReadSingle(),
                y = r.ReadSingle(),
                z = r.ReadSingle(),
            };
            // Read the Scale Vector 3
            Scale = new Vector3
            {
                x = r.ReadSingle(),
                y = r.ReadSingle(),
                z = r.ReadSingle(),
            };
            // read the controller pos/rot/scale
            PosCtrlID = r.ReadInt32();
            RotCtrlID = r.ReadInt32();
            SclCtrlID = r.ReadInt32();
            Properties = r.ReadPString();
        }

        public override void WriteChunk()
        {
            Log($"*** START Node Chunk ***");
            Log($"    ChunkType:           {ChunkType}");
            Log($"    Node ID:             {ID:X}");
            Log($"    Node Name:           {Name}");
            Log($"    Object ID:           {ObjectNodeID:X}");
            Log($"    Parent ID:           {ParentNodeID:X}");
            Log($"    Number of Children:  {__NumChildren}");
            Log($"    Material ID:         {MatID:X}"); // 0x1 is mtllib w children, 0x10 is mtl no children, 0x18 is child
            Log($"    Position:            {Pos.x:F7}   {Pos.y:F7}   {Pos.z:F7}");
            Log($"    Scale:               {Scale.x:F7}   {Scale.y:F7}   {Scale.z:F7}");
            LogFormat("    Transformation:      {0:F7}  {1:F7}  {2:F7}  {3:F7}", Transform.m00, Transform.m01, Transform.m02, Transform.m03);
            LogFormat("                         {0:F7}  {1:F7}  {2:F7}  {3:F7}", Transform.m10, Transform.m11, Transform.m12, Transform.m13);
            LogFormat("                         {0:F7}  {1:F7}  {2:F7}  {3:F7}", Transform.m20, Transform.m21, Transform.m22, Transform.m23);
            LogFormat("                         {0:F7}  {1:F7}  {2:F7}  {3:F7}", Transform.m30 / 100, Transform.m31 / 100, Transform.m32 / 100, Transform.m33);
            Log($"    Transform_sum:       {TransformSoFar.x:F7}  {TransformSoFar.y:F7}  {TransformSoFar.z:F7}");
            Log($"    Rotation_sum:");
            RotSoFar.WriteMatrix3x3();
            Log($"*** END Node Chunk ***");
        }
    }
}
