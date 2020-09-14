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
        /// <typeparam name="TTenantMapping">Tenant's Mappings Type</typeparam>
        /// <param name="configureEvents">Middleware events</param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddMultiTenantKit<TTenant>(this IServiceCollection services, Action<MultiTenantKitMiddlewareEvents<TTenant>> configureEvents)
            where TTenant : ITenant
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            if (configureEvents == null)
            {
                configureEvents = (ev) =>
                {

                };
            }

            services.Configure(configureEvents);

            services.Configure<TenantMiddlewareOptions>(options =>
            {
                options.TenantType = typeof(TTenant);
            });

            return new MultiTenantKitBuilder(services, typeof(TTenant));
        }

        /// <summary>
        /// Adds MultiTenantKit to the ServiceCollection
        /// </summary>
        /// <typeparam name="TTenant">Tenant's Entity Type</typeparam>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddMultiTenantKit<TTenant>(this IServiceCollection services)
            where TTenant : ITenant
        {
            return services.AddMultiTenantKit<TTenant>(null);
        }

        /// <summary>
        /// Adds MultiTenantKit to the ServiceCollection
        /// </summary>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddMultiTenantKit(this IServiceCollection services)
        {
            return services.AddMultiTenantKit<Tenant>();
        }

        /// <summary>
        /// Adds MultiTenantKit to the ServiceCollection
        /// </summary>
        /// <param name="configureEvents">Middleware events</param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddMultiTenantKit(this IServiceCollection services, Action<MultiTenantKitMiddlewareEvents<Tenant>> configureEvents)
        {
            return services.AddMultiTenantKit<Tenant>(configureEvents);
        }
    }
}
