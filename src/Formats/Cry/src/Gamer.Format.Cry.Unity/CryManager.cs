using Gamer.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static Gamer.Core.Debug;

namespace Gamer.Format.Cry
{
    /// <summary>
    /// Manages loading and instantiation of NIF models.
    /// </summary>
    public class CryManager
    {
        readonly IAssetPack _asset;
        readonly MaterialManager _materialManager;
        GameObject _prefabContainerObj;
        readonly Dictionary<string, Task<object>> _objFilePreloadTasks = new Dictionary<string, Task<object>>();
        readonly Dictionary<string, GameObject> _objPrefabs = new Dictionary<string, GameObject>();
        readonly int _markerLayer;

        public CryManager(IAssetPack asset, MaterialManager materialManager, int markerLayer)
        {
            _asset = asset;
            _materialManager = materialManager;
            _markerLayer = markerLayer;
        }

        /// <summary>
        /// Instantiates a CRY file.
        /// </summary>
        public GameObject InstantiateObj(string filePath)
        {
            EnsurePrefabContainerObjectExists();
            // Get the prefab.
            if (!_objPrefabs.TryGetValue(filePath, out var prefab))
            {
                // Load & cache the CRY prefab.
                prefab = LoadObjPrefabDontAddToPrefabCache(filePath);
                _objPrefabs[filePath] = prefab;
            }
            // Instantiate the prefab.
            return Object.Instantiate(prefab);
        }

        public void PreloadObjFileAsync(string filePath)
        {
            // If the CRY prefab has already been created we don't have to load the file again.
            if (_objPrefabs.ContainsKey(filePath))
                return;
            // Start loading the CRY asynchronously if we haven't already started.
            if (!_objFilePreloadTasks.TryGetValue(filePath, out var cryFileLoadingTask))
            {
                cryFileLoadingTask = _asset.LoadObjectInfoAsync(filePath);
                _objFilePreloadTasks[filePath] = cryFileLoadingTask;
            }
        }

        void EnsurePrefabContainerObjectExists()
        {
            if (_prefabContainerObj == null)
            {
                _prefabContainerObj = new GameObject("CRY Prefabs");
                _prefabContainerObj.SetActive(false);
            }
        }

        GameObject LoadObjPrefabDontAddToPrefabCache(string filePath)
        {
            Assert(!_objPrefabs.ContainsKey(filePath));
            PreloadObjFileAsync(filePath);
            var file = (CryFile)_objFilePreloadTasks[filePath].Result;
            _objFilePreloadTasks.Remove(filePath);
            // Start pre-loading all the NIF's textures.
            foreach (var texturePath in file.GetTexturePaths())
                _materialManager.TextureManager.PreloadTextureFileAsync(texturePath);
            var objBuilder = new CryObjectBuilder(file, _materialManager, _markerLayer);
            var prefab = objBuilder.BuildObject();
            prefab.transform.parent = _prefabContainerObj.transform;
            // Add LOD support to the prefab.
            var LODComponent = prefab.AddComponent<LODGroup>();
            var LODs = new LOD[1]
            {
                new LOD(0.015f, prefab.GetComponentsInChildren<Renderer>())
            };
            LODComponent.SetLODs(LODs);
            return prefab;
        }
    }
}