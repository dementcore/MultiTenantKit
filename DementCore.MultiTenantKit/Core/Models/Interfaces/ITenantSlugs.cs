using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Models
{
    public interface ITenantSlugs
    {
        List<string> Slugs { get; set; }

        string TenantId { get; set; }
    }
}
