using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MultiTenantKit.Core;
using MultiTenantKit.Core.Context;
using MultiTenantKit.Core.Enumerations;
using MultiTenantKit.Core.Models;
using MultiTenantKit.Core.Services;
using MultiTenantKit.Hosting.Events;
using System;
using System.Threading.Tasks;

namespace MultiTenantKit.TestBranch
{
    /// <summary>
    /// This middleware needs the three services: Resolver, Mapper,Info
    /// </summary>
    /// <typeparam name="TTenant"></typeparam>
    public class MultiTenantPipelineMiddleware<TTenant>
        where TTenant : ITenant
    {

        private RequestDelegate _next { get; set; }

        private HttpContext HttpContext { get; set; }
        private IApplicationBuilder _rootBuilder { get; set; }

        private Action<TenantContext<TTenant>, IApplicationBuilder> _pipeline { get; }

        public MultiTenantPipelineMiddleware(RequestDelegate next, IApplicationBuilder rootBuilder, Action<TenantContext<TTenant>, IApplicationBuilder> pipeline)
        {
            _next = next;
            _pipeline = pipeline;
            _rootBuilder = rootBuilder;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            HttpContext = httpContext;


            var tCtx = HttpContext.GetTenantContext<TTenant>();

            if (tCtx != null)
            {
                var branch = _rootBuilder.New();
                _pipeline(tCtx, branch);

                _next = branch.Build();
            }

            await _next(httpContext);
        }
    }
}
