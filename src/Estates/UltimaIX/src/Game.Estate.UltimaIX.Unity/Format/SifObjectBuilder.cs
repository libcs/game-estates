using Game.Core;
using System;
using UnityEngine;
using static Game.Core.CoreDebug;

namespace Game.Estate.UltimaIX.Format
{
    public class SifObjectBuilder
    {
        const bool KinematicRigidbodies = true;

        readonly SiFile _obj;
        readonly MaterialManager _materialManager;
        readonly int _markerLayer;

        public SifObjectBuilder(SiFile obj, MaterialManager materialManager, int markerLayer)
        {
            _obj = obj;
            _materialManager = materialManager;
            _markerLayer = markerLayer;
        }

        public GameObject BuildObject()
        {
            Assert(_obj.Name != null && _obj.Meshes.Length > 0);

            // SIF files can have any number of meshes.
            // If there is only one mesh, instantiate that directly.
            // If there are multiple meshes, create a container GameObject and parent it to the roots.
            if (_obj.Meshes.Length == 1)
            {
                var siObject = _obj.Meshes[0];
                var gameObject = InstantiateRootSiObject(siObject);
                // If the file doesn't contain any NiObjects we are looking for, return an empty GameObject.
                if (gameObject == null)
                {
                    Log($"{_obj.Name} resulted in a null GameObject when instantiated.");
                    gameObject = new GameObject(_obj.Name);
                }
                //// If gameObject != null and the root NiObject is an NiNode, discard any transformations (Morrowind apparently does).
                //else if (false)
                //{
                //    gameObject.transform.position = Vector3.zero;
                //    gameObject.transform.rotation = Quaternion.identity;
                //    gameObject.transform.localScale = Vector3.one;
                //}
                return gameObject;
            }
            else
            {
                Log($"{_obj.Name} has multiple roots.");
                var gameObject = new GameObject(_obj.Name);
                foreach (var siObject in _obj.Meshes)
                {
                    var child = InstantiateRootSiObject(siObject);
                    if (child != null)
                        child.transform.SetParent(gameObject.transform, false);
                }
                return gameObject;
            }
        }

        GameObject InstantiateRootSiObject(SiMesh data)
        {
            var gameObject = InstantiateSiObject(data);
            return gameObject;
        }

        GameObject InstantiateSiObject(SiMesh data)
        {
            var r = InstantiateNiTriShape(data, true, false);
            return null;
        }

        GameObject InstantiateNiTriShape(SiMesh data, bool visual, bool collidable)
        {
            Assert(visual || collidable);
            var mesh = MeshLodDataToMesh(data.Lod);
            var obj = new GameObject();
            if (visual)
            {
                obj.AddComponent<MeshFilter>().mesh = mesh;
                var materialProps = MeshLodDataToMaterialProperties(data.Lod);
                var meshRenderer = obj.AddComponent<MeshRenderer>();
                meshRenderer.material = _materialManager.BuildMaterialFromProperties(materialProps);
                obj.isStatic = true;
            }
            if (collidable)
            {
                obj.AddComponent<MeshCollider>().sharedMesh = mesh;
                if (KinematicRigidbodies)
                    obj.AddComponent<Rigidbody>().isKinematic = true;
            }
            return obj;
        }

        //void ApplyNiAVObject(NiAVObject niAVObject, GameObject obj)
        //{
        //    obj.transform.position = NifUtils.NifPointToUnityPoint(niAVObject.Translation);
        //    obj.transform.rotation = NifUtils.NifRotationMatrixToUnityQuaternion(niAVObject.Rotation);
        //    obj.transform.localScale = niAVObject.Scale * Vector3.one;
        //}

        Mesh MeshLodDataToMesh(SiMeshLod data)
        {
            // vertex positions
            var vertices = new Vector3[data.Vertices.Length];
            for (var i = 0; i < vertices.Length; i++)
                vertices[i] = data.Vertices[i].ToUnityVector(ConvertUtils.MeterInUnits);
            var hasNormals = false;
            var faces = data.Faces;
            // vertex normals
            Vector3[] normals = null;
            if (hasNormals) // has normals
            {
                //normals = new Vector3[faces.Length];
                //for (var i = 0; i < normals.Length; i++)
                //    normals[i] = data.Faces[i].NormalVector.ToUnityVector();
            }
            // vertex UV coordinates
            Vector2[] UVs = null;
            if (true) // has uv
            {
                //UVs = new Vector2[faces.Length];
                //for (var i = 0; i < UVs.Length; i++)
                //{
                //    var NiTexCoord = data.UVSets[0, i];
                //    UVs[i] = new Vector2(NiTexCoord.u, NiTexCoord.v);
                //}
            }
            // triangle vertex indices
            var triangles = new int[faces.Length];
            for (var i = 0; i < faces.Length; i++)
            {
                var baseI = 3 * i;
                // Reverse triangle winding order.
                triangles[baseI] = (int)faces[i].Points[0].Point;
                triangles[baseI + 1] = (int)faces[i].Points[1].Point;
                triangles[baseI + 2] = (int)faces[i].Points[2].Point;
            }
            // Create the mesh.
            var mesh = new Mesh
            {
                vertices = vertices,
                normals = normals,
                uv = UVs,
                triangles = triangles
            };
            if (!hasNormals)
                mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }

        MaterialProps MeshLodDataToMaterialProperties(SiMeshLod data)
        {
            // Create the material properties.
            var mp = new MaterialProps();
            var tp = new MaterialTextures();
            foreach (var material in data.Materials)
                if (tp.MainFilePath == null)
                    tp.MainFilePath = material.Texture;
            mp.Textures = tp;
            return mp;
        }
    }
}