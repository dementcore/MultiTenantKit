using MultiTenantKit.Core.Models;
using MultiTenantKit.Core.Stores;
using MultiTenantKit.Core.Stores.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions
{
    public static class InMemory
    {
        #region TenantsStore

        public static IMultiTenantKitBuilder AddInMemoryTenantsStore(this IMultiTenantKitBuilder builder, IEnumerable<ITenant> tenants)
        {
            Type ITenantStoreType = typeof(ITenantStore<>).MakeGenericType(builder.TenantType);
            Type STenantStoreType = typeof(InMemoryTenantStore<>).MakeGenericType(builder.TenantType);

            builder.Services.AddScoped(ITenantStoreType, s =>
            {
                return Activator.CreateInstance(STenantStoreType, tenants);
            });

            return builder;
        }

        public static IMultiTenantKitBuilder AddInMemoryTenantsStore(this IMultiTenantKitBuilder builder, IConfigurationSection configurationSection)
        {
            Type tenantListType = typeof(List<>).MakeGenericType(builder.TenantType);

            object tenants = Activator.CreateInstance(tenantListType);

            configurationSection.Bind(tenants);

            return builder.AddInMemoryTenantsStore((IEnumerable<ITenant>)tenants);
        }

        #endregion

    }
}
