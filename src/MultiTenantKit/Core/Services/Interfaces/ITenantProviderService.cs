using MultiTenantKit.Core;
using MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantKit.Core.Services
{
    /// <summary>
    /// Interface de servicio para proveer de la instancia del inquilino
    /// </summary>
    /// <typeparam name="TTenant"></typeparam>
    public interface ITenantProvider<out TTenant> where TTenant : ITenant
    {
        TTenant GetTenant();
    }
}
