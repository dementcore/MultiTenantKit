using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Configuration.Options
{
    public class PathResolverOptions
    {
        /// <summary>
        /// Determines the name of the segment to search in the route path to extract the tenant slug
        /// </summary>
        public string RouteSegmentName { get; set; }
    }
}
