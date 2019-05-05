using Gamer.Core;
using UnityEngine;

namespace Gamer.Estate.Ultima.Resources
{
    public abstract class AAnimationFrame
    {
        public Vector2Int Center { get; protected set; }
        public Texture2DInfo Texture { get; protected set; }
        public abstract bool IsPointInTexture(int x, int y);
    }
}
