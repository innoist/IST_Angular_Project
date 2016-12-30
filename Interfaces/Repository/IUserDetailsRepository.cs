using IST.Models.DomainModels;

namespace IST.Interfaces.Repository
{
    /// <summary>
    /// Interface for User Detail repository
    /// </summary>
    public interface IUserDetailsRepository : IBaseRepository<UserDetail, long>
    {
        /// <summary>
        /// Finds user details by user id
        /// </summary>
        UserDetail FindByUserId(string userId);

        /// <summary>
        /// Executes the procedure for creating default data
        /// </summary>
        void CopyUserDefaultData(string userId, long domainKey);


        /// <summary>
        /// To check if URL is available 
        /// </summary>
        bool IsCompanyUrlAvailable(string url);

        /// <summary>
        /// Get User Id for URL 
        /// </summary>
        string GetUserIdByCompanyUrl(string url);


    }
}
