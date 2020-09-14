using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MultiTenantKit.Configuration.Options;
using MultiTenantKit.Core.Context;
using MultiTenantKit.Core.Models;
using MultiTenantKit.Hosting;
using MultiTenantKit.Hosting.Events;
using MultiTenantKit.TestBranch;
using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class MultiTenantKitApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds MultiTenantKit tenant resolution middleware to the Microsoft.AspNetCore.Builder.IApplicationBuilder request execution pipeline.
        /// </summary>
        /// <typeparam name="TTenant">Type of Tenant's Entity</typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMultiTenantKit(this IApplicationBuilder builder)
        {
            IOptionsMonitor<TenantMiddlewareOptions> options =
                builder.ApplicationServices.GetRequiredService(typeof(IOptionsMonitor<TenantMiddlewareOptions>)) as IOptionsMonitor<TenantMiddlewareOptions>;

            TenantMiddlewareOptions tenantMiddlewareOptions = options.CurrentValue;

            Type middlewareType = typeof(MultiTenantKitMiddleware<>).MakeGenericType(tenantMiddlewareOptions.TenantType);

            return builder.UseMiddleware(middlewareType);
        }

        public static IApplicationBuilder UsePerTenant<TTenant>(this IApplicationBuilder builder,Action<TenantContext<TTenant>,IApplicationBuilder> pipeline) 
            where TTenant:ITenant
        {
            IOptionsMonitor<TenantMiddlewareOptions> options =
                builder.ApplicationServices.GetRequiredService(typeof(IOptionsMonitor<TenantMiddlewareOptions>)) as IOptionsMonitor<TenantMiddlewareOptions>;

            TenantMiddlewareOptions tenantMiddlewareOptions = options.CurrentValue;

            Type middlewareType = typeof(MultiTenantPipelineMiddleware<>).MakeGenericType(tenantMiddlewareOptions.TenantType);

            builder.UseMiddleware(middlewareType,builder, pipeline);

            return builder;
        }
    }
}

