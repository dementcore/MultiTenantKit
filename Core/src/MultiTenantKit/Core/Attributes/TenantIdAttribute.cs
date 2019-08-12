using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantKit.Core.Attributes
{
    /// <summary>
    /// Attribute used to mark the Tenant's Id Property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TenantIdAttribute : Attribute
    {
    }
}
