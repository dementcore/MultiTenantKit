using DementCore.MultiTenantKit.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Configuration.DependencyInjection
{
    public interface IMultiTenantKitBuilder
    {
        /// <summary>
        /// Servicios del sistema
        /// </summary>
        IServiceCollection Services { get; }

    }
}

