using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Services
{
    /// <summary>
    /// Interface used to implement services to resolve the tenant from the HttpContext
    /// </summary>
    public interface ITenantResolverService
    {
        Task<TenantResolveResult> ResolveTenantAsync(HttpContext httpContext);
    }
}
