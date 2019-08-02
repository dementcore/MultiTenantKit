using DementCore.MultiTenantKit.Configuration.DependencyInjection;
using DementCore.MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions;
using DementCore.MultiTenantKit.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MultiTenantKitServiceCollectionExtensions
    {

        public static IMultiTenantKitBuilder AddMultiTenantKit(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return new MultiTenantKitBuilder(services);
        }

        public static void AddDefaultMultiTenantKit(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            MultiTenantKitBuilder builder = new MultiTenantKitBuilder(services);

            builder.AddInMemoryTenants<Tenant>(configuration.GetSection("Tenants:TenantsData"))
                .AddInMemoryTenantSlugs<TenantSlugs>(configuration.GetSection("Tenants:TenantsSlugs"))
                .AddTenantPathResolverService()
                .AddDefaultTenantMapperService<TenantSlugs>()
                .AddDefaultTenantInfoService<Tenant>();
        }

        public static void AddDefaultMultiTenantKit(this IServiceCollection services, List<Tenant> tenants, List<TenantSlugs> tenantSlugs)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            MultiTenantKitBuilder builder = new MultiTenantKitBuilder(services);

            builder.AddInMemoryTenants<Tenant>(tenants)
                .AddInMemoryTenantSlugs<TenantSlugs>(tenantSlugs)
                .AddTenantPathResolverService()
                .AddDefaultTenantMapperService<TenantSlugs>()
                .AddDefaultTenantInfoService<Tenant>();

        }
    }
}
