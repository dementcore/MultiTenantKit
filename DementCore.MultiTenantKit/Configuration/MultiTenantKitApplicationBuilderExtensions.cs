using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Hosting;
using Microsoft.AspNetCore.Internal;

namespace Microsoft.AspNetCore.Builder
{
    public static class MultiTenantKitApplicationBuilderExtensions
    {

        public static IApplicationBuilder UseMultiTenantKit<TTenant>(this IApplicationBuilder builder)
            where TTenant : ITenant
        {
            builder.UseEndpointRouting();

            return builder.UseMiddleware<MultiTenantKitMiddleware<TTenant>>();
        }
    }
}

