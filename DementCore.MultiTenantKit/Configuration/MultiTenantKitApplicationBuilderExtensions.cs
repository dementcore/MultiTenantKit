using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Hosting;

namespace Microsoft.AspNetCore.Builder
{
    public static class MultiTenantKitApplicationBuilderExtensions
    {
        /// <summary>
        /// Registra el middleware de resolución de inquilinos en la peticion
        /// </summary>
        /// <typeparam name="TTenant">Tipo que representa el inquilino</typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseTenantResolverMiddleware<TTenant>(this IApplicationBuilder builder)
            where TTenant : ITenant
        {
            return builder.UseMiddleware<MultiTenantKitMiddleware<TTenant>>();
        }

        /// <summary>
        /// Registra el middleware de resolución de inquilinos en la peticion
        /// </summary>
        /// <typeparam name="TTenant">Tipo que representa el inquilino</typeparam>
        /// <param name="builder"></param>
        /// <param name="options">Opciones de configuración</param>
        /// <returns></returns>
        public static IApplicationBuilder UseTenantResolverMiddleware<TTenant>(this IApplicationBuilder builder, MultiTenantKitMiddlewareOptions options)
            where TTenant : ITenant
        {
            return builder.UseMiddleware<MultiTenantKitMiddleware<TTenant>>(options);
        }
    }
}

