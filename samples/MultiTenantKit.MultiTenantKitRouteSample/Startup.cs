using MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions;
using MultiTenantKit.Configuration.Options;
using MultiTenantKit.Core.Models;
using MultiTenantKit.Core.Stores.InMemory;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantKit.MultiTenantRouteSample.Models;
using MultiTenantKit.MultiTenantRouteSample.MultiTenantImplementations;
using System.Collections.Generic;

namespace MultiTenantKit.MultiTenantRouteSample
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
           
            services.AddMultiTenantKit<CustomTenant>()
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

            app.UsePerTenant<CustomTenant>((ctx, builder) =>
            {
                builder.
                if (ctx.Tenant.Id == "e1009e1b-da1e-481a-b902-417e84ad349a")
                {
                    builder.Run(async (next) =>
                    {
                        await next.Response.WriteAsync("Soy el tenant: " + ctx.Tenant.Name);
                    });
                }
            });

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{Tenant=tenant1}/{action=Index}/{id?}");
            });
        }
    }
}
