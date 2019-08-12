using MultiTenantKit.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiTenantKit.Core.Stores
{
    /// <summary>
    /// Interface de servicio para realizar la extracción de información del inquilino y construcción de la entidad que lo representa.
    /// </summary>
    /// <typeparam name="TTenant"></typeparam>
    public interface ITenantStore<out TTenant> where TTenant :  ITenant
    {
        TTenant GetTenantByTenantId(string tenantId);
    }
}
