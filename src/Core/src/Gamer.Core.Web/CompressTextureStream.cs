using Gamer.Core.Format;
using System.IO;
using System.Text;
using System.Web;
using UnityEngine;

namespace Core
{
    public class CompressTextureStream : TransformStream
    {
        public CompressTextureStream(Stream stream) : base(stream) { }

        protected override MemoryStream Transform(MemoryStream stream)
        {
            var dds = DdsReader.LoadDDSTexture(stream);
            var tex = dds.ToTexture2DSlim();
            throw new HttpException(500, $"a:{tex}");
            //tex.co
            //EditorUtility.CompressTexture(tex, TextureFormat.ETC2_RGB, 50);
            ////
            //File.WriteAllBytes(Application.dataPath + "/../SavedRenderTexture.exr", bytes);
            //return new MemoryStream(tex.GetRawTextureData());
        }
    }
}
