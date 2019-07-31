using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Services;
using DementCore.MultiTenantKit.Core.Services.Default;
using DementCore.MultiTenantKit.Core.Stores;
using DementCore.MultiTenantKit.Core.Stores.Default;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DementCore.MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions
{
    public static class MultiTenantKitBuilderExtensions
    {

        #region Default Services

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

        /// <summary>
        /// Agrega el servicio de resolución de inquilino por defecto.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="routePattern">Función que retorna la plantilla de ruta</param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddDefaultTenantResolver(this IMultiTenantKitBuilder builder, Func<string> routePattern)
        {
            //builder.Services.AddTransient<ITenantResolver, DefaultTenantResolver>();
            builder.Services.AddTransient<ITenantResolver, DefaultTenantResolver>((tr) =>
            {
                string routeTemplate = routePattern();
                return new DefaultTenantResolver(routeTemplate);
            });

            return builder;
        }

        /// <summary>
        /// Agrega el servicio de resolución de inquilino por defecto.
        /// Resuelve el slug del inquilino a partir del patrón de ruta configurado en la propiedad TenantSlugPattern
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="routePattern">Plantilla de ruta de la que extraer el slug</param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddDefaultTenantResolver(this IMultiTenantKitBuilder builder, string routePattern)
        {
            //builder.Services.AddTransient<ITenantResolver, DefaultTenantResolver>();
            builder.Services.AddTransient<ITenantResolver, DefaultTenantResolver>((tr) =>
            {
                return new DefaultTenantResolver(routePattern);
            });

            return builder;
        }

        /// <summary>
        /// Registra el servicio de mapeo de slug a identificador de inquilino por defecto. 
        /// Resuelve el identificador a partir de los slugs configurados en la sección Tenants/TenantsSlugs de la Configuración
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddDefaultTenantMapper(this IMultiTenantKitBuilder builder)
        {
            builder.Services.AddScoped<ITenantMapper, DefaultTenantMapper>();

            return builder;
        }

        /// <summary>
        /// Registra el servicio de recuperación de información sobre el inquilino por defecto.
        /// Resuelve el identificador a partir de la información configurada en la sección Tenants/TenantsData de la Configuración
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddDefaultTenantStore<TTenant>(this IMultiTenantKitBuilder builder) where TTenant : ITenant
        {
            builder.Services.AddScoped<ITenantStore<TTenant>, DefaultTenantStore<TTenant>>();

            return builder;
        }

        #endregion

        #region Custom Services

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
        /// Registra un servicio personalizado de mapeo del slug del inquilino al identificador del inquilino
        /// </summary>
        /// <typeparam name="T">Tipo de la implementación de ITenantMapper</typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder AddCustomTenantMapper<T>(this IMultiTenantKitBuilder builder)
            where T : class, ITenantMapper
        {
            builder.Services.AddTransient<ITenantMapper, T>();

            return builder;
        }

        #endregion

    }
}
