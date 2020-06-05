using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Globomantics.Constraints;
using Globomantics.Services;
using Globomantics.Theme;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Globomantics
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
            services.AddControllersWithViews(); 
            services.AddMvc(options=>options.EnableEndpointRouting=false);
            services.AddSingleton<ILoanService, LoanService>();
            services.AddTransient<IQuoteService, QuoteService>();
            services.AddTransient<IFeatureService, FeatureService>();
            services.AddTransient<IRateService, RateService>();
            services.Configure<IConfiguration>(Configuration);
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.Configure<RazorViewEngineOptions>(
                options=>options.ViewLocationExpanders.Add(new ThemeExpander())
            );

            services.AddDistributedMemoryCache();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            services.Configure<RouteOptions>(options => {
                options.ConstraintMap.Add("tokenCheck", typeof(TokenConstraint));
                options.ConstraintMap.Add("versionCheck", typeof(VersionConstraint));
            });

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            // else
            // {
            //     app.UseExceptionHandler("/Home/Error");
            //     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //     app.UseHsts();
            // }
            // app.UseHttpsRedirection();
            // app.UseStaticFiles();

            // app.UseRouting();

            // app.UseAuthorization();

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapControllerRoute(
            //         name: "default",
            //         pattern: "{controller=Home}/{action=Index}/{id?}");
            // });
            // loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            // loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                // app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
