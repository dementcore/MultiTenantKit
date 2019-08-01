using DementCore.MultiTenantKit.Core.Attributes;
using DementCore.MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;

namespace MyMultitenantWebApplication.MultiTenantImplementations
{
    public class MyTenant : ITenant
    {

        [TenantId]
        public string Id { get; set; }

        public string Name { get; set; }

        public string CSSTheme { get; set; }

    }
}
