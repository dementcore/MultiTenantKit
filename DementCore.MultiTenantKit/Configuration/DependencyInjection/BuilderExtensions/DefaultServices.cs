using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions
{
    public static class DefaultServices
    {
        public static IMultiTenantKitBuilder AddDefaultTenantResolverService(this IMultiTenantKitBuilder builder)
        {
            builder.Services.AddTransient<ITenantResolverService, TenantResolverService>();

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultTenantMapperService<TTenantSlugs>(this IMultiTenantKitBuilder builder)
            where TTenantSlugs : ITenantSlugs
        {
            builder.Services.AddTransient<ITenantMapperService, TenantMapperService<TTenantSlugs>>();

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultTenantInfoService<TTenant>(this IMultiTenantKitBuilder builder)
            where TTenant : ITenant
        {
            builder.Services.AddTransient<ITenantInfoService<TTenant>, TenantInfoService<TTenant>>();

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultTenantProviderService<TTenant>(this IMultiTenantKitBuilder builder)
            where TTenant : ITenant
        {
            builder.Services.AddScoped<ITenantProvider<TTenant>, TenantProviderService<TTenant>>();
            
            return builder;
        }

    }
}
