using MultiTenantKit.Core.Enumerations;
using MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantKit.Core
{
    public class TenantMapResult<TTenantMapping> where TTenantMapping : ITenantMapping
    {
        #region Public Constructors

        public TenantMapResult(TTenantMapping value)
        {
            if (value.Equals(default(TTenantMapping)))
            {
                MappingResult = MappingResult.NotFound;
            }
            else
            {
                MappingResult = MappingResult.Success;
            }

            Value = value;
        }

        #endregion

        #region Private Constructors

        private TenantMapResult(MappingResult mappingResult)
        {
            MappingResult = mappingResult;
            Value = default;
        }

        #endregion

        #region Public Static Properties

        public static TenantMapResult<TTenantMapping> NotFound { get; } = new TenantMapResult<TTenantMapping>(MappingResult.NotFound);

        #endregion

        #region Public Properties

        public TTenantMapping Value { get; }

        public MappingResult MappingResult { get; }

        #endregion
    }
}
