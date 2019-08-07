using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMultitenantWebApplication.Models
{
    public class IndexModel
    {
        public string TenantName { get; set; }

        public string TenantLogo { get; set; }
    }
}
