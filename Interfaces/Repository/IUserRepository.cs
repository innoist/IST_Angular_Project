using System.Threading.Tasks;
using IST.Models.IdentityModels;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;

namespace IST.Interfaces.Repository
{
    public interface   IUserRepository : IBaseRepository<AspNetUser, long>
    {
        /// <summary>
        /// To get the maximum user domain key
        /// </summary>
        double GetMaxUserDomainKey();

        /// <summary>
        /// Finds user by user id
        /// </summary>
        AspNetUser FindUserById(string userId);

        /// <summary>
        /// Get User by Name
        /// </summary>
        AspNetUser GetLoggedInUser();

        UsersSearchResponse GetUsersSearchResponse(UsersSearchRequest searchRequest);
        AspNetUser FindUserByUserName(string userName);
        Task<AspNetUser> GetByUserNameAsync(string userName);
    }
}
