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
        public static IMultiTenantKitBuilder AddInMemoryTenants<TTenant>(this IMultiTenantKitBuilder builder, List<TTenant> tenants)
         where TTenant : ITenant
        {
            builder.Services.AddScoped<ITenantStore<TTenant>, InMemoryTenantStore<TTenant>>(x =>
            {
                return new InMemoryTenantStore<TTenant>(tenants);
            });

            return builder;
        }

        public static IMultiTenantKitBuilder AddInMemoryTenants<TTenant>(this IMultiTenantKitBuilder builder, IConfigurationSection configurationSection)
            where TTenant : ITenant
        {
            List<TTenant> tenants = new List<TTenant>();

            configurationSection.Bind(tenants);

            return builder.AddInMemoryTenants(tenants);
        }

        public static IMultiTenantKitBuilder AddInMemoryTenantSlugs<TTenantSlugs>(this IMultiTenantKitBuilder builder, List<TTenantSlugs> tenantSlugs)
            where TTenantSlugs : ITenantSlugs
        {
            builder.Services.AddScoped<ITenantSlugsStore<TTenantSlugs>, InMemoryTenantSlugsStore<TTenantSlugs>>(x =>
            {
                return new InMemoryTenantSlugsStore<TTenantSlugs>(tenantSlugs);
            });

            return builder;
        }

        public static IMultiTenantKitBuilder AddInMemoryTenantSlugs<TTenantSlugs>(this IMultiTenantKitBuilder builder, IConfigurationSection configurationSection)
             where TTenantSlugs : ITenantSlugs
        {
            List<TTenantSlugs> tenantsSlugs = new List<TTenantSlugs>();

            configurationSection.Bind(tenantsSlugs);

            return builder.AddInMemoryTenantSlugs(tenantsSlugs);
        }
    }
}
