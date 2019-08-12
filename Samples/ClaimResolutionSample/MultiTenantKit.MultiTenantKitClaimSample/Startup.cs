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
using MultiTenantKit.MultiTenantClaimSample.Models;
using MultiTenantKit.MultiTenantClaimSample.MultiTenantImplementations;
using System.Collections.Generic;

namespace MultiTenantKit.MultiTenantClaimSample
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
                .AddCookie("Cookies", options =>
                 {
                     options.LoginPath = "/Login";
                     options.Cookie.HttpOnly = true;
                     options.Cookie.Name = "AuthCookie";
                     options.Cookie.IsEssential = true;
                     options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                 });

            services.AddMultiTenantKit<CustomTenant>()
                .AddInMemoryTenantsStore(Configuration.GetSection("Tenants:TenantsData"))
                .AddDefaultTenantInfoService()
                .AddDefaultClaimResolverService(options =>
                {
                    options.ClaimName = "TenantId";
                    options.OnlyAuthenticated = true;
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
            app.UseAuthentication();

            app.UseMultiTenantKit();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
