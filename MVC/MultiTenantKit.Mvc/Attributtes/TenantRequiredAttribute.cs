using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MultiTenantKit.Configuration.Options;
using MultiTenantKit.Core.Models;
using System.Threading.Tasks;

namespace MultiTenantKit.Mvc.Attributtes
{
    public class TenantRequiredAttribute : ActionFilterAttribute
    {
        public int StatusCode = 404;


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Core.Context.TenantContext<ITenant> tCtx = context.HttpContext.GetTenantContext();

            if (tCtx.Tenant == null)
            {
                context.Result = new StatusCodeResult(StatusCode);
            }
            else
            {
                base.OnActionExecuting(context);
            }

        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Core.Context.TenantContext<ITenant> tCtx = context.HttpContext.GetTenantContext();

            if (tCtx.Tenant == null)
            {
                context.Result = new StatusCodeResult(StatusCode);

                return Task.CompletedTask;
            }
            else
            {
                return base.OnActionExecutionAsync(context, next);
            }
        }
    }
}
