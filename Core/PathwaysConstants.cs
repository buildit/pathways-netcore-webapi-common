namespace pathways_common.Core
{
    public static class PathwaysConstants
    {
        public static class Claim
        {
            public const string ObjectId = "http://schemas.microsoft.com/identity/claims/objectidentifier";
            public const string TenantId = "http://schemas.microsoft.com/identity/claims/tenantid";
            public const string tid = "tid";
        }

        public static class Oidc
        {
            public const string AdditionalClaims = "claims";
            public const string ScopeOfflineAccess = "offline_access";
            public const string ScopeProfile = "profile";
            public const string ScopeOpenId = "openid";
        }

        public static class Graph
        {
            public const string Url = "https://graph.microsoft.com/beta";
            public const string ScopeUserRead = "User.Read";
            public const string BearerAuthorizationScheme = "Bearer";
        }
    }
}