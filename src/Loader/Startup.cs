using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loader
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ////services.AddDistributedMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }
        //    else
        //    {
        //        app.UseExceptionHandler("/Error");
        //        app.UseHsts();
        //    }

        //    //app.UsePathBase("/u");
        //    //app.Use((context, next) =>
        //    //{
        //    //    context.Request.PathBase = "/u";
        //    //    return next();
        //    //});

        //    app.UseHttpsRedirection();
        //    app.UseStaticFiles();
        //    app.UseSpaStaticFiles();

        //    app.UseMvc(routes =>
        //    {
        //        routes.MapRoute(
        //            name: "default",
        //            template: "{controller}/{action=Index}/{id?}");
        //    });

        //    app.UseSpa(spa =>
        //    {
        //        spa.Options.SourcePath = "ClientApp";

        //        if (env.IsDevelopment())
        //        {
        //            spa.UseReactDevelopmentServer(npmScript: "start");
        //        }
        //    });
        //}
    }
}
