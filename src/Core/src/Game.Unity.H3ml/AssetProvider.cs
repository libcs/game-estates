using H3ml.Asset;
using System.Collections.Generic;

namespace Game.Unity
{
    public class AssetProvider : IAssetProvider
    {
        public object CreateObject(string url, Dictionary<string, string> attributes)
        {
            return null;
        }

        public void Dispose()
        {
        }

        public string MakeKey(string url, Dictionary<string, string> attributes) => url;
    }
}