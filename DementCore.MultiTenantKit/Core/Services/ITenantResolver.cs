using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Services
{
    /// <summary>
    /// Interface de servicio para realizar la resolución del slug del tenant a partir de la ruta
    /// </summary>
    public interface ITenantResolver
    {
        Task<string> ResolveTenantAsync(HttpContext httpRequest);
    }
}
