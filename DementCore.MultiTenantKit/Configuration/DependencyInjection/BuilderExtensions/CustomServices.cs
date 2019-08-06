using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

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

        public static IMultiTenantKitBuilder AddCustomTenantInfoService<TInfoService>(this IMultiTenantKitBuilder builder)
            where TInfoService : class
        {

            Type ITenantInfoServiceType = typeof(ITenantInfoService<>).MakeGenericType(builder.TenantType);
            Type STenantInfoServiceType = typeof(TInfoService);

            if (!ITenantInfoServiceType.GetTypeInfo().IsAssignableFrom(STenantInfoServiceType.GetTypeInfo()))
            {
                throw new InvalidOperationException($"You must use a type that implements {ITenantInfoServiceType.ToString()}!");
            }

            builder.Services.AddTransient(ITenantInfoServiceType, STenantInfoServiceType);

            return builder;
        }

        public static IMultiTenantKitBuilder AddCustomTenantMapperService<TMapperService>(this IMultiTenantKitBuilder builder)
            where TMapperService : class
        {
            Type ITenantMapperServiceType = typeof(ITenantMapperService<>).MakeGenericType(builder.TenantMappingType);
            Type STenantMapperServiceType = typeof(TMapperService);

            if (!ITenantMapperServiceType.GetTypeInfo().IsAssignableFrom(STenantMapperServiceType.GetTypeInfo()))
            {
                throw new InvalidOperationException($"You must use a type that implements {ITenantMapperServiceType.ToString()}!");
            }

            builder.Services.AddTransient(ITenantMapperServiceType, STenantMapperServiceType);

            return builder;
        }
    }
}
