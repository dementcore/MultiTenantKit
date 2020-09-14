using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantKit.OptionsPerTenant
{
    //public class MultitenantKitOptionsFactory<TOptions> : IOptionsFactory<TOptions> where TOptions : class, new()
    //{
    //    public MultitenantKitOptionsFactory(Func<TOptions> configuration)
    //    {
    //        _configuration = configuration;
    //    }

    //    public Func<TOptions> _configuration { get; }

    //    public TOptions Create(string name)
    //    {
    //        return _configuration();//throw new NotImplementedException();
    //    }
    //}

    //class MultitenantKitOptionsSnapshot<TOptions> : IOptionsSnapshot<TOptions> where TOptions : class, new()
    //{
    //    public TOptions Value => throw new NotImplementedException();

    //    public TOptions Get(string name)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class MultitenantKitPostConfigureOptions<TTenant, TOptions> : IPostConfigureOptions<TOptions> where TOptions : class, new()
        where TTenant : Core.Models.ITenant
    {
        public MultitenantKitPostConfigureOptions(Action<TOptions, TTenant> configuration, Core.Services.ITenantProvider<TTenant> tenantProvider)
        {
            _configuration = configuration;
            TenantProvider = tenantProvider;
        }

        public Core.Services.ITenantProvider<TTenant> TenantProvider { get; }

        public Action<TOptions, TTenant> _configuration { get; }

        public void PostConfigure(string name, TOptions options)
        {
            _configuration(options,TenantProvider.GetTenant());
        }
    }
}
