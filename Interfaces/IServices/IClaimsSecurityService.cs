using System;
using System.Security.Claims;

namespace IST.Interfaces.IServices
{
    /// <summary>
    /// Service that adds security claims to the identity
    /// </summary>
    public interface IClaimsSecurityService
    {
        /// <summary>
        /// Adds user claims to Identity
        /// </summary>
        void AddClaimsToIdentity(string defaultRole, string userName, string userId, TimeSpan userTimeZoneOffset, ClaimsIdentity identity);

    }
}
