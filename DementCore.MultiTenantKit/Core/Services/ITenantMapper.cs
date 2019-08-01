﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Services
{
    /// <summary>
    /// Interface de servicio para realizar el mapeo del slug del inquilino al identificador del inquilino
    /// </summary>
    public interface ITenantMapper
    {
        Task<string> MapTenantFromSlug(string slug);
    }
}
