using DementCore.MultiTenantKit.Core;
using DementCore.MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Services
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
