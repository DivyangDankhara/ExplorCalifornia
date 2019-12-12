using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExplorCalifornia.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ExplorCalifornia
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FormattingService>();

            services.AddTransient<FeatureToggles>(x => new FeatureToggles
            {
                DeveloperException = configuration.GetValue<bool>("EnableDeveloperException")
            });

            services.AddDbContext<BlogDataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("BlogDataContext");
                options.UseSqlServer(connectionString);
            });
            
            services.AddDbContext<IdentityDataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("IdentityDataContext");
                options.UseSqlServer(connectionString);
            });

            services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityDataContext>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,FeatureToggles features)
        {

            app.UseExceptionHandler("/error.html");
            
            if (features.DeveloperException)
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/divyang", async context =>
                {
                    throw new Exception("Error");
                });

                endpoints.MapControllerRoute("Default","{controller=Home}/{action=index}/{id?}");
            });

            app.UseFileServer();
        }
    }
}
