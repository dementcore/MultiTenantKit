using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;

namespace DementCore.MultiTenantKit.Core.Services.Default
{
    public class DefaultTenantResolver : ITenantResolver
    {

        //https://blog.markvincze.com/matching-route-templates-manually-in-asp-net-core/


        //https://gunnarpeipman.com/net/ef-core-global-query-filters/


        public RouteValueDictionary Match(string routeTemplate, string requestPath)
        {
            RouteTemplate template = TemplateParser.Parse(routeTemplate);

            TemplateMatcher matcher = new TemplateMatcher(template, GetDefaults(template));

            RouteValueDictionary values = null;

            matcher.TryMatch(requestPath, values);

            return values;
        }

        // This method extracts the default argument values from the template.
        private RouteValueDictionary GetDefaults(RouteTemplate parsedTemplate)
        {
            var result = new RouteValueDictionary();

            foreach (var parameter in parsedTemplate.Parameters)
            {
                if (parameter.DefaultValue != null)
                {
                    result.Add(parameter.Name, parameter.DefaultValue);
                }
            }

            return result;
        }

        public Task<string> ResolveTenantAsync(HttpContext httpRequest)
        {
            throw new NotImplementedException();
        }
    }
}
