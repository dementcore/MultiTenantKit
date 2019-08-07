using DementCore.MultiTenantKit.Configuration.DependencyInjection;
using DementCore.MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions;
using DementCore.MultiTenantKit.Configuration.Options;
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

            services.Configure<TenantMiddlewareOptions>(options =>
            {
                options.TenantType = typeof(TTenant);
                options.TenantMappingType = typeof(TTenantMapping);
            });

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
            return services.AddMultiTenantKit<TTenant, TenantMapping>();
        }

        /// <summary>
        /// Adds MultiTenantKit to the ServiceCollection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddMultiTenantKit(this IServiceCollection services)
        {
            return services.AddMultiTenantKit<Tenant, TenantMapping>();
        }
    }
}
