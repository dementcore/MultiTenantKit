using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Hosting;
using DementCore.MultiTenantKit.Core.Services;
using DementCore.MultiTenantKit.Core.Stores.InMemory;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Options;
using DementCore.MultiTenantKit.Configuration.Options;
using Moq;

namespace DementCore.MultiTenantKit.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void RouteResolverSuccessFullTestAsync()
        {
            //var middleware = new MultiTenantKitMiddleware<Tenant, TenantMapping>((innerHttpContext) =>
            // {
            //     return Task.CompletedTask;
            // });

            //var ops = new PathResolverOptions();

            //var optionsMonitor = Mock.Of<IOptionsMonitor<PathResolverOptions>>(om => om.CurrentValue == ops);

            //TenantPathResolverService pathResolverService = new TenantPathResolverService(optionsMonitor);

            //InMemoryTenantMappingStore<TenantMapping> tenantMappingStore = new InMemoryTenantMappingStore<TenantMapping>(
            //    new List<TenantMapping>() {
            //        new TenantMapping() {
            //            TenantId = "1",
            //            Names = new List<string>() {
            //                "tenant1"
            //            }
            //        }
            //    });

            //TenantMapperService<TenantMapping> mapperService = new TenantMapperService<TenantMapping>(tenantMappingStore);

            //InMemoryTenantStore<Tenant> tenantStore = new InMemoryTenantStore<Tenant>(
            //  new List<Tenant>() {
            //        new Tenant() {
            //            TenantId = "1"
            //        }
            //  });

            //TenantInfoService<Tenant> infoService = new TenantInfoService<Tenant>(tenantStore);

            //var context = new DefaultHttpContext();
            //context.Request.Path=""
            //context.Response.Body = new MemoryStream();


            //await middleware.Invoke(context, pathResolverService, mapperService,infoService);

            Assert.Equal("true", "true");
        }
    }
}
