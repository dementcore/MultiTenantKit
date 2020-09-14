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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

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

            services.AddMultiTenantKit<CustomTenant>()
                .AddInMemoryTenantsStore(Configuration.GetSection("Tenants:TenantsData"))
                .AddDefaultTenantInfoService()
                .AddDefaultRouteResolverService()
                .AddDefaultTenantProviderService();

            services.AddTransient<IPostConfigureOptions<CookieAuthenticationOptions>>((services) =>
            {
                return new OptionsPerTenant.MultitenantKitPostConfigureOptions<CustomTenant, CookieAuthenticationOptions>((options, tenant) =>
                {
                    options.LoginPath = $"/{tenant.Id}/Login";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Name = tenant.Id + ".AuthCookie";
                    options.Cookie.IsEssential = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    options.Events.OnValidatePrincipal = (ctx) =>
                    {
                        var tenant = ctx.HttpContext.GetTenantContext<CustomTenant>().Tenant;
                        if (((ClaimsIdentity)ctx.Principal.Identity).FindFirst("tenant").Value != tenant.Id)
                        {
                            ctx.RejectPrincipal();
                        }

                        return Task.CompletedTask;
                    };

                    options.Events.OnSigningIn = (ctx) =>
                    {
                        var tenant = ctx.HttpContext.GetTenantContext<CustomTenant>().Tenant;
                        ((ClaimsIdentity)ctx.Principal.Identity).AddClaim(new Claim("tenant", tenant.Id));

                        return Task.CompletedTask;
                    };
                },
                services.GetService<Core.Services.ITenantProvider<CustomTenant>>());
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie("Cookies");
            services.AddAuthorization();


            services.AddMvc();
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
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseMultiTenantKit();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints((configure) =>
            {
                configure.MapDefaultControllerRoute();
            });
        }
    }
}
