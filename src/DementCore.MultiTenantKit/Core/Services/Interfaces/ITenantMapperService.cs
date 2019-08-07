using DementCore.MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Services
{
    /// <summary>
    /// Interface used to implement services to map between tenant's names and it's ids.
    /// </summary>
    public interface ITenantMapperService<TTenantMapping> where TTenantMapping : ITenantMapping
    {
        Task<TenantMapResult<TTenantMapping>> MapTenantAsync(string tenantName);
    }
}
