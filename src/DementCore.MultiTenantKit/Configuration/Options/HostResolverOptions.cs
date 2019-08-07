using DementCore.MultiTenantKit.Core;
using DementCore.MultiTenantKit.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Configuration.Options
{
    public class HostResolverOptions
    {
        /// <summary>
        /// Determines the domain template used to search and extract the tenant's name
        /// </summary>
        public string DomainTemplate { get; set; } = "{0}.midomain.com";

        /// <summary>
        /// Determines a domain list where the resolution is not performed
        /// </summary>
        public List<string> ExcludedDomains { get; set; } = new List<string>();

        /// <summary>
        /// Determines if the domain contains Tenant Name or Tenant Id. By default is TenantName.
        /// </summary>
        public ResolutionType ResolutionType { get; set; } = ResolutionType.TenantName;
    }
}
