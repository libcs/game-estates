using Gamer.Core.Format;
using System.IO;
using System.Web;
using UnityEngine;

namespace Gamer.Core
{
    public class CompressTextureStream : TransformStream
    {
        readonly HttpContext _ctx;
        readonly string _platform;

        public CompressTextureStream(HttpContext ctx, Stream stream, string platform) : base(stream)
        {
            _ctx = ctx;
            _platform = platform;
        }

        protected override MemoryStream Transform(MemoryStream stream)
        {
            //return new MemoryStream(new byte[] { 1, 2, 3 });
            switch (_platform)
            {
                case "WindowsEditor":
                case "WindowsPlayer":
                case "Android":
                    //var filename = Path.ChangeExtension(Path.GetFileName(_ctx.Request.Url.LocalPath), ".img");
                    //_ctx.Response.AddHeader("Content-Disposition", $"filename={HttpUtility.UrlEncode(filename)}");
                    var dds = DdsReader.LoadDDSTexture(stream);
                    dds.Compress(TextureFormat.ETC_RGB4);
                    return new MemoryStream(dds.GetImage());
                default: return stream;
            }
        }
    }
}
