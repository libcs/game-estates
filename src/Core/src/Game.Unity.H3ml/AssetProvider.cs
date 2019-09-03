using System;
using System.Collections.Generic;

namespace Game.Unity
{
    public interface IAssetProvider : IDisposable
    {
        string MakeKey(string url, Dictionary<string, string> attributes);
        object CreateObject(string url, Dictionary<string, string> attributes);
    }

    public class AssetProvider : IAssetProvider
    {
        public object CreateObject(string url, Dictionary<string, string> attributes)
        {
            return null;
        }

        public void Dispose() { }

        public string MakeKey(string url, Dictionary<string, string> attributes) => url;
    }
}