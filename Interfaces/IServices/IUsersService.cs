using System.Collections.Generic;
using System.Threading.Tasks;
using IST.Models.IdentityModels;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;

namespace IST.Interfaces.IServices
{
    public interface IUsersService
    {
        IEnumerable<AspNetUser> GetAllUsers();
        AspNetUser GetUser(string userName);
        AspNetUser GetUserById(string userId);
        Task<AspNetUser> GetUserAsync(string userName);
        UsersSearchResponse GetAllUsers(UsersSearchRequest searchRequest);
        IEnumerable<UserRole> GetAllRoles();
    }
}
