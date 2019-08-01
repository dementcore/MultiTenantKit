﻿using DementCore.MultiTenantKit.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Stores
{
    /// <summary>
    /// Interface de servicio para realizar la extracción de información del inquilino y construcción de la entidad que lo representa.
    /// </summary>
    /// <typeparam name="TTenant"></typeparam>
    public interface ITenantStore<TTenant> where TTenant :  ITenant
    {
        List<TTenant> GetTenants();

        TTenant GetTenantByTenantId(string tenantId);
    }
}
