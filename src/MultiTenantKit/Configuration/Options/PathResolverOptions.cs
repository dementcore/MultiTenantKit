using MultiTenantKit.Core;
using MultiTenantKit.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantKit.Configuration.Options
{
    public class PathResolverOptions
    {
        /// <summary>
        /// Determines the name of the segment to search in the path and extract the tenant's name
        /// </summary>
        public string RouteSegmentName { get; set; } = "Tenant";

        /// <summary>
        /// Determines a route list where the resolution is not performed
        /// </summary>
        public List<string> ExcludedRouteTemplates { get; set; } = new List<string>();

        /// <summary>
        /// Determines if the route segment contains Tenant Name or Tenant Id. By default is TenantName.
        /// </summary>
        public ResolutionType ResolutionType { get; set; } = ResolutionType.TenantName;
    }
}
