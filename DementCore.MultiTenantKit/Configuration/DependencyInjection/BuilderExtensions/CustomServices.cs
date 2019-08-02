using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DementCore.MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions
{
    public static class MultiTenantKitBuilderExtensions
    {
        public static IMultiTenantKitBuilder AddCustomTenantResolverService<TResolverService>(this IMultiTenantKitBuilder builder)
            where TResolverService : class, ITenantResolverService
        {
            builder.Services.AddTransient<ITenantResolverService, TResolverService>();

            return builder;
        }

        public static IMultiTenantKitBuilder AddCustomTenantInfoService<TTenant, TInfoService>(this IMultiTenantKitBuilder builder)
            where TTenant : ITenant
            where TInfoService : class, ITenantInfoService<TTenant>
        {
            builder.Services.AddTransient<ITenantInfoService<TTenant>, TInfoService>();

            return builder;
        }

        public static IMultiTenantKitBuilder AddCustomTenantMapperService<TMapperService>(this IMultiTenantKitBuilder builder)
            where TMapperService : class, ITenantMapperService
        {
            builder.Services.AddTransient<ITenantMapperService, TMapperService>();

            return builder;
        }
    }
}
