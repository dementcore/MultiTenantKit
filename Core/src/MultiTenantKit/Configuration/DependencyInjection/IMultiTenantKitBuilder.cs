using MultiTenantKit.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantKit.Configuration.DependencyInjection
{
    public interface IMultiTenantKitBuilder
    {
        /// <summary>
        /// Service Collection
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// Type representing Tenant's Entity
        /// </summary>
        Type TenantType { get; }

        /// <summary>
        /// Type representing Tenant Mappings Entity
        /// </summary>
        Type TenantMappingType { get; }

    }
}

