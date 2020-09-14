using MultiTenantKit.Core.Enumerations;
using MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantKit.Core
{
    public sealed class TenantInfoResult<TTenant> where TTenant : ITenant
    {
        #region Public Constructors

        public TenantInfoResult(TTenant value)
        {
            if (value == null)
            {
                InfoResult = InfoResult.NotFound;
            }
            else
            {
                InfoResult = InfoResult.Success;
            }

            Value = value;
        }

        #endregion

        #region Private Constructors

        private TenantInfoResult(InfoResult mappingResult)
        {
            InfoResult = mappingResult;
            Value = default;
        }

        #endregion

        #region Public Static Properties

        public static TenantInfoResult<TTenant> NotFound { get; } = new TenantInfoResult<TTenant>(InfoResult.NotFound);

        #endregion

        #region Public Properties

        public TTenant Value { get; }

        public InfoResult InfoResult { get; }

        #endregion
    }
}
