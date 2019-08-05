using DementCore.MultiTenantKit.Configuration.Options;
using DementCore.MultiTenantKit.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Services
{
    internal class TenantHostResolverService : ITenantResolverService
    {
        private HostResolverOptions Options { get; }

        public TenantHostResolverService(IOptionsMonitor<HostResolverOptions> options)
        {
            Options = options.CurrentValue;
        }

        public Task<TenantResolveResult> ResolveTenantAsync(HttpContext httpContext)
        {
            string tenantSubdomain = "";

            try
            {
                object[] partes = httpContext.Request.Host.Host.Unformat(Options.DomainTemplate);

                if (partes.Length <= 0)
                {
                    return Task.FromResult(TenantResolveResult.NotFound);
                }
                else
                {
                    tenantSubdomain = string.Join('-', partes);

                    return Task.FromResult(new TenantResolveResult(tenantSubdomain, ResolutionType.TenantName));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new TenantResolveResult(ex));
            }

        }
    }
}
