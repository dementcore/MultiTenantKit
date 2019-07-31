using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Services
{
    /// <summary>
    /// Interface de servicio para realizar el mapeo del slug del inquilino al identificador del inquilino
    /// </summary>
    public interface ITenantMapper
    {
        string MapTenantFromSlug(string slug);
    }
}
