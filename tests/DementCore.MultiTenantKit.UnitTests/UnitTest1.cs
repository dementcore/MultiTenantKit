using System;
using Xunit;

namespace DementCore.MultiTenantKit.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void RouteResolverTest()
        {
            var middleware = new CustomExceptionMiddleware((innerHttpContext) =>
            {
                throw new NotFoundCustomException("Test", "Test");
            });

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            //Act
            await middleware.Invoke(context);
        }
    }
}
