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

        public static IMultiTenantKitBuilder AddCustomTenantSlugsStore<TTenantSlug, TTenantSlugStore>(this IMultiTenantKitBuilder builder)
            where TTenantSlug : ITenantSlugs
            where TTenantSlugStore : class, ITenantSlugsStore<TTenantSlug>
        {
            builder.Services.AddTransient<ITenantSlugsStore<TTenantSlug>, TTenantSlugStore>();

            return builder;
        }
    }
}
