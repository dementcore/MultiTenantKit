using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Hosting;
using Microsoft.AspNetCore.Internal;

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
        public static IApplicationBuilder UseMultiTenantKit<TTenant>(this IApplicationBuilder builder)
            where TTenant : ITenant
        {
            builder.UseEndpointRouting();

            return builder.UseMiddleware<MultiTenantKitMiddleware<TTenant>>();
        }
    }
}

