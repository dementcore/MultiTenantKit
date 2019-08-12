using Microsoft.AspNetCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MultiTenantKit.Configuration.Options;
using MultiTenantKit.Hosting;
using MultiTenantKit.Hosting.Events;
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

            Type middlewareType = typeof(MultiTenantKitMiddleware<,>).MakeGenericType(tenantMiddlewareOptions.TenantType, tenantMiddlewareOptions.TenantMappingType);

            builder.UseEndpointRouting();

            return builder.UseMiddleware(middlewareType);
        }
    }
}

