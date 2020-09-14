using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantKit.MultiTenantRouteSample.Models
{
    public class IndexModel
    {
        public string TenantName { get; set; }

        public string TenantCssTheme { get; set; }

        public string TenantId { get; set; }
    }
}
