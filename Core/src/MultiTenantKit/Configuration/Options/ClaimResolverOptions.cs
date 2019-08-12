using MultiTenantKit.Core.Enumerations;

namespace MultiTenantKit.Configuration.Options
{
    public class ClaimResolverOptions
    {
        /// <summary>
        /// Determines the claim name used to search and extract the tenant's. By default is TenantId
        /// </summary>
        public string ClaimName { get; set; } = "TenantId";

        /// <summary>
        /// Determines if the claim value contains TenantName or Tenant Id. By default is ResolutionType.TenantId.
        /// </summary>
        public ResolutionType ResolutionType { get; set; } = ResolutionType.TenantId;

        /// <summary>
        /// Determines if the resolver should do the resolution if the user is authenticated or do the resolution even if the user is not authenticated. By default is true.
        /// </summary>
        public bool OnlyAuthenticated { get; set; } = true;
    }
}
