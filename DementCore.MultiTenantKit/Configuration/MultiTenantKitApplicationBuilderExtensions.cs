using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Hosting;
using Microsoft.AspNetCore.Internal;

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
        public static IApplicationBuilder UseMultiTenantKit<TTenant>(this IApplicationBuilder builder)
            where TTenant : ITenant
        {
            //esto hace que podamos obtener el RouteData en un middleware previo al middleware de MVC que por norma general es el ultimo.
            builder.UseEndpointRouting();

            return builder.UseMiddleware<MultiTenantKitMiddleware<TTenant>>();
        }
    }
}

