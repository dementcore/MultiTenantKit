using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Stores;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DementCore.MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions
{
    public static class Store
    {
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
    }
}
