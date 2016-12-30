using System;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;

namespace IST.Implementation.Services
{
    /// <summary>
    /// Web Api Authentication Service
    /// </summary>
    public sealed class WebApiAuthenticationService : IWebApiAuthenticationService
    {
        #region Private
        private readonly IWebApiUserRepository webApiUserRepository;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public WebApiAuthenticationService(IWebApiUserRepository webApiUserRepository)
        {
            if (webApiUserRepository == null)
            {
                throw new ArgumentNullException("webApiUserRepository");
            }
            this.webApiUserRepository = webApiUserRepository;
        }
        #endregion
        #region Public
        /// <summary>
        /// Is Valid WebApi User
        /// </summary>
        public bool IsValidWebApiUser(string userName, string password)
        {
            return webApiUserRepository.AuthenticateWebApiUser(userName, password);
        }
        #endregion
    }
}
