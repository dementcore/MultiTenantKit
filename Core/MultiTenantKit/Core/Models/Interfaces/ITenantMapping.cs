using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantKit.Core.Models
{
    /// <summary>
    /// Interface that represents the relation between tenant names and it's tenant id
    /// </summary>
    public interface ITenantMapping
    {
        List<string> Names { get; set; }

        string TenantId { get; set; }
    }
}
