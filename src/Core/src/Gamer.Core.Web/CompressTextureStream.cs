using Gamer.Core.Format;
using System.IO;
using System.Web;
using UnityEngine;

namespace Gamer.Core
{
    public class CompressTextureStream : TransformStream
    {
        public CompressTextureStream(Stream stream) : base(stream) { }

        protected override MemoryStream Transform(MemoryStream stream)
        {
            var dds = DdsReader.LoadDDSTexture(stream);
            var tex = dds.ToTexture2DSlim();
            tex.Compress(TextureFormat.ETC_RGB4);
            File.WriteAllBytes(@"C:\T_\Sample.dds", tex.textureData);
            throw new HttpException(500, $"a:{tex}");
            //
            //return new MemoryStream(tex.GetRawTextureData());
        }
    }
}
