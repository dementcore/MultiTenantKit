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
            return UseMultiTenantKit<TTenant>(builder, true);
        }

        public static IApplicationBuilder UseMultiTenantKit<TTenant>(this IApplicationBuilder builder, bool UseMapperService)
          where TTenant : ITenant
        {
            builder.UseEndpointRouting();

            if (UseMapperService)
            {
                return builder.UseMiddleware<MultiTenantKitFullMiddleware<TTenant>>();
            }
            else
            {
                return builder.UseMiddleware<MultiTenantKitWithoutMapperMiddleware<TTenant>>();
            }
        }

    }
}

