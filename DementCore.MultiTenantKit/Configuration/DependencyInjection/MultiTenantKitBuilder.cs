using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace DementCore.MultiTenantKit.Configuration.DependencyInjection
{
    public class MultiTenantKitBuilder : IMultiTenantKitBuilder
    {
        public MultiTenantKitBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
