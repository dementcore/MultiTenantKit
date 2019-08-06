using DementCore.MultiTenantKit.Configuration.Options;
using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Hosting;
using Microsoft.AspNetCore.Internal;
using Microsoft.Extensions.Options;
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

            object options = builder.ApplicationServices.GetService(typeof(IOptionsMonitor<TenantMiddlewareOptions>));

            TenantMiddlewareOptions tenantMiddlewareOptions = null;

            if (options != null)
            {
                tenantMiddlewareOptions = ((IOptionsMonitor<TenantMiddlewareOptions>)options).CurrentValue;
            }

            if (tenantMiddlewareOptions == null)
            {
                throw new InvalidOperationException("Unable to add middleware to request pipeline because you have not registered the MultiTenantKit Services.");
            }

            Type middlewareType = typeof(MultiTenantKitMiddleware<,>).MakeGenericType(tenantMiddlewareOptions.TenantType, tenantMiddlewareOptions.TenantMappingType);

            builder.UseEndpointRouting();

            return builder.UseMiddleware(middlewareType);
        }
    }
}

