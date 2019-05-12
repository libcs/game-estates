using Gamer.Core;
using System;
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
            return null;
        }
    }
}