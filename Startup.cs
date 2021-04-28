using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Westwind.AspNetCore.LiveReload;



namespace RazorPagesMovie
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Environment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IMvcBuilder builder = services.AddRazorPages();

            // Use SqLite for development and SQLServer for production
            if(Environment.IsDevelopment())
            {
                // Configure LiveReload
                // https://github.com/RickStrahl/Westwind.AspnetCore.LiveReload
                services.AddLiveReload();


                // for runtime compilation, ie, watching file changes
                // Note that you will still need to reload the page in the browser 
                builder.AddRazorRuntimeCompilation();

                services.AddDbContext<RazorPagesMovieContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("RazorPagesMovieContext")));
            }
            else 
            {
                services.AddDbContext<RazorPagesMovieContext>(options => 
                options.UseSqlServer(
                    Configuration.GetConnectionString("MovieContext")));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // LiveReload
                // https://github.com/RickStrahl/Westwind.AspnetCore.LiveReload
                app.UseLiveReload();
                
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
