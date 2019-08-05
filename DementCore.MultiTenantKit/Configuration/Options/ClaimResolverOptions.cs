using DementCore.MultiTenantKit.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Configuration.Options
{
    public class ClaimResolverOptions
    {
        /// <summary>
        /// Determines the claim name used to search and extract the tenant's
        /// </summary>
        public string ClaimName { get; set; } = "TenantId";

        /// <summary>
        /// Determines if the claim value contains TenantName or Tenant Id. By default is TenantId.
        /// </summary>
        public ResolutionType ResolutionType { get; set; } = ResolutionType.TenantId;

        /// <summary>
        /// Determines if the resolver should do the resolution if the user is authenticated or do the resolution even if the user is not authenticated.
        /// </summary>
        public bool OnlyAuthenticated { get; set; } = true;
    }
}
