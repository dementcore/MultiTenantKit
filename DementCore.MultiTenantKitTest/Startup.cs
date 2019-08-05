using DementCore.MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions;
using DementCore.MultiTenantKit.Configuration.Options;
using DementCore.MultiTenantKit.Core.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMultitenantWebApplication.MultiTenantImplementations;
using System.Collections.Generic;

namespace MyMultitenantWebApplication
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

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie("Cookies",options=>
                {
                    options.LoginPath = "/";
                });

            services.AddMultiTenantKit()
                .AddInMemoryTenantsStore<MyTenant>(Configuration.GetSection("Tenants:TenantsData"))
                .AddInMemoryTenantMappingsStore<TenantMapping>(Configuration.GetSection("Tenants:TenantMappings"))
                .AddDefaultTenantMapperService<TenantMapping>()
                .AddDefaultTenantInfoService<MyTenant>()
                .AddDefaultTenantClaimResolverService(options =>
                {
                    options.ClaimName = "Inquilino";
                    options.OnlyAuthenticated = false;
                    options.ResolutionType = DementCore.MultiTenantKit.Core.ResolutionType.TenantName;
                })
                //.AddDefaultTenantRouteResolverService(options =>
                //{
                //    options.ExcludedRouteTemplates = new List<string> { "/" };
                //    options.RouteSegmentName = "Inquilino";
                //})
                //.AddDefaultTenantDomainResolverService(options =>
                //{
                //    options.ExcludedDomains = new List<string> { "admin.midominio.com" };
                //    options.DomainTemplate = "{0}.midominio.com";
                //})
                ;

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
            app.UseAuthentication();

            app.UseMultiTenantKit<MyTenant>();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
