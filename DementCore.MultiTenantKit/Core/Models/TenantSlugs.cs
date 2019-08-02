using System.Collections.Generic;

namespace DementCore.MultiTenantKit.Core.Models
{
    public class TenantSlugs : ITenantSlugs
    {
        public List<string> Slugs { get; set; }

        public string TenantId { get; set; }
    }
}
