using System;
using System.Web;

namespace Core
{
    // https://go.microsoft.com/?linkid=8101007
    public class CompressTextureModule : IHttpModule
    {
        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(OnPreRequestHandlerExecute);
        }

        public void OnPreRequestHandlerExecute(object source, EventArgs e)
        {
            var app = (HttpApplication)source;
            var request = app.Context.Request;
            //if (!string.IsNullOrEmpty(request.Headers["Referer"]))
            throw new HttpException(309, "Uh-uh!");
        }
    }
}
