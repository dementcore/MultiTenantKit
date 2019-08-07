using System.Collections.Generic;

namespace DementCore.MultiTenantKit.Core.Models
{
    /// <summary>
    /// Class that represents the relation between tenant names and it's tenant id
    /// </summary>
    public class TenantMapping : ITenantMapping
    {
        public List<string> Names { get; set; }

        public string TenantId { get; set; }
    }
}
