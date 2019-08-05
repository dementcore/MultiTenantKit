using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Stores;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DementCore.MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions
{
    public static class Store
    {

        public static IMultiTenantKitBuilder AddCustomTenantStore<TTenant, TTenantStore>(this IMultiTenantKitBuilder builder)
            where TTenant : ITenant
            where TTenantStore : class, ITenantStore<TTenant>
        {
            builder.Services.AddTransient<ITenantStore<TTenant>, TTenantStore>();

            return builder;
        }

        public static IMultiTenantKitBuilder AddCustomTenantMappingStore<TTenantMapping, TTenantMappingStore>(this IMultiTenantKitBuilder builder)
            where TTenantMapping : ITenantMapping
            where TTenantMappingStore : class, ITenantMappingStore<TTenantMapping>
        {
            builder.Services.AddTransient<ITenantMappingStore<TTenantMapping>, TTenantMappingStore>();

            return builder;
        }
    }
}
