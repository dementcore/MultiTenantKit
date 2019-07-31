using DementCore.MultiTenantKit.Core.Attributes;
using DementCore.MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;

namespace MyMultitenantWebApplication.MultiTenantImplementations
{
    public class MyTenant : ITenant
    {
        public MyTenant()
        {
            Properties = new Dictionary<object, object>();
        }

        [TenantId]
        public string Id { get; set; }

        public string Name { get; set; }

        public string CSSTheme { get; set; }

        public Dictionary<object, object> Properties { get; set; }

    }
}
