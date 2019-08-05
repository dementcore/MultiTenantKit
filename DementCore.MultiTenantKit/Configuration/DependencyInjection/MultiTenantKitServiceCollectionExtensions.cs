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
        /// <summary>
        /// Adds MultitenantKit service and returns a builder to configure the system.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddMultiTenantKit(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return new MultiTenantKitBuilder(services);
        }

        /// <summary>
        /// Add MultitenantKit default services with Route Resolver, default Mapper Service and default Info Service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddDefaultMultiTenantKit(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            MultiTenantKitBuilder builder = new MultiTenantKitBuilder(services);

            builder.AddInMemoryTenantsStore<Tenant>(configuration.GetSection("Tenants:TenantsData"))
                .AddInMemoryTenantMappingsStore<TenantMapping>(configuration.GetSection("Tenants:TenantMappings"))
                .AddDefaultTenantRouteResolverService()
                .AddDefaultTenantMapperService<TenantMapping>()
                .AddDefaultTenantInfoService<Tenant>();
        }

        public static void AddDefaultMultiTenantKit(this IServiceCollection services, List<Tenant> tenants, List<TenantMapping> tenantMappings)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            MultiTenantKitBuilder builder = new MultiTenantKitBuilder(services);

            builder.AddInMemoryTenantsStore(tenants)
                .AddInMemoryTenantMappingsStore(tenantMappings)
                .AddDefaultTenantRouteResolverService()
                .AddDefaultTenantMapperService<TenantMapping>()
                .AddDefaultTenantInfoService<Tenant>();

        }
    }
}
