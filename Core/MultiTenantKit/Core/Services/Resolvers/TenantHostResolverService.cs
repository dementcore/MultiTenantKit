﻿using MultiTenantKit.Configuration.Options;
using MultiTenantKit.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiTenantKit.Core.Services
{
    internal class TenantHostResolverService : ITenantResolverService
    {
        private HostResolverOptions Options { get; }

        public TenantHostResolverService(IOptionsMonitor<HostResolverOptions> options)
        {
            Options = options.CurrentValue;

            if (Options.ExcludedDomains == null)
            {
                Options.ExcludedDomains = new List<string>();
            }
        }

        public Task<TenantResolveResult> ResolveTenantAsync(HttpContext httpContext)
        {
            string tenantInfo = "";

            if (Options.ExcludedDomains.Contains(httpContext.Request.Host.Host))
            {
                //if the request domain is in the exclusion list, the resolution does not apply.
                return Task.FromResult(TenantResolveResult.NotApply);
            }

            object[] parts;

            if (!httpContext.Request.Host.Host.Unformat(Options.DomainTemplate, out parts))
            {
                //if the request domain not match the resolution does not apply
                return Task.FromResult(TenantResolveResult.NotApply);
            }

            if (parts.Length <= 0)
            {
                //if the request domain matches against template but the extracted parts are 0 or less than 0 
                //we have not found the tenant
                return Task.FromResult(TenantResolveResult.NotFound);
            }
            
            tenantInfo = string.Concat(parts);

            return Task.FromResult(new TenantResolveResult(tenantInfo, Options.ResolutionType));
        }
    }
}
