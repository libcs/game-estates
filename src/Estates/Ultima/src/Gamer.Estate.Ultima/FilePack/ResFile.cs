using Gamer.Estate.Ultima.Resources;
using System;

namespace Gamer.Estate.Ultima.FilePack
{
    public partial class ResFile : IDisposable
    {
        //readonly AnimationResource _animationResource = new AnimationResource(null);
        //readonly FontsResource _fontsResource = new FontsResource(null);
        readonly ArtMulResource _artmulResource = new ArtMulResource(null);
        readonly GumpMulResource _gumpMulResource = new GumpMulResource(null);
        readonly TexmapResource _texmapResource = new TexmapResource(null);

        public ResFile() { }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~ResFile() => Close();

        public void Close() { }
    }
}