using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Configuration.Options
{
    public class HostResolverOptions
    {
        /// <summary>
        /// Determines the domain template used to search and extract the tenant's Slug
        /// </summary>
        public string DomainTemplate { get; set; }
    }
}
