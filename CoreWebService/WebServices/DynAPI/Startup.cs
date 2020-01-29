using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.ExtensionsCore;
using Library.HelpersCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Engine.Data;
using Engine.Contracts.Interfaces;
using Engine.Business;
using Engine.Base.Contracts.Interfaces;

namespace DynAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SCIContext>(c =>
            c.UseSqlServer(Configuration.GetConnectionString("SCIDbConnection")));
            //services.AddDbContext<AnalyticsContext>(c =>
            //c.UseSqlServer(Configuration.GetConnectionString("AxDbConnection")));
            services.AddControllers();

            //Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IHostRepository, HostRepository>();
            //Services
            services.AddScoped<IHostBS, HostBS>();

            services.AddAuthentication(options =>
            {
                //options.DefaultScheme = "Bearer";
            })
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options =>
            {
                //options.Cookie.Name = "cookie1";
                //options.LoginPath = "/loginc1";
            });

            services.BearerAuthenticationBasic(Configuration.GetSection("JwtBearerConfig"), null);

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:60742")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
                });
            });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }



            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UsePathBase(new PathString("/dyn"));

            app.UseRouting();

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "dyn/api/{controller=Host}/{action=Index}/{id?}");
                    //routes.MapControllerRoute(
                    //    name: "default",
                    //    pattern: "api/{controller}/{action=app}/{id?}");
            });
        }
    }
}
