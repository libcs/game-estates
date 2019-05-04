using Gamer.Base.Core;
using Gamer.Estate.Ultima.Resources.UI;
using UnityEngine;

namespace Gamer.Estate.Ultima.Resources
{
    public interface IResourceProvider
    {
        AAnimationFrame[] GetAnimation(int body, ref int hue, int action, int direction);
        Texture2DInfo GetUITexture(int textureId, bool replaceMask080808 = false);
        Texture2DInfo GetItemTexture(int textureId);
        Texture2DInfo GetLandTexture(int textureId);
        Texture2DInfo GetTexmapTexture(int textureId);

        bool IsPointInUITexture(int textureId, int x, int y);
        bool IsPointInItemTexture(int textureId, int x, int y, int extraRange = 0);
        void GetItemDimensions(int textureId, out int width, out int height);

        ushort GetWebSafeHue(Color32 color);
        IFont GetUnicodeFont(int fontIndex);
        IFont GetAsciiFont(int fontIndex);
        string GetString(int strIndex);

        void RegisterResource<T>(IResource<T> resource);
        T GetResource<T>(int resourceIndex);
    }
}
