using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Services
{
    /// <summary>
    /// Interface used to implement services to map between tenant's names and it's ids.
    /// </summary>
    public interface ITenantMapperService
    {
        Task<string> MapTenantAsync(string tenantName);
    }
}
