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
        public static IMultiTenantKitBuilder<TTenant> AddDefaultTenantProviderService<TTenant>(this IMultiTenantKitBuilder<TTenant> builder)
            where TTenant : ITenant
        {
            builder.Services.AddScoped<ITenantProvider<TTenant>, DefaultTenantProvider<TTenant>>();

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
        public static IMultiTenantKitBuilder<TTenant> AddCustomTenantResolver<TTenant>(this IMultiTenantKitBuilder<TTenant> builder, Type implementationType)
            where TTenant : ITenant
        {
            builder.Services.AddTransient(typeof(ITenantResolverService), implementationType);

            return builder;
        }

        /// <summary>
        /// Registra un servicio personalizado de recuperación de información sobre el inquilino.
        /// </summary>
        /// <typeparam name="T">Tipo de la implementacion de ITenantStore</typeparam>
        /// <typeparam name="TTenant">Tipo que representa el inquilino</typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder<TTenant> AddCustomTenantStore<TTenant>(this IMultiTenantKitBuilder<TTenant> builder, Type implementationType)
    where TTenant : ITenant
        {
            builder.Services.AddTransient(typeof(ITenantStore<TTenant>), implementationType);

            return builder;
        }

        /// <summary>
        /// Registra un servicio personalizado de mapeo del slug del inquilino al identificador del inquilino
        /// </summary>
        /// <typeparam name="T">Tipo de la implementación de ITenantMapper</typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder<TTenant> AddCustomTenantMapper<TTenant>(this IMultiTenantKitBuilder<TTenant> builder, Type implementationType)
            where TTenant : ITenant
        {
            builder.Services.AddTransient(typeof(ITenantMapperService), implementationType);

            return builder;
        }

        #endregion

    }
}
