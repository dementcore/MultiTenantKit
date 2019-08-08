using DementCore.MultiTenantKit.Configuration.Options;
using DementCore.MultiTenantKit.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Services
{
    internal class TenantClaimResolverService : ITenantResolverService
    {
        private ClaimResolverOptions Options { get; }

        public TenantClaimResolverService(IOptionsMonitor<ClaimResolverOptions> options)
        {
            Options = options.CurrentValue;
        }

        public Task<TenantResolveResult> ResolveTenantAsync(HttpContext httpContext)
        {
            string tenantInfo = "";

            try
            {
                ClaimsIdentity identity = httpContext.User.Identity as ClaimsIdentity;

                if (identity == null)
                {
                    return Task.FromResult(TenantResolveResult.NotApply);
                }

                if (Options.OnlyAuthenticated && !httpContext.User.Identity.IsAuthenticated)
                {
                    //if the user is not authenticated and the system is configured to only resolve in authenticated user 
                    //the resolution does not apply
                    return Task.FromResult(TenantResolveResult.NotApply);
                }

                //the identity of the user is not claims identity so this system does not apply by default
                if (identity == null)
                {
                    return Task.FromResult(TenantResolveResult.NotApply);
                }

                if (identity.HasClaim(c => c.Type == Options.ClaimName))
                {
                    tenantInfo = identity.FindFirst(Options.ClaimName).Value;

                    //the identity has the claim but the value is empty
                    if (string.IsNullOrWhiteSpace(tenantInfo))
                    {
                        return Task.FromResult(TenantResolveResult.NotFound);
                    }

                    //the identity has the claim and contains value
                    return Task.FromResult(new TenantResolveResult(tenantInfo, Options.ResolutionType));
                }
                else
                {
                    //the identity not have the claim
                    return Task.FromResult(TenantResolveResult.NotApply);
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new TenantResolveResult(ex));
            }

        }
    }
}
