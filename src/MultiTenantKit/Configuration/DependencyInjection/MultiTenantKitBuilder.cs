using System;
using System.Collections.Generic;
using System.Text;
using MultiTenantKit.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MultiTenantKit.Configuration.DependencyInjection
{
    public class MultiTenantKitBuilder : IMultiTenantKitBuilder
    {
        public MultiTenantKitBuilder(IServiceCollection services, Type tenantType)
        {
            Services = services;
            TenantType = tenantType;
        }

        public Type TenantType { get; }

        public IServiceCollection Services { get; }

    }
}
