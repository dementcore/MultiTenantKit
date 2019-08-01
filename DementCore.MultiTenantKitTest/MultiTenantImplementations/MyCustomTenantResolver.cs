using DementCore.MultiTenantKit.Core.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyMultitenantWebApplication.MultiTenantImplementations
{
    public class MyCustomTenantResolver : ITenantResolverService
    {
        public Task<string> ResolveTenantSlugAsync(HttpContext httpRequest)
        {
            return Task.FromResult("carlos");
        }
    }
}
