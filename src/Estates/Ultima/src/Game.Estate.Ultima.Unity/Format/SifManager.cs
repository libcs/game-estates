using Game.Core;
using Game.Estate.Ultima.FilePack;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static Game.Core.Debug;

namespace Game.Estate.Ultima.Format
{
    public class SifManager
    {
        readonly ResFile _asset;
        readonly MaterialManager _materialManager;
        GameObject _prefabContainerObj;
        readonly Dictionary<string, Task<object>> _preloadTasks = new Dictionary<string, Task<object>>();
        readonly Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();

        public SifManager(ResFile asset, MaterialManager materialManager)
        {
            _asset = asset;
            _materialManager = materialManager;
        }

        public GameObject CreateObject(string filePath)
        {
            EnsurePrefabContainerObjectExists();
            // Load & cache the STA prefab.
            if (!_prefabs.TryGetValue(filePath, out var prefab))
                prefab = _prefabs[filePath] = LoadPrefabDontAddToPrefabCache(filePath);
            // Instantiate the prefab.
            return Object.Instantiate(prefab);
        }

        public void PreloadObjectTask(string filePath)
        {
            // If the STA prefab has already been created we don't have to load the file again.
            if (_prefabs.ContainsKey(filePath))
                return;
            // Start loading the STA asynchronously if we haven't already started.
            if (!_preloadTasks.TryGetValue(filePath, out var preloadTask))
                preloadTask = _preloadTasks[filePath] = _asset.LoadObjectInfoAsync(filePath);
        }

        void EnsurePrefabContainerObjectExists()
        {
            if (_prefabContainerObj == null)
            {
                _prefabContainerObj = new GameObject("STA Prefabs");
                _prefabContainerObj.SetActive(false);
            }
        }

        GameObject LoadPrefabDontAddToPrefabCache(string filePath)
        {
            Assert(!_prefabs.ContainsKey(filePath));
            PreloadObjectTask(filePath);
            var file = (SiFile)_preloadTasks[filePath].Result();
            _preloadTasks.Remove(filePath);
            // Start pre-loading all the STA's textures.
            foreach (var saObject in file.Blocks)
                if (saObject is SiSourceTexture stSourceTexture)
                    if (!string.IsNullOrEmpty(stSourceTexture.FilePath))
                        _materialManager.TextureManager.PreloadTextureTask(stSourceTexture.FilePath);
            var objBuilder = new SifObjectBuilder(file, _materialManager, 0);
            var prefab = objBuilder.BuildObject();
            prefab.transform.parent = _prefabContainerObj.transform;
            // Add LOD support to the prefab.
            var LODComponent = prefab.AddComponent<LODGroup>();
            var LODs = new LOD[1]
            {
                //new LOD(0.015F, prefab.GetComponentsInChildren<Renderer>())
                new LOD(0F, prefab.GetComponentsInChildren<Renderer>())
            };
            LODComponent.SetLODs(LODs);
            return prefab;
        }
    }
}