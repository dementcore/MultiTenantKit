using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core
{
    public class TenantResolveResult
    {
        public bool Success { get; set; }

        public string Value { get; set; }

        public string ErrorMessage { get; set; }

        public ResolvedType ResolvedType { get; set; }
    }
}
