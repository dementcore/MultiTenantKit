using MultiTenantKit.Core.Models;
using MultiTenantKit.Core.Stores;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions
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
    }
}
