using DementCore.MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMultitenantWebApplication.MultiTenantImplementations;
using System;

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

            services.Configure<MultiTenantKitMiddlewareOptions>(options =>
            {
                options.IncludeInUserClaims = true;
                options.IncludeOnlyIfAuthenticated = false;
                options.IncludeInHttpContext = true;
                options.ClaimsPrefix = "Inquilino_";
            });

            services.AddTenantResolver()
                .AddDefaultTenantProviderService<MyTenant>()
                .AddCustomTenantResolver<MyCustomTenantResolver>()
                .AddCustomTenantStore<MyCustomTenantStore, MyTenant>();

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

            app.UseTenantResolverMiddleware<MyTenant>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{tenant}/{action=Index}/{id?}");
            });
        }
    }
}
