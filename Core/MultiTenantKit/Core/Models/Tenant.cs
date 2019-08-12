using MultiTenantKit.Core.Attributes;

namespace MultiTenantKit.Core.Models
{
    /// <summary>
    /// Class that represents a barebones Tenant's Entity
    /// </summary>
    public class Tenant : ITenant
    {
        [TenantId]
        public string TenantId { get; set; }

        public string TenantName { get; set; }
    }
}
