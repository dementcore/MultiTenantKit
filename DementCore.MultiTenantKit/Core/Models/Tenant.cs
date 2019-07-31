using DementCore.MultiTenantKit.Core.Attributes;

namespace DementCore.MultiTenantKit.Core.Models
{
    public class Tenant : ITenant
    {
        [TenantId]
        public string TenantId { get; set; }
    }
}
