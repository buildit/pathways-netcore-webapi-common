namespace pathways_common.Authentication.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    public static class IdentityExtensions
    {
        public static string GetEmail(this IEnumerable<Claim> claims)
        {
            return (claims.FirstOrDefault(c => c.Type == IdentityConstants.Email) ?? claims.FirstOrDefault(c => c.Type == IdentityConstants.PreferredUsername))?.Value;
        }

        public static string GetName(this IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(c => c.Type == IdentityConstants.Name)?.Value;
        }

        public static class IdentityConstants
        {
            public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
            public const string Name = "name";
            public const string DefaultAvatar = "/images/cover/profile-placeholder.png";
            public const string PreferredUsername = "preferred_username";
        }
    }
}