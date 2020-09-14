using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions;
using MultiTenantKit.Core.Models;
using MultiTenantKit.MultiTenantCustomEventsSample.MultiTenantImplementations;
using System.Collections.Generic;

namespace MultiTenantKit.MultiTenantCustomEventsSample
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMultiTenantKit<CustomTenant>((e) =>
            {
                e.TenantResolutionSuccessEvent = (res, result) =>
                {
                    string a = result;
                    res.WriteAsync("\r\nTenant resolved success:" + a);
                };

                e.TenantResolutionNotFoundEvent = (response) =>
                {
                    response.WriteAsync("\r\nTenant not resolved.");
                };

                e.TenantMappingSuccessEvent = (res, result) =>
                {
                    TenantMapping a = result.Value;

                    res.WriteAsync("\r\nTenant id mapped sucess: " + a.TenantId);
                };

                e.TenantMappingNotFoundEvent = (response) =>
                 {
                     response.WriteAsync("\r\nTenant mapping not found");
                 };

                e.TenantInfoSuccessEvent = (res, result) =>
                {
                    CustomTenant a = result.Value;

                    res.WriteAsync("\r\nTenant info instance success: " + a.Name);
                };

                e.TenantInfoNotFoundEvent = (response) =>
                {
                    response.WriteAsync("\r\nTenant info not found");
                };
            })
                .AddInMemoryTenantsStore(Configuration.GetSection("Tenants:TenantsData"))
                .AddInMemoryTenantMappingsStore(Configuration.GetSection("Tenants:TenantMappings"))
                .AddDefaultTenantMapperService()
                .AddDefaultTenantInfoService()
                .AddDefaultRouteResolverService(options =>
                {
                    options.ExcludedRouteTemplates = new List<string> { "/" };
                    options.RouteSegmentName = "Tenant";
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMultiTenantKit();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{Tenant=tenant1}/{action=Index}/{id?}");
            });
        }
    }
}
