using MultiTenantKit.Core.Attributes;
using MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;

namespace MultiTenantKit.MultiTenantRouteSample.MultiTenantImplementations
{
    public class CustomTenant : ITenant
    {
        [TenantId]
        public string Id { get; set; }

        public string Name { get; set; }

        public string CSSTheme { get; set; }

    }
}
