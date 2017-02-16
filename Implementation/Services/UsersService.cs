using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IST.Commons;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.IdentityModels;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;

namespace IST.Implementation.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository userRepository;
        private readonly IAspNetRoleRepository aspNetRoleRepository;

        public UsersService(IUserRepository userRepository, IAspNetRoleRepository aspNetRoleRepository)
        {
            this.userRepository = userRepository;
            this.aspNetRoleRepository = aspNetRoleRepository;
        }

        public IEnumerable<AspNetUser> GetAllUsers()
        {
            return userRepository.GetAll();
        }

        public AspNetUser GetUser(string userName)
        {
            return userRepository.FindUserByUserName(userName);
        }

        public AspNetUser GetUserById(string userId)
        {
            return userRepository.FindUserById(userId);
        }

        public async Task<AspNetUser> GetUserAsync(string userName)
        {
            return await userRepository.GetByUserNameAsync(userName);
        }

        public UsersSearchResponse GetAllUsers(UsersSearchRequest searchRequest)
        {
            return userRepository.GetUsersSearchResponse(searchRequest);
        }

        public IEnumerable<UserRole> GetAllRoles()
        {
            return aspNetRoleRepository.GetAll();
        }

        public bool UsernameExistsOrNot(string username)
        {
            return userRepository.UsernameExistsOrNot(username);
        }
    }
}
