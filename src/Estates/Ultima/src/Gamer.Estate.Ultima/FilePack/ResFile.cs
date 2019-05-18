using Gamer.Estate.Ultima.Resources;
using Gamer.Proxy;
using System;

namespace Gamer.Estate.Ultima.FilePack
{
    public partial class ResFile : IDisposable
    {
        readonly ProxySink _proxySink;
        //readonly AnimationResource _animationResource = new AnimationResource(null);
        //readonly FontsResource _fontsResource = new FontsResource(null);
        readonly ArtMulResource _artmulResource = new ArtMulResource(null);
        readonly GumpMulResource _gumpMulResource = new GumpMulResource(null);
        readonly TexmapResource _texmapResource = new TexmapResource(null);

        public ResFile(ProxySink proxySink) => _proxySink = proxySink;

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~ResFile() => Close();

        public void Close() { }
    }
}