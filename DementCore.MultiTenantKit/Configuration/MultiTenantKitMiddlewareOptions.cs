namespace Microsoft.AspNetCore.Builder
{
    public class MultiTenantKitMiddlewareOptions
    {
        private bool _includeinuserclaims = false;
        private bool _includeinhttpcontext = true;
        private bool _includeonlyifauthenticated = true;
        private string tenantClaimsPrefix = "Tenant_";

        /// <summary>
        /// Indica si se debe insertar los datos del tenant en los claims de identidad del usuario
        /// </summary>
        public bool IncludeInUserClaims { get => _includeinuserclaims; set => _includeinuserclaims = value; }

        /// <summary>
        /// Indica si el usuario debe estar autenticado para insertar los datos del tenant en sus claims de identidad
        /// Solo se tiene en cuenta si <see cref="IncludeInUserClaims"/> es true.
        /// Por defecto: true
        /// </summary>
        public bool IncludeOnlyIfAuthenticated { get => _includeonlyifauthenticated; set => _includeonlyifauthenticated = value; }

        /// <summary>
        /// Indica el prefijo que se usará al añadir los claims en la identidad del usuario. 
        /// Solo se tiene en cuenta si <see cref="IncludeInUserClaims"/> es true. 
        /// Por defecto: "Tenant_"
        /// </summary>
        public string ClaimsPrefix { get => tenantClaimsPrefix; set => tenantClaimsPrefix = value; }

        /// <summary>
        /// Indica si la entidad que representa al inquilino será incluida en el HttpContext
        /// </summary>
        public bool IncludeInHttpContext { get => _includeinhttpcontext; set => _includeinhttpcontext = value; }
    }
}
