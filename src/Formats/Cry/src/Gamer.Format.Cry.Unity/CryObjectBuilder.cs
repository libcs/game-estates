using Gamer.Core;
using Gamer.Format.Cry.Core;
using System;
using System.IO;
using UnityEngine;
using static Gamer.Core.Debug;

namespace Gamer.Format.Cry
{
    public class CryObjectBuilder
    {
        const bool KinematicRigidbodies = true;

        readonly CryFile _obj;
        readonly MaterialManager _materialManager;
        readonly int _markerLayer;

        public CryObjectBuilder(CryFile obj, MaterialManager materialManager, int markerLayer)
        {
            _obj = obj;
            _materialManager = materialManager;
            _markerLayer = markerLayer;
        }

        public GameObject BuildObject()
        {
            Assert(_obj.Models.Count > 0);

            Log($"COUNT: {_obj.Models.Count}");
            if (_obj.Models.Count == 1)
            {
                var gameObject = InstantiateRootNiObject(_obj.RootNode);
                if (gameObject == null)
                {
                    Log($"{_obj.InputFile} resulted in a null GameObject when instantiated.");
                }
                gameObject = new GameObject(Path.GetFileName(_obj.InputFile));
                return gameObject;
            }
            else
                throw new NotImplementedException();
        }

        GameObject InstantiateRootNiObject(ChunkNode obj)
        {
            var gameObject = InstantiateNiObject(obj);
            //ProcessExtraData(obj, out var shouldAddMissingColliders, out var isMarker);
            //if (_obj.Name != null && IsMarkerFileName(_obj.Name))
            //{
            //    shouldAddMissingColliders = false;
            //    isMarker = true;
            //}
            //// Add colliders to the object if it doesn't already contain one.
            //if (shouldAddMissingColliders && gameObject.GetComponentInChildren<Collider>() == null)
            //    GameObjectUtils.AddMissingMeshCollidersRecursively(gameObject);
            //if (isMarker)
            //    GameObjectUtils.SetLayerRecursively(gameObject, _markerLayer);
            return gameObject;
        }

        GameObject InstantiateNiObject(ChunkNode obj)
        {
            //if (obj.GetType() == typeof(NiNode)) return InstantiateNiNode((NiNode)obj);
            //else if (obj.GetType() == typeof(NiBSAnimationNode)) return InstantiateNiNode((NiNode)obj);
            //else if (obj.GetType() == typeof(NiTriShape)) return InstantiateNiTriShape((NiTriShape)obj, true, false);
            //else if (obj.GetType() == typeof(RootCollisionNode)) return InstantiateRootCollisionNode((RootCollisionNode)obj);
            //else if (obj.GetType() == typeof(NiTextureEffect)) return null;
            //else if (obj.GetType() == typeof(NiBSAnimationNode)) return null;
            //else if (obj.GetType() == typeof(NiBSParticleNode)) return null;
            //else if (obj.GetType() == typeof(NiRotatingParticles)) return null;
            //else if (obj.GetType() == typeof(NiAutoNormalParticles)) return null;
            //else if (obj.GetType() == typeof(NiBillboardNode)) return null;
            //else 
            throw new NotImplementedException($"Tried to instantiate an unsupported ChunkNode ({obj.GetType().Name}).");
        }
    }
}