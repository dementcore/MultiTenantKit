using DementCore.MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Stores.Default
{
    public class DefaultTenantStore<TTenant> : ITenantStore<TTenant> where TTenant : ITenant
    {
        public Task<TTenant> GetTenantInfo(string tenant)
        {
            throw new NotImplementedException();
        }
    }
}
