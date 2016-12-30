using System;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Models.IdentityModels;
using IST.Models.IdentityModels.ViewModels;

namespace IST.Implementation.Services
{
    /// <summary>
    /// Register User Service 
    /// </summary>
    public class RegisterUserService : IRegisterUserService
    {
        #region Private

        private readonly IUserRepository userRepository;
        private readonly IUserDetailsRepository userDetailsRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        public RegisterUserService(IUserRepository userRepository, IUserDetailsRepository userDetailsRepository)
        {
            this.userRepository = userRepository;
            this.userDetailsRepository = userDetailsRepository;
        }
        #endregion
        #region Public

        /// <summary>
        /// Gives the maximum domain key from the records
        /// </summary>
        public double GetMaxUserDomainKey()
        {
            return userRepository.GetMaxUserDomainKey();
        }

        /// <summary>
        /// Saves user details provided while signup
        /// </summary>
        public void SaveUserDetails(AspNetUser addedUser, RegisterViewModel model)
        {
            UserDetail user = userDetailsRepository.Create();
            user.UserId = addedUser.Id;
            userDetailsRepository.Add(user);
            userDetailsRepository.SaveChanges();
        }
        /// <summary>
        /// Executes store procedure for copying default data for newly registered user
        /// </summary>
        public void SetupUserDefaultData(string userId)
        {
            UserDetail userDetails = userDetailsRepository.FindByUserId(userId);
            if (userDetails != null)
            {
                AspNetUser user = userRepository.FindUserById(userId);
                if (user == null)
                {
                    throw new Exception("User not found!");
                }
                userDetailsRepository.CopyUserDefaultData(userId, user.UserDomainKey);
                // We Need to keep the Userdetails it should not be deleted
                //userDetailsRepository.Delete(userDetails);
                //userDetailsRepository.SaveChanges();
            }
        }


        /// <summary>
        /// To check if URL is available 
        /// </summary>
        public bool IsCompanyUrlAvailable(string url)
        {
          return  userDetailsRepository.IsCompanyUrlAvailable(url);
        }

        #endregion
    }
}