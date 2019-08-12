using MultiTenantKit.Configuration.DependencyInjection;
using MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions;
using MultiTenantKit.Configuration.Options;
using MultiTenantKit.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;
using MultiTenantKit.Hosting.Events;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MultiTenantKitServiceCollectionExtensions
    {
        /// <summary>
        /// Adds MultiTenantKit to the ServiceCollection
        /// </summary>
        /// <typeparam name="TTenant">Tenant's Entity Type</typeparam>
        /// <typeparam name="TTenantMapping">Tenant's Mapping Entity Type</typeparam>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddMultiTenantKit<TTenant, TTenantMapping>(this IServiceCollection services)
            where TTenant : ITenant
            where TTenantMapping : ITenantMapping
        {
            return services.AddMultiTenantKit<TTenant, TTenantMapping>((ce) => { });
        }

        /// <summary>
        /// Adds MultiTenantKit to the ServiceCollection
        /// </summary>
        /// <typeparam name="TTenant">Tenant's Entity Type</typeparam>
        /// <typeparam name="TTenantMapping">Tenant's Mappings Type</typeparam>
        /// <param name="configureEvents">Middleware events</param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddMultiTenantKit<TTenant, TTenantMapping>(this IServiceCollection services, Action<MultiTenantKitMiddlewareEvents<TTenant, TTenantMapping>> configureEvents)
            where TTenant : ITenant
            where TTenantMapping : ITenantMapping
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure(configureEvents);

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
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddMultiTenantKit<TTenant>(this IServiceCollection services)
            where TTenant : ITenant
        {
            return services.AddMultiTenantKit<TTenant, TenantMapping>();
        }

        /// <summary>
        /// Adds MultiTenantKit to the ServiceCollection
        /// </summary>
        /// <typeparam name="TTenant">Tenant's Entity Type</typeparam>
        /// <param name="configureEvent">Middleware events</param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddMultiTenantKit<TTenant>(this IServiceCollection services, Action<MultiTenantKitMiddlewareEvents<TTenant, TenantMapping>> configureEvent)
         where TTenant : ITenant
        {
            return services.AddMultiTenantKit<TTenant, TenantMapping>(configureEvent);
        }

        /// <summary>
        /// Adds MultiTenantKit to the ServiceCollection
        /// </summary>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddMultiTenantKit(this IServiceCollection services)
        {
            return services.AddMultiTenantKit<Tenant, TenantMapping>();
        }

        /// <summary>
        /// Adds MultiTenantKit to the ServiceCollection
        /// </summary>
        /// <param name="configureEvents">Middleware events</param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddMultiTenantKit(this IServiceCollection services, Action<MultiTenantKitMiddlewareEvents<Tenant, TenantMapping>> configureEvents)
        {
            return services.AddMultiTenantKit<Tenant, TenantMapping>(configureEvents);
        }
    }
}
