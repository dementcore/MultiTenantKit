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
        /// Adds MultiTenantKit to the ServiceCollection
        /// </summary>
        /// <typeparam name="TTenant">Tenant's Entity Type</typeparam>
        /// <typeparam name="TTenantMapping">Tenant's Mappings Type</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddMultiTenantKit<TTenant, TTenantMapping>(this IServiceCollection services)
            where TTenant : ITenant
            where TTenantMapping : ITenantMapping
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return new MultiTenantKitBuilder(services, typeof(TTenant), typeof(TTenantMapping));
        }

        /// <summary>
        /// Adds MultiTenantKit to the ServiceCollection
        /// </summary>
        /// <typeparam name="TTenant">Tenant's Entity Type</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddMultiTenantKit<TTenant>(this IServiceCollection services)
          where TTenant : ITenant
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return new MultiTenantKitBuilder(services, typeof(TTenant));
        }

        ///// <summary>
        ///// Add MultitenantKit default services with Route Resolver, default Mapper Service and default Info Service
        ///// </summary>
        ///// <param name="services"></param>
        ///// <param name="configuration"></param>
        //public static void AddDefaultMultiTenantKit(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        //    MultiTenantKitBuilder builder = new MultiTenantKitBuilder(services, typeof(Tenant));

        //    builder.AddInMemoryTenantsStore(configuration.GetSection("Tenants:TenantsData"))
        //        .AddInMemoryTenantMappingsStore<TenantMapping>(configuration.GetSection("Tenants:TenantMappings"))
        //        .AddDefaultTenantRouteResolverService()
        //        .AddDefaultTenantMapperService<TenantMapping>()
        //        .AddDefaultTenantInfoService<Tenant>();
        //}

        //public static void AddDefaultMultiTenantKit(this IServiceCollection services, List<ITenant> tenants, List<TenantMapping> tenantMappings)
        //{
        //    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        //    MultiTenantKitBuilder builder = new MultiTenantKitBuilder(services, typeof(Tenant));

        //    builder.AddInMemoryTenantsStore(tenants)
        //        .AddInMemoryTenantMappingsStore(tenantMappings)
        //        .AddDefaultTenantRouteResolverService()
        //        .AddDefaultTenantMapperService<TenantMapping>()
        //        .AddDefaultTenantInfoService<Tenant>();

        //}
    }
}
