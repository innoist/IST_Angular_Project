using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;

namespace IST.Commons
{
    /// <summary>
    /// Helper for and extension 
    /// </summary>
    public static class ClaimHelper
    {
        #region Public

        /// <summary>
        /// Serialize the value
        /// </summary>
        public static string Serialize<T>(T value)
            where T : class
        {
            return SerializeHelper.Serialize(value);
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        public static T Deserialize<T>(this Claim claim)
            where T : class
        {
            return SerializeHelper.Deserialize<T>(claim.Value);
        }
        /// <summary>
        /// Deserialize
        /// </summary>
        public static T Deserialize<T>(string claimValue)
            where T : class
        {
            return SerializeHelper.Deserialize<T>(claimValue);
        }

        /// <summary>
        /// Add single claim to identity
        /// </summary>
        public static void AddClaim(Claim claim, ClaimsIdentity identity)
        {
           identity.AddClaim(claim);
        }


        /// <summary>
        /// Add single claim to identity
        /// </summary>
        public static void AddListOfClaims(IEnumerable<Claim> claims, ClaimsIdentity identity)
        {
            identity.AddClaims(claims);
        }
       /// <summary>
       /// Set principal
       /// </summary>
        public static void SetCurrentPrincipal(ClaimsIdentity innerIdentity)
        {
            HttpContext.Current.User = new ClaimsPrincipal(innerIdentity);
            Thread.CurrentPrincipal = HttpContext.Current.User;  
        }

        /// <summary>
        /// Get claims matching the claim and value type
        /// </summary>

        public static IList<T> GetDeserializedClaims<T>(string claimType)
            where T : class
        {
            if (string.IsNullOrEmpty(claimType))
            {
                throw new ArgumentException("claimType");
            }
            var claims = GetClaimsIdentity().Claims.Where(c => c.Type == claimType);
            return claims.Select(claim => claim.Deserialize<T>()).ToList();
        }

        public static Claim GetClaimToString(string claimType)
        {
            return GetClaimsIdentity().Claims.FirstOrDefault(claim => claim.Type == claimType);
        }

        /// <summary>
        /// Gives Identity
        /// </summary>
        private static ClaimsIdentity GetClaimsIdentity()
        {

            var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (claimsPrincipal == null)
            {
                throw new InvalidOperationException("claimsPrincipal");
            }

            var identity = claimsPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
            {
                throw new InvalidOperationException("identity");
            }
            return identity;
        }
        #endregion
    }
}
