using MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantKit.Core.Services
{
    public interface ITenantInfoService<TTenant> where TTenant : ITenant
    {
        Task<TenantInfoResult<TTenant>> GetTenantInfoAsync(string tenantId);
    }
}
