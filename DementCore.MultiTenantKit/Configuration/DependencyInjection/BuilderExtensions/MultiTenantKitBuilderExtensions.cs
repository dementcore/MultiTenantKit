using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Services;
using DementCore.MultiTenantKit.Core.Services.Default;
using DementCore.MultiTenantKit.Core.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace DementCore.MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions
{
    public static class MultiTenantKitBuilderExtensions
    {
        /// <summary>
        /// Agrega el servicio de resolución de inquilino por defecto.
        /// Resuelve el slug del inquilino a partir del patrón de ruta configurado en la configuracion TenantSlugPattern
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddDefaultTenantResolver(this IMultiTenantKitBuilder builder)
        {
            builder.Services.AddTransient<ITenantResolver, DefaultTenantResolver>();

            return builder;
        }

        /// <summary>
        /// Registra un servicio personalizado de resolucion de inquilino.
        /// </summary>
        /// <typeparam name="T">Tipo de la implementación de ITenantResolver</typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddCustomTenantResolver<T>(this IMultiTenantKitBuilder builder)
            where T : class, ITenantResolver
        {
            builder.Services.AddTransient<ITenantResolver, T>();

            return builder;
        }

        /// <summary>
        /// Registra un servicio personalizado de recuperación de información sobre el inquilino.
        /// </summary>
        /// <typeparam name="T">Tipo de la implementacion de ITenantStore</typeparam>
        /// <typeparam name="TTenant">Tipo que representa el inquilino</typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddCustomTenantStore<T, TTenant>(this IMultiTenantKitBuilder builder)
            where T : class, ITenantStore<TTenant>
            where TTenant : ITenant
        {
            builder.Services.AddTransient<ITenantStore<TTenant>, T>();

            return builder;
        }

        /// <summary>
        /// Registra el servicio que proporciona la entidad del inquilino (Por defecto se recupera del HttpContext)
        /// </summary>
        /// <typeparam name="TTenant"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddDefaultTenantProviderService<TTenant>(this IMultiTenantKitBuilder builder)
            where TTenant : ITenant
        {
            builder.Services.AddScoped<ITenantProvider<TTenant>, DefaultTenantProvider<TTenant>>();

            return builder;
        }
    }
}
