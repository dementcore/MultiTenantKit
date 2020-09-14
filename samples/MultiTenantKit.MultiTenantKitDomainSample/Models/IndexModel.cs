using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantKit.MultiTenantDomainSample.Models
{
    public class IndexModel
    {
        public string TenantName { get; set; }

        public string TenantCssTheme { get; set; }

        public string TenantId { get; set; }
    }
}
