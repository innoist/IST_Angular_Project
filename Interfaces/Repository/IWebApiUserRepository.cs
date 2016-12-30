using IST.Models.DomainModels;

namespace IST.Interfaces.Repository
{
    /// <summary>
    /// Interface for Web Api User 
    /// </summary>
    public interface IWebApiUserRepository : IBaseRepository<WebApiUser, int>
    {
        /// <summary>
        /// Authenticate Web Api User
        /// </summary>
        /// <returns> Returns true if web api user exists with given username and password.</returns>
        bool AuthenticateWebApiUser(string userName, string password);
    }
}
