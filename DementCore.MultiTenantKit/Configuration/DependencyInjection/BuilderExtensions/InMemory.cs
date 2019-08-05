using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Stores;
using DementCore.MultiTenantKit.Core.Stores.Default;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions
{
    public static class InMemory
    {
        public static IMultiTenantKitBuilder AddInMemoryTenantsStore<TTenant>(this IMultiTenantKitBuilder builder, List<TTenant> tenants)
         where TTenant : ITenant
        {
            builder.Services.AddScoped<ITenantStore<TTenant>, InMemoryTenantStore<TTenant>>(x =>
            {
                return new InMemoryTenantStore<TTenant>(tenants);
            });

            return builder;
        }

        public static IMultiTenantKitBuilder AddInMemoryTenantsStore<TTenant>(this IMultiTenantKitBuilder builder, IConfigurationSection configurationSection)
            where TTenant : ITenant
        {
            List<TTenant> tenants = new List<TTenant>();

            configurationSection.Bind(tenants);

            return builder.AddInMemoryTenantsStore(tenants);
        }


        public static IMultiTenantKitBuilder AddInMemoryTenantMappingsStore<TTenantMapping>(this IMultiTenantKitBuilder builder, List<TTenantMapping> tenantMappings)
            where TTenantMapping : ITenantMapping
        {
            builder.Services.AddScoped<ITenantMappingStore<TTenantMapping>, InMemoryTenantMappingStore<TTenantMapping>>(x =>
            {
                return new InMemoryTenantMappingStore<TTenantMapping>(tenantMappings);
            });

            return builder;
        }

        public static IMultiTenantKitBuilder AddInMemoryTenantMappingsStore<TTenantMapping>(this IMultiTenantKitBuilder builder, IConfigurationSection configurationSection)
             where TTenantMapping : ITenantMapping
        {
            List<TTenantMapping> tenantMappings = new List<TTenantMapping>();

            configurationSection.Bind(tenantMappings);

            return builder.AddInMemoryTenantMappingsStore(tenantMappings);
        }
    }
}
