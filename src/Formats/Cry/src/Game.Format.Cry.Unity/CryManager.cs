using Game.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static Game.Core.CoreDebug;

namespace Game.Format.Cry
{
    /// <summary>
    /// Manages loading and instantiation of NIF models.
    /// </summary>
    public class CryManager
    {
        readonly IAssetUnityPack _asset;
        readonly MaterialManager _materialManager;
        GameObject _prefabContainerObj;
        readonly Dictionary<string, Task<object>> _preloadTasks = new Dictionary<string, Task<object>>();
        readonly Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
        readonly int _markerLayer;

        public CryManager(IAssetUnityPack asset, MaterialManager materialManager, int markerLayer)
        {
            _asset = asset;
            _materialManager = materialManager;
            _markerLayer = markerLayer;
        }

        public GameObject InstantiateObj(string filePath)
        {
            EnsurePrefabContainerObjectExists();
            // Load & cache the CRY prefab.
            if (!_prefabs.TryGetValue(filePath, out var prefab))
                prefab = _prefabs[filePath] = LoadPrefabDontAddToPrefabCache(filePath);
            // Instantiate the prefab.
            return Object.Instantiate(prefab);
        }

        public void PreloadObjectTask(string filePath)
        {
            // If the CRY prefab has already been created we don't have to load the file again.
            if (_prefabs.ContainsKey(filePath))
                return;
            // Start loading the CRY asynchronously if we haven't already started.
            if (!_preloadTasks.TryGetValue(filePath, out var preloadTask))
                preloadTask = _preloadTasks[filePath] = _asset.LoadObjectInfoAsync(filePath);
        }

        void EnsurePrefabContainerObjectExists()
        {
            if (_prefabContainerObj == null)
            {
                _prefabContainerObj = new GameObject("CRY Prefabs");
                _prefabContainerObj.SetActive(false);
            }
        }

        GameObject LoadPrefabDontAddToPrefabCache(string filePath)
        {
            Assert(!_prefabs.ContainsKey(filePath));
            PreloadObjectTask(filePath);
            var file = (CryFile)_preloadTasks[filePath].Result();
            _preloadTasks.Remove(filePath);
            // Start pre-loading all the NIF's textures.
            foreach (var texturePath in file.GetTexturePaths())
                _materialManager.TextureManager.PreloadTextureTask(texturePath);
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