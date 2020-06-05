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
using Microsoft.AspNetCore.Http;
using Venkat.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Venkat
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
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthorization(options=>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy=>policy.RequireClaim("Delete Role"));

                options.AddPolicy("EditRolePolicy",
                    policy=>policy.RequireClaim("Edit Role","true"));
            });
                
            services.AddControllersWithViews();
            services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            services.AddMvc(options=>options.EnableEndpointRouting=false);

            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            
            app.UseMvcWithDefaultRoute();

            // app.UseMvc(routes=>{
            //     routes.MapRoute("default","{controller}/{action}/{id?}");
            // });

            // app.UseRouting();


            app.UseAuthorization();

            // app.Run(async (context)=>
            // {
            //     await context.Response.WriteAsync("Hello World!");
            // });

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapControllerRoute(
            //         name: "default",
            //         pattern: "{controller=Home}/{action=Index}/{id?}");
            // });
        }
    }
}
