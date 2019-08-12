using System;
using System.Collections.Generic;
using System.Text;
using MultiTenantKit.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MultiTenantKit.Configuration.DependencyInjection
{
    public class MultiTenantKitBuilder : IMultiTenantKitBuilder
    {
        public MultiTenantKitBuilder(IServiceCollection services, Type tenantType, Type tenantMappingType)
        {
            Services = services;
            TenantType = tenantType;
            TenantMappingType = tenantMappingType;
        }

        public Type TenantType { get; }

        public Type TenantMappingType { get; }

        public IServiceCollection Services { get; }

    }
}
