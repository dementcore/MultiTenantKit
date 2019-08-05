using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Stores;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DementCore.MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions
{
    public static class Store
    {

        public static IMultiTenantKitBuilder AddCustomTenantStore<TTenantStore>(this IMultiTenantKitBuilder builder)
            where TTenantStore : class
        {
            Type ICustomTenantStoreType = typeof(ITenantStore<>).MakeGenericType(builder.TenantType);
            Type SCustomTenantStoreType = typeof(TTenantStore);

            if (!ICustomTenantStoreType.GetTypeInfo().IsAssignableFrom(SCustomTenantStoreType.GetTypeInfo()))
            {
                throw new InvalidOperationException($"You must use a type that implements {ICustomTenantStoreType.ToString()}!");
            }

            builder.Services.AddTransient(ICustomTenantStoreType,SCustomTenantStoreType);

            return builder;
        }

        public static IMultiTenantKitBuilder AddCustomTenantMappingStore<TTenantMappingStore>(this IMultiTenantKitBuilder builder)
            where TTenantMappingStore : class
        {

            Type ICustomTenantMappingStoreType = typeof(ITenantMappingStore<>).MakeGenericType(builder.TenantMappingType);
            Type SCustomTenantMappingStoreType = typeof(TTenantMappingStore);

            if (!ICustomTenantMappingStoreType.GetTypeInfo().IsAssignableFrom(SCustomTenantMappingStoreType.GetTypeInfo()))
            {
                throw new InvalidOperationException($"You must use a type that implements {ICustomTenantMappingStoreType.ToString()}!");
            }

            builder.Services.AddTransient(ICustomTenantMappingStoreType, SCustomTenantMappingStoreType);

            return builder;
        }
    }
}
