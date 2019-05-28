using System;
using System.IO;
using System.Web;

namespace Gamer.Core
{
    /// <summary>
    /// CompressTextureModule
    /// </summary>
    /// <seealso cref="System.Web.IHttpModule" />
    public class CompressTextureModule : IHttpModule
    {
        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication" /> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context) => context.PreRequestHandlerExecute += new EventHandler(OnPreRequestHandlerExecute);

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule" />.
        /// </summary>
        public void Dispose() { }

        void OnPreRequestHandlerExecute(object source, EventArgs e)
        {
            var app = (HttpApplication)source;
            var req = app.Context.Request;
            var fileExtension = Path.GetExtension(req.Url.LocalPath);
            //var platform = req.QueryString["p"] ?? req.Headers["Platform"];
            //throw new HttpException(500, $"ext: {fileExtension} platform: {platform}");
            if (fileExtension.ToLowerInvariant() != ".dds")
                return;
            var res = app.Context.Response;
            var platform = req.QueryString["p"] ?? req.Headers["Platform"];
            //throw new HttpException(500, $"ext: {fileExtension} platform: {platform}");
            if (!string.IsNullOrEmpty(platform))
                res.Filter = new CompressTextureStream(app.Context, res.Filter, platform);
        }
    }
}
